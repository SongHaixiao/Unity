using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // singleton pattern : T must write MonoBehaviour 

    static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType<T>();
            }

            return instance;
        }


    }
}
