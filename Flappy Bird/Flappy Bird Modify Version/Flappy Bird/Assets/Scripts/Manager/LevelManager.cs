﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public List<Level> levels;

    public int currentLevelId = 1;    // current level id

    public Level level;

    public void LoadLevel(int levelID)
    {

        this.level = Instantiate<Level>(levels[levelID - 1]);
    }
}
