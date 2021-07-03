using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropRule : MonoBehaviour
{
    public Item item;
    public float dropRatie;

    // execute item drop rule
    public void Execute(Vector3 pos)
    {
        // when the probability 0f ~ 100f
        // less than the drop rate
        // item will drop
        if (Random.Range(0f, 100f) < dropRatie)
        {
            Item rule = Instantiate<Item>(item);    // instance a item
            rule.transform.position = pos;          // set the item position
        }
    }
}
