using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public PlayerController player;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseLevel()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }
    public void resumeLevel()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(1);
        player.NextPanel.SetActive(false);
        
    }
    public void OnHome()
    {
        SceneManager.LoadScene(0);
        player.NextPanel.SetActive(false);
        panel.SetActive(false);
    }
}
