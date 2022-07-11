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

        [SerializeField] private GameObject[] fxeffects;

        private TeamController currentTeam;

        private void Awake()
        {
            TourController.Instance.AddTourListener(this);
        }

        private void Update()
        {
            for (int i = 0; i < 4; i++)
            {
                fxeffects[i].SetActive(i == (((int)currentResource) - 2));
            }
        }

        public void TakeOverControll(TeamController teamController)
        {
            if (!IsPossibleToTakeOverControll(teamController))
                return;
            if(currentTeam!=null)
                currentTeam.ResourcesController.RemoveResource(currentResource, specialResources);
            currentTeam = teamController;
            
            currentTeam.ResourcesController.AddResource(currentResource, specialResources);
        }

        public bool IsPossibleToTakeOverControll(TeamController teamController)
        {
            return teamController != currentTeam;
        }

        public bool IsPossibleToSetSpecialResource()
        {
            return currentResource != PlayerResource.none && currentResource != PlayerResource.lucky;
        }

        public void SetSpecialResource(PlayerResource playerResource)
        {
            if(currentTeam!=null)
                currentTeam.ResourcesController.RemoveResource(currentResource, specialResources);
            currentResource = playerResource;
            currentTeam.ResourcesController.AddResource(currentResource, specialResources);
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