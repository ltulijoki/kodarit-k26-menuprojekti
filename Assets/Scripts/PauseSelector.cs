using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSelector : MonoBehaviour
{
    public TMP_Text[] items;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    private int index = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateColors();
    }

    // Update is called once per frame
    void Update()
    {
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
            StartCoroutine(ChangeToScene("PikkupelitMenu"));
        if (i == 1)
            StartCoroutine(ChangeToScene("Settings"));
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
}
