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
        

        public void ShowScreen(UIPopups popups, int totalScore = 0)
        {
            uiGameOver.gameObject.SetActive(false);
            gameUI.gameObject.SetActive(false);
            switch (popups)
            {
                case UIPopups.Game:
                    gameUI.gameObject.SetActive(true);
                    break;
                case UIPopups.Gameover:
                    uiGameOver.gameObject.SetActive(true);
                    uiGameOver.ShowScore(totalScore);
                    break;
            }    
        }   
    }
}
