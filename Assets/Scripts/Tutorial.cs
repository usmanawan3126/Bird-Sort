using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    public Button b1;
    public Button b2;
    int count = 0;
   public  bool tutor = false;
   public List<Transform> spaces = new List<Transform>();
    public List<Bird> birds = new List<Bird>();
    public GameObject block;
    public GameObject tutorCanvas;
    public GameObject victory;
    public GameObject bird;
    public GameObject mask;
    public GameObject Hand;
    public AudioClip clip1;
    AudioSource source;
    AudioClip clip;
    public AudioClip levelComplete;
    // Start is called before the first frame update
    private void Awake()
    {

        if (PlayerPrefs.GetInt("LeveltoLoad") >0)
        {
            tutor = false;
        }
        else
        {
            tutor =true;
        }
    }
    void Start()
    {
        mask.SetActive(true);
        Hand.SetActive(false);
        victory.SetActive(false);
        tutorCanvas.SetActive(false);
        bird.SetActive(false);
        Debug.Log("Level to Load"+PlayerPrefs.GetInt("LeveltoLoad"));
        if (PlayerPrefs.GetInt("LeveltoLoad") == 0)
        {
            mask.transform.DOScale(new Vector3(1.577979f, 1.577979f, 1.577979f), 1f).OnComplete(
                () =>
                {
                    Hand.SetActive(true);
                }
                );
            tutor = true;
            tutorCanvas.SetActive(true);
            bird.SetActive(true);
        }
        b1.enabled = true;
        b2.enabled = false;
        Debug.Log("Hello World");
        //b1.gameObject.GetComponent<Button>().enabled = true;
        //b2.gameObject.GetComponent<Button>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BClick()
    {
        float moveDelay = 0.9f;
        count++;
        Hand.SetActive(false);
        int c = 0;
        if (count==2)
        {
            mask.transform.DOScale(new Vector3(14.34762f, 14.34762f, 14.34762f), 1f);
            Hand.SetActive(false);
            birds[0].birdAnim?.SetTrigger("IdleToFly");
            birds[0].transform.DOMove(spaces[0].transform.position, 1.5f);
            birds[1].birdAnim?.SetTrigger("IdleToFly");
            birds[1].transform.DOMove(spaces[1].transform.position, 1.5f).OnComplete(() =>
                {
                    source = b1.GetComponent<AudioSource>();
                    source.PlayOneShot(clip1);
                    birds[0].transform.localScale = new Vector2(-birds[0].gameObject.transform.localScale.x, birds[0].transform.localScale.y);
                    birds[1].transform.localScale = new Vector2(-birds[1].gameObject.transform.localScale.x, birds[1].transform.localScale.y);

                    for (int i = 0; i < 4; i++)
                    {
                        birds[i].birdAnim?.SetTrigger("IdleToFly");
                        Debug.Log("Moved");

                        birds[i].transform.DOMove(block.transform.position, moveDelay).OnComplete(
                            () =>
                            {
                                tutor = false;
                                if (c==3)
                                {
                                    //tutor = false;
                                    Destroy(tutorCanvas);
                                    this.gameObject.GetComponent<AudioSource>().PlayOneShot(levelComplete);
                                    victory.SetActive(true);
                                }
                                c++;
                                
                            });
                        moveDelay = moveDelay + 0.3f;
                       
                    }
                    //mask.SetActive(false);
                    //mask1.SetActive(false);
                });
            int level = PlayerPrefs.GetInt("LeveltoLoad");
            level++;
            PlayerPrefs.SetInt("LeveltoLoad", level);
        }
        source=birds[0].GetComponent<Bird>().source;
        clip = birds[0].GetComponent<Bird>().clip;
        source.PlayOneShot(clip);
        mask.transform.DOMove(new Vector3(1.75999999f, -0.709999979f, 0), 1.5f).OnComplete(
            ()=>
            {
                Hand.SetActive(true);
            });
        Hand.transform.DOMove(new Vector2(2.05f, -1.13f), 1.5f);

        //mask.SetActive(false);
        //mask1.SetActive(true);
        b1.enabled = false;
        b2.enabled = true;

    }
}
