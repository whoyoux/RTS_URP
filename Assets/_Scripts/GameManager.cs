using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private bool IS_GAME_PAUSED = false;

    [Header("Key Bindings")]
    public KeyCode pauseKey = KeyCode.Escape;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public Button resumeGameBtn;
    public Button settingsBtn;
    public Button exitGameBtn;

    void Start()
    {
        SetupUI();
        SetupButtons();
    }

    void Update()
    {
        if(Input.GetKeyDown(pauseKey))
        {
            if (IS_GAME_PAUSED)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void SetupUI()
    {
        pauseMenu.SetActive(false);
    }

    private void SetupButtons()
    {
        resumeGameBtn.onClick.AddListener(() => ResumeGame());
        settingsBtn.onClick.AddListener(() => Debug.Log("TODO: Settings"));
        exitGameBtn.onClick.AddListener(() => ExitGame());
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        IS_GAME_PAUSED = false;
        pauseMenu.SetActive(false);
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        IS_GAME_PAUSED = true;
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    public bool IsGamePaused()
    {
        return IS_GAME_PAUSED;
    }
}
