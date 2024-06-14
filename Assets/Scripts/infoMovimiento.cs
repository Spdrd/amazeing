using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class infoMovimiento : MonoBehaviour
    {
        public bool control;
        public float tEsperado;

        public infoMovimiento()
        {
            control = false;
            tEsperado = 0.0f;
        }
        public infoMovimiento(bool control, float tEsperado)
        {
            this.control = control;
            this.tEsperado = tEsperado;
        }

    }
}