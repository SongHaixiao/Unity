using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int LevelID;
    public string Name;

    public Boss Boss;

    public List<SpawnRule> Rules = new List<SpawnRule>();

    public UnitManager unitManager;   // Unit Manager

    float timer = 0;

    float bossTime = 60f;   // boss time 60s

    float timerSinceLevelStart = 0; // record the time from level start to current

    float levelStartTime = 0;   // the time for starting level

    public Player currentPlayer;    // target

    Boss boss = null;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Rules.Count; i++)
        {
            SpawnRule rule = Instantiate<SpawnRule>(Rules[i]);
            rule.unitManager = this.unitManager;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timerSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;

        // reach the boss time
        if (timerSinceLevelStart > bossTime)
        {
            // boss is not null
            if (boss == null)
            {
                boss = (Boss)unitManager.GenerateEnemy(this.Boss.gameObject);
                boss.target = currentPlayer;
            }
        }
    }
}
