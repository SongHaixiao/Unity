using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float speed; // speed variable

    public float minRange;
    public float maxRange;

    // Start is called before the first frame update
    void Start()
    {
        // generate a pipeline
        this.Init();
    }

    float t = 0;    // time count

    // generate a pipeline with random height
    public void Init()
    {
        // randomly change the height of pipelines
        float y = Random.Range(minRange, maxRange);
        this.transform.localPosition = new Vector3(0, y, 0);
    }



    // Update is called once per frame
    void Update()
    {
        // move towards to left x - axie left in 10/frame
        this.transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;


        // regenerate a pipline per 6 scends
        t += Time.deltaTime;
        if (t > 6f)
        {
            t = 0;
            this.Init();
        }
    }
}
