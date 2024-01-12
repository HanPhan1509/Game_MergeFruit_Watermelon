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

        [SerializeField] private UnityEvent OnButtonX;

        private List<ItemBackground> itemsBG;
        private List<ItemObject> itemsObj;
        private Action<ItemBackground> OnClickedItemBG;
        private Action<ItemObject> OnClickedItemObj;
        private TypeShop typePage;

        public void ButtonX() { OnButtonX?.Invoke(); }

        public void OpenShop(List<ItemBackground> itemsBG, List<ItemObject> itemsObj, Action<ItemBackground> OnClickedItemBG, Action<ItemObject> OnClickedItemObj)
        {
            this.itemsBG = itemsBG;
            this.itemsObj = itemsObj;
            this.OnClickedItemBG = OnClickedItemBG;
            this.OnClickedItemObj = OnClickedItemObj;
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
            OnClickedItemBG?.Invoke(item);
        }
        private void OnClickItemObj(ItemObject item)
        {
            previewItem.sprite = item.preview;
            OnClickedItemObj?.Invoke(item);
        }
    }
}
