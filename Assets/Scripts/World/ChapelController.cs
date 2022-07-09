using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Team;
using Interfaces;
using Other;
using UnityEngine;

namespace World
{
    public class ChapelController : MonoBehaviour, ITourListener
    {
        [SerializeField] private int resourcePerRound = 0;
        [SerializeField] private int specialResources = 0;
        [SerializeField] private bool isItSpecialChapel=false;

        [SerializeField] private PlayerResource currentResource;

        private TeamController currentTeam;

        private void Awake()
        {
            TourController.Instance.AddTourListener(this);
        }

        public void TakeOverControll(TeamController teamController)
        {
            if (!IsPossibleToTakeOVerControll(teamController))
                return;
            if(currentTeam!=null)
                currentTeam.ResourcesController.RemoveResource(currentResource, specialResources);
            currentTeam = teamController;
            
            currentTeam.ResourcesController.AddResource(currentResource, specialResources);
        }

        public bool IsPossibleToTakeOVerControll(TeamController teamController)
        {
            return teamController != currentTeam;
        }

        public void SetSpecialResource(PlayerResource playerResource)
        {
            currentResource = playerResource;
        }
        
        public void OnTourEnd()
        {
            if (currentTeam != null)
            {
                currentTeam.ResourcesController.AddLucky(resourcePerRound);
            }
        }
    }
}