using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public AudioClip clip;

    private void Start()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        
    }
    public void startLevel()
    {
       SceneManager.LoadScene(1);
        Destroy(clip);
    }
}
