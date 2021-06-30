using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{

    public float lifeTime = 4f;     // life time

    public ENEMY_TYPE enemyType;    // enemy type

    public Vector2 range;

    float initY = 0;    // initial y

    // Use this for initialization
    public override void OnStart()
    {
        // destroy object per life time
        Destroy(this.gameObject, lifeTime);         

        // randomly change the height of pipelines
        initY = Random.Range(range.x, range.y);
        this.transform.localPosition = new Vector3(0, initY, 0);
        //this.transform.localPosition = new Vector3(6.0f, initY, 0);
        this.Fly();
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        float y = 0;

        // set the SWING enmey's y position
        if (enemyType == ENEMY_TYPE.SWING_ENEMY)
        {
            y = Mathf.Sin(Time.timeSinceLevelLoad) * 3f;
        }

        // change the position of enemy bird :
        // the distance per frame in x - axis
        this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime * speed, initY + y, 0);


        this.Fire();
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
