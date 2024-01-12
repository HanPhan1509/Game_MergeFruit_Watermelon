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

    private void Start()
    {
        itemBG = model.ItemsBG[0];
        view.ShowScreen(UIPopups.Home);
    }

    private void PassData(ItemBackground itemBG)
    {
        //GameObject newObject = new GameObject();
        //newObject.transform.SetParent(view.transform, false);
        DataManager myObj = GameObject.Find("Data").GetComponent<DataManager>();
        myObj.ibackground = itemBG;
    }    

    #region HOME
    public void ButtonStart()
    {
        PassData(itemBG);
        SceneManager.LoadScene("Game");
    }
    
    public void ButtonNewGame()
    {
        PassData(itemBG);
        SceneManager.LoadScene("Game");
    }   
    
    public void ButtonShop()
    {
        view.ShowScreen(UIPopups.Shop);
        view.LoadShopScreen(model.ItemsBG, model.ItemsObj, OnClickedItemBG);
    }
    
    private void OnClickedItemBG(ItemBackground item)
    {
        this.itemBG = item;
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
