using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public Text score;
    public Image endingImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        score.text = $"Score - {Collectable.PlayerCollectableCount}";
    }

    private void OnCollisionEnter()
    {
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        endingImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartMenu");
    }
}
