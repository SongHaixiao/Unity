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

    public PipelineManager pipelineManager; // PipelineManager

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
        uiReady.SetActive(true);
        this.Status = GAME_STATUS.READY;
        this.player.OnDeath += Player_OnDeath;
        this.player.OnScore = OnPlayerScore;
    }

    // when player is died
    // load death PanelGameOver
    private void Player_OnDeath()
    {
        this.Status = GAME_STATUS.OVER;
        UpdateUI();
        this.pipelineManager.Stop();
    }

    // score method
    void OnPlayerScore(int score)
    {
        this.Score += score;
    }


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
        UpdateUI();
    }


    // game start
    public void StartGame()
    {
        this.Status = GAME_STATUS.INGAME;

        Debug.LogFormat(" Start Game : {0}", this.Status);

        this.UpdateUI();

        pipelineManager.StartRun();
        player.Fly();
    }

    // restart game
    public void Restart()
    {
        this.Status = GAME_STATUS.READY;
        this.pipelineManager.Init();
        this.player.Init();
        this.Score = 0;
    }
}
