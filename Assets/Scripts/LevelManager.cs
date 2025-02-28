using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }
    [SerializeField]
    private float maxTime;

    public float currentTime = 0;
    private int currentScore = 0;

    public UnityEvent<int> ScoreChanged;

    public int CurrentScore { 
        get { return currentScore; }
        set { currentScore = value; ScoreChanged.Invoke(currentScore); }
    }

    [field: SerializeField] public int TargetScore { get; private set; }

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

    private void OnDisable()
    {
        instance = null;
    }
}
