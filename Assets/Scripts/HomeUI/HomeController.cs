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
    private List<int> ownedItemBackgrounds = new();
    private List<int> ownedItemObjecs = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentCoin += 10000;
            saveManager.SetCoin(currentCoin);
            view.ShowCoin(currentCoin);
        }
    }

    private void Start()
    {
        //owned list
        ownedItemBackgrounds = saveManager.GetListBackground();
        if (ownedItemBackgrounds != null)
        {
            foreach (var item in ownedItemBackgrounds)
            {
                var i = model.ItemsBG.Find(x => x.id == item);
                if (i != null)
                    i.isLock = false;
            }
        }
        ownedItemObjecs = saveManager.GetListObject();
        if (ownedItemObjecs != null)
        {
            foreach (var item in ownedItemObjecs)
            {
                var i = model.ItemsObj.Find(x => x.id == item);
                if (i != null)
                    i.isLock = false;
            }
        }
        //score
        view.Home.ShowHighScore(saveManager.GetHighScore());
        //coin
        currentCoin = saveManager.GetCoin();
        view.ShowCoin(currentCoin);
        if (saveManager.GetAddCoin() != 0)
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

    public void ButtonItemsObject(ItemObject item)
    {
        string textButton = null;
        if (item.isLock)
            textButton = item.price.ToString();
        else
        {
            if (saveManager.GetObject() != item.id)
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
            {
                if (itemBG.price <= currentCoin)
                    BuyBGItems(itemBG);
                else
                    Debug.Log("Not enough money");

            }
            else
            {
                saveManager.SetBackground(itemBG.id);
                view.Shop.ChangeStateButton(false, "Equiped");
            }
        }
        else
        {
            if (itemObj.isLock)
            {
                if (itemObj.price <= currentCoin)
                    BuyObjectItems(itemObj);
                else
                    Debug.Log("Not enough money");
            }
            else
            {
                saveManager.SetObject(itemObj.id);
                view.Shop.ChangeStateButton(false, "Equiped");
            }
        }
    }

    public void BuyBGItems(ItemBackground itemBG)
    {
        view.ChangeCoin(-itemBG.price);
        currentCoin -= itemBG.price;
        saveManager.SetCoin(currentCoin);
        saveManager.SetBackground(itemBG.id);
        view.Shop.UnlockItem(TypeShop.Background, itemBG.id);
        model.ItemsBG[itemBG.id].isLock = false;
        ownedItemBackgrounds.Add(itemBG.id);
        saveManager.SaveListBackground(ownedItemBackgrounds);
        view.Shop.ChangeStateButton(false, "Equiped");
    }

    public void BuyObjectItems(ItemObject item)
    {
        view.ChangeCoin(-item.price);
        currentCoin -= item.price;
        saveManager.SetCoin(currentCoin);
        saveManager.SetObject(item.id);
        view.Shop.UnlockItem(TypeShop.Object, item.id);
        model.ItemsObj[item.id].isLock = false;
        ownedItemObjecs.Add(item.id);
        saveManager.SaveListObject(ownedItemBackgrounds);
        view.Shop.ChangeStateButton(false, "Equiped");
    }
    #endregion
}
