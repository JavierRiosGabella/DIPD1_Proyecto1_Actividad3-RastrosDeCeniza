using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UP_SceneLoader : MonoBehaviour {

    public void UP_LoadScene(int sceneIndex)
    {
        if(sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void UP_LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);        
    }

    public void UP_LoadPreviousScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int previousScene = currentScene - 1;
        if (previousScene >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(previousScene);
        }
    }

    public void UP_LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

	public void UP_ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void UP_ExitGame()
    {
        Application.Quit();
    }
}
