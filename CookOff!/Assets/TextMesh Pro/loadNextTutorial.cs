using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNextTutorial : MonoBehaviour
{
    public string menu;
    public GameObject[] tutorials;
    int current = 0;

    public void loadNext ()
    {
        tutorials[current].SetActive(false);

        if (current == tutorials.Length - 1)
        {
            SceneManager.LoadScene(menu);
        } else
        {
            current++;
            tutorials[current].SetActive(true);
        }

    }
}
