using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameModel model;
        [SerializeField] private GameView view;
        [SerializeField] private Transform spawnObject;
        [SerializeField] private Transform poolObject;
        [SerializeField] private GameObject prefabFruit;
        [SerializeField] private GameOver lineGameOver;

        [Header("UI")]
        [SerializeField] private SpriteRenderer srBackground;

        private int totalScore = 0;
        private PropertiesFruits fruit;
        private LevelFruit nextFruit = LevelFruit.Zero;

        private void Awake()
        {
            //model.ThrowIfNull();
            lineGameOver.Initialized(Gameover);
        }

        void Start()
        {
            StartGame();
        }

        #region GAMEPLAY

        private void StartGame()
        {
            NextFruit();
            StartCoroutine(SpawnFruit((LevelFruit)Random.Range(0, model.LimitLevelSpawn)));
        }    

        // Update is called once per frame
        void Update()
        {
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
            PropertiesFruits newFruit = SimplePool.Spawn(prefabFruit, spawnObject.position, Quaternion.identity).GetComponent<PropertiesFruits>();
            newFruit.transform.SetParent(spawnObject);
            newFruit.Initialized(model.LstObjects[(int)currentFruit], (int)currentFruit, MergeFruit, Gameover);
            fruit = newFruit;
            NextFruit();
            if (fruit.IsEnd)
                Gameover();
        }

        private void NextFruit()
        {
            int random = Random.Range(0, model.LimitLevelSpawn);
            view.GameUI.ShowNextFruit(model.LstObjects[random]);
            nextFruit = (LevelFruit)random;
        }

        private void MergeFruit(PropertiesFruits f1, PropertiesFruits f2, int level)
        {
            if (!f1.IsColide && !f2.IsColide)
            {
                Debug.Log(((level + 1) * (level + 2)) / 2);
                totalScore += ((level + 1) * (level + 2)) / 2;
                view.GameUI.AddScore(totalScore);
                Vector2 pos = (f1.transform.position + f2.transform.position) / 2;
                PropertiesFruits levelUpFruit = SimplePool.Spawn(prefabFruit, pos, Quaternion.identity).GetComponent<PropertiesFruits>();
                levelUpFruit.Initialized(model.LstObjects[level + 1], level + 1, MergeFruit, Gameover, true);
                SimplePool.Despawn(f1.gameObject);
                SimplePool.Despawn(f2.gameObject);
            }
        }

        private void Gameover()
        {
            Debug.Log("GameOver");
            view.ShowScreen(UIPopups.Gameover, totalScore);
        }

        #endregion

        #region UI

        private void GetData()
        {
            //if (GameObject.FindGameObjectWithTag(Constants.ParamsTag) != null)
            //{

            //}    
        }    

        private void ChangeBackground(Sprite spriteBG)
        {
            srBackground.sprite = spriteBG;
        }    

        public void ButtonLoadScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
        #endregion
    }
}
