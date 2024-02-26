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
        [SerializeField] private TextMeshProUGUI txtCoin;
        [SerializeField] private RectTransform tranformBoxCoin;
        [SerializeField] private UnityEvent OnButtonShop;
        [SerializeField] private UnityEvent OnButtonSettings;
        [SerializeField] private UnityEvent OnButtonLeaderboard;
        [SerializeField] private UnityEvent OnButtonGift;
        [SerializeField] private UnityEvent OnButtonStart;
        [SerializeField] private UnityEvent OnButtonNewGame;

        public void AddCoin(int addCoin)
        {
            int coin = Int32.Parse(txtCoin.text);
            DOTween.To(() => coin, x => coin = x, coin + addCoin, 1f)
            .OnUpdate(() => {
                txtCoin.text = coin.ToString();
            })
            .SetEase(Ease.Linear);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + addCoin);
        }

        public void ShowHighScore(int score)
        {
            txtHighScore.text = score.ToString();
        }

        public void ShowCoin(int coin)
        {
            txtCoin.text = coin.ToString();
        }    

        public void ButtonStart() { OnButtonStart?.Invoke(); }
        public void ButtonNewGame() { OnButtonNewGame?.Invoke(); }
        public void ButtonShop() { OnButtonShop?.Invoke(); }
        public void ButtonSettings() { OnButtonSettings?.Invoke(); }
        public void ButtonLeaderboard() { OnButtonLeaderboard?.Invoke(); }
        public void ButtonGift() { OnButtonGift?.Invoke(); }
    }
}
