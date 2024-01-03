using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        public void ChangeDetailItem(ItemBackground item)
        {
            this.item = item;
            previewItem.sprite = item.itemBackground;
        }    
    }
}
