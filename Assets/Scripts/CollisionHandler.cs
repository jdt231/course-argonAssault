using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [Tooltip("In seconds")] [SerializeField] float levelLoadTime = 2;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadTime);

    }
    private void ReloadScene() // string referenced.
    {
        SceneManager.LoadScene(1);
    }

}
