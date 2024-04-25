using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    [SerializeField] private List<Menu> menuList = new();
    private Stack<Menu> menuStack = new();

    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        // kalau back / esc
        CheckBackInput();
    }
    private void CheckBackInput()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (menuStack.Count > 0)
            {
                CloseMenu();
            }
            else if (GameManager.Instance.GameMode != GameMode.NotInGame)
            {
                OpenMenu("PauseMenu");
            }

        }
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
        CloseAllMenu();
        Time.timeScale = 1.0f;
    }
    private Menu GetMenu(string name)
    {
        if (menuList.Count == 0)
        {
            return null;
        }
        return menuList.Find((x) => x.Name == name);
    }

    public void OpenMenu(string name)
    {
        Menu menu = GetMenu(name);

        if(menu is null)
        {
            Debug.LogWarning($"Couldn't find [{name}] in the Menu List");
            return;
        }

        //Disables top most Menu
        if(menuStack.TryPeek(out Menu m))
        {
            m.Canvas.enabled = false;
        }

        menu.Canvas.enabled = true;
        menuStack.Push(menu);

        CheckForPause();
    }

    public void OpenMenuOverlap(string name)
    {
        Menu menu = GetMenu(name);
        if (menu is null)
        {
            Debug.LogWarning($"Couldn't find [{name}] in the Menu List");
            return;
        }

        menu.Canvas.enabled = true;
        menuStack.Push(menu);

        CheckForPause();
    }

    public void CloseAllMenu()
    {
        if (menuStack.Count == 0) return;
        
        foreach(Menu menu in menuStack.ToArray())
        {
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        if (menuStack.Count == 0)
        {
            Debug.Log("MenuStack is empty");
            return;
        }
        menuStack.Pop().Canvas.enabled = false;

        if (menuStack.TryPeek(out Menu m))
        {
            m.Canvas.enabled = true;
        }

        CheckForPause();
    }

    private void CheckForPause()
    {
        if (menuStack.TryPeek(out Menu result) && result.PauseWhenOpen)
        {
            GameManager.Instance.IsGamePaused = true;
            //pause
        }
        else
        {
            GameManager.Instance.IsGamePaused = false;
            //unpause
        }
    }

}
