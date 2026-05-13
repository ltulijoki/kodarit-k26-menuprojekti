using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSelector : MonoBehaviour
{
    public TMP_Text[] items;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public GameObject pauseMenu;
    private int index = 0;
    private bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        UpdateColors();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }

        if (!isPaused) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index = (index - 1 + items.Length) % items.Length;
            UpdateColors();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index = (index + 1) % items.Length;
            UpdateColors();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ActivateItem(index);
        }
    }

    void UpdateColors()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].color = (i == index) ? highlightColor : normalColor;
        }
    }

    public void ActivateItem(int i)
    {
        if (i == 0)
            ResumeGame();
        if (i == 1)
        {
            Time.timeScale = 1;
            StartCoroutine(ChangeToScene("MainMenu"));
        }
        if (i == 2)
            QuitGame();

    }

    public void SetIndex(int newIndex)
    {
        index = newIndex;
        UpdateColors();
    }

    IEnumerator ChangeToScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    void PauseGame()
    {
        isPaused = true;
        index = 0;
        pauseMenu.SetActive(true);
        UpdateColors();
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
