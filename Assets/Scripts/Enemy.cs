using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Robot
{

    new void Start()
    {
        direction = RandomDirection();
        transform.position = GameController.instance.RandomPosition();
        target = transform.position;
 
    }

    public new void RunProgram()
    {

        transform.position = target;
        target = transform.position + direction;
        target = new Vector3(Mathf.Round(target.x), Mathf.Round(target.y), Mathf.Round(target.z));

    }
}
