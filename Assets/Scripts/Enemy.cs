using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Robot
{
    public new void RunProgram()
    {
        transform.position = target;
     
        //direction = north;
        //direction = east;
        //direction = south;
        direction = west;
        target = transform.position + direction;
        target = new Vector3(Mathf.Round(target.x), Mathf.Round(target.y), Mathf.Round(target.z));

    }
}
