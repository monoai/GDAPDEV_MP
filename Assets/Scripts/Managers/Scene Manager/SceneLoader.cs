using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }
    */

    public void LoadGameScene()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadLevel(0));
    }

    //Temporary function for Beta.
    public void LoadLevelOne()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetAllScenes().Length)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log("level index = " + levelIndex);
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
