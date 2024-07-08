using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BotonCambio : MonoBehaviour
{
    [SerializeField] bool dentro;

    [SerializeField] float horizontalXMax = float.NegativeInfinity;
    [SerializeField] float horizontalYMin = float.PositiveInfinity;
    [SerializeField] float verticalXMax = float.NegativeInfinity;

    [SerializeField] string escenaDestino;
    [SerializeField] string nivelTXT;

    // Start is called before the first frame update
    void Start()
    {
        ajustarHitBox();
    }

    // Update is called once per frame
    void Update()
    {
        detectar();
    }

    private void OnMouseOver()
    {
        dentro = true;
    }

    private void OnMouseExit()
    {
        dentro = false;
    }

    public void detectar()
    {
        if (dentro)
        {
            foreach (Transform hijo in transform)
            {
                SpriteRenderer spriteRenderer = hijo.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.green;
                
            }
            if (Input.GetMouseButton(0))
            {
                cambiarEscena();
            }
        }
        if (!dentro)
        {
            foreach (Transform hijo in transform)
            {
                SpriteRenderer spriteRenderer = hijo.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.red;

            }
        }
        
    }

    public void cambiarEscena()
    {
        ClaseEstatica.nivelSeleccionado = nivelTXT;
        SceneManager.LoadScene(escenaDestino);
    }

    public void ajustarHitBox()
    {

        foreach (Transform hijo in transform)
        {
            Vector3 hijoAjustado = hijo.localPosition;

            if(hijo.name.Contains("limiteHorizontal"))
            {
                if(horizontalXMax < hijoAjustado.x)
                {
                    horizontalXMax = hijoAjustado.x;
                }

                if (horizontalYMin > hijoAjustado.y)
                {
                    horizontalYMin = hijoAjustado.y;
                }
            }

            if(hijo.name.Contains("limiteVertical"))
            {;
                if(verticalXMax < hijoAjustado.x)
                {
                    verticalXMax = hijoAjustado.x;
                }
            }
        }

        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        Vector2 offset = new Vector2(horizontalXMax / 2, horizontalYMin / 2);
        collider.offset = offset;


        Vector2 size = new Vector2(verticalXMax + 0.5f, -horizontalYMin);
        collider.size = size;
    }
}
