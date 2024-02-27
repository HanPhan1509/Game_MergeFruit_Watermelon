using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class HomeController : MonoBehaviour
{
    [SerializeField] private HomeModel model;
    [SerializeField] private HomeView view;
    [SerializeField] private SaveManager saveManager;
    private int currentCoin = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentCoin += 500;
            saveManager.SetCoin(currentCoin);
            view.ShowCoin(currentCoin);
        }
    }

    private void Start()
    {
        view.Home.ShowHighScore(saveManager.GetHighScore());
        currentCoin = saveManager.GetCoin();
        view.ShowCoin(currentCoin);
        if(saveManager.GetAddCoin() != 0)
        {
            int addCoin = saveManager.GetAddCoin();
            view.ChangeCoin(addCoin);
            saveManager.SetCoin(currentCoin + addCoin);
            saveManager.SetAddCoin(0);
        }
        view.ShowScreen(UIPopups.Home);
    }

    private void PassData()
    {
        //GameObject newObject = new GameObject();
        //newObject.transform.SetParent(view.transform, false);
        DataManager myObj = GameObject.Find("Manager").GetComponent<DataManager>();
        myObj.ibackground = model.ItemsBG[saveManager.GetBackground()];
        myObj.lstObj = model.ItemsObj[saveManager.GetObject()].allObjects;
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
        view.Shop.OpenShop(model.ItemsBG, model.ItemsObj, ButtonItemsBG);
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
    public void ButtonItemsBG(ItemBackground item)
    {
        string textButton = null;
        if (item.isLock)
            textButton = item.price.ToString();
        else
        {
            if (saveManager.GetBackground() != item.id)
                textButton = "Get";
            else
                textButton = "Equiped";
        }

        view.Shop.ChangeStateButton(item.isLock, textButton);
    }    
    public void ButtonX()
    {
        view.ShowScreen(UIPopups.Home);
    }

    public void ButtonGet(TypeShop typePage, ItemBackground itemBG, ItemObject itemObj)
    {
        if (typePage == TypeShop.Background)
        {
            if (itemBG.isLock)
            {;
                if (itemBG.price < currentCoin)
                {
                    BuyBGItems(itemBG);
                }
            }
            if (itemBG.id != PlayerPrefs.GetInt("background", 0))
            {
                PlayerPrefs.SetInt("background", itemBG.id);
            }
        }
        

        if (typePage == TypeShop.Object)
            PlayerPrefs.SetInt("object", itemObj.id);
    }

    public void BuyBGItems(ItemBackground itemBG)
    {
        view.ChangeCoin(-itemBG.price);
        currentCoin -= itemBG.price;
        saveManager.SetCoin(currentCoin);
        saveManager.SetBackground(itemBG.id);
        //Luu list => unlock item trong list
        view.Shop.ChangeStateButton(false, "Equiped");
    }    
    #endregion
}
