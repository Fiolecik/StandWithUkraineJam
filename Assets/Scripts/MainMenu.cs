using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject authorPanel;
    [SerializeField] private GameObject menuPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void TurnOffAll()
    {
        mapPanel.SetActive(false);
        authorPanel.SetActive(false);
        menuPanel.SetActive(false);
    }

    public void ShowMap()
    {
        TurnOffAll();
        mapPanel.SetActive(true);
    }

    public void ShowMenu()
    {
        TurnOffAll();
        menuPanel.SetActive(true);
    }

    public void ShowAuthor()
    {
        TurnOffAll();
        authorPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
