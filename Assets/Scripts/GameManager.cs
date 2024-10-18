using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameMode
{
    CollectTrash,
    SeperateTrash,
    NotInGame
}


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    { 
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        } 
    }

    [SerializeField] private Canvas mobileInputCanvas;

    private GameMode gameMode;
    
    public GameMode GameMode { get { return gameMode; } }

    private CinemachineVirtualCamera playerVCam;
    private CinemachineVirtualCamera tableVCam;

    private bool isGamePaused = false;

    public UnityEvent<GameMode> GameModeChanged = new UnityEvent<GameMode>();
    
    public bool IsGamePaused 
    {   get 
        {
            if (GameMode == GameMode.NotInGame)
                return false;
            return isGamePaused; 
        } 
        set 
        {
            isGamePaused = value;
            Time.timeScale = value ? 0 : 1;
        } 
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        Screen.SetResolution(Screen.height * 16/9, Screen.height, FullScreenMode.FullScreenWindow);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameModeChanged.AddListener(OnGameModeChanged);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameModeChanged.RemoveAllListeners();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)//main menu
        {
            ChangeGameMode(GameMode.NotInGame);
        }
        else
        {
            FindCameras();
            EnterCollectTrashMode();
            isGamePaused = false;
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void FindCameras()
    {
        CinemachineVirtualCamera[] vCams = FindObjectsByType<CinemachineVirtualCamera>(FindObjectsSortMode.None);

        if(vCams.Length != 2)
        {
            Debug.LogWarning("Please make sure there are 2 Virtual Camera with the correct Tags");
            return;
        }

        for(int i = 0; i < vCams.Length; i++)
        {
            if (vCams[i].CompareTag("PlayerVCam"))
            {
                playerVCam = vCams[i];
            }
            else if (vCams[i].CompareTag("TableVCam"))
            {
                tableVCam = vCams[i];
            }
        }

        if(playerVCam is null || tableVCam is null)
        {
            playerVCam = vCams[0];
            tableVCam = vCams[1];
        }
        playerVCam.enabled = true;
        tableVCam.enabled = false;
    }


    public void ChangeGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
        this.GameModeChanged.Invoke(this.gameMode);
    }

    public void OnGameModeChanged(GameMode gameMode)
    {
        PlayMusic(gameMode);
        if (gameMode == GameMode.SeperateTrash)
        {
            playerVCam.enabled = false;
            tableVCam.enabled = true;
            SetCursorVisible(true);
            if (mobileInputCanvas != null)
            {
                mobileInputCanvas.enabled = false;
            }
        }
        else if (gameMode == GameMode.CollectTrash)
        {
            playerVCam.enabled = true;
            tableVCam.enabled = false;
            if (mobileInputCanvas != null)
            {
                mobileInputCanvas.enabled = true;
            }
            //SetCursorVisible(false);
        }
        else if(gameMode == GameMode.NotInGame)
        {
            if (mobileInputCanvas != null)
            {
                mobileInputCanvas.enabled = false;

            }
        }
    }

    private void PlayMusic(GameMode gameMode)
    {
        if(gameMode == GameMode.NotInGame)
        {
            if (AudioManager.Instance.IsSoundPlaying("GameMusic"))
            {
                AudioManager.Instance.StopSound("GameMusic");
            }
            AudioManager.Instance.PlaySound("MenuMusic");
        }
        else if(!AudioManager.Instance.IsSoundPlaying("GameMusic"))
        {
            if (AudioManager.Instance.IsSoundPlaying("MenuMusic"))
            {
                AudioManager.Instance.StopSound("MenuMusic");
            }
            AudioManager.Instance.PlaySound("GameMusic");
        }
    }

    private void SetCursorVisible(bool visible)
    {
        Cursor.visible = visible;
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EnterSeperateTrashMode()
    {
        ChangeGameMode(GameMode.SeperateTrash);
    }

    public void EnterCollectTrashMode()
    {
        ChangeGameMode(GameMode.CollectTrash);
    }
}
