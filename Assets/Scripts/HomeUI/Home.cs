using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Home : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnButtonShop;
        [SerializeField] private UnityEvent OnButtonSettings;
        [SerializeField] private UnityEvent OnButtonLeaderboard;
        [SerializeField] private UnityEvent OnButtonGift;
        [SerializeField] private UnityEvent OnButtonStart;
        [SerializeField] private UnityEvent OnButtonNewGame;

        public void ButtonStart() { OnButtonStart?.Invoke(); }
        public void ButtonNewGame() { OnButtonNewGame?.Invoke(); }
        public void ButtonShop() { OnButtonShop?.Invoke(); }
        public void ButtonSettings() { OnButtonSettings?.Invoke(); }
        public void ButtonLeaderboard() { OnButtonLeaderboard?.Invoke(); }
        public void ButtonGift() { OnButtonGift?.Invoke(); }
    }
}
