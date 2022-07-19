using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int splashScreenLoadingTime = 1;

    void Start()
    {
        Invoke("LoadMainGame", splashScreenLoadingTime);
    }

    private void LoadMainGame()
    {
        SceneManager.LoadScene(1);
    }
}