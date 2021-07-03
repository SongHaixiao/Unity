using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public List<Enemy> enemies = new List<Enemy>();

    // stop coroutine for generate pipelines
    public void Clear()
    {
        // stop runner coroutine
        this.enemies.Clear();
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
