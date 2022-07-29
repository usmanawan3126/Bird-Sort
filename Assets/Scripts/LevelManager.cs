using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class LevelManager : MonoBehaviour
{
    //public List<Branch> b=new List<Branch>();
    public GameObject Player;
    public LevelDataManager levelData;
    public Branch branchPre;
    public int No_Of_Braches;
    public int No_Of_Empty;
    public List<Transform> Bspace = new List<Transform>();
    public GameObject canvasParent;
    int LeveltoLoad = 0;
    public Tutorial t;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
        //for (int i = No_Of_Empty; i > 0; i--)
        //{
        //    player.Branches[i].empty = true;
        //}
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void GenerateLevel()
    {
        if (t.tutor == true)
        {
            return;
        }
        else
        {
            LeveltoLoad = PlayerPrefs.GetInt("LeveltoLoad");
            No_Of_Braches = levelData.leveDatas[LeveltoLoad].No_of_Branches;
            No_Of_Empty = levelData.leveDatas[LeveltoLoad].No_of_EmptyBranches;
            Debug.Log("No Of Branchessssssssssssssss  :  " + No_Of_Braches);
            for (int i = 0; i < No_Of_Braches; i++)
            {
                player.Branches.Add(Instantiate(branchPre, transform.position = canvasParent.transform.position, Bspace[i].rotation));
                if (i < No_Of_Empty)
                {
                    player.Branches[i].empty = true;
                }
                player.Branches[i].transform.SetParent(canvasParent.transform);
                if (i % 2 != 0)
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

        }
            /// LeveltoLoad++;

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
    //public void Undo()
    //{
    //    Debug.Log("Number Of Branches  :  "+ player.Selected.Count);
    //    Debug.Log("Branch 1 name  :  "+ player.Selected[0].name);
    //}

    private PlayerController player
    {
        get
        {
            return PlayerController.instance;
        }
    }
    public void ReaetData()
    {
      //  int c = 0;
        PlayerPrefs.SetInt("LeveltoLoad",0);
    }
}
