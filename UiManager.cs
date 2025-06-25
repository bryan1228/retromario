using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject startMenu;
    public GameObject deathScreen;
    public GameObject goalScreen;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        ShowStartMenu();
    }

    void Update()
    {

        if (Time.timeScale == 0f &&
            (deathScreen.activeSelf || goalScreen.activeSelf) &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
    }

    public void ShowStartMenu()
    {
        startMenu.SetActive(true);
        deathScreen.SetActive(false);
        goalScreen.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGoalScreen()
    {
        goalScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
