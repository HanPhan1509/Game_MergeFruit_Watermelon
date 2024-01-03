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

        private bool isLock = false;
        private ItemBackground item;
        private Action<ItemBackground> OnClickedItem;

        public void ChangeDetailItem(ItemBackground item, Action<ItemBackground> OnClickedItem)
        {
            this.item = item;
            this.OnClickedItem = OnClickedItem;
            previewItem.sprite = item.itemBackground;
            lockItem.SetActive(item.isLock);
        }

        public void OnClick()
        {
            OnClickedItem?.Invoke(this.item);
        }    
    }
}
