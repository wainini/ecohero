using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    private Canvas canvas;
    [SerializeField]

    private GameObject levelButtonContainer;

    private GameSaveData saveData;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        StartCoroutine(WaitUntilDataAvailable());
    }

    private IEnumerator WaitUntilDataAvailable()
    {
        yield return new WaitUntil(() => SaveLoadManager.Instance?.GetGameSaveData is not null);

        saveData = SaveLoadManager.Instance.GetGameSaveData; //save the reference to be access/changed everytime game data changed
        int maxLevelCleared = saveData.ClearedLevelData.Count > 0 ? saveData.ClearedLevelData.Max(x => x.Level) : 0;

        for (int i = 0; i<levelButtonContainer.transform.childCount; i++)
        {
            GameObject levelButtonObject = levelButtonContainer.transform.GetChild(i).gameObject;
            if (levelButtonObject != null)
            {
                LevelButton levelButton = levelButtonObject.GetComponent<LevelButton>();
                levelButton.SetEnabled(maxLevelCleared);
            }
        }
    }

}
