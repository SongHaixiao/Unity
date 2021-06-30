using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int LevelID;
    public string Name;

    // Level Rule
    public class SpawnRule
    {
        public int ID;
        public string Name;

        public Unit Monster;
        public int Period;
        public int MaxNum;
    }

    public Unit Monster1;
    public int Monster1Period;
    public int Monster1MaxNum;

    public Unit Monster2;
    public int Monster2Period;
    public int Monster2MaxNum;

    public Unit Monster3;
    public int Monster3Period;
    public int Monster3MaxNum;

    public Unit Monster4;
    public int Monster4Period;
    public int Monster4MaxNum;

    public List<SpawnRule> Rules = new List<SpawnRule>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
