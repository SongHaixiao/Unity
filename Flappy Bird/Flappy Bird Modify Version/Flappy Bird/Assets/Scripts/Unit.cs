using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public Rigidbody2D rigibodyBird;

    public Animator ani;            // operate animation

    public float speed = 100f;      // the speed of object

    public float fireRate = 10f;    // fire rate

    protected bool death = false;     // die flag

    public delegate void DeathMotify();  // delegate ???
    
    public UnityAction<int> OnScore;    // generalization ？？？

    protected Vector3 initPos;  // inital position

    public GameObject bulletTemplate;   // bullet template

    protected float fireTimer = 0f;  // fire counter

    public float HP = 1000f;       // HP value


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if dead, do nothing
        if (this.death) return;

        // record the time of per frame
        fireTimer += Time.deltaTime;
    }

    // initialization
    public void Init()
    {
        this.transform.position = initPos;  // reset  to inital position
        this.Idle();                        // set animition to Idle state
        this.death = false;                 // reset to alive
    }

    // Idle trigger
    public void Idle()
    {
        // when is Idle state

        // this.rigibodyBird.Sleep();           // close rigibody component
        this.rigibodyBird.simulated = false;    // close rigibody component
        this.ani.SetTrigger("Idle");            // set animition to Idel
    }

    // Fly trigger
    public void Fly()
    {
        // when is Fly state

        // this.rigibodyBird.WakeUp();          // open rigibody component
        this.rigibodyBird.simulated = true;     // open rigibody component
        this.ani.SetTrigger("Fly");             // set animition to Fly

    }

    // fire method
    public void Fire()
    {
        // set the bullet interval
        // 1f / 10 -> 0.1s
        // a bullet / 0.1s -> 10 bullets /1
        if (fireTimer > 1f / fireRate)
        {
            GameObject go = Instantiate(bulletTemplate);
            go.transform.position = this.transform.position;
            go.GetComponent<Element>().direction = -1;   // change enemy bullet as left
            fireTimer = 0f;
        }
    }

  
}
