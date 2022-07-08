using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

namespace World
{
    public class GridController : SingletonMonoBehaviour<GridController>
    {
        private List<GridObstacle> obstacles = new List<GridObstacle>();
        
        public void AddObstacle(GridObstacle obstacle)
        {
            obstacles.Add(obstacle);
        }

        public void RemoveObstacle(GridObstacle obstacle)
        {
            obstacles.Remove(obstacle);
        }

        public Vector2[] FindPath(Vector2 point)
        {


            return null;
        }
    }
}