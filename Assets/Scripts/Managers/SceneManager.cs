using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    //make a function that will load the next scene
    public void LoadNextScene()
    {
        //get the current scene index
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        //load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadFirstScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

    }
    public void QuitGame()
    {
        Application.Quit();
    }

}