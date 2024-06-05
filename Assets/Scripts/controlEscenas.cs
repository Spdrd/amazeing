using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlEscenas : MonoBehaviour
{
    [SerializeField] string[] niveles;
    [SerializeField] int nivelSeleccionado = -1;

    // Start is called before the first frame update
    void Start()
    {
        if(nivelSeleccionado != -1)
        {
            ClaseEstatica.nivelSeleccionado = niveles[nivelSeleccionado];
            SceneManager.LoadScene("Assets/Scenes/Nivel TXT.unity");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
