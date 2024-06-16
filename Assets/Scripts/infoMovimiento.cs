using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class InfoMovimiento : MonoBehaviour
    {

        public bool enMov;
        public float tEsperado;

        public InfoMovimiento()
        {
            enMov = false;
            tEsperado = 0.0f;
        }
    }
}