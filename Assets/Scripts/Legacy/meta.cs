using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class meta : MonoBehaviour
{
    [SerializeField] bool nivelTerminado;

    [SerializeField] Tilemap limite1;
    [SerializeField] Tilemap limite2;

    [SerializeField] float tCompletado = 0;
    [SerializeField] float tCompletadoBordes;
    [SerializeField] float tCompletadoFinal;

    [SerializeField] Transform posJugador;

    [SerializeField] Tilemap grid1;
    [SerializeField] Tilemap grid2;
    [SerializeField] SpriteRenderer renderer1;
    [SerializeField] SpriteRenderer renderer2;
    [SerializeField] SpriteRenderer renderer3;
    [SerializeField] SpriteRenderer renderer4;
    [SerializeField] SpriteRenderer renderer5;
    [SerializeField] SpriteRenderer fondoAzul;
    [SerializeField] string SiguienteEscena;
    [SerializeField] float tiempoAntesEscena;

    // Start is called before the first frame update
    void Start()
    {
        limite1.color = Color.red;
        limite2.color = Color.red;
        Color color = renderer1.color;
        color.a = 0;
        renderer1.color = color;
        renderer2.color = color;
        renderer3.color = color;
        renderer4.color = color;
        renderer5.color = color;
        fondoAzul.color = color;
        grid1.color = color;
        grid2.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(posJugador.position == transform.position)
        {
            nivelTerminado = true;
        }
        if (nivelTerminado)
        {
            tCompletado += Time.deltaTime;
            limite1.color = Color.Lerp(limite1.color, Color.blue, Time.deltaTime / tCompletadoBordes);
            limite2.color = limite1.color;
            materializarSprite(renderer1, Color.white);
            materializarSprite(renderer2, Color.white);
            materializarSprite(renderer3, Color.white);
            materializarSprite(renderer4, Color.white);
            materializarSprite(renderer5, Color.white);
            materializarSprite(fondoAzul, Color.blue);
            materializarGrid(grid1, Color.white);
            materializarGrid(grid2, Color.white);
        }
        else
        {
            limite1.color = Color.red;
            limite2.color = Color.red;
            Color color = renderer1.color;
            color.a = 0;
            renderer1.color = color;
            renderer2.color = color;
            renderer3.color = color;
            renderer4.color = color;
            renderer5.color = color;
            fondoAzul.color = color;
            grid1.color = color;
            grid2.color = color;
        }
        if (Input.GetKey(KeyCode.R))
        {
            tCompletado = 0;
            nivelTerminado = false;
            limite1.color = Color.red;
            limite2.color = Color.red;
            Color color = renderer1.color;
            color.a = 0;
            renderer1.color = color;
            renderer2.color = color;
            renderer3.color = color;
            renderer4.color = color;
            renderer5.color = color;
            fondoAzul.color = color;
            grid1.color = color;
            grid2.color = color;
        }
        if(tCompletado > tiempoAntesEscena)
        {
            SceneManager.LoadScene(SiguienteEscena);
        }
    }

    private void materializarSprite(SpriteRenderer sprite, Color colorDestino)
    {
        sprite.color = Color.Lerp(sprite.color, colorDestino, Time.deltaTime / tCompletadoFinal);
    }

    private void materializarGrid(Tilemap tilemap, Color colorDestino)
    {
        tilemap.color = Color.Lerp(tilemap.color, colorDestino, Time.deltaTime / tCompletadoFinal);
    }
}
