using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject enemyTemplate1;
    public GameObject enemyTemplate2;
    public GameObject enemyTemplate3;

    public List<Enemy> enemies = new List<Enemy>();

    public float speed1 = 1f;   // enemy1 generating speed 
    public float speed2 = 3f;   // enemy2 generating speed
    public float speed3 = 6f;   // enemy3 generating speed

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

    // start coroutine for generate pipelines
    public void StartRun()
    {

        // run runner coroutine 
        runner = StartCoroutine(GenerateEnemies());

    }

    // stop coroutine for generate pipelines
    public void Stop()
    {
        // stop runner coroutine
        StopCoroutine(runner);
        this.enemies.Clear();
    }

    int timer1 = 0;     // time count for enemy1
    int timer2 = 0;     // time count for enemy2
    int timer3 = 0;     // time count for enemy3

    // generate single enemy once a time
    IEnumerator GenerateEnemies()
    {

        while (true)
        {
            // generate enemy1
            if (timer1 > speed1)
            {
                GenerateEnemy(enemyTemplate1);
                timer1 = 0;
            }

            // generate enemy2
            if(timer2 > speed2)
            {
                GenerateEnemy(enemyTemplate2);
                timer2 = 0;
            }

            // generate enemy3
            if (timer3 > speed3)
            {
                GenerateEnemy(enemyTemplate3);
                timer3 = 0;
            }

            timer1++;
            timer2++;
            timer3++;

            // coroutine run once a time / 1s
            yield return new WaitForSeconds(1f);
        }
        
    }

    // generate single enemy
    public Enemy GenerateEnemy(GameObject templates)
    {
        if (templates == null) return null;

        // clone the object original and returns the clone
        // template : clone target
        // this.transform : put these objects cloned as the childer object
        GameObject obj = Instantiate(templates, this.transform);

        Enemy e = obj.GetComponent<Enemy>();
        this.enemies.Add(e);
        return e;

    }
}
