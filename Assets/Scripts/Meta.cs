using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Meta : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ClaseEstatica.posMeta = transform.position;
        }
    }
}