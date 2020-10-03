 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 direction = Vector3.zero, target = Vector3.zero;
    public float maxSpeed;
    float speed;
    protected static Vector3 north = new Vector3(0, 1, 0);
    protected static Vector3 east = new Vector3(1, 0, 0);
    protected static Vector3 south = new Vector3(0, -1, 0);
    protected static Vector3 west = new Vector3(-1, 0, 0);
    protected static Vector3[] directions = new Vector3[] { north, east, south, west };

    void Start()
    {
        direction = RandomDirection();
        transform.position = GameController.instance.RandomPosition();
        target = transform.position;

    }

    private void Update()
    {
        speed = Mathf.Min(Vector2.Distance(transform.position, target), 1) * maxSpeed / GameController.instance.timeStep;
        transform.position += speed * direction * Time.deltaTime;
        GameController.instance.CheckBounds(this);
    }

    public void Move()
    {

        transform.position = target;
        target = transform.position + direction;
        target = new Vector3(Mathf.Round(target.x), Mathf.Round(target.y), Mathf.Round(target.z));

    }

    public Vector3 RandomDirection()
    {
        return directions[(int)(Random.value * (directions.Length - 0.1f))];
    }
}

