using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    [SerializeField] private HomeModel model;
    [SerializeField] private HomeView view;
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Transform canvasTransform;
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
        //Shop
        view.Shop.OpenShop(canvasTransform.localScale.x, model.ItemsBG, model.ItemsObj, ButtonItemsBG, ButtonItemsObject);
        //score
        view.Home.ShowHighScore(saveManager.GetHighScore());
        //coin
        currentCoin = saveManager.GetCoin();
        view.ShowCoin(currentCoin);
        if (saveManager.GetAddCoin() != 0)
        {
            int addCoin = saveManager.GetAddCoin();
            view.ChangeCoin(addCoin);
            currentCoin += addCoin;
            saveManager.SetCoin(currentCoin);
            saveManager.SetAddCoin(0);
        }
        view.ShowScreen(UIPopups.Home);
        soundManager.PlaySound(SoundType.soundBG);
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
        soundManager.PlaySound(SoundType.click);
        PassData();
        SceneManager.LoadScene("Game");
    }

    public void ButtonShop()
    {
        soundManager.PlaySound(SoundType.click);
        view.ShowScreen(UIPopups.Shop);
        //view.Shop.OpenShop(canvasTransform.localScale.x, model.ItemsBG, model.ItemsObj, ButtonItemsBG, ButtonItemsObject);
    }
    #endregion

    #region SHOP
    public void ButtonItemsBG(ItemBackground item)
    {
        soundManager.PlaySound(SoundType.click);
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
        soundManager.PlaySound(SoundType.click);
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
        soundManager.PlaySound(SoundType.click);
        view.ShowScreen(UIPopups.Home);
    }

    public void ButtonGet(TypeShop typePage, ItemBackground itemBG, ItemObject itemObj)
    {
        soundManager.PlaySound(SoundType.click);
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
        saveManager.SaveListObject(ownedItemObjecs);
        view.Shop.ChangeStateButton(false, "Equiped");
    }
    #endregion
}
