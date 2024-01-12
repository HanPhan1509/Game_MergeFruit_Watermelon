using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameModel : MonoBehaviour
    {
        [SerializeField] private List<Sprite> lstObjects = new List<Sprite>();
        [SerializeField] private int limitLevelSpawn = 5;
        [SerializeField] private float timeSpawn = 1.5f;

        public int LimitLevelSpawn { get => limitLevelSpawn;}
        public float TimeSpawn { get => timeSpawn; }
        public List<Sprite> LstObjects { get => lstObjects; set => lstObjects = value; }
    }
}
