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
        int x = int.Parse(tamArray[0]) / 2;
        int y = int.Parse(tamArray[1]) / 2;
        int posY = (y - 1) / 2;

        GenerarLinea(reader.ReadLine(), x, posY);
        posY--;

        string lineaV;

        while ((lineaV = reader.ReadLine()) != null)
        {
            GenerarLinea(lineaV, x, posY);
            GenerarLinea(reader.ReadLine(), x, posY);
            posY--;
        }


        reader.Close();
    }

    public void GenerarLinea(string linea, int x, int y)
    {
        string[] infoLinea = linea.Split("r", StringSplitOptions.RemoveEmptyEntries);

        int posX = -(x - 1) / 2;


        for (int i = 0;  i < infoLinea.Length; i++)
        {
            if (infoLinea[i].Equals("W"))
            {
                Instanciar(posX, y, muroV, 1);
            }

            if (infoLinea[i].Equals("L"))
            {
                Instanciar(posX, y, limiteV, 1);
            }
            if (infoLinea[i].Equals("w"))
            {
                Instanciar(posX, y, muroH, 2);
            }

            if (infoLinea[i].Equals("l"))
            {
                Instanciar(posX, y, limiteH, 2);
            }
            posX++;

        }
    }

    public void Instanciar(int x, int y, GameObject objeto, int tipo)
    {
        float posX = x;
        float posY = y;

        // Vacio 0
        // Vertical 1
        // Horizontal 2
        // Intermedio 3

        if (tipo == 1)
        {
            posX -= 0.5f;
            posY += 0.5f;
        }
        if(tipo == 3)
        {
            posX += 0.5f;
        }

        Vector3 pos = new Vector3(posX, posY, 0);

        if(tipo != 0)
        {
            Instantiate(objeto, pos, Quaternion.identity);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
