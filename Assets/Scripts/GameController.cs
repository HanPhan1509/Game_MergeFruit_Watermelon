using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameModel model;
        [SerializeField] private GameView view;
        [SerializeField] private Transform spawnObject;
        [SerializeField] private Transform poolObject;
        [SerializeField] private GameObject prefabFruit;

        private PropertiesFruits fruit;
        private LevelFruit nextFruit = LevelFruit.Zero;

        private void Awake()
        {
            //model.ThrowIfNull();
        }

        void Start()
        {
            NextFruit();
            StartCoroutine(SpawnFruit((LevelFruit)Random.Range(0, model.LimitLevelSpawn)));
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.Z))
            {
                Time.timeScale = 0;
            }
            if(Input.GetKey(KeyCode.X))
            {
                Time.timeScale = 1;
            }

            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnObject.position = new Vector2(mouseWorldPos.x, spawnObject.position.y);
            if (Input.GetMouseButtonUp(0))
            {
                fruit.transform.SetParent(poolObject);
                fruit.OnClick();
                StartCoroutine(SpawnFruit(nextFruit));
                //Invoke(nameof(SpawnFruit), model.TimeSpawn);
            }
        }

        private IEnumerator SpawnFruit(LevelFruit currentFruit)
        {
            yield return new WaitForSeconds(model.TimeSpawn);
            PropertiesFruits newFruit = RandomFruit();
            newFruit.transform.SetParent(spawnObject);
            newFruit.Initialized(model.LstObjects[(int)currentFruit], (int)currentFruit);
            fruit = newFruit;
            NextFruit();
        }    

        private void NextFruit()
        {
            int random = Random.Range(0, model.LimitLevelSpawn);
            view.GameUI.ShowNextFruit(model.LstObjects[random]);
            nextFruit = (LevelFruit)random;
        }

        private PropertiesFruits RandomFruit()
        {
            return SimplePool.Spawn(prefabFruit, spawnObject.position, Quaternion.identity).GetComponent<PropertiesFruits>();
        }    
    }
}
