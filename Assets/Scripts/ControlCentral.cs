using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Assets.Scripts
{
    public class ControlCentral : MonoBehaviour
    {
        // Muros
        [SerializeField] float separacionRayMuros = 0.06f;
        [SerializeField] float longitudRayCastMuros = 0.7f;
        [SerializeField] float velocidadMuros = 1.0f;
        [SerializeField] bool enMovH;
        [SerializeField] bool enMovV;
        [SerializeField] int murosEnMovH;
        [SerializeField] int murosEnMovV;
        [SerializeField] Dictionary<Guid, InfoMuro> diccionarioMuros;

        // Bola
        [SerializeField] float distanciaDetectBola = 0.15f;
        [SerializeField] float velocidadBola = 1.4f;

        // Meta
        [SerializeField] bool nivelCompletado = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            validarNivelCompletado();
            visualizarInfo();
        }

        public void visualizarInfo()
        {
            // Actualizacion info Muros
            ClaseEstatica.separacionRayMuros = separacionRayMuros;
            ClaseEstatica.longitudRayCastMuros = longitudRayCastMuros;
            diccionarioMuros = ClaseEstatica.infoMuros;

            // Recibir datos InfoMov
            enMovH = ClaseEstatica.infoMovimientoH.enMov;
            enMovV = ClaseEstatica.infoMovimientoV.enMov;
            murosEnMovH = ClaseEstatica.infoMovimientoH.numMurosEnMov;
            murosEnMovV = ClaseEstatica.infoMovimientoV.numMurosEnMov;

            // Actualizacion info Bola
            ClaseEstatica.distDetectBola = distanciaDetectBola;
            ClaseEstatica.velocidadBola = velocidadBola;

        }

        public void validarNivelCompletado()
        {
            if (ClaseEstatica.posBola == ClaseEstatica.posMeta)
            {
                ClaseEstatica.nivelCompletado = true;
            }
        }
    }
}