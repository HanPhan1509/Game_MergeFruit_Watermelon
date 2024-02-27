using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class DetailItem : MonoBehaviour
    {
        [SerializeField] private Image      previewItem;
        [SerializeField] private GameObject highlight;
        [SerializeField] private GameObject lockItem;

        private TypeShop typeShop;

        private ItemBackground itemBG = null;
        private Action<ItemBackground> OnClickedItemBG;
        
        private ItemObject itemObj = null;
        private Action<ItemObject> OnClickedItemObj;

        public void ChangeDetailItemBG(TypeShop typeShop, ItemBackground itemBG, Action<ItemBackground> OnClickedItem)
        {
            this.typeShop = typeShop;
            this.itemBG = itemBG;
            this.OnClickedItemBG = OnClickedItem;
            previewItem.sprite = itemBG.itemBackground;
            lockItem.SetActive(itemBG.isLock);
        }

        public void ChangeDetailItemObj(TypeShop typeShop, ItemObject itemObj, Action<ItemObject> OnClickedItem)
        {
            this.typeShop = typeShop;
            this.itemObj = itemObj;
            this.OnClickedItemObj = OnClickedItem;
            previewItem.sprite = itemObj.itemObject;
            lockItem.SetActive(itemObj.isLock);
        }

        public void OnClick()
        {
            if(typeShop == TypeShop.Background) OnClickedItemBG?.Invoke(this.itemBG);
            else OnClickedItemObj?.Invoke(this.itemObj);
            ChoosingItem(true);
        }

        public void ChoosingItem(bool choosing)
        {
            highlight.SetActive(choosing);
        }    
    }
}
