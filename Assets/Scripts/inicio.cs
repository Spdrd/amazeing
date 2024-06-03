using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class inicio : MonoBehaviour
{
    [SerializeField] SpriteRenderer fondoAzul;
    [SerializeField] SpriteRenderer sprite1;
    [SerializeField] SpriteRenderer sprite2;
    [SerializeField] SpriteRenderer sprite3;
    [SerializeField] SpriteRenderer sprite4;
    [SerializeField] SpriteRenderer sprite5;
    [SerializeField] Tilemap grid1;
    [SerializeField] Tilemap grid2;
    [SerializeField] TextMeshProUGUI texto;

    [SerializeField] float tiempoFade;

    private bool start = false;

    [SerializeField] bool escenaFinal = false;
    private float temporizadorEscenaFinal = 0;


    // Start is called before the first frame update
    void Start()
    {
        Color color = new Color();
        color.a = 0;
        fondoAzul.color = Color.blue;
        sprite1.color = Color.white;
        sprite2.color = Color.white;
        sprite3.color = Color.white;
        sprite4.color = Color.white;
        sprite5.color = Color.white;
        grid1.color = Color.white;
        grid2.color = Color.white;
        texto.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            texto.color = Color.Lerp(texto.color, Color.white, Time.deltaTime / 2);
        }

        if (escenaFinal)
        {
            temporizadorEscenaFinal += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Return) || start || (temporizadorEscenaFinal > 3))
        {
            start = true;
            Color color = new Color();
            color.a = 0;
            fondoAzul.color = Color.Lerp(fondoAzul.color, color, Time.deltaTime / tiempoFade);
            sprite1.color = Color.Lerp(sprite1.color, color, Time.deltaTime / tiempoFade);
            sprite2.color = Color.Lerp(sprite2.color, color, Time.deltaTime / tiempoFade);
            sprite3.color = Color.Lerp(sprite3.color, color, Time.deltaTime / tiempoFade);
            sprite4.color = Color.Lerp(sprite4.color, color, Time.deltaTime / tiempoFade);
            sprite5.color = Color.Lerp(sprite5.color, color, Time.deltaTime / tiempoFade);
            grid1.color = Color.Lerp(fondoAzul.color, color, Time.deltaTime / tiempoFade);
            grid2.color = Color.Lerp(fondoAzul.color, color, Time.deltaTime / tiempoFade);
            texto.color = Color.Lerp(texto.color, color, Time.deltaTime / tiempoFade);
        }
    }
}
