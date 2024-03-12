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

    private CinemachineVirtualCamera followPlayerVCam;
    private CinemachineVirtualCamera collectTrashVCam;

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

        gameMode = GameMode.CollectTrash;
        FindCameras();
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
            if (vCams[i].CompareTag("FollowPlayerVCam"))
            {
                followPlayerVCam = vCams[i];
            }
            else if (vCams[i].CompareTag("CollectTrashVCam"))
            {
                collectTrashVCam = vCams[i];
            }
        }

        if(followPlayerVCam is null || collectTrashVCam is null)
        {
            followPlayerVCam = vCams[0];
            collectTrashVCam = vCams[1];
        }
        followPlayerVCam.enabled = true;
        collectTrashVCam.enabled = false;
    }


    public void ChangeGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }

#if UNITY_EDITOR
    [ContextMenu("EnterSeperateTrash")]
    public void EnterSeperateTrashMode()
    {
        ChangeGameMode(GameMode.SeperateTrash);
        followPlayerVCam.enabled = false;
        collectTrashVCam.enabled = true;
    }

    [ContextMenu("EnterCollectTrash")]
    public void EnterCollectTrashMode()
    {
        ChangeGameMode(GameMode.CollectTrash);
        followPlayerVCam.enabled = true;
        collectTrashVCam.enabled = false;
    }
#endif
}
