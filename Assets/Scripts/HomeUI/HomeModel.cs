using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class ItemObject
    {
        public TypeObject typeObject;
        public Sprite itemObject;
        public Sprite preview;
        public List<Sprite> allObjects = new List<Sprite>();
    }

    [System.Serializable]
    public class ItemBackground
    {
        public int id;
        public TypeBackground typeBG;
        public Sprite itemBackground;
        public Sprite preview;
        public Sprite background;
        public Sprite detailBG;
        public Sprite ground;
        public bool isLock = true;
    }
    public class HomeModel : MonoBehaviour
    {
        [SerializeField] private List<ItemBackground> itemsBG = new();
        [SerializeField] private List<ItemObject> itemsObject = new();

        public List<ItemBackground> ItemsBG { get => itemsBG; /*set => itemsBG = value;*/ }
    }
}
