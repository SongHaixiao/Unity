using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public int LevelID;
    public string Name;

    public Boss Boss;

    public List<SpawnRule> Rules = new List<SpawnRule>();

    public UnitManager unitManager;   // Unit Manager

    public UnityAction<LEVEL_RESULT> OnLevelEnd;    // generalization ？？？

    public float bossTime = 60f;   // boss time 60s

    float timerSinceLevelStart = 0; // record the time from level start to current

    float levelStartTime = 0;   // the time for starting level

    Boss boss = null;

    public enum LEVEL_RESULT           // level result type : no result, success, faild 
    {
        NONE,
        SUCCESS,
        FAILD
    }

    public LEVEL_RESULT resultType = LEVEL_RESULT.NONE; // result type

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunLevel());
    }

    IEnumerator RunLevel()
    {
        UIManager.Instance.ShowLevelStart(string.Format("LEVEL {0} {1}", this.LevelID, this.Name));
        yield return new WaitForSeconds(2f);

        // spawn monster rules
        for (int i = 0; i < Rules.Count; i++)
        {
            SpawnRule rule = Instantiate<SpawnRule>(Rules[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;

        // reach the boss time
        if (timerSinceLevelStart > bossTime)
        {
            // avoid 
            if (this.resultType != LEVEL_RESULT.NONE)   return;

            // boss is not null
            if (boss == null)
            {

                boss = (Boss)UnitManager.Instance.GenerateEnemy(this.Boss.gameObject);
                boss.target = Game.Instance.player;
                boss.Fly();
                boss.OnDeath += Boss_OnDeath;
            }
        }
    }

    // boss death
    private void Boss_OnDeath(Unit sender)
    {
        // boss dide,level success
        this.resultType = LEVEL_RESULT.SUCCESS;

        // boss dide, load
        if (this.OnLevelEnd != null)
        {
            this.OnLevelEnd(this.resultType);
        }
    }
}
