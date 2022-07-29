using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Levelgenerator",menuName = "LevelDataManager")]
public class LevelDataManager : ScriptableObject
{
    [SerializeField]
    public List<LeveData> leveDatas;   
}

[System.Serializable]
public class LeveData
{
    public int Level_no;
    public int No_of_Branches;
    public int No_of_EmptyBranches;
    bool locked;
}
