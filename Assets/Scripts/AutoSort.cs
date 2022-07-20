using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AutoSort : MonoBehaviour
{
    [HideInInspector]
    public int c ;
    [HideInInspector]
    public int c1;
    public PlayerController playerController;
    public static bool MoveDone=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void autosort()
    {
        List<Branch> SelectedBranches = new List<Branch>();
        for (int i = 0; i < playerController.Branches.Count; i++)
        {
            for (int j = i + 1; j < playerController.Branches.Count; j++)
            {
                if (playerController.Branches[j].getlastBird().tag != playerController.birdEmpty.tag)
                {


                    // Debug.Log("Last Bird Tag  :  " + playerController.Branches[j].getlastBird().tag + "  " + i);
                    if (playerController.Branches[i].getlastBird().tag == playerController.birdEmpty.tag) /*&& playerController.Branches[j].getlastBird().tag != playerController.birdEmpty.tag) || (playerController.Branches[j].getlastBird().tag == playerController.birdEmpty.tag && playerController.Branches[i].getlastBird().tag != playerController.birdEmpty.tag)*/
                    {
                        Debug.Log("Entered in Empty");
                        playerController.SelectB(playerController.Branches[j]);
                        playerController.SelectB(playerController.Branches[i]);
                        return;
                    }
                    //break;


                    else if (playerController.Branches[i].getlastBird().tag == playerController.Branches[j].getlastBird().tag)
                    {
                        if (playerController.Branches[i].getEmptySpace() > playerController.Branches[j].getEmptySpace())
                        {
                            playerController.SelectB(playerController.Branches[j]);
                            playerController.SelectB(playerController.Branches[i]);
                            return;
                        }
                        else
                        {
                            Debug.Log("Entered in Filled");
                            playerController.SelectB(playerController.Branches[i]);
                            playerController.SelectB(playerController.Branches[j]);
                            return;
                        }

                    }
                    Debug.Log("Value Of iiiiiiiiiiiiiiiiiiiiiiiiiiiiiii   :  " + i);
                    Debug.Log("Value Of jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj   :  " + j);
                }
            }
        }
    }
    public void Auto()
    {
        StartCoroutine(AutoMate());
    }
    IEnumerator AutoMate()
    {
        MoveDone = false;
        autosort();
        yield return new WaitUntil(()=> MoveDone);
        //if() untill birds sort
        StartCoroutine(AutoMate());
    }
}
