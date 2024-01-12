using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Image imgNextFruit;
        [SerializeField] private TextMeshProUGUI txtScore;
        [SerializeField] private UnityEvent OnButtonReturnHome;

        public void ButtonReturn()
        {
            OnButtonReturnHome?.Invoke();
        }    

        public void AddScore(int score)
        {
            txtScore.text = score.ToString();
        }

        public void ShowNextFruit(Sprite sprite)
        {
            imgNextFruit.sprite = sprite;
        }
    }
}
