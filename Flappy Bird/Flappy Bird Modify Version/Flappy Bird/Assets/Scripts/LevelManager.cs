using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels;

    public int currentLevelId = 1;    // current level id

    public Level level;

    public UnitManager unitManager;   // Unit Manager

    public Player currentPlayer;    // target

    public void LoadLevel(int levelID)
    {

        this.level = Instantiate<Level>(levels[levelID - 1]);
        this.level.unitManager = this.unitManager;
        this.level.currentPlayer = this.currentPlayer;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
