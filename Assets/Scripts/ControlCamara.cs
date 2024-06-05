using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Console.Write("Lejos");
            Camera.main.orthographicSize++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Console.Write("Cerca");
            Camera.main.orthographicSize--;
        }
    }
}
