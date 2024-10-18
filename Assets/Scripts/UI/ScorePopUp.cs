using UnityEngine;
using UnityEngine.UI;  // Use 'TMPro' if using TextMeshPro
using UnityEngine.EventSystems;
using TMPro;

public class ScorePopUp : MonoBehaviour
{
    public float floatSpeed = 50f;
    public TextMeshProUGUI popUpText;  // Replace 'Text' with 'TMP_Text' if using TextMeshPro
    public RectTransform canvasRectTransform;  // Reference to the canvas' RectTransform
    public float displayDuration = 2f;  // Time to display the text

    private float timer = 2f;

    public void init(bool isCorrect, string message = "")
    {
        if (isCorrect)
        {
            popUpText.text = "+100";
            popUpText.color = Color.green;
        }
        else
        {
            popUpText.text = "-50";
            popUpText.color = Color.red;
        }

        if (!string.IsNullOrEmpty(message))
        {
            popUpText.text = message;
        }
        ShowPopUp();
        
    }
    // Call this method to show the pop-up text at the global mouse position
    public void ShowPopUp()
    {
        Vector3 mousePosition = Input.mousePosition;
        popUpText.gameObject.SetActive(true);
        timer = displayDuration;

        // Convert the mouse position into Canvas space
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, null, out anchoredPos);

        // Set the pop-up text position
        popUpText.rectTransform.anchoredPosition = anchoredPos;
    }

    void Update()
    {
        if (popUpText.gameObject.activeSelf)
        {
            // Move the text upwards
            popUpText.transform.Translate(Vector2.up * floatSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                popUpText.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
