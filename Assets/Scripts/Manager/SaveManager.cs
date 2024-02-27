using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SaveManager : MonoBehaviour
    {
        // Key
        private const string BackgroundKey = "bgr";
        private const string ObjectKey     = "obj";
        private const string CoinKey       = "coin";
        private const string AddCoinKey    = "acn";
        private const string HighScoreKey  = "hsc";

        //Save current Background
        public void SetBackground(int id)
        {
            PlayerPrefs.SetInt(BackgroundKey, id);
        }
        public int GetBackground()
        {
            return PlayerPrefs.GetInt(BackgroundKey, 0);
        }

        //Save current Object
        public void SetObject(int id)
        {
            PlayerPrefs.SetInt(ObjectKey, id);
        }
        public int GetObject()
        {
            return PlayerPrefs.GetInt(ObjectKey, 0);
        }

        //Save Coin
        public void SetCoin(int coin)
        {
            PlayerPrefs.SetInt(CoinKey, coin);
        }
        public int GetCoin()
        {
            return PlayerPrefs.GetInt(CoinKey, 0);
        }

        //Save add Coin
        public void SetAddCoin(int coin)
        {
            PlayerPrefs.SetInt(AddCoinKey, coin);
        }
        public int GetAddCoin()
        {
            return PlayerPrefs.GetInt(AddCoinKey, 0);
        }

        //Save HighScore
        public void SetHighScore(int score)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }
        public int GetHighScore()
        {
            return PlayerPrefs.GetInt(HighScoreKey, 0);
        }
    }
}
