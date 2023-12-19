using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtScore;
        [SerializeField] private UnityEvent OnButtonPlayAgain;
        [SerializeField] private UnityEvent OnButtonHome;

        public void ShowScore(int scoreGame)
        {
            txtScore.text = scoreGame.ToString();
        }

        public void ButtonHome()
        {
            OnButtonHome?.Invoke();
        }    

        public void ButtonPlayAgain()
        {
            OnButtonPlayAgain?.Invoke();
        }    
    }
}
