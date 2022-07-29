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
    public GameObject player;
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
        for (int i = 0; i < player.gameObject.GetComponent<PlayerController>().Branches.Count; i++)
        {
            for (int j = i + 1; j < player.gameObject.GetComponent<PlayerController>().Branches.Count; j++)
            {
                if (player.gameObject.GetComponent<PlayerController>().Branches[j].getlastBird().tag != player.gameObject.GetComponent<PlayerController>().birdEmpty.tag)
                {


                    // Debug.Log("Last Bird Tag  :  " + playerController.Branches[j].getlastBird().tag + "  " + i);
                    if (player.gameObject.GetComponent<PlayerController>().Branches[i].getlastBird().tag == player.gameObject.GetComponent<PlayerController>().birdEmpty.tag) /*&& playerController.Branches[j].getlastBird().tag != playerController.birdEmpty.tag) || (playerController.Branches[j].getlastBird().tag == playerController.birdEmpty.tag && playerController.Branches[i].getlastBird().tag != playerController.birdEmpty.tag)*/
                    {
                        Debug.Log("Entered in Empty");
                        player.gameObject.GetComponent<PlayerController>().SelectB(player.gameObject.GetComponent<PlayerController>().Branches[j]);
                        player.gameObject.GetComponent<PlayerController>().SelectB(player.gameObject.GetComponent<PlayerController>().Branches[i]);
                        return;
                    }
                    //break;


                    else if (player.gameObject.GetComponent<PlayerController>().Branches[i].getlastBird().tag == player.gameObject.GetComponent<PlayerController>().Branches[j].getlastBird().tag)
                    {
                        if (player.gameObject.GetComponent<PlayerController>().Branches[i].getEmptySpace() > player.gameObject.GetComponent<PlayerController>().Branches[j].getEmptySpace())
                        {
                            player.gameObject.GetComponent<PlayerController>().SelectB(player.gameObject.GetComponent<PlayerController>().Branches[j]);
                            player.gameObject.GetComponent<PlayerController>().SelectB(player.gameObject.GetComponent<PlayerController>().Branches[i]);
                            return;
                        }
                        else
                        {
                            Debug.Log("Entered in Filled");
                            player.gameObject.GetComponent<PlayerController>().SelectB(player.gameObject.GetComponent<PlayerController>().Branches[i]);
                            player.gameObject.GetComponent<PlayerController>().SelectB(player.gameObject.GetComponent<PlayerController>().Branches[j]);
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
