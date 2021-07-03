using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoSingleton<Game>
{
    public Player player;

    public Player currentPlayer;    // target

    // the id for current level
    public int currentLevelId = 1;

    GAME_STATUS status;         // attribute

    public GAME_STATUS Status
    {
        get { return status; }
        set
        {
            this.status = value;
            UIManager.Instance.UpdateUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Status = GAME_STATUS.READY;
        this.player.OnDeath += Player_OnDeath;
    }

    // when player is died
    // load death PanelGameOver
    private void Player_OnDeath(Unit sender)
    {
        // if player no left life
        // player die, update over ui, stop player
        if (player.life <= 0)
        {
            this.Status = GAME_STATUS.OVER;
            UnitManager.Instance.Clear();
        }

        // player die but left lief, bring back to life
        else
        {
            player.Rebirth();   // bring back to life
        }
       
    }

    // game start
    public void StartGame()
    {
        InitPlayer();
        this.Status = GAME_STATUS.INGAME;
        Debug.LogFormat(" Start Game : {0}", this.Status);
        player.Fly();
        LoadLevel();
    }

    // load level
    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(this.currentLevelId);   // load new id level
        LevelManager.Instance.level.OnLevelEnd = OnLevelEnd;  // set the level type
    }

    // when level end, check the level result type
    void OnLevelEnd(Level.LEVEL_RESULT result)
    {
        // if this level success
        if(result == Level.LEVEL_RESULT.SUCCESS)
        {
            this.currentLevelId++;
            this.LoadLevel();

        }

        // if this level failed
        else 
        {
            this.Status = GAME_STATUS.OVER;
        }
    } 

    // restart game
    public void Restart()
    {
        this.Status = GAME_STATUS.READY;
        InitPlayer();
    }

    // initialize player
    void InitPlayer()
    {
        this.player.Init();
    }
}
