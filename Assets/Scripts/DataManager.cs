using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DataManager : MonoBehaviour
    {
        public List<Sprite> lstObj;
        public ItemBackground ibackground;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
