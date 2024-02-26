using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class HomeController : MonoBehaviour
{
    [SerializeField] private HomeModel model;
    [SerializeField] private HomeView view;
    private ItemBackground itemBG;
    private ItemObject itemObj;

    private void Start()
    {
        itemBG = model.ItemsBG[0];
        itemObj = model.ItemsObj[0];
        view.Home.ShowHighScore(PlayerPrefs.GetInt("highscore", 0));
        view.Home.ShowCoin((PlayerPrefs.GetInt("coin", 0)));
        if(PlayerPrefs.GetInt("addcoin", 0) != 0)
        {
            view.Home.AddCoin(PlayerPrefs.GetInt("addcoin", 0));
            PlayerPrefs.SetInt("addcoin", 0);
        }
        view.ShowScreen(UIPopups.Home);
    }

    private void PassData()
    {
        //GameObject newObject = new GameObject();
        //newObject.transform.SetParent(view.transform, false);
        DataManager myObj = GameObject.Find("Data").GetComponent<DataManager>();
        myObj.ibackground = model.ItemsBG[PlayerPrefs.GetInt("background", 0)];
        myObj.lstObj = model.ItemsObj[PlayerPrefs.GetInt("object", 0)].allObjects;
    }    

    #region HOME
    public void ButtonNewGame()
    {
        PassData();
        SceneManager.LoadScene("Game");
    }   
    
    public void ButtonShop()
    {
        view.ShowScreen(UIPopups.Shop);
        view.LoadShopScreen(model.ItemsBG, model.ItemsObj);
    }   


    public void ButtonSettings()
    {

    }   
    
    public void ButtonGift()
    {

    }   
    
    public void ButtonLeaderboard()
    {

    }
    #endregion

    #region SHOP
    public void ButtonX()
    {
        view.ShowScreen(UIPopups.Home);
    }    
    #endregion
}
