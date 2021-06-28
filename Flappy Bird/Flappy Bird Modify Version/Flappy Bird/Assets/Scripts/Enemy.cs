using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{

    public float lifeTime = 4f;     // life time

    public event DeathMotify OnDeath;   // event ???

    public ENEMY_TYPE enemyType;    // enemy type

    public Vector2 range;

    float initY = 0;    // initial y

    // Start is called before the first frame update
    void Start()
    {
        this.ani = this.GetComponent<Animator>();   // get the Animator component : no - null value
        this.Fly();     // want the flying enemy when start
        initPos = this.transform.position;

        Destroy(this.gameObject, lifeTime);

        // randomly change the height of pipelines
        initY = Random.Range(range.x, range.y);
        this.transform.localPosition = new Vector3(6.0f, initY, 0);
    }
    
    //// enemy initialization
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

        float y = 0;

        // set the SWING enmey's y position
        if(enemyType == ENEMY_TYPE.SWING_ENEMY)
        {
            y = Mathf.Sin(Time.timeSinceLevelLoad) * 3f;
        }

        // change the position of enemy bird :
        // the distance per frame in x - axis
        this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime * speed, initY + y, 0);

        
        this.Fire();
       
    }

    //// Idle trigger
    //public void Idle()
    //{
    //    // when bird is Idle state

    //    // this.rigibodyBird.Sleep();           // close rigibody component
    //    this.rigibodyBird.simulated = false;    // close rigibody component
    //    this.ani.SetTrigger("Idle");            // set animition to Idel
    //}

    // Fly trigger

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
        this.ani.SetTrigger("Die");             // set animition to Die
        if (this.OnDeath != null)
        {
            this.OnDeath();
        }

        Destroy(this.gameObject, 0.2f);
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
    //        go.GetComponent<Element>().direction = -1;   // change enemy bullet as left

    //        // change the color or bullets, this method consumes the CPU very much when running

    //        /*
    //         * Method I ( consume a lot of CPU) : 
    //         *      
    //         * get the SpriteRenderer compoent to dynamicly change the color for bullets 
    //         */

    //        //SpriteRenderer[] sprs = go.GetComponentsInChildren<SpriteRenderer>();   // get the bullets' SpriteRenderer component

    //        //for (int i = 0; i < sprs.Length; i++)    // literately chang the color for bullets
    //        //{
    //        //    sprs[i].color = Color.red;          // color attribute under the SpriteRenderer componet
    //        //}



    //        /**
    //         * Method II ( reduce the CPU when running) :
    //         * 
    //         * we can make another format bullet update alone 
    //         * */

    //        fireTimer = 0f;
    //    }
    //}

    // collider

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Enemy : OnCollisionEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //this.Die();
    }

    // enter trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        Element bullet = col.gameObject.GetComponent<Element>();

        // no coming across with bullet
        if (bullet == null)
        {
            return;
        }

        Debug.Log("Enemy : OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        // if meet at the enemy bullet, player die
        if (bullet.side == SIDE.PLAYER)
        {
            this.Die();
        }
    }

    // exit trigger
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Enemy : ExitTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
            {
                this.OnScore(1);
            }
        }
    }
}
