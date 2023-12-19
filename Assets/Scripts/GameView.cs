using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameUI gameUI;
        [SerializeField] private UIGameOver uiGameOver;

        public GameUI GameUI => gameUI;
        public UIGameOver UIGameOver => uiGameOver;

        public void ShowGameOver(int totalScore)
        {
            uiGameOver.gameObject.SetActive(true);
            uiGameOver.ShowScore(totalScore);
        }    
    }
}
