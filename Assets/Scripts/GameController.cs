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
        [SerializeField] private Transform[] wall = new Transform[2];

        [Header("UI")]
        [SerializeField] private SpriteRenderer[] srBackground = new SpriteRenderer[3];

        private int totalScore = 0;
        private PropertiesFruits fruit;
        private LevelFruit nextFruit = LevelFruit.Zero;
        private ItemBackground itemBG;
        private bool isGameOver = false;
        private SaveManager saveManager;
        private SoundManager soundManager;
        Camera mainCamera;

        private void Awake()
        {
            //model.ThrowIfNull();
            lineGameOver.Initialized(Gameover);
            GetData();
            ChangeGameBackground();
            SetWallPosition();
        }

        private void SetWallPosition()
        {
            mainCamera = Camera.main;
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            wall[0].position = new Vector2((-cameraWidth / 2) - (wall[0].localScale.x/2), 0);
            wall[1].position = new Vector2((cameraWidth / 2) + (wall[0].localScale.x/2), 0);
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
            if (!isGameOver)
            {
                //var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //spawnObject.position = new Vector2(mouseWorldPos.x, spawnObject.position.y);
                //if (Input.GetMouseButtonUp(0))
                //{
                //    soundManager.PlaySound(SoundType.drop);
                //    fruit.transform.SetParent(poolObject);
                //    fruit.OnClick();
                //    StartCoroutine(SpawnFruit(nextFruit));
                //}

                int touchCount = Input.touchCount;
                Vector2 touchPosition = Vector2.zero;
                if (touchCount > 0)
                {
                    for (int i = 0; i < touchCount; i++)
                    {
                        Touch touch = Input.GetTouch(i);

                        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                        {
                            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                            spawnObject.position = new Vector2(touchPosition.x, spawnObject.position.y);
                        }
                        if (touch.phase == TouchPhase.Ended)
                        {
                            soundManager.PlaySound(SoundType.drop);
                            fruit.transform.SetParent(poolObject);
                            fruit.OnClick();
                            StartCoroutine(SpawnFruit(nextFruit));
                        }
                    }
                }
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
                soundManager.PlaySound(SoundType.merge);
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
            isGameOver = true;
            saveManager.SetAddCoin(totalScore);
            if (saveManager.GetHighScore() < totalScore)
                saveManager.SetHighScore(totalScore);
            view.ShowScreen(UIPopups.Gameover, totalScore);
        }

        #endregion

        #region UI

        private void GetData()
        {
            GameObject manager = GameObject.Find("Manager");
            DataManager myData = manager.GetComponent<DataManager>();
            this.saveManager   = manager.GetComponent<SaveManager>();
            this.soundManager  = manager.GetComponent<SoundManager>();
            if (myData != null)
            {
                this.itemBG = myData.ibackground;
                model.LstObjects = myData.lstObj;
                isGameOver = false;
            }
            soundManager.PlayMusic();
        }

        private void ChangeGameBackground()
        {
            srBackground[0].sprite = this.itemBG.background;
            srBackground[1].sprite = this.itemBG.detailBG;
            srBackground[2].sprite = this.itemBG.ground;
        }

        public void ButtonLoadScene(string nameScene)
        {
            soundManager.PlaySound(SoundType.click);
            SceneManager.LoadScene(nameScene);
        }
        #endregion
    }
}
