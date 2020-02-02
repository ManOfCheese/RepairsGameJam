﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_GGJ : MonoBehaviour
{
    public void PreviousScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.LogWarning("Previous scene couldn't be loaded. Current scene build index is 0.");
            return;
        }
        Debug.Log("Load previous scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount)
        {
            Debug.LogWarning("Next scene couldn't be loaded. Current scene build index is equal to scene count.");
            return;
        }
        Debug.Log("Load next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CycleScenes()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount)
        {
            Debug.Log("Next scene couldn't be loaded. Current scene build index is equal to scene count. Loading first scene");
            SceneManager.LoadScene(0);
            return;
        }
        NextScene();
    }

    public void Quit()
    {
        Debug.Log("Quit application");
        Application.Quit();
    }
}
