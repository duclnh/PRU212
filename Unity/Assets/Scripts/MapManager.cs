using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public void StartGame()
    {
        GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in gameObjects)
        {
            Destroy(obj);
        }
        SceneManager.LoadScene("Sence Start");
    }
    public void Play()
    {
        SceneManager.LoadScene("Sence 1");
    }

    public void Ranking()
    {
        SceneManager.LoadScene("Sence 2");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
