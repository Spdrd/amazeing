using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControlCentral : MonoBehaviour
    {
        // Muros
        [SerializeField] float separacionRay = 0.06f;
        [SerializeField] float longitudRayCast = 0.7f;
        [SerializeField] float velocidadMuros = 1.0f;
        [SerializeField] bool enMovH;
        [SerializeField] bool enMovV;

        // Bola
        [SerializeField] float distanciaDetectBola = 0.15f;
        [SerializeField] float velocidadBola = 1.4f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Actualizacion info Muros
            ClaseEstatica.separacionRayMuros = separacionRay;
            ClaseEstatica.longitudRayCastMuros = longitudRayCast;

            // Recibir datos InfoMov
            enMovH = ClaseEstatica.infoMovimientoH.enMov;
            enMovV = ClaseEstatica.infoMovimientoV.enMov;

            // Actualizacion info Bola
            ClaseEstatica.distDetectBola = distanciaDetectBola;
            ClaseEstatica.velocidadBola = velocidadBola;

        }
    }
}