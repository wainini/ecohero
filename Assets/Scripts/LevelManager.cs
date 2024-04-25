using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField]
    private float maxTime;

    public float currentTime = 0;
    private int currentScore = 0;

    public UnityEvent<int> ScoreChanged;

    public int CurrentScore { 
        get { return currentScore; }
        set { currentScore = value; ScoreChanged.Invoke(currentScore); }
    }

    public UnityEvent GameOverEvent = new UnityEvent();

    private bool isGameOver;
    public bool IsGameOver
    {
        get
        {
            if (GameManager.Instance.GameMode == GameMode.NotInGame)
                return false;
            return isGameOver;
        }
        set
        {
            isGameOver = value;
            if (IsGameOver)
            {
                GameOverEvent.Invoke();
            }
        }
    }
    private void Awake()
    {
        if (Instance is not null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
        CurrentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGamePaused && !isGameOver)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                IsGameOver = true;
            }
        }
    }
}
