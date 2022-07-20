using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //public GameObject[] birds;
   
    public Bird item;
    List<Bird> Birds = new List<Bird>();
    List<Bird> Empty = new List<Bird>();
    public List<Branch> Branches =new List<Branch>();
    public List<Branch> Selected = new List<Branch>();
    List<GameObject> SelectedB = new List<GameObject>();
    float pos = 0.1f;
    public float i = 0.21f;
    Transform firstChild;
    [HideInInspector]
    public int currentB = 0;
    //[HideInInspector]
    public int emptyBranch = 0;
    Random r = new Random();
    public List<Bird> Addedbirds = new List<Bird>();
    public Bird birdEmpty;
    int b1;
    public GameObject block;
    public Undo u;

    //public List<Branch> Branc = new List<Branch>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(block,transform.position=new Vector3(0, 5.3145f, 0),Quaternion.identity);
        

        int birdCount = 0;
        int c1 = 0;
        for (int i = 0; i < Branches.Count; i++)
        {
            Debug.Log("Branches : "+i);
            Branches[i].Init(Branches[i].empty, getBirdType());
            Birds.AddRange(Branches[i].getBird());
            int c = UnityEngine.Random.Range(2, 4);
            birdCount += c;
            Branches[i].birdcount = c;
        }
         c1 = (Branches.Count() * 4) - emptyBranch * 4;
        Debug.Log("Value of Empty Branch : "+ emptyBranch);
        Debug.Log("Bird Count Before : " + birdCount);
        if (birdCount!=c1)
        {
            while (birdCount != c1)
            {
                birdCount++;
            }
        }
        Debug.Log("Bird Count After : " + birdCount);
        int BirdsRemain = ((Branches.Count()) * 4) - birdCount;
        Debug.Log("Birds Remain" + BirdsRemain);

        for (int i = 0; i < BirdsRemain; i++)
        {
            Empty.Add(birdEmpty);
        }
        Birds.AddRange(Empty);
        Debug.Log("birdCount : " + birdCount);
        //if (birdCount > Birds.Count)
        //{
        //    Debug.Log("Birds.Count : " + Birds.Count);
        //    int remaing = birdCount - Birds.Count;

        //    Debug.Log("Remaining : " + remaing);
        //    Branches[Branches.Count()-1].birdcount -= remaing;
        //}
        //else if (birdCount < Birds.Count)
        //{
        //    Debug.Log("Birds.Count : "+ Birds.Count);
        //    int remaing = Birds.Count - birdCount;
        //    Debug.Log("Remaining : " + remaing);
        //    //Branches[0].birdcount = Birds.Count- remaing;
        //    Branches[Branches.Count()-1].birdcount += remaing;
        //}
        //Birds = items.Select(x => new { value = x, order = rnd.Next() })
        //     .OrderBy(x => x.order).Select(x => x.value).ToList()
        //foreach (var item in Birds)
        //{
        //    Debug.Log(item);
        //}
        var RndB = new System.Random();
        Birds = Birds.Select(x => new { value = x, order = r.Next() })
             .OrderBy(x => x.order).Select(x => x.value).ToList();
        //Debug.Log("----------------------");
        //foreach (var item in Birds)
        //{
        //    Debug.Log(item);
        //}
        int count = 0;
        for (int i = 0; i < Branches.Count; i++)
        {
            count = Branches[i].birdcount;
           // Debug.Log(count);
            Branches[i].AssignBirds(Birds.GetRange(0, 4));
            Birds.RemoveRange(0, 4);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // i++;
        //int c = 0;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 mousepos2D = new Vector2(mousepos.x, mousepos.y);
        //    RaycastHit2D hit = Physics2D.Raycast(mousepos2D, Vector2.zero);
        //    if (hit.collider.gameObject.tag == "Branch")
        //    {
        //       currentI = Branches.IndexOf(hit.collider.gameObject);
        //        if (branch.Count == 0)
        //        {
        //            Debug.Log("Children are 0");
        //            Debug.Log(Selected.Count);
        //            if (Selected.Count > 0)
        //            {
        //                for (int i = 0; i < c; i++)
        //                {
        //                    Debug.Log(Selected[i].name);
        //                    Selected[i].gameObject.transform.parent = branch[currentI].transform;
        //                    Selected[i].transform.position = Vector3.Lerp(Selected[0].transform.position, branch[currentI].gameObject.transform.position, Time.time);
        //                    pos = pos + pos;
        //                }
        //                return;
        //            }

        //        }


        //        firstChild = branch[currentI].transform.GetChild(0);
        //        c = Selected.Count;
        //        Debug.Log(c);

        //        if (Selected.Count > 0 && firstChild.gameObject.tag == Selected[0].gameObject.tag)
        //        {

        //            for (int i = 0; i < c; i++)
        //            {
        //                Selected[i].gameObject.transform.parent = branch[currentI].transform;
        //                Selected[i].transform.position = Vector3.Lerp(Selected[0].transform.position, new Vector2(firstChild.gameObject.transform.position.x - pos, firstChild.gameObject.transform.position.y), Time.time);
        //                pos = pos + pos;
        //            }
        //            Selected.Clear();
        //        }
        //        else
        //        {
        //            Debug.Log("Colours not matched");
        //            Selected.Clear();
        //            Selected.Add(firstChild.gameObject);
        //            firstChild = branch[currentI].transform.GetChild(1);
        //            Debug.Log("Added 1st Object");
        //            if (firstChild.gameObject.tag == Selected[0].gameObject.tag)
        //            {
        //                Selected.Add(firstChild.gameObject);
        //                Debug.Log("Added 2nd Object");
        //            }
        //        }
        //    }
        //}
        //Debug.Log("sELECTED : "+ Selected.Count);

    }
    public void SelectB(Branch b)
    {
        //foreach (var item in b.GetMatchingBrids())
        //{
        //    Debug.Log("Bird Tag: "+ item.tag);
        //}
        Debug.Log("Selected");
        if (Selected.Count < 2)
        {
            if (Selected.Contains(b))
            {
                return;
            }
            Selected.Add(b);
            // return;
        }
        if (Selected.Count == 2)
        {
            u.BrancUndo.Add(Selected[0]);
            u.BrancUndo.Add(Selected[1]);
            Selected[1].AddBirds(Selected[0], Selected[1]);
            Selected.Clear();
            return;
        }

    }
    int same = 0;
    BirdType getBirdType()
    {
        if (same < 9)
        {
            same++;
        }
        if (same == 9)
        {
            same = 0;
        }
        return (BirdType)same;
    }
    public void Res()
    {
        SceneManager.LoadScene(0);
    }
}

public enum BirdType
{
    Parrot,
    Parrot2,
    Toucan,
    Tweety,
    BlackBird,
    Pigeon,
    Redbird,
    Sparrow,
    YellowSparrow
    //Yellow,
    //Green,
    //Red,

    //Blue
}