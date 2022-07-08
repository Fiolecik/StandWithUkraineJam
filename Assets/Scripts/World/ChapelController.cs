using System.Collections;
using System.Collections.Generic;
using Game;
using Interfaces;
using Other;
using UnityEngine;

namespace World
{
    public class ChapelController : MonoBehaviour, ITourListener
    {
        [SerializeField] private int resourcePerRound = 0;
        [SerializeField] private bool isItSpecialChapel=false;

        [SerializeField] private PlayerResource currentResource;

        private TeamController currentTeam;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeOverControll(TeamController teamController)
        {
            
        }

        public void SetSpecialResource(PlayerResource playerResource)
        {
            
        }

        public int GetBaiscResource()
        {
            return resourcePerRound;
        }

        public (int, PlayerResource) GetSpecialResource()
        {
            return (resourcePerRound, currentResource);
        }

        public void OnTourEnd()
        {
            
        }
    }
}