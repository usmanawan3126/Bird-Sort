using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Branch : MonoBehaviour
{
    public List<Bird> birds = new List<Bird>();
    
    public Bird[] bird1;
    private GameObject player;
    //public GameObject[] branches;
    GameObject[] birdCheck;
    public bool empty;
    Animator anim;
    public List<Transform> spaces = new List<Transform>();
    int Totalbirds;
    public Branch block;
    public AutoSort auto;
    
    public int getAutoValue;
    Branch Selected0;
    Branch Selected1;
    private List<Bird> BirdsUndo = new List<Bird>();
    int UndoBirdIndex = 0;
    public AudioClip clip;
    public AudioSource source;
    public AudioClip completeAudio;
    
    // public GameObject canvaParent;

    // List<GameObject> Empty = new List<GameObject>();


    float pos = 0f;
    float e1 = -1.021f;
    float e2 = -1.245762f;
    float e3 = -1.455762f;
    float e4 = -1.681f;
    bool Arrangebirds = true;
    int indexofBird = 0;
    public int birdcount;
    public LevelManager Lm;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        player.gameObject.GetComponent<PlayerController>().block.transform.localScale = new Vector3(1, 0.3f, 0);
        player.gameObject.GetComponent<PlayerController>().block.transform.position = new Vector3(0, 5.3145f, 0);
    }

    public void SelectBranch()
    {
        player.gameObject.GetComponent<PlayerController>().SelectB(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Init(bool spawn, BirdType birdtype)
    {
        if (spawn)
        {
            player.gameObject.GetComponent<PlayerController>().emptyBranch++;
            Debug.Log("Branch Emptyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
            return;
        }
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Value of i : " + birdtype);
            birds.Add(Instantiate(bird1[(int)birdtype], new Vector2(transform.position.x, transform.position.y), Quaternion.identity));
            birds[i].name = this.name+" Bird: "+i;
            birds[i].transform.position = spaces[i].position;
            Debug.Log("Birds Added successfully");
        }
    }
    public void AssignBirds(List<Bird> Bird)
    {
        int counter = 0;
        birds.Clear();
        for (int i = 0; i < Bird.Count; i++)
        {
            if (Bird[i].gameObject.tag == "empty")
            {
                Bird.Remove(Bird[i]);
            }
        }
        foreach (var item in Bird.ToArray())
        {
            if (item.tag == "empty")
            {
                // Empty.Add(item);
                Bird.Remove(item);
            }
        }
        birds.AddRange(Bird);

        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].transform.position = spaces[i].position;
            birds[i].flipPos();
            //if (birds[i].transform.position.x < 0)
            //{
            //    birds[i].transform.localScale = new Vector2(birds[counter].transform.localScale.x * -1, birds[counter].transform.localScale.y);
            //}
        }
        //birds.AddRange(Empty);
        if (birds.Count != spaces.Count)
        {
            Debug.Log("Birds Count : " + birds.Count);
            for (int i = birds.Count; i < spaces.Count; i++)
            {
                birds.Add(player.gameObject.GetComponent<PlayerController>().birdEmpty);
                birds[i].transform.position = spaces[i].position;
                counter++;
            }
        }

    }

    public void AssignBirds(Bird Bird)
    {
        birds.Clear();
        birds.Add(Bird);
    }

    public List<Bird> getBird()
    {
        return birds;
    }
    public void MoveBird(List<Bird> bird, Branch Selected0, Action callback)
    {
        BirdsUndo.Clear();
        float moveDelay = 0.9f;
        int counter = 0;
        for (int i = 0; i < birds.Count; i++)
        {
            if (birds[i].tag == "empty")
            {
                if (counter < bird.Count)
                {
                    birds[i] = bird[counter];
                    BirdsUndo.Add(bird[counter]);
                    Debug.Log("Selected 00000000000000000000000000000000000000000000000000000000"+Selected0.birds[Selected0.birds.IndexOf(bird[counter])]);
                    Selected0.birds[Selected0.birds.IndexOf(bird[counter])] = player.gameObject.GetComponent<PlayerController>().birdEmpty;
                    bird[counter].birdAnim?.SetTrigger("IdleToFly");
                    bird[counter].transform.DOMove(spaces[i].position, moveDelay).OnComplete(
                    () =>
                    {
                        for (int i = 0; i < counter; i++)
                        {
                            bird[i].flipPos();
                        }
                    });
                    moveDelay = moveDelay + 0.3f;
                    // bird1[counter].flipPos();
                    //  Debug.Log("Play Anim");
                    counter++;
                }
            }
        }

        Debug.Log("Birdssssssssssssssssssssssssssssssssssssssssss    Countttttttttttttttttttttttt   :   "+BirdsUndo.Count);
        

        DOVirtual.DelayedCall(1.5f,
            () =>
            {
                int Bcounter = BirdsUndo.Count - 1;
                Bird b;
                for (int i = 0; i < BirdsUndo.Count / 2; i++)
                {
                    b = BirdsUndo[i];
                    BirdsUndo[i] = BirdsUndo[Bcounter];
                    BirdsUndo[Bcounter] = b;
                }
                FlyingBirds fb = new FlyingBirds
                {
                    Birds = new List<Bird>(BirdsUndo),
                    FromBranch = Selected0,
                    ToBranch = this
                };
                if (player.gameObject.GetComponent<PlayerController>().ExtraUndo==true)
                {
                    GameObject Undo = GameObject.FindGameObjectWithTag("Undo");
                    Undo.gameObject.GetComponent<Undo>().undo();
                   //player.gameObject.GetComponent<PlayerController>().ExtraUndo = false;
                }
                player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Add(fb);
                callback?.Invoke();
            });
        

    }
    public int CheckBirds(Branch b)
    {
        int s = birds.Count;
        s = 4 - s;
        return s;
    }
    public List<Bird> GetMatchingBrids(Branch Selected_1)
    {
        List<Bird> collectedBirds = new List<Bird>();
        // Debug.Log(birds.Count);
        //Debug.Log("Selected Count : "+ Selected_1);
        //Debug.Log("Last Bird Tag of getlast : "+ Selected_1);
        for (int i = 3; i >= 0; i--)
        {
            if (birds[i].tag != "empty")
            {
               
                if (Selected_1.getlastBird().tag == birds[i].tag)
                {
                    birds[i].GetComponent<Bird>().source.PlayOneShot(birds[i].GetComponent<Bird>().clip);
                    Debug.Log("Last Bird Tag  :  " + birds[i].tag);
                    collectedBirds.Add(birds[i]);
                    Debug.Log("Bird Name   :    " + birds[i].name);
                }
                else
                {
                    break;
                }
            }
        }
        return collectedBirds;
    }

    //public void AddBirds(Branch Selected_0)
    //{
    //    int index = 0;
    //    int c = 0;

    //    //for (int i = birds.Count - 1; i > 0; i--)
    //    //{
    //    //    if (birds[i].tag == birds[i - 1].tag)
    //    //    {
    //    //        if (birds[i].tag != "empty")
    //    //        {
    //    //            index = i;
    //    //            break;
    //    //        }
    //    //        else
    //    //        {
    //    //            Debug.Log("c++");
    //    //            c++;
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        if (birds[i].tag != "empty")
    //    //        {
    //    //            index = i;
    //    //            break;
    //    //        }
    //    //        //else
    //    //        //{
    //    //        //    Debug.Log("c++");
    //    //        //    c++;
    //    //        //}
    //    //    }
    //    //}
    //    //Debug.Log("Value Of C  :   " + c);
    //    //if (c == 3)
    //    //{
    //    //    Debug.Log("C is true");
    //    //    Debug.Log("Birds Count for Selected : " + Selected_0.birds.Count);
    //    //    for (int i = Selected_0.birds.Count - 1; i >= 0; i--)
    //    //    {
    //    //        if (Selected_0.birds[i].tag == "empty")
    //    //        {
    //    //            continue;
    //    //        }

    //    //        else if (Selected_0.birds[i].tag == Selected_0.birds[i--].tag)
    //    //        {
    //    //            i++;
    //    //            Player.Addedbirds.Add(Selected_0.birds[i]);
    //    //        }
    //    //        else
    //    //        {
    //    //            //  Player.Addedbirds.Add(Selected_0.birds[i]);
    //    //            break;
    //    //        }
    //    //    }
    //    //}
    //    //else
    //    //{


    //    //    for (int i = Selected_0.birds.Count - 1; i >= 0; i--)
    //    //    {
    //    //        if (Selected_0.birds[i].tag == "empty")
    //    //        {
    //    //            continue;
    //    //        }
    //    //        else if (Selected_0.birds[i].tag == birds[index].tag)
    //    //        {
    //    //            Player.Addedbirds.Add(Selected_0.birds[i]);
    //    //        }
    //    //        else
    //    //        {
    //    //            break;

    //    //        }
    //    //    }
    //    //}
    //    this.MoveBird(Selected_0.GetMatchingBrids(),
    //        () =>
    //        {
    //            if (birds.Count == 4 && birds[0].tag == birds[1].tag && birds[0].tag == birds[2].tag && birds[0].tag == birds[3].tag && birds[0].tag != "empty")
    //            {
    //                StartCoroutine(Damage());

    //            }
    //            Debug.Log("Cleared Player List");
    //            Player.Selected.Clear();
    //        });



    //}
    public void AddBirds(Branch Selected_0, Branch Selected_1)
    {
        
        //Debug.Log("Selected Count : " + Player.Selected.Count);
        int index = 0;
        int c = 0;
        if (Selected_0.GetMatchingBrids(Selected_1).Count == 0 && Selected_1.getlastBird().tag == player.gameObject.GetComponent<PlayerController>().birdEmpty.tag)
        {
            this.MoveBird(Selected_0.GetMatchingBrids(Selected_0), Selected_0,
            () =>
            {
                if (birds.Count == 4 && birds[0].tag == birds[1].tag && birds[0].tag == birds[2].tag && birds[0].tag == birds[3].tag && birds[0].tag != "empty")
                {
                    
                    StartCoroutine(Damage());
                   
                }
                else
                {
                    Debug.Log("Cleared Player List");
                    // Player.Selected.Clear();
                    AutoSort.MoveDone = true;
                }
            });
           

        }
        else
        {
            this.MoveBird(Selected_0.GetMatchingBrids(Selected_1), Selected_0,
            () =>
            {
                if (birds.Count == 4 && birds[0].tag == birds[1].tag && birds[0].tag == birds[2].tag && birds[0].tag == birds[3].tag && birds[0].tag != "empty")
                {
                   
                    StartCoroutine(Damage());
                   
                }
                else
                {
                    Debug.Log("Cleared Player List");
                    // Player.Selected.Clear();
                    AutoSort.MoveDone = true;
                }

            });
            
        }
        

    }
    IEnumerator Damage()
    {
        
        
        AutoSort.MoveDone = false;
        //player.gameObject.GetComponent<PlayerController>().SelectedB.Clear();
        float moveDelay = 0.9f;
        Debug.Log(BirdsUndo.Count);
        //birds.Remove(birds[birds.Count-1]);
        List<Bird> RemoveBirds= new List<Bird>();
       // RemoveBirds = birds;
        //for (int i = 0; i < 4-BirdsUndo.Count; i++)
        //{
        //    RemoveBirds.Add(birds[i]);
        //}
      //  RemoveBirds.Remove(RemoveBirds[RemoveBirds.Count - 1]);
        Debug.Log("Birds Counttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt : "+birds.Count);

        FlyingBirds fb = new FlyingBirds
        {
            Birds = new List<Bird>(birds),

            FromBranch = player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count - 1].ToBranch,
            ToBranch = this
        };
        player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Add(fb);
        player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count - 2].ToBranch = player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count - 2].FromBranch;
        //player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count - 1].Birds = new List<Bird>(birds);
        // player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count - 1].FromBranch = player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count - 1].ToBranch;

       
        //FlyingBirds fb = new FlyingBirds
        //{
        //    Birds = new List<Bird>(birds),
        //    //FromBranch = Selected0,
        //    //ToBranch = this
        //};
        // player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Add(fb);
        yield return new WaitForSeconds(0.5f);
        bool flydone = false;
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Bird To Flyyyyyyyyy : " + birds[i].name);
            birds[i].birdAnim?.SetTrigger("IdleToFly");
            Debug.Log("Moved");
            
            birds[i].transform.DOMove(player.gameObject.GetComponent<PlayerController>().block.transform.position, moveDelay).OnComplete(
                ()=>
                {
                    
                    if (count==3)
                    {
                        flydone = true;
                        Debug.Log("Value of Flydone  :  "+flydone);
                    }
                    count++;
                });
            moveDelay = moveDelay + 0.3f;
        }
        source.PlayOneShot(clip);
        birds.Clear();
        for (int i = 0; i < 4; i++)
        {
            birds.Add(player.gameObject.GetComponent<PlayerController>().birdEmpty);
            AutoSort.MoveDone = true;
        }
        yield return new WaitUntil(()=>flydone);
        player.gameObject.GetComponent<PlayerController>().LevelendCounter++;
        if (player.gameObject.GetComponent<PlayerController>().LevelendCounter == (player.gameObject.GetComponent<PlayerController>().Branches.Count - player.gameObject.GetComponent<PlayerController>().LD.leveDatas[PlayerPrefs.GetInt("LeveltoLoad")].No_of_EmptyBranches))
        {
            source.PlayOneShot(completeAudio);
            player.gameObject.GetComponent<PlayerController>().NextPanel.SetActive(true);
            player.gameObject.GetComponent<PlayerController>().LeveltoLoad = PlayerPrefs.GetInt("LeveltoLoad");
            player.gameObject.GetComponent<PlayerController>().LeveltoLoad++;
            PlayerPrefs.SetInt("LeveltoLoad", player.gameObject.GetComponent<PlayerController>().LeveltoLoad);

        }
        

        // player.gameObject.GetComponent<PlayerController>().LevelendCounter++;
    }
    public int getEmptySpace()
    {
        int emptyCount = 0;
        for (int i = 3; i >= 0; i--)
        {
            if (birds[i].tag == "empty")
            {
                emptyCount++;
            }

        }
        return emptyCount;
    }
    public Bird getlastBird()
    {
        Bird bird = player.gameObject.GetComponent<PlayerController>().birdEmpty;
        for (int i = 3; i >= 0; i--)
        {
            if (birds[i].tag != "empty")
            {

                bird = birds[i];
                indexofBird = i;
                Debug.Log("index of Bird   :  " + indexofBird);
                break;
            }
        }
        return bird;
    }
}



