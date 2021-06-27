using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigibodyBird;

    public Animator ani;            // operate animation

    public float force = 150f;

    private bool death = false;     // die flag

    public delegate void DeathMotify();  // delegate ???

    public event DeathMotify OnDeath;   // event ???

    public UnityAction<int> OnScore;    // generalization ？？？


    private Vector3 initPos;  // inital position

    // Start is called before the first frame update
    void Start()
    {
        this.ani = this.GetComponent<Animator>();   // get the Animator component : no - null value
        this.Idle();
    }

    // player initialization
    public void Init()
    {
        this.transform.position = initPos;  // reset the bird to inital position
        this.Idle();                        // set animition to Idle state
        this.death = false;                 // reset the bird to alive
    }

    // Update is called once per frame
    void Update()
    {
        // if dead, do nothing
        if (this.death) return;     

        // if alive, click mouse left key to control the bird
        if (Input.GetMouseButtonDown(0))
        {
            rigibodyBird.velocity = Vector2.zero;
            rigibodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }
    }
     
    // Idle trigger
    public void Idle()
    {
        // when bird is Idle state
        
        // this.rigibodyBird.Sleep();           // close rigibody component
        this.rigibodyBird.simulated = false;    // close rigibody component
        this.ani.SetTrigger("Idle");            // set animition to Idel
    }

    // Fly trigger
    public void Fly()
    {
        // when bird is Fly state

        // this.rigibodyBird.WakeUp();          // open rigibody component
        this.rigibodyBird.simulated = true;     // open rigibody component
        this.ani.SetTrigger("Fly");             // set animition to Fly
    }

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
        Debug.Log("OnCollisionEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        this.Die();
    }
    
    // enter trigger
   void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("On TriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.name.Equals("ScoreArea"))
        {
            // empty
        }
        else
        {
            this.Die();
        }
    }

    // exit trigger
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Exit TriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if(this.OnScore != null)
            {
                this.OnScore(1);
            }
        }
    }
}
