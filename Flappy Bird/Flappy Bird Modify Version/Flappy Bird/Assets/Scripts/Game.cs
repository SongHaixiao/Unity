using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject uiReady;
    public GameObject uiIngame;
    public GameObject uiGameOver;

    public Player player;

    public int score;       // record score

    public Text uiScore;    // score text in game
    public Text uiScore2;   // score text in end

    public Slider hpBar;    // hp bar slider

    public int Score
    {
        get { return score; }
        set
        {
            this.score = value;
            this.uiScore.text = this.score.ToString();
            this.uiScore2.text = this.score.ToString();
        }
    }

    // public PipelineManager pipelineManager; // Pipeline Manager
    public UnitManager enemyManager;   // Enemy Manager

    public enum GAME_STATUS
    {
        READY,
        INGAME,
        OVER
    }

    GAME_STATUS status;         // attribute


    private GAME_STATUS Status
    {
        get { return status; }
        set
        {
            this.status = value;
            this.UpdateUI();
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
        this.Status = GAME_STATUS.OVER;
        UpdateUI();
        this.enemyManager.Stop();
    }

    //private void Player_OnDeath(Unit sender)
    //{
    //    if (player.life <= 0)
    //    {
    //        this.Status = GAME_STATUS.OVER;
    //        UnitManager.Instance.Clear();
    //    }
    //    else
    //    {
    //        player.Rebirth();
    //    }
    //}



    // update UI
    void UpdateUI()
    {
        uiReady.SetActive(this.Status == GAME_STATUS.READY);        // if game state is Ready active the panle ready to start the game
        uiIngame.SetActive(this.Status == GAME_STATUS.INGAME);      // if game state is InGame active the panle in game to play the game
        uiGameOver.SetActive(this.Status == GAME_STATUS.OVER);  // if game state is GameOver active the panle game over to end the game  
    }

    // Update is called once per frame
    void Update()
    {
        // linearly interpolates between a and b by t
        this.hpBar.value = Mathf.Lerp(this.hpBar.value,this.player.HP, 0.1f);

        UpdateUI();
    }


    // game start
    public void StartGame()
    {
        InitPlayer();

        this.Status = GAME_STATUS.INGAME;

        Debug.LogFormat(" Start Game : {0}", this.Status);

        this.UpdateUI();

        // this.pipelineManager.StartRun();
        this.enemyManager.StartRun();

        player.Fly();

        this.hpBar.value = this.player.HP;
    }

    //public void StartGame()
    //{
    //    InitPlayer();
    //    this.Status = GAME_STATUS.INGAME;
    //    Debug.LogFormat("StartGame:{0}", this.status);
    //    player.Fly();
    //    LoadLevel();
    //}

    // level load
    //private void LoadLevel()
    //{
    //    LevelManager.Instance.LoadLevel(this.currentLevelId);
    //    LevelManager.Instance.level.OnLevelEnd = OnLevelEnd;
    //}

    //// level end
    //void OnLevelEnd(Level.LEVEL_RESULT result)
    //{
    //    if (result == Level.LEVEL_RESULT.SUCCESS)
    //    {
    //        this.currentLevelId++;
    //        this.LoadLevel();
    //    }
    //    else
    //    {
    //        this.Status = GAME_STATUS.OVER;
    //    }
    //}

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
