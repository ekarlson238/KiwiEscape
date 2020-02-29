using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{


    public void LoadMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

    //public void LoadGameScene()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene();
    //}

}
