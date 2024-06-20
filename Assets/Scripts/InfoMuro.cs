using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class InfoMuro : MonoBehaviour
    {
        public Vector3 posActual;

        public Vector3 posDestino;
        public Vector3 prevPosDestino;

        public Vector3 inicio;

        public InfoMuro(Vector3 posActual)
        {
            this.posDestino = posActual;
            this.posActual = posActual;
            this.prevPosDestino = posActual;
            this.inicio = posActual;
        }
    }
}