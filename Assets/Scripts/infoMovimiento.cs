using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class InfoMovimiento : MonoBehaviour
    {

        public bool enMov;
        public int numMurosEnMov;

        public InfoMovimiento()
        {
            enMov = false;
            numMurosEnMov = 0;
        }
    }
}