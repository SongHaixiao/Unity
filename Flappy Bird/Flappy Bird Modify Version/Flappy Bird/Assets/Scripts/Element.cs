using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public float speed;

    public int direction = 1;   // bullet direction : 1 - right , -1 - left

    public SIDE side;

    public float power = 1f;    // power of bullet 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // change the bullet position speed / deltaTime
        this.transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0);

        // destroy bullet gameObject if bullet over the screen safe area
        if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)) == false)
        {
            // destroy bullet 
            Destroy(this.gameObject, 1f);
;        }
    }
}
