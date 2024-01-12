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
        view.ShowScreen(UIPopups.Home);
    }

    private void PassData(ItemBackground itemBG, ItemObject itemObj)
    {
        //GameObject newObject = new GameObject();
        //newObject.transform.SetParent(view.transform, false);
        DataManager myObj = GameObject.Find("Data").GetComponent<DataManager>();
        myObj.ibackground = itemBG;
        myObj.lstObj = itemObj.allObjects;
        Debug.Log($"Pass data = {myObj.lstObj.Count} {myObj.lstObj}");
        Debug.Log($"Pass data = {itemObj.allObjects.Count} - {itemObj.typeObject}");
    }    

    #region HOME
    public void ButtonStart()
    {
        PassData(itemBG, itemObj);
        SceneManager.LoadScene("Game");
    }
    
    public void ButtonNewGame()
    {
        PassData(itemBG, itemObj);
        SceneManager.LoadScene("Game");
    }   
    
    public void ButtonShop()
    {
        view.ShowScreen(UIPopups.Shop);
        view.LoadShopScreen(model.ItemsBG, model.ItemsObj, OnClickedItemBG, OnClickedItemObject);
    }
    
    private void OnClickedItemBG(ItemBackground item)
    {
        this.itemBG = item;
    }    
    private void OnClickedItemObject(ItemObject item)
    {
        this.itemObj = item;
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
