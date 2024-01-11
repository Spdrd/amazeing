using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class generarNivel : MonoBehaviour
{
    [SerializeField] string path = "Assets/Niveles/nivel1AMZNG.txt";
    [SerializeField] GameObject limiteH;
    [SerializeField] GameObject limiteV;
    [SerializeField] GameObject muroH;
    [SerializeField] GameObject muroV;
    [SerializeField] GameObject meta;
    [SerializeField] GameObject bola;

    // Start is called before the first frame update
    void Start()
    {
        LeerNivel();
    }
    public void LeerNivel()
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string tamCuadricula = reader.ReadLine();
        string[] tamArray = tamCuadricula.Split("X");
        int x = int.Parse(tamArray[0]);
        int y = int.Parse(tamArray[1]);



        for(int i = 0; i < y; i++)
        {

        }

        reader.Close();
    }

    public void GenerarLineaX(string lineaH, int x)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
