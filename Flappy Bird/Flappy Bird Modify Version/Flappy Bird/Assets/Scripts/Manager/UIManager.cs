using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject uiReady;
    public GameObject uiIngame;
    public GameObject uiGameOver;

    public Slider hpBar;    // hp bar slider

    public Text uiLife;             // player life text in game
    public Text uiLevelName;        // text for level name
    public Text uiLevelStartName;   // text for start level name

    public GameObject uiLevelStart; // ui level start
    public GameObject uiLevelEnd;   // ui level end


    // Start is called before the first frame update
    void Start()
    {
        uiReady.SetActive(true);
    }

    // update level name
    public void ShowLevelStart(string name)
    {
        this.uiLevelName.text = name;
        this.uiLevelStartName.text = name; 
        uiLevelStart.SetActive(true);
    }

    // update life
    public void UpdateLife(int life)
    {
        this.uiLife.text = life.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // linearly interpolates between a and b by t
        this.hpBar.value = Mathf.Lerp(this.hpBar.value, Game.Instance.player.HP, 0.1f);
        if (Game.Instance.player != null)
            this.uiLife.text = Game.Instance.player.life.ToString();
    }

    // update UI
   public void UpdateUI()
    {
        uiReady.SetActive(Game.Instance.Status == GAME_STATUS.READY);        // if game state is Ready active the panle ready to start the game
        uiIngame.SetActive(Game.Instance.Status == GAME_STATUS.INGAME);      // if game state is InGame active the panle in game to play the game
        uiGameOver.SetActive(Game.Instance.Status == GAME_STATUS.OVER);     // if game state is GameOver active the panle game over to end the game  
        this.hpBar.maxValue = Game.Instance.player.MaxHP;
        this.hpBar.value = Game.Instance.player.HP;
    }
}
