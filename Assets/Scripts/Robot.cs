
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Vector3 direction = Vector3.zero, target = Vector3.zero;
    public float maxSpeed;
    float speed;
    static Vector3 north = new Vector3(0, 1, 0);
    static Vector3 east = new Vector3(1, 0, 0);
    static Vector3 south = new Vector3(0, -1, 0);
    static Vector3 west = new Vector3(-1, 0, 0);
    static Vector3[] directions = new Vector3[] { north, east, south, west };
    
    List<Instruction> instructions = new List<Instruction>();

    int currentLine = 0;

    // Start is called before the first frame update
    void Start()
    {
        AddInstruction(new MoveLeftInstruction(Commander.RobotXLessThan, "100"));
    }

    public void AddInstruction(Instruction instruction) {
        instructions.Add(instruction);
    }

    public void MoveNorth() {
        MoveDirection(Robot.north);
    }

    public void MoveSouth() {
        MoveDirection(Robot.south);
    }

    public void MoveEast() {
        MoveDirection(Robot.east); 
    }

    public void MoveWest() {
        MoveDirection(Robot.west);
    }

    // Update is called once per frame
    void Update()
    {
        print("Current Line: " + currentLine);

        if (instructions.Count > 0) {
            var instruction = instructions[currentLine];

            instruction.Run(this);
        }

        currentLine++;
        if (currentLine >= instructions.Count) {
            currentLine = 0;
        }
        
        speed = Mathf.Min(Vector2.Distance(transform.position, target), 1) * maxSpeed / GameController.instance.timeStep;

        if (direction != null) {
            transform.position += speed * direction * Time.deltaTime;
        }
        
        GameController.instance.CheckBounds(this);
        direction = Vector3.zero;
    }

    private void MoveDirection(Vector3 direction) {
        target = transform.position + direction;
        target = new Vector3(Mathf.Round(target.x), Mathf.Round(target.y), Mathf.Round(target.z));
    }

    public void RunProgram()
    {
        transform.position = target;
    }
}
