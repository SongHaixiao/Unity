﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRule : MonoBehaviour
{   
    public Unit Monster;        // monster
    public float InitTime;      // initial time
    public float Period;        // generate period
    public int MaxNum;          // maxmun quality
   

    public int HP;              // monster blood volume 
    public int Attack;          // monster attack

    float timerSinceLevelStart = 0; // record the time from level start to current

    float levelStartTime = 0;   // the time for starting level

    int num = 0;    // record the quality of monster

    float timer = 0;

    public UnitManager unitManager;   // Unit Manager

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;

        // monster quality more than maxmum, do nothing
        if (num >= MaxNum) return;

        if (timerSinceLevelStart > InitTime)
        {
            // spawn monster

            timer += Time.deltaTime;    // record per frame time for spawn monster period

            // if timer more than spawn monster period
            // timer reset 0
            // generate enemy
            if (timer > Period)
            {
                timer = 0;
                Enemy enemy = unitManager.GenerateEnemy(this.Monster.gameObject);
                enemy.MaxHP = this.HP;
                enemy.Attack = this.Attack;
                num++;

            }

        }

        
    }
}
