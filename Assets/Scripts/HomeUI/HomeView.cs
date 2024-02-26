using DG.Tweening;
using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField] private Shop uiShop;
        [SerializeField] private Home uiHome;

        [SerializeField] private TextMeshProUGUI txtCoin;
        [SerializeField] private RectTransform tranformBoxCoin;

        public Shop Shop => uiShop;
        public Home Home => uiHome;

        public void ShowScreen(UIPopups popups)
        {
            uiHome.gameObject.SetActive(false);
            uiShop.gameObject.SetActive(false);
            switch (popups)
            {
                case UIPopups.Home:
                    uiHome.gameObject.SetActive(true);
                    break;
                case UIPopups.Shop:
                    uiShop.gameObject.SetActive(true);
                    break;
            }
        }

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
        public void ShowCoin(int coin)
        {
            txtCoin.text = coin.ToString();
        }
    }
}
