using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour
{
    public Branch b;
    public List<Branch> BrancUndo = new List<Branch>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void undoBirds()
    {
        Debug.Log("Branch Undo Count"+BrancUndo.Count);
        for (int i = 3; i >= 0; i--)
        {
            if (BrancUndo[0].birds[i].tag!="empty")
            {
                Debug.Log("Bird Nameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee  :  "+ b.BrancUndo[1].birds[i].tag);
            }
            
        }
        //for (int i = 0; i < b.BirdsUndo.Count; i++)
        //{
            
        //}
    }
}
