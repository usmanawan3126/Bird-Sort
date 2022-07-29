using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
using UnityEngine.SceneManagement;
[System.Serializable]
public class FlyingBirds
{
    [SerializeField]
    public List<Bird> Birds;
    [SerializeField]
    public Branch ToBranch;
    [SerializeField]
    public Branch FromBranch;
    [SerializeField]
    public FlyingBirds flyBird; 

}
 public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public List<FlyingBirds> FlyingBirdsList =new List<FlyingBirds>();
    public Bird item;
    List<Bird> Birds = new List<Bird>();
    List<Bird> Empty = new List<Bird>();
    public List<Branch> Branches =new List<Branch>();
    public List<Branch> Selected = new List<Branch>();
    public List<Branch> SelectedB = new List<Branch>();
    float pos = 0.1f;
    public float i = 0.21f;
    Transform firstChild;
    [HideInInspector]
    public int currentB = 0;
    public int emptyBranch = 0;
    Random r = new Random();
    public Bird birdEmpty;
    int b1;
  public GameObject block;
    public static PlayerController instance;
    public List<Bird> Undobirds = new List<Bird>();
    public List<List<Bird>> UndobirdsMain = new List<List<Bird>>();
    public List<BranchHistory> birdsList=new List<BranchHistory>();
    public bool AddUndo = true;
    public bool ExtraUndo=false;
    public int LevelendCounter = 0;
    public LevelDataManager LD;
    public GameObject NextPanel;
    public int LeveltoLoad=0;
    public AudioClip clip;
    // public BranchHistory undoObject;
    private void Awake()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        instance = this;
    }

    void Start()
    {
        
        NextPanel.SetActive(false);
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
      
        var RndB = new System.Random();
        Birds = Birds.Select(x => new { value = x, order = r.Next() })
             .OrderBy(x => x.order).Select(x => x.value).ToList();
       
        int count = 0;
        for (int i = 0; i < Branches.Count; i++)
        {
            count = Branches[i].birdcount;
          
            Branches[i].AssignBirds(Birds.GetRange(0, 4));
            Birds.RemoveRange(0, 4);
        }
    }
    void Update()
    {
        

    }
    public void SelectB(Branch b)
    {
        Debug.Log("Selected");
        if (this.Selected.Count < 2)
        {
            b.GetComponent<Branch>().GetMatchingBrids(b);
            if (this.Selected.Contains(b))
            {
                return;
            }
            this.Selected.Add(b);
        }
        if (Selected.Count == 2)
        {
            this.SelectedB.Add(this.Selected[0]);
            this.SelectedB.Add(this.Selected[1]);
            
            this.Selected[1].AddBirds(this.Selected[0], this.Selected[1]);
            this.Selected.Clear();  
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
    
    [SerializeField]
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
}
