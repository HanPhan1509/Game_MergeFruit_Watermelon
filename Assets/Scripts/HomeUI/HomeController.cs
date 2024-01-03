using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    [SerializeField] private HomeModel model;
    [SerializeField] private HomeView view;

    private void Start()
    {
        view.ShowScreen(UIPopups.Home);
    }

    #region HOME
    public void ButtonStart()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ButtonNewGame()
    {
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
