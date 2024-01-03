using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Transform contentBG;
        [SerializeField] private GameObject prefItems;

        [SerializeField] private UnityEvent OnButtonX;

        private List<ItemBackground> itemsBG;

        public void ButtonX() { OnButtonX?.Invoke(); }

        public void OpenShop(List<ItemBackground> itemsBG)
        {
            this.itemsBG = itemsBG;
            LoadItemsBG();
        }    

        private void LoadItemsBG()
        {
            for(int i = 0; i < itemsBG.Count; i++)
            {
                GameObject item = SimplePool.Spawn(prefItems, Vector2.zero, Quaternion.identity);
                item.transform.SetParent(contentBG);
                item.GetComponent<DetailItem>().ChangeDetailItem(itemsBG[i]);
            }
        }    
    }
}
