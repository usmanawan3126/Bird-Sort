using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour
{
    // Start is called before the first frame update
   //private GameObject player;
    public PlayerController player;
    List<Bird> setA = new List<Bird>();
    List<Bird> setB = new List<Bird>();
    void Start()
    {
        
    }
    //private void OnEnable()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
    public void undo()
    {
        player.gameObject.GetComponent<PlayerController>().ExtraUndo = false;
        int i = player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count - 1;
        int j = player.gameObject.GetComponent<PlayerController>().SelectedB.Count - 1;
        if (player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count != 0)
        {

            if (player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i].Birds.Count == 4)
            {
                for (int k = 0; k < (player.FlyingBirdsList[player.FlyingBirdsList.Count - 1].Birds.Count ) - (player.FlyingBirdsList[player.FlyingBirdsList.Count - 2].Birds.Count ); k++)
                {
                    setA.Add(player.FlyingBirdsList[player.FlyingBirdsList.Count - 1].Birds[k]);
                }
                for (int k = 0; k < player.FlyingBirdsList[player.FlyingBirdsList.Count - 2].Birds.Count; k++)
                {
                    setB.Add(player.FlyingBirdsList[player.FlyingBirdsList.Count - 2].Birds[k]);
                }
                Debug.Log("Set AAAAAAAA : "+setA.Count);
                player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i].FromBranch.MoveBird(setA, player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i].ToBranch,
                () =>
                {
                    for (int k = 0; k < setA.Count; k++)
                    {
                        player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i].ToBranch.birds[k] = setA[k];

                    }

                    player.gameObject.GetComponent<PlayerController>().ExtraUndo = true;

                    player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Remove(player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i]);
                    player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Remove(player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i]);
                    player.gameObject.GetComponent<PlayerController>().SelectedB.Remove(player.gameObject.GetComponent<PlayerController>().SelectedB[j]);
                    player.gameObject.GetComponent<PlayerController>().SelectedB.Remove(player.gameObject.GetComponent<PlayerController>().SelectedB[j - 1]);
                });
                player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i - 1].FromBranch.MoveBird(player.FlyingBirdsList[player.FlyingBirdsList.Count - 2].Birds, player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i - 1].ToBranch,
                () =>
                {
                    for (int k = 0; k < player.FlyingBirdsList[player.FlyingBirdsList.Count - 2].Birds.Count; k++)
                    {
                        player.FlyingBirdsList[i-1].ToBranch.birds[k] = setB[k];

                    }

                    player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Remove(player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i-1]);
                    player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Remove(player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i-1]);
                    player.gameObject.GetComponent<PlayerController>().SelectedB.Remove(player.gameObject.GetComponent<PlayerController>().SelectedB[j-1]);
                    player.gameObject.GetComponent<PlayerController>().SelectedB.Remove(player.gameObject.GetComponent<PlayerController>().SelectedB[j - 1]);
                });
            }
            else
            {
                Debug.Log("Player  Fb List  :  " + player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Count);

                player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i].FromBranch.MoveBird(player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i].Birds, player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i].ToBranch,
                    () =>
                    {

                        player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Remove(player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i]);
                        player.gameObject.GetComponent<PlayerController>().FlyingBirdsList.Remove(player.gameObject.GetComponent<PlayerController>().FlyingBirdsList[i]);
                        player.gameObject.GetComponent<PlayerController>().SelectedB.Remove(player.gameObject.GetComponent<PlayerController>().SelectedB[j]);
                        player.gameObject.GetComponent<PlayerController>().SelectedB.Remove(player.gameObject.GetComponent<PlayerController>().SelectedB[j - 1]);
                    });
            }
                
            
            
           
        }
    }
}
