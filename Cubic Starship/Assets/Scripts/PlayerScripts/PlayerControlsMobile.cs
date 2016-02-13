using UnityEngine;
using System.Collections;

public class PlayerControlsMobile : KillableObject
{
    void Update()
    {
        Debug.Log(Input.touchCount);
        Debug.Log(Input.touchSupported);
    }
}
