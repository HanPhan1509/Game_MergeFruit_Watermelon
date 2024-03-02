using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameOver : MonoBehaviour
    {
        private float timer = 0.0f;
        private Action endGame;
        public void Initialized(Action endGame)
        {
            this.endGame = endGame;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            timer = 0;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("Object"))
            {
                timer += Time.deltaTime;
                if(timer >= 1.0f) endGame?.Invoke();
            }
        }
    }
}
