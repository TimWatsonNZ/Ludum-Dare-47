using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Vector3 direction = Vector3.zero, target = Vector3.zero;
    public float maxSpeed;
    float speed;
    protected static Vector3 north = new Vector3(0, 1, 0);
    protected static Vector3 east = new Vector3(1, 0, 0);
    protected static Vector3 south = new Vector3(0, -1, 0);
    protected static Vector3 west = new Vector3(-1, 0, 0);
    protected static Vector3[] directions = new Vector3[] { north, east, south, west };


    // Start is called before the first frame update
    protected void Start()
    {
        direction = RandomDirection();
    }

    public Vector3 RandomDirection()
    {
        return directions[(int)(Random.value * (directions.Length - 0.1f))];
    }

    // Update is called once per frame
    protected void Update()
    {

        speed = Mathf.Min(Vector2.Distance(transform.position, target), 1) * maxSpeed / GameController.instance.timeStep;
        transform.position += speed * direction * Time.deltaTime;
        GameController.instance.CheckBounds(this);
    }

    public void RunProgram()
    {
        transform.position = target;
        direction = RandomDirection();
        //direction = north;
        //direction = east;
        //direction = south;
        //direction = west;
        target = transform.position + direction;
        target = new Vector3(Mathf.Round(target.x), Mathf.Round(target.y), Mathf.Round(target.z));

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy")) {
            transform.position = Vector3.zero;
            target = Vector3.zero;
            print("death");
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            speed = 0;
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            direction = -direction;
            target = transform.position + direction;
            
            print("wall");
        }
    }

}
