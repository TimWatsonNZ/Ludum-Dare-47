﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private List<Vector3> gridPositions = new List<Vector3>();
    public Base playerBase;
    public List<Robot> robots;
    public List<Floor> floor;
    public List<Wall> walls;
    public List<Enemy> enemies;
    public List<Resource> resources;
    public float timeStep, time;

    public GameObject robotPrefab, floorPrefab, wallPrefab, resourcePrefab, enemyPrefab;

    public Vector2 gridSize = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        InitGrid();
    }

    void InitGrid()
    {
        gridPositions.Clear();

        for (int i = 0; i <= gridSize.x; i++)
        {
            for (int j = 0; j <= gridSize.y; j++)
            {
                Vector3 pos = new Vector3(i - gridSize.x / 2, j - gridSize.y / 2, 0);
                gridPositions.Add(pos);
                Floor floorTile = Instantiate(floorPrefab, transform).GetComponent<Floor>();
                float c = (Random.value + 9) / 10;
                floorTile.GetComponent<SpriteRenderer>().color = new Color(c, c, c);
                floorTile.transform.position = pos;
                floor.Add(floorTile);
            }
        }






    }

    public Robot SpawnRobot()
    {
        Robot robot = Instantiate(robotPrefab).GetComponent<Robot>();
        robots.Add(robot);
        return robot;
    }

    public Vector3 RandomPosition() {
        return new Vector3((int)(gridSize.x * Random.value - gridSize.x / 2), (int)(gridSize.y * Random.value - gridSize.y / 2), 0);
    }

    public void CheckBounds(Robot robot)
    {
        Vector2 pos = robot.transform.position;
        Vector2 newPos = pos;
        Vector2 target = robot.target;
        Vector2 newTarget = target;
        bool outOfBounds = false;

        if (target.x > gridSize.x / 2 && pos.x > gridSize.x / 2)
        {
            newPos.x = -gridSize.x / 2 - 1;
            newTarget.x = -gridSize.x / 2;
            outOfBounds = true;
        }
        if (target.x < -gridSize.x / 2 && pos.x < -gridSize.x / 2)
        {
            newPos.x = gridSize.x / 2 + 1;
            newTarget.x = gridSize.x / 2;
            outOfBounds = true;
        }
        if (target.y > gridSize.y / 2 && pos.y > gridSize.y / 2)
        {
            newPos.y = -gridSize.y / 2 - 1;
            newTarget.y = -gridSize.y / 2;
            outOfBounds = true;
        }
        if (target.y < -gridSize.y / 2 && pos.y < -gridSize.y / 2)
        {
            newPos.y = gridSize.y / 2 + 1;
            newTarget.y = gridSize.y / 2;
            outOfBounds = true;
        }
        if (outOfBounds)
        {
            robot.target = newTarget;
            robot.transform.position = newPos;
        }
    }



    // Update is called once per frame
    protected void Update()
    {
        if (time > timeStep)
        {
            time = 0;
            for (int i = 0; i < robots.Count; i++)
            {
                Robot robot = robots[i];
                robot.RunProgram();
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.RunProgram();
            }
        }
        time += Time.deltaTime;
    }
}