using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBase : MonoBehaviour
{
    Renderer rend;
    public static bool isGameStarted = false;
    public bool isAlive = false;
    public static int x;// x grid coordinate 
    public static int y;// y grid coordinate

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.white);
        x = Convert.ToInt32(this.name.Substring(6, 1));// x grid coordinate 
        y = Convert.ToInt32(this.name.Substring(8, 1));// y grid coordinate
    }

    // Update is called once per frame
    void Update()
    {

    }


    // User Selection 
    void OnMouseDown()
    {
        if(isGameStarted == false)
        {
            //White = not alive
            //Black = alive
            //Check if white and assign black 
            if (rend.material.GetColor("_Color") == Color.white)
            {
                // this object was clicked - do something
                rend.material.SetColor("_Color", Color.black);
                isAlive = true;
            }
            //Check if black and assign white
            else if (rend.material.GetColor("_Color") == Color.black)
            {
                // this object was clicked - do something
                rend.material.SetColor("_Color", Color.white);
                isAlive = false;
            }
        }
    }
}

