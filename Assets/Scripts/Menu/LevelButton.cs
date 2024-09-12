using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int Level = 1;
    [SerializeField]
    private GameObject DisabledImage;

    private bool IsEnabled = false;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onLevelButtonClick);
    }

    private void onLevelButtonClick()
    {
        GameManager.Instance.ChangeScene("Level" + Level);
    }

    public void SetEnabled(bool IsEnabled)
    {
        this.IsEnabled = IsEnabled;
        DisabledImage.SetActive(!IsEnabled);
        button.enabled = IsEnabled;
    }

}
