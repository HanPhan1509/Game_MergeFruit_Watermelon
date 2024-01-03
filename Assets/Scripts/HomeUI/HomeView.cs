using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField] private Shop uiShop;
        [SerializeField] private Home uiHome;

        public Shop Shop => uiShop;
        public Home Home => uiHome;

        public void ShowScreen(UIPopups popups)
        {
            uiHome.gameObject.SetActive(false);
            uiShop.gameObject.SetActive(false);
            switch (popups)
            {
                case UIPopups.Home:
                    uiHome.gameObject.SetActive(true);
                    break;
                case UIPopups.Shop:
                    uiShop.gameObject.SetActive(true);
                    break;
            }
        }

        public void LoadShopScreen(List<ItemBackground> itemsBG, List<ItemObject> itemsObj)
        {
            Shop.OpenShop(itemsBG, itemsObj);
        }    
    }
}
