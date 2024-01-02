using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    [SerializeField] private HomeModel model;
    [SerializeField] private HomeView view;

    #region HOME
    public void ButtonStart()
    {

    }
    
    public void ButtonNewGame()
    {

    }   
    
    public void ButtonShop()
    {
        view.ShowScreen(UIPopups.Shop);
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
