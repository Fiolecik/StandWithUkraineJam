using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

namespace Game
{
    public class TourController : SingletonMonoBehaviour<TourController>
    {
        private List<TeamController> teams = new List<TeamController>();
        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MakeTeams(int count)
        {
            
        }
    }
}