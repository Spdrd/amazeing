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

    [SerializeField] Camera camara;
    [SerializeField] GameObject camaraGO;

    private float camX = 0;
    private float camY = 0;

    // Start is called before the first frame update
    void Start()
    {
        LeerNivel();
        MoverCamara();
    }

    public void MoverCamara()
    {
        Vector3 posCamara = new Vector3(camX, camY, 0);
        camara.gameObject.transform.position += posCamara;

        //camara.gameObject.transform = (4.19f * 2 * camY) / 9;
    }

    public void LeerNivel()
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string tamCuadricula = reader.ReadLine();
        string[] tamArray = tamCuadricula.Split("X");
        int posY = 0;

        GenerarLinea(reader.ReadLine(), posY);

        posY--;

        string lineaV;

        while ((lineaV = reader.ReadLine()) != null)
        {
            GenerarLinea(lineaV, posY);
            GenerarLinea(reader.ReadLine(), posY);
            posY--;
        }

        int posUltima = posY;

        camY = posUltima / 2;


        reader.Close();
    }

    public void GenerarLinea(string linea, int y)
    {
        char[] infoLinea = linea.ToCharArray();
        int posX = 0;
        int posPrimera = posX;

        for (int i = 0;  i < infoLinea.Length; i++)
        {
            if (infoLinea[i].Equals('W'))
            {
                Instanciar(posX, y, muroV, 1);
            }

            if (infoLinea[i].Equals('L'))
            {
                Instanciar(posX, y, limiteV, 1);
            }
            if (infoLinea[i].Equals('w'))
            {
                Instanciar(posX, y, muroH, 2);
            }

            if (infoLinea[i].Equals('l'))
            {
                Instanciar(posX, y, limiteH, 2);
            }

            if (infoLinea[i].Equals('B'))
            {
                Instanciar(posX, y, bola, 3);
            }

            if (infoLinea[i].Equals('M'))
            {
                Instanciar(posX, y, meta, 3);
            }

            if (!infoLinea[i].Equals('r') && !infoLinea[i].Equals('B') && !infoLinea[i].Equals('M'))
            {
                posX++;
            }

        }

        int posUltima = posX;
        camX = posUltima / 2;
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
            posX -= 1;
            posY += 0.5f;
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
