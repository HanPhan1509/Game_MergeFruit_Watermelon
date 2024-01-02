using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnButtonX;

        public void ButtonX() { OnButtonX?.Invoke(); }
    }
}
