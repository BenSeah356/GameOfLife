using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBase : MonoBehaviour
{
    public static bool isGameStarted = false; //The state of whether user has clicked start or not for the game to run
    private const int count = 100; //# of game object (finite)
    GameObject[] Cubes = new GameObject[count];//A place to store gameobjects
    public int[,] GameState = new int [100,3];// A place to store gamestate each cycle
    private int frames = 0; // run game every x amount of frames
    // Start is called before the first frame update
    void Start()
    {
        Cubes = UnityEngine.GameObject.FindGameObjectsWithTag("GameBoard");
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames % 10 == 0)
        { //If the remainder of the current frame divided by 10 is 0 run the function.
            if (isGameStarted == true)
            {
                for (var i = 0; i < count; ++i)//Grab Current GameState and store it into GameState[,]
                {
                    int x = Convert.ToInt32(Cubes[i].name.Substring(6, 1));// x grid coordinate 
                    int y = Convert.ToInt32(Cubes[i].name.Substring(8, 1));// y grid coordinate
                    GameState[i, 0] = x;
                    GameState[i, 1] = y;
                    int numberOfLiveNeighbors = GetLiveCellNeighborCount(x, y);
                    if (Cubes[i].GetComponent<CubeBase>().isAlive == true)
                    {
                        if (numberOfLiveNeighbors == 2 || numberOfLiveNeighbors == 3)
                        {
                            //stays alive
                            GameState[i, 2] = 1;//store alive
                        }
                        else
                        {
                            //dies
                            GameState[i, 2] = 0;//store dead
                        }
                    }
                    if (Cubes[i].GetComponent<CubeBase>().isAlive == false)
                    {
                        if (numberOfLiveNeighbors == 3)
                        {
                            GameState[i, 2] = 1;//store alive
                        }
                    }
                }
                for (var i = 0; i < count; ++i)//Output New GameState by changing isAlive amd color.
                {
                    if (GameState[i, 2] == 1)// Find Game Object with name of "Cube (x,y)",  Change isAlive = true, and Change Renderer Material Color to black
                    {
                        GameObject tempObject;
                        tempObject = GameObject.Find("Cube (" + GameState[i, 0] + "," + GameState[i, 1] + ")");
                        tempObject.GetComponent<CubeBase>().isAlive = true;
                        tempObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    }
                    if (GameState[i, 2] == 0)// Find Game Object with name of "Cube (x,y)",  Change isAlive = false, and Change Renderer Material Color to white
                    {
                        GameObject tempObject;
                        tempObject = GameObject.Find("Cube (" + GameState[i, 0] + "," + GameState[i, 1] + ")");
                        tempObject.GetComponent<CubeBase>().isAlive = false;
                        tempObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    }
                }
            }
        }
    }

    int GetLiveCellNeighborCount(int x, int y)
    {
        int numberOfLiveCells = 0;
        GameObject northwest;
        GameObject north;
        GameObject northeast;
        GameObject east;
        GameObject southeast;
        GameObject south;
        GameObject southwest;
        GameObject west;
        string northweststr = "";
        string northstr = "";
        string northeaststr = "";
        string eaststr = "";
        string southeaststr = "";
        string southstr = "";
        string southweststr = "";
        string weststr = "";
        bool isEdgePiece = false;
        int newx;
        int newy;
        if ((x + 1) == 10)//Check if wrap right and assign x value GameObject names in string variable
        {
            isEdgePiece = true;
            newx = 0;
            northweststr = "Cube (" + (x - 1) + ",";
            northstr = "Cube (" + (x) + ",";
            northeaststr = "Cube (" + (newx) + ",";
            eaststr = "Cube (" + (newx) + ",";
            southeaststr = "Cube (" + (newx) + ",";
            southstr = "Cube (" + (x) + ",";
            southweststr = "Cube (" + (x - 1) + ",";
            weststr = "Cube (" + (x - 1) + ",";
        }
        else if ((x - 1) == -1) //Check if wrap left and assign x value GameObject names in string variable
        {
            isEdgePiece = true;
            newx = 9;
            northweststr = "Cube (" + (newx) + ",";
            northstr = "Cube (" + (x) + ",";
            northeaststr = "Cube (" + (x + 1) + ",";
            eaststr = "Cube (" + (x + 1) + ",";
            southeaststr = "Cube (" + (x + 1) + ",";
            southstr = "Cube (" + (x) + ",";
            southweststr = "Cube (" + (newx) + ",";
            weststr = "Cube (" + (newx) + ",";
        }
        else //Non "x" edge piece and assign x value GameObject names in string variable
        {
            isEdgePiece = false;
            northweststr = "Cube (" + (x - 1) + ",";
            northstr = "Cube (" + (x) + ",";
            northeaststr = "Cube (" + (x + 1) + ",";
            eaststr = "Cube (" + (x + 1) + ",";
            southeaststr = "Cube (" + (x + 1) + ",";
            southstr = "Cube (" + (x) + ",";
            southweststr = "Cube (" + (x - 1) + ",";
            weststr = "Cube (" + (x - 1) + ",";
        }
        if ((y + 1) == 10) // Check if wrap up and assign y value GameObject names in string variable
        {
            isEdgePiece = true;
            newy = 0;
            northweststr = northweststr + (newy) + ")";
            northstr = northstr + (newy) + ")";
            northeaststr = northeaststr + (newy) + ")";
            eaststr = eaststr + (y) + ")";
            southeaststr = southeaststr + (y - 1) + ")";
            southstr = southstr + (y - 1) + ")";
            southweststr = southweststr + (y - 1) + ")";
            weststr = weststr + (y) + ")";
            northwest = GameObject.Find(northweststr);
            north = GameObject.Find(northstr);
            northeast = GameObject.Find(northeaststr);
            east = GameObject.Find(eaststr);
            southeast = GameObject.Find(southeaststr);
            south = GameObject.Find(southstr);
            southwest = GameObject.Find(southweststr);
            west = GameObject.Find(weststr);
            numberOfLiveCells = AddLiveNeigbors(northwest, north, northeast, east, southeast, south, southwest, west);
        }
        else if ((y - 1) == -1) //Check if wrap down and assign y value GameObject names in string variable
        {
            isEdgePiece = true;
            newy = 9;
            northweststr = northweststr + (y + 1) + ")";
            northstr = northstr + (y + 1) + ")";
            northeaststr = northeaststr + (y + 1) + ")";
            eaststr = eaststr + (y) + ")";
            southeaststr = southeaststr + (newy) + ")";
            southstr = southstr + (newy) + ")";
            southweststr = southweststr + (newy) + ")";
            weststr = weststr + (y) + ")";
            northwest = GameObject.Find(northweststr);
            north = GameObject.Find(northstr);
            northeast = GameObject.Find(northeaststr);
            east = GameObject.Find(eaststr);
            southeast = GameObject.Find(southeaststr);
            south = GameObject.Find(southstr);
            southwest = GameObject.Find(southweststr);
            west = GameObject.Find(weststr);
            numberOfLiveCells = AddLiveNeigbors(northwest, north, northeast, east, southeast, south, southwest, west);
        }
        else//Non "x" edge piece and assign y value GameObject names in string variable
        {
            isEdgePiece = false;
            northweststr = northweststr + (y + 1) + ")";
            northstr = northstr + (y + 1) + ")";
            northeaststr = northeaststr + (y + 1) + ")";
            eaststr = eaststr + (y) + ")";
            southeaststr = southeaststr + (y - 1) + ")";
            southstr = southstr + (y - 1) + ")";
            southweststr = southweststr + (y - 1) + ")";
            weststr = weststr + (y) + ")";
            //Find neighboring GameObjects using "GameObject.Find()"
            northwest = GameObject.Find(northweststr);
            north = GameObject.Find(northstr);
            northeast = GameObject.Find(northeaststr);
            east = GameObject.Find(eaststr);
            southeast = GameObject.Find(southeaststr);
            south = GameObject.Find(southstr);
            southwest = GameObject.Find(southweststr);
            west = GameObject.Find(weststr);
            //Pass Neighbors into AddLiveNeigbors function
            numberOfLiveCells = AddLiveNeigbors(northwest, north, northeast, east, southeast, south, southwest, west);
        }
        return numberOfLiveCells;
    }

    int AddLiveNeigbors(GameObject northwest, GameObject north, GameObject northeast, GameObject east, GameObject southeast, GameObject south, GameObject southwest, GameObject west)
    {
        int total = 0;
        if (northwest.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)//If it is a black color, it is alive and add 1 to total.
        {
            total = total + 1;
        }
        if (north.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)
        {
            total = total + 1;
        }
        if (northeast.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)
        {
            total = total + 1;
        }
        if (east.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)
        {
            total = total + 1;
        }
        if (southeast.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)
        {
            total = total + 1;
        }
        if (south.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)
        {
            total = total + 1;
        }
        if (southwest.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)
        {
            total = total + 1;
        }
        if (west.GetComponent<Renderer>().material.GetColor("_Color") == Color.black)
        {
            total = total + 1;
        }
        return total;
    }

}
