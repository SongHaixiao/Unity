using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public event DeathMotify OnDeath;   // event ???

    // Start is called before the first frame update
    void Start()
    {
        this.ani = this.GetComponent<Animator>();   // get the Animator component : no - null value
        this.Idle();        // want idle player bird
    }

    
    //// player initialization
    //public void Init()
    //{
    //    this.transform.position = initPos;  // reset the bird to inital position
    //    this.Idle();                        // set animition to Idle state
    //    this.death = false;                 // reset the bird to alive
    //}
   

    // Update is called once per frame

    void Update()
    {
        //// if dead, do nothing
        //if (this.death) return;

        //// record the time of per frame
        //fireTimer += Time.deltaTime;

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
     
    //// Idle trigger
    //public void Idle()
    //{
    //    // when bird is Idle state
        
    //    // this.rigibodyBird.Sleep();           // close rigibody component
    //    this.rigibodyBird.simulated = false;    // close rigibody component
    //    this.ani.SetTrigger("Idle");            // set animition to Idel
    //}

    //// Fly trigger
    //public void Fly()
    //{
    //    // when bird is Fly state

    //    // this.rigibodyBird.WakeUp();          // open rigibody component
    //    this.rigibodyBird.simulated = true;     // open rigibody component
    //    this.ani.SetTrigger("Fly");             // set animition to Fly

    //}

    // die state

    public void Die()
    {
        this.death = true;
        if(this.OnDeath != null)
        {
            this.OnDeath();
        }
    }

    // collider
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Player : OnCollisionEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
         // this.Die();
    }
    
    // enter trigger
   void OnTriggerEnter2D(Collider2D col)
    {

        Element bullet = col.gameObject.GetComponent<Element>();
        Enemy enemy = col.gameObject.GetComponent<Enemy>();

        // no coming across with bullet
        if(bullet == null && enemy == null)
        {
            return;
        }

        Debug.Log("Player : OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        // if meet at the enemy bullet, reduce the HP
        // and 
        if (bullet != null && bullet.side == SIDE.ENEMY) 
        {
            // hit by one bullet, reduce HP with bullet power value
            this.HP -= bullet.power;

            // when player HP <= 0, to die
            if (this.HP <= 0) 
            {
                this.Die();
            }
        }

        // if meet at the enemy, player die
        if (enemy != null)
        {
            this.HP = 0;
            this.Die();
        }
    }

    // exit trigger
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Player : ExitTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if(this.OnScore != null)
            {
                this.OnScore(1);
            }
        }
    }

    //// fire method
    //public void Fire()
    //{
    //    // set the bullet interval
    //    // 1f / 10 -> 0.1s
    //    // a bullet / 0.1s -> 10 bullets /1
    //    if (fireTimer > 1f / fireRate)
    //    {
    //        GameObject go = Instantiate(bulletTemplate);
    //        go.transform.position = this.transform.position;

    //        fireTimer = 0f;
    //    }
    //}

}
