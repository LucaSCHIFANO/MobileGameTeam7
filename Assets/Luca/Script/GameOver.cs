using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("letsgotnul");
    }

    public IEnumerator letsgotnul()
    {
        yield return new WaitForSeconds(0.7f);
        AudioManager.Instance.Play("GameOver");
        yield return new WaitForSeconds(2.3f);
        SceneManager.LoadScene("MainMenu");
    }
}
