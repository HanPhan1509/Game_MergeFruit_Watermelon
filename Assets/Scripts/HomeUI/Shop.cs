using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Game
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private Image previewItem;
        [SerializeField] private GameObject prefItems;
        [SerializeField] private GameObject btnGet;

        [SerializeField] private UnityEvent OnButtonX;
        [SerializeField] private UnityEvent OnButtonGet;

        private List<ItemBackground> itemsBG;
        private List<ItemObject> itemsObj;
        private TypeShop typePage;
        private ItemBackground itemBG;
        private ItemObject itemObj;

        public void ButtonX() { OnButtonX?.Invoke(); }

        public void ButtonGet()
        {
            if(typePage == TypeShop.Background)
                PlayerPrefs.SetInt("background", itemBG.id);
            if (typePage == TypeShop.Object)
                PlayerPrefs.SetInt("object", itemObj.id);
            btnGet.SetActive(false);
        }

        public void OpenShop(List<ItemBackground> itemsBG, List<ItemObject> itemsObj)
        {
            this.itemsBG = itemsBG;
            this.itemsObj = itemsObj;
            SwitchToggle(0);
        }

        public void SwitchToggle(int typeShop)
        {
            typePage = (TypeShop)typeShop;
            ClearItems();
            switch (typePage)
            {
                case TypeShop.Background:
                    LoadItemsBG();
                    break;
                case TypeShop.Object:
                    LoadItemsObject();
                    break;
            }
        }

        private void ClearItems()
        {
            if (content.childCount > 0)
            {
                for (int i = 0; i < content.childCount; i++)
                {
                    Transform child = content.GetChild(i);
                    SimplePool.Despawn(child.gameObject);
                }
            }
        }

        private void LoadItemsBG()
        {
            for (int i = 0; i < itemsBG.Count; i++)
            {
                DetailItem item = SimplePool.Spawn(prefItems, Vector2.zero, Quaternion.identity).GetComponent<DetailItem>();
                item.transform.SetParent(content);
                itemsBG[i].id = i;
                item.ChangeDetailItemBG(TypeShop.Background, itemsBG[i], OnClickItemBG);
            }
        }

        private void LoadItemsObject()
        {
            for (int i = 0; i < itemsObj.Count; i++)
            {
                GameObject item = SimplePool.Spawn(prefItems, Vector2.zero, Quaternion.identity);
                item.transform.SetParent(content);
                itemsObj[i].id = i;
                item.GetComponent<DetailItem>().ChangeDetailItemObj(TypeShop.Object, itemsObj[i], OnClickItemObj);
            }
        }

        private void OnClickItemBG(ItemBackground item)
        {
            previewItem.sprite = item.preview;
            this.itemBG = item;
            btnGet.SetActive(PlayerPrefs.GetInt("background", 0) != item.id);
                
        }
        private void OnClickItemObj(ItemObject item)
        {
            previewItem.sprite = item.preview;
            this.itemObj = item;
            btnGet.SetActive(PlayerPrefs.GetInt("object", 0) != item.id);
        }
    }
}
