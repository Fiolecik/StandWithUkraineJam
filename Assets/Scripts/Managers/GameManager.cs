using System.Collections;
using System.Collections.Generic;
using Classes;
using Patterns;
using UnityEngine;

namespace Managers
{
    public enum GameStatus
    {
        map,
        fighting
    }
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public GameStatus GameStatus { get => gameStatus; }

        public GameSettings GameSettings { get; set; } = new GameSettings();

        private GameStatus gameStatus;

        public void StartGame()
        {
            
        }
    }
}