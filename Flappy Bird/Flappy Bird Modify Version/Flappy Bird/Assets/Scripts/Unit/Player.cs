using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{

    public float invincibleTime = 3f;   // protect time

    private float timer = 0;            

    // Update is called once per frame
    public override void OnUpdate()
    {
        if (this.death)
            return;

        timer += Time.deltaTime;

        // get current location
        Vector2 pos = this.transform.position;

        // change the position of x - axis and y - axis via keyboard input
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;  // A，D --- Horizontal Moving
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;    // W，S --- Vertical   Moving

        // give the new position to original character
        this.transform.position = pos;

        // if clicke mouse left button player bird fire
        if (Input.GetButton("Fire1"))
        {
            this.Fire();
        }

    }

    // bring back to life
    public void Rebirth()
    {
        StartCoroutine(DoRebirth());
    }

    // bring back to life
    IEnumerator DoRebirth()
    {
        yield return new WaitForSeconds(2f);
        timer = 0;
        this.Init();
        this.Fly();

    }

    // protect time
    public bool IsInvincible
    {
        // if timer < protect time, player is invincible
        get { return timer < this.invincibleTime; }
    }


    // enter trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.death)
            return;

        if (this.IsInvincible)
            return;

        Item item = col.gameObject.GetComponent<Item>();

        // if player get the item, use this item
        if (item != null)
        {
            item.Use(this);
        }


        Element bullet = col.gameObject.GetComponent<Element>();
        Enemy enemy = col.gameObject.GetComponent<Enemy>();

        // no coming across with bullet
        if(bullet == null && enemy == null)
        {
            return;
        }

        Debug.Log("Player : OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        // if meet at the enemy bullet, reduce the HP
        // bullet is not null and bullet should be from enemy
        if (bullet != null && bullet.side == SIDE.ENEMY) 
        {
            // do damage
            this.Damage(bullet.power);
        }

        // if meet at the enemy, player die
        if (enemy != null)
        {
            this.Die();
        }
    }

    // exit trigger
    void OnTriggerExit2D(Collider2D col)
    {
        if (this.death)
            return;

        if (this.IsInvincible)
            return;

        Debug.Log("Player : ExitTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if(this.OnScore != null)
            {
                this.OnScore(1);
            }
        }
    }

}
