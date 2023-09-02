using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    //load a scene by string name
    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
