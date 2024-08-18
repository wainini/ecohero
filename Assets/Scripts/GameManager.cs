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
    public static GameManager Instance;

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
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            this.GameModeChanged.AddListener(OnGameModeChanged);
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Update()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

#if UNITY_EDITOR
    [ContextMenu("EnterSeperateTrash")]
    public void EnterSeperateTrashMode()
    {
        ChangeGameMode(GameMode.SeperateTrash);
    }

    [ContextMenu("EnterCollectTrash")]
    public void EnterCollectTrashMode()
    {
        ChangeGameMode(GameMode.CollectTrash);
    }
#endif
}
