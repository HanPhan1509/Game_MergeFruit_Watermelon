using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameOver : MonoBehaviour
    {
        private Action endGame;
        public void Initialized(Action endGame)
        {
            this.endGame = endGame;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("Object"))
            {
                endGame?.Invoke();
            }
        }
    }
}
