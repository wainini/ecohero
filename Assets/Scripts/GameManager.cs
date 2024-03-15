using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    CollectTrash,
    SeperateTrash,
    NotInGame
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameMode gameMode;

    public GameMode GameMode { get { return gameMode; } }

    private CinemachineVirtualCamera playerVCam;
    private CinemachineVirtualCamera tableVCam;

    private bool isGamePaused = false;
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
        } 
    }

    private void Awake()
    {
        if(Instance is not null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        FindCameras();
        EnterCollectTrashMode();
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
        playerVCam.enabled = false;
        tableVCam.enabled = true;
        SetCursorVisible(true);
    }

    [ContextMenu("EnterCollectTrash")]
    public void EnterCollectTrashMode()
    {
        ChangeGameMode(GameMode.CollectTrash);
        playerVCam.enabled = true;
        tableVCam.enabled = false;
        SetCursorVisible(false);
    }
#endif
}
