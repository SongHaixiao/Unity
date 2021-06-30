using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject missileTemplate;  // load the missile object

                                    //          firePoint1 - gun --- Unit firePoin
    public Transform firePoint2;    //          firePoint2 - battery which will always aim at the player
    public Transform firePoint3;    //          firePoint3 - missile

    public Transform battery;        // rotating firpoint2 - battery


    Missile missile = null;

    public Unit target;
            
    public float fireRate2 = 10f;  // firePoint2 fire rate
    float fireTimer2 = 0;          // firePoint2 fire timer

    public float UltCD = 10f;       // skill : missle firePoint2 rate
    float fireTimer3 = 0;           // firePoint2 timer

    public override void OnStart()
    {
        this.Fly(); // set initial state as flaying
        StartCoroutine(Enter());
    }


    IEnumerator Enter()
    {
        // set boss initial position out of screen 
        this.transform.position = new Vector3(10, 0, 0);

        // move boss to (-5,0,0)
        yield return MoveTo(new Vector3(5, 0, 0));

        // boss attack method
        yield return Attack();

        yield return null;
    }

    // attack
    IEnumerator Attack()
    {
        while (true)
        {
            fireTimer2 += Time.deltaTime;

            Fire();     // gun
            Fire2();    // battery

            fireTimer3 += Time.deltaTime;

            // use missile skill
            if (fireTimer3 > UltCD)
            {
                yield return UltraAttack();
                fireTimer3 = 0;
            }

            yield return null;
        }
    }

    // main skill : missle
    // change the positon of boss to show
    // that boss is going to use skill --- missile
    IEnumerator UltraAttack()
    {
        yield return MoveTo(new Vector3(5, 3, 0));  // go to new position
        yield return FireMissile();                 // fire missile
        yield return MoveTo(new Vector3(5, 0, 0));  // come back 
    }

    // move boss
    IEnumerator MoveTo(Vector3 pos)
    {
        while (true)
        {
            // get the vector for distance between pos and boss
            Vector3 dir = (pos - this.transform.position);

            // if distance < 0.1 stop moving
            if (dir.magnitude < 0.1)
            {
                break;
            }

            // change the position of boss
            this.transform.position += dir.normalized * speed * Time.deltaTime;

            yield return null;
        }
    }



    IEnumerator FireMissile()
    {

        ani.SetTrigger("Skill");

        // wait 5s
        yield return new WaitForSeconds(5f);
    }

    // firePoint2 fire
    // want to run Fire() and Fire2() at the same time,
    // but Coroutine just can run only one coroutine at one time,
    // chang Fire2() Coroutine way to function to make Fire() and Fire2()
    // run at the same time
    void Fire2()
    {
        if (fireTimer2 > 1f / fireRate2)
        {
            // initialize the bullet at firePoint2 location with battery rotation
            GameObject go = Instantiate(bulletTemplate, firePoint2.position, battery.rotation);
            Element bullent = go.GetComponent<Element>();
            bullent.direction = (target.transform.position - firePoint2.position).normalized;
            fireTimer2 = 0f;
        }
    }

    // missile load
    public void OnMissileLoad()
    {
        Debug.LogError("OnMissileLoad");

        GameObject go = Instantiate(missileTemplate, firePoint3);
        missile = go.GetComponent<Missile>();
        missile.transform.position = firePoint3.position;
        missile.target = this.target.transform;
       
    }

    // missile launch
    public void OnMissileLaunch()
    {
        if (missile == null)
            return;

        missile.transform.SetParent(null);
        missile.Launch();
    }

    public override void OnUpdate()
    {
        // roate the battery's angle
        // battery will always aim at the player
        // this algorithm is similar to Missile traches the player
        if (target != null)
        {
            Vector3 dir = target.transform.position - battery.position; // get the length between target(player) and battery
            battery.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);     // roate the battery
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Enemy:OnCollisionEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //this.Die();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Element bullet = col.gameObject.GetComponent<Element>();
        if (bullet == null)
        {
            return;
        }
        Debug.Log("Enemy:OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (bullet.side == SIDE.PLAYER)
        {
            this.Damage(bullet.power);
        }
    }
}
