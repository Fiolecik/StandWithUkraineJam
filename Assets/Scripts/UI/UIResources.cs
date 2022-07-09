using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIResources : MonoBehaviour
    {
        [SerializeField] private TMP_Text lucky;

        [SerializeField] private TMP_Text[] resources;

        [SerializeField] private TMP_Text currentTour;
        
        void Update()
        {
            var currentMoving = TourController.Instance.CurrentMoving;
            if (currentMoving != null)
            {
                lucky.text = currentMoving.ResourcesController.CurrentLucky.ToString();
                for (int i = 0; i < resources.Length; i++)
                {
                    resources[i].text = currentMoving.ResourcesController.Resource[i].ToString();
                }

                currentTour.text = currentMoving.Name;
            }
        }
    }
}