using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public float speed;

    //public int direction = 1;   // bullet direction : 1 - right , -1 - left

    public Vector3 direction = Vector3.zero;

    public SIDE side;

    public float power = 1f;    // power of bullet 

    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    //
    public virtual void OnUpdate()
    {
        // change the bullet position speed / deltaTime
        this.transform.position += speed * Time.deltaTime * direction;

        // destroy bullet gameObject if bullet over the screen safe area
        if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)) == false)
        {
            // destroy bullet 
            Destroy(this.gameObject, 1f);
        }

        //this.transform.position += speed * Time.deltaTime * direction;

        //if (!GameUtil.Instance.InScreen(this.transform.position))
        //{
        //    Destroy(this.gameObject, 1f);
        //}
    }
}
