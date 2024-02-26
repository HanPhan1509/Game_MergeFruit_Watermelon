using System;
using System.Collections;
using static DG.Tweening.DOTween;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Game
{
    public class Home : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtHighScore;
        [SerializeField] private UnityEvent OnButtonShop;
        [SerializeField] private UnityEvent OnButtonSettings;
        [SerializeField] private UnityEvent OnButtonLeaderboard;
        [SerializeField] private UnityEvent OnButtonGift;
        [SerializeField] private UnityEvent OnButtonStart;
        [SerializeField] private UnityEvent OnButtonNewGame;

        public void ShowHighScore(int score)
        {
            txtHighScore.text = score.ToString();
        }

        public void ButtonStart() { OnButtonStart?.Invoke(); }
        public void ButtonNewGame() { OnButtonNewGame?.Invoke(); }
        public void ButtonShop() { OnButtonShop?.Invoke(); }
        public void ButtonSettings() { OnButtonSettings?.Invoke(); }
        public void ButtonLeaderboard() { OnButtonLeaderboard?.Invoke(); }
        public void ButtonGift() { OnButtonGift?.Invoke(); }
    }
}
