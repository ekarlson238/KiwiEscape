using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Image introCutscene;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadGameScene()
    {
        StartCoroutine(StartGame());
        //SceneManager.LoadScene(1);
    }

    IEnumerator StartGame()
    {
        introCutscene.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        introCutscene.sprite = image2;
        yield return new WaitForSeconds(0.5f);
        introCutscene.sprite = image3;
        yield return new WaitForSeconds(0.5f);
        introCutscene.sprite = image4;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

}
