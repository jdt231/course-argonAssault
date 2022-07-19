using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;
        print("Number of players " + numMusicPlayer);

        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        { 
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }
}
