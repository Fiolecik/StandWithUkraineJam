using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Other;
using UnityEngine;

namespace Team
{
    public class ResourcesController : MonoBehaviour
    {
        public int CurrentLucky
        {
            get => currentLucky;
        }

        public int[] Resource
        {
            get => resources;
        }

        [SerializeField] private int baseLucky = 250;
        private int currentLucky;
        private int[] resources = new int[4];

        private void Awake()
        {
            currentLucky = baseLucky;
        }

        public void AddLucky(int value)
        {
            currentLucky += value;
        }

        public bool RemoveLucky(int value)
        {
            if (!CanRemoveLucky(value))
                return false;
            currentLucky -= baseLucky;
            return true;
        }

        public bool CanRemoveLucky(int value)
        {
            return value <= currentLucky;
        }

        public void AddResource(PlayerResource playerResource, int value)
        {
            int resId = (int)playerResource;
            if (resId < 0)
                return;
            resources[resId] += value;
        }
        
        public void RemoveResource(PlayerResource playerResource, int value)
        {
            int resId = (int)playerResource;
            if (resId < 0)
                return;
            resources[resId] -= value;
        }

        public bool HasResource(PlayerResource playerResource, int value)
        {
            int resId = (int)playerResource;
            if (resId < 0)
                return false;
            return resources[resId] >= value;
        }
    }
}