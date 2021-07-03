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
    protected bool isFlying = false;   // flaying flag
    
    public UnityAction<int> OnScore;    // generalization ？？？

    public delegate void DeathNotify(Unit sender); // delegate
    public event DeathNotify OnDeath;

    protected Vector3 initPos;  // inital position

    public GameObject bulletTemplate;   // bullet template

    protected float fireTimer = 0f;  // fire counter

    public float hp = 100f;       // hp value

    public float HP
    {
        get { return this.hp; }
    }

    public float MaxHP = 100f;

    public float Attack;    // attack power

    public int life = 3;


    public SIDE side;

    public Transform firePoint; // Boos firePoint1

    public bool destroyOnDeath = false;

    public float destroyTime = 0.6f;    // destroy time


    // Start is called before the first frame update
    void Start()
    {
        this.ani = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;
        this.Init();
        OnStart();
        
    }

    public virtual void OnStart()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if dead, do nothing
        if (this.death) return;

        // if not flying, do nothing
        if (!this.isFlying) return;

        // record the time of per frame
        fireTimer += Time.deltaTime;

        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    // initialization
    public void Init()
    {
        this.transform.position = initPos;  // reset  to inital position
        this.Idle();                        // set animition to Idle state
        this.death = false;                 // reset to alive
        this.hp = this.MaxHP;               // reset to max hp
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
            go.transform.position = firePoint.position;
            go.GetComponent<Element>().direction = this.side == SIDE.PLAYER ? Vector3.right : Vector3.left;   // change enemy bullet as left
            fireTimer = 0f;
        }
    }

    // Idle trigger
    public void Idle()
    {
        // when is Idle state

        this.rigibodyBird.simulated = false;    // close rigibody component == this.rigibodyBird.Sleep()
        this.ani.SetTrigger("Idle");            // set animition to Idel
        this.isFlying = false;                  // idle state, is not flying
    }

    // Fly trigger
    public void Fly()
    {
        // when is Fly state

        this.rigibodyBird.simulated = true;     // open rigibody component == this.rigibodyBird.WakeUp();
        this.ani.SetTrigger("Fly");             // set animition to Fly
        this.isFlying = true;                   // fly state, is flying

    }

    // Die tirgger
    public void Die()
    {
        // when is Die state
        if (this.death) return;

        this.life--;
        this.hp = 0;
        this.death = true;
        this.ani.SetTrigger("Die");

        if (this.OnDeath != null)
        {
            this.OnDeath(this);
        }

        if (destroyOnDeath)
            Destroy(this.gameObject, destroyTime);
    }

    // damage
    public void Damage(float power)
    {
        Debug.Log("Unit : Damage Power : " + power);

        this.hp -= power;

        if (this.hp <= 0)
            this.Die();
    }

    // add hp
    public void AddHP(int hp)
    {
        this.hp += hp;
        if (this.hp > this.MaxHP)
            this.hp = this.MaxHP;
    }
}
