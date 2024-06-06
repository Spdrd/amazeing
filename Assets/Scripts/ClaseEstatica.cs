using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClaseEstatica
{
    // Variables globales de selección de nivel

    public static string nivelSeleccionado = "Assets/Niveles/1x1Test.txt";

    // Funciones

    public static bool esPar(int eval)
    {

        if((eval % 2) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
