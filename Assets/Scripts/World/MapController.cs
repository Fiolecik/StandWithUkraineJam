using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{

    [System.Serializable]
    public struct Spawn
    {
        public Transform baseSpawn;
        public Transform unitSpawn;
    }
    public class MapController : MonoBehaviour
    {
        [SerializeField] private Spawn[] spawns;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}