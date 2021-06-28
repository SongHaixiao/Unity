using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject pipelineTemplate;     // put in pipeline sample

    List<Pipeline> pipelines = new List<Pipeline>();    // pipelines list to store pipelines

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // define a Coroutine variable
    Coroutine runner = null;

    // PipelineManager initialization
    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++)
        {
            Destroy(pipelines[i].gameObject);
        }

        pipelines.Clear();
    }


    // start coroutine for generate pipelines
    public void StartRun()
    {

        // run runner coroutine 
        runner = StartCoroutine(GeneratePipelines());

    }

    // stop coroutine for generate pipelines
    public void Stop()
    {
        // stop runner coroutine
        StopCoroutine(runner);

        for (int i = 0; i < pipelines.Count; i++)
            pipelines[i].enabled = false;
    }

    /* IEnumerator : supports a simple iteration over a non - generic collection 
     *  
     *  - define coroutine variable : Coroutine coroutineVariable;
     *  
     *  - start coroutine : coroutineVarible = StartCoroutine(IEnumerator Method);
     *  
     *  - stop coroutine : StopCoroutine(coroutine);
     */

    // generate multiple pipes 
    IEnumerator GeneratePipelines()
    {
        for (int i = 0; i < 3; i++)
        {
            if (pipelines.Count < 3)
                GeneratePipeline();
            else
            {
                pipelines[i].enabled = true;
                pipelines[i].Init();
            }

            // wait for speed default in 2 seconds
            yield return new WaitForSeconds(speed);
        }
    }

    // generate single piepline
    void GeneratePipeline()
    {
        // generate pipeline when less than 3
        if(pipelines.Count < 3)
        {
            // clone the object original and returns the clone
            // template : clone target
            // this.transform : put these objects cloned as the childer object
            GameObject obj = Instantiate(pipelineTemplate, this.transform);

            Pipeline p = obj.GetComponent<Pipeline>();
            pipelines.Add(p);
        }
    }
}
