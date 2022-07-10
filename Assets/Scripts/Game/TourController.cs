using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Managers;
using Patterns;
using UnityEngine;
using Team;

namespace Game
{
    public class TourController : SingletonMonoBehaviour<TourController>
    {
        public TeamController CurrentMoving
        {
            get
            {
                return teams[currentIdTeam];
            }
        }

        public bool CPU
        {
            get
            {
                if (CurrentMoving != null)
                {
                    return CurrentMoving.CPU;
                }

                return false;
            }
        }
        private List<TeamController> teams = new List<TeamController>();
        private List<ITourListener> tourListeners = new List<ITourListener>();

        private int currentIdTeam = 0;

        public void AddTeam(TeamController teamController)
        {
            teams.Add(teamController);
            teamController.Name = "Player_" + teams.Count + "_" + (teamController.CPU ? "CPU" : "User");
        }

        public void RemoveTeam(TeamController teamController)
        {
            teams.Remove(teamController);
        }

        public void AddTourListener(ITourListener tourListener)
        {
            tourListeners.Add(tourListener);
        }

        public void EndTour()
        {
            NextTeam();
        }

        private void NextTeam()
        {
            currentIdTeam++;
            if (currentIdTeam >= teams.Count)
            {
                currentIdTeam = 0;
                foreach (var VARIABLE in tourListeners)
                {
                    VARIABLE.OnTourEnd();
                }
            }
        }
    }
}