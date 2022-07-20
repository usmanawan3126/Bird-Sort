using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    //public List<Branch> b=new List<Branch>();
    public GameObject Player;

    public Branch branchPre;
    public int No_Of_Braches;
    public int No_Of_Empty;
    public PlayerController player;
    public List<Transform> Bspace = new List<Transform>();
    public GameObject canvasParent;
    // Start is called before the first frame update
    private void Awake()
    {
      //  Instantiate(Player, transform.position, Quaternion.identity);
    }
    void Start()
    {
        
        for (int i = 0; i < No_Of_Braches; i++)
        {
            player.Branches.Add(Instantiate(branchPre, transform.position = canvasParent.transform.position, Bspace[i].rotation));
            if (i < No_Of_Empty)
            {
                player.Branches[i].empty = true;
                
            }
            player.Branches[i].transform.SetParent(canvasParent.transform);
            if (i%2!=0)
            {
                player.Branches[i].gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                player.Branches[i].gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            
            player.Branches[i].transform.position = Bspace[i].position;
            if (i < No_Of_Empty)
            {
                player.Branches[i].empty = true;
            }

            Debug.Log("Branch Spawn");
        }
        //for (int i = No_Of_Empty; i > 0; i--)
        //{
        //    player.Branches[i].empty = true;
        //}
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void AddBranch()
    {
      
        player.Branches.Add(Instantiate(branchPre, transform.position = canvasParent.transform.position, Bspace[No_Of_Braches].rotation));
        player.Branches[No_Of_Braches].transform.SetParent(canvasParent.transform);
       // player.Branches[No_Of_Braches].transform.position = Bspace[No_Of_Braches].position;
        //  player.Branches[No_Of_Braches].empty = true;
        if (No_Of_Braches % 2 != 0)
        {
            player.Branches[No_Of_Braches].gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            player.Branches[No_Of_Braches].gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        for (int i = 0; i <= 3; i++)
        {
            Debug.Log("Name of  of Branches  :  " + player.Branches[player.Branches.Count-1].name);
            player.Branches[player.Branches.Count-1].birds.Add(player.birdEmpty);
            player.Branches[player.Branches.Count - 1].birds[i].transform.position = player.Branches[player.Branches.Count - 1].spaces[i].position;
        }
        player.Branches[No_Of_Braches].transform.position = Bspace[No_Of_Braches].position;
        No_Of_Braches++;

    }
}
