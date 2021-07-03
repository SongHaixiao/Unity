using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil :Singleton<GameUtil>
{
    public bool InScreen(Vector3 position)
    {
        // check if the object is in target screen area
        return Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(position));
    }
}
