using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace UI
{
    public class UITour : MonoBehaviour
    {
        public void EndTour()
        {
            if (TourController.Instance.CPU)
                return;
            TourController.Instance.EndTour();
        }
    }
}