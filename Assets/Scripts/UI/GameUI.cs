using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image imgNextFruit;
    
    public void ShowNextFruit(Sprite sprite)
    {
        imgNextFruit.sprite = sprite;
    }    
}
