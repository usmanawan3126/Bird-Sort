using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shuffle : MonoBehaviour
{
    public GameObject player;
    public Branch branch;
    Bird bird;
    Bird bird1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shuffle()
    {
        int BirdsToSort = 4;
        int counter;
        bool AnimDone = false;
        Vector3 pos;
        for (int i = player.gameObject.GetComponent<PlayerController>().Selected[0].birds.Count - 1; i >= 0; i--)
        {
            if (player.gameObject.GetComponent<PlayerController>().Selected[0].birds[i].tag == player.gameObject.GetComponent<PlayerController>().birdEmpty.tag)
            {
                BirdsToSort--;
            }
        }
        counter = BirdsToSort - 1;
        for (int i = 0; i < player.gameObject.GetComponent<PlayerController>().Selected[0].birds.Count; i++)
        {
            if (counter>=0)
            {
                bird = player.gameObject.GetComponent<PlayerController>().Selected[0].birds[0];
                bird1 = player.gameObject.GetComponent<PlayerController>().Selected[0].birds[counter];
                pos = player.gameObject.GetComponent<PlayerController>().Selected[0].birds[0].transform.position;
                player.gameObject.GetComponent<PlayerController>().Selected[0].birds[0].birdAnim?.SetTrigger("IdleToFly");
                player.gameObject.GetComponent<PlayerController>().Selected[0].birds[counter].birdAnim?.SetTrigger("IdleToFly");
                player.gameObject.GetComponent<PlayerController>().Selected[0].birds[0].transform.DOMove(player.gameObject.GetComponent<PlayerController>().Selected[0].spaces[counter].position, 1.0f);
                // player.Selected[0].birds[0].transform.position = player.Selected[0].spaces[counter].position;
                //player.Selected[0].birds[counter].transform.DOMove(pos, 1.0f).OnComplete(
                //        () =>
                //        {
                player.gameObject.GetComponent<PlayerController>().Selected[0].birds[0] = bird1;
                player.gameObject.GetComponent<PlayerController>().Selected[0].birds[counter] = bird;
                counter--;
            }
               
                        //});
            // player.Selected[0].birds[counter].transform.position = pos;
            //player.Selected[0].birds[0].birdAnim?.SetTrigger("IdleToFly");
            //player.Selected[0].birds[i].birdAnim?.SetTrigger("IdleToFly");
            //pos = player.Selected[0].birds[0].transform.position;
            //player.Selected[0].birds[0].transform.position = player.Selected[0].birds[i].transform.position;
            //player.Selected[0].birds[i] = bird;
            //Vector3 oldPos = player.Selected[0].birds[i].transform.position;
            //player.Selected[0].birds[i].transform.position = bird.transform.position;
            //bird.transform.position = oldPos;
            //player.Selected[0].birds[i].transform.position = pos;
            // player.Selected[0].birds[i].transform.position = player.Selected[0].spaces[0].transform.position;
        }
        player.gameObject.GetComponent<PlayerController>().Selected.Clear();


    }

    
}
