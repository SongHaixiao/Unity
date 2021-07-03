using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Element
{
    // missile's target is player

    public Transform target;  // get the player position

    private bool running = false;

    public GameObject fxExplode;    // get the exploe partical effect
    //
    public override void OnUpdate()
    {
        if (!running)
            return;


        if (target != null)
        {
            // get the Vector3 for direction towards player
            
            Vector3 dir = target.position - this.transform.position;

            // magnitude : get a vector length
            // when the length of vector dir less 0.1
            // missile will be exploded
            if (dir.magnitude < 0.1)
            {
                this.Explode();
            }

            // roate the missile angel towards player
            this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);

            // change the missle position
            this.transform.position += speed * Time.deltaTime * dir;
        }
    }

    public void Launch()
    {
        running = true;
    }

    // player die effect for missile
    // 1. destroy self
    // 2. display an exlode partical effect
    public void Explode()
    {
        Destroy(this.gameObject);   // destroy missile itself

        Instantiate(fxExplode, this.transform.position, Quaternion.identity); // display an explode partical effect at the shot player

        if (target != null)
        {
            // missile damage to player
            Player p = target.GetComponent<Player>();
            p.Damage(power);
        }
    }
}
