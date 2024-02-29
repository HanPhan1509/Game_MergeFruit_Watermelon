using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        private const string ItemsBackgroundKey  = "ibg";
        private const string ItemsObjectKey  = "ioj";

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

        //Save owned list BG
        public List<int> GetListBackground(List<int> defaultValue = null)
        {
            return GetList<int>(ItemsBackgroundKey, defaultValue);
        }

        public void SaveListBackground(List<int> lstBG)
        {
            SaveList<int>(ItemsBackgroundKey, lstBG);
        }

        //Save owned list Object
        public List<int> GetListObject(List<int> defaultValue = null)
        {
            return GetList<int>(ItemsObjectKey, defaultValue);
        }

        public void SaveListObject(List<int> lstBG)
        {
            SaveList<int>(ItemsObjectKey, lstBG);
        }

        //Save/Get List T => string
        private void SaveList<T>(string key, List<T> value)
        {
            if (value == null)
            {
                value = new List<T>();
            }
            if (value.Count == 0)
            {
                PlayerPrefs.SetString(key, string.Empty);
                return;
            }
            Debug.Log("save list = " + string.Join(" ", value));
            PlayerPrefs.SetString(key, string.Join(" ", value));
        }
        private List<T> GetList<T>(string key, List<T> defaultValue)
        {
            if (PlayerPrefs.GetString(key) == string.Empty)
            {
                //return new List<T>();
                return new List<T>(0);
            }
            string temp = PlayerPrefs.GetString(key);
            string[] listTemp = temp.Split(" ");
            List<T> list = new List<T>();

            foreach (string s in listTemp)
            {
                list.Add((T)Convert.ChangeType(s, typeof(T)));
            }
            return list;
        }
    }
}
