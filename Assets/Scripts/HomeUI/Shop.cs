using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Game
{
    public class Shop : MonoBehaviour
    {
        [Header("BUTTON")]
        [SerializeField] private Sprite[] buttons = new Sprite[2];
        [SerializeField] private Image frameButton;
        [SerializeField] private TextMeshProUGUI txtButton;
        [SerializeField] private Button btnEquiped;

        [Header("PAGE")]
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform[] content = new RectTransform[2];
        [SerializeField] private Image previewItem;
        [SerializeField] private GameObject prefItems;

        [SerializeField] private UnityEvent OnButtonX;
        [SerializeField] private UnityEvent<TypeShop, ItemBackground, ItemObject> OnButtonGet;
        private Action<ItemBackground> OnClickedBGitems;

        private List<ItemBackground> itemsBG;
        private List<ItemObject> itemsObj;
        private List<DetailItem> detailitemsBG = new();
        private List<DetailItem> detailitemsObj = new();
        private TypeShop typePage;
        private ItemBackground itemBG;
        private ItemObject itemObj;

        public void ButtonX() { OnButtonX?.Invoke(); }

        public void ButtonGet()
        {
            OnButtonGet?.Invoke(typePage, itemBG, itemObj);
        }

        public void ChangeStateButton(bool isLock, string textButton)
        {
            if (isLock)
                frameButton.sprite = buttons[1];
            else
                frameButton.sprite = buttons[0];
            btnEquiped.interactable = !(textButton == "Equiped");
            txtButton.text = textButton;
        }

        public void OpenShop(List<ItemBackground> itemsBG, List<ItemObject> itemsObj, Action<ItemBackground> OnClickedBGitems)
        {
            this.itemsBG = itemsBG;
            this.itemsObj = itemsObj;
            this.OnClickedBGitems = OnClickedBGitems;
            LoadItemsBG();
            LoadItemsObject();
            SwitchToggle(0);
        }

        public void SwitchToggle(int typeShop)
        {
            typePage = (TypeShop)typeShop;
            switch (typePage)
            {
                case TypeShop.Background:
                    itemObj = null;
                    scrollRect.content = content[0];
                    content[0].gameObject.SetActive(true);
                    content[1].gameObject.SetActive(false);
                    detailitemsBG[0].OnClick();
                    break;
                case TypeShop.Object:
                    itemBG = null;
                    scrollRect.content = content[1];
                    content[0].gameObject.SetActive(false);
                    content[1].gameObject.SetActive(true);
                    detailitemsObj[0].OnClick();
                    break;
            }
        }
        #region LOAD ITEMS
        private void LoadItemsBG()
        {
            detailitemsBG.Clear();
            for (int i = 0; i < itemsBG.Count; i++)
            {
                DetailItem item = SimplePool.Spawn(prefItems, Vector2.zero, Quaternion.identity).GetComponent<DetailItem>();
                detailitemsBG.Add(item);
                item.transform.SetParent(content[0]);
                item.ChangeDetailItemBG(TypeShop.Background, itemsBG[i], OnClickItemBG);
            }
        }

        private void LoadItemsObject()
        {
            detailitemsObj.Clear();
            for (int i = 0; i < itemsObj.Count; i++)
            {
                DetailItem item = SimplePool.Spawn(prefItems, Vector2.zero, Quaternion.identity).GetComponent<DetailItem>();
                detailitemsObj.Add(item);
                item.transform.SetParent(content[1]);
                item.ChangeDetailItemObj(TypeShop.Object, itemsObj[i], OnClickItemObj);
            }
        }
        #endregion

        public void UnlockItem(TypeShop typeShop, int idUnlock)
        {
            switch(typeShop)
            {
                case TypeShop.Background:
                    detailitemsBG[idUnlock].UnlockItem();
                    break;
                case TypeShop.Object:
                    detailitemsObj[idUnlock].UnlockItem();
                    break;
            }    
        }    
        private void OnClickItemBG(ItemBackground item)
        {
            ClearHighlight();
            previewItem.sprite = item.preview;
            this.itemBG = item;
            OnClickedBGitems?.Invoke(item);
        }
        private void OnClickItemObj(ItemObject item)
        {
            ClearHighlight();
            previewItem.sprite = item.preview;
            this.itemObj = item;
            if (item.isLock)
            {
                frameButton.sprite = buttons[1];
                txtButton.text = item.price.ToString();
            }
            else
            {
                frameButton.sprite = buttons[0];
                if (PlayerPrefs.GetInt("background", 0) != item.id)
                    txtButton.text = "Get";
                else
                    txtButton.text = "Equiped";
            }
        }

        private void ClearHighlight()
        {
            foreach (DetailItem i in (typePage == TypeShop.Background) ? detailitemsBG : detailitemsObj)
            {
                i.ChoosingItem(false);
            }
        }
    }
}
