using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControlCentral : MonoBehaviour
    {

        [SerializeField] float separacionRay = 0.06f;
        [SerializeField] float longitudRayCast = 0.7f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ClaseEstatica.separacionRay = separacionRay;
            ClaseEstatica.longitudRayCast = longitudRayCast;
        }
    }
}