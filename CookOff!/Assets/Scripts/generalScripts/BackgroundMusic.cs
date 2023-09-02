using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] bgSounds = GameObject.FindGameObjectsWithTag("music");
        DontDestroyOnLoad(gameObject); //so sound is transfered between scenes

        if (bgSounds.Length == 2) //destroy current object if we happen to have two background music objects
        {
            Destroy(gameObject);
        } 

    }



}
