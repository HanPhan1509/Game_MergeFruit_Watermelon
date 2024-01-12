using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServices : MonoBehaviour
{
    private const string indexBG = "ibg";
    public void SaveBackground(int idBG)
    {
        PlayerPrefs.SetInt(indexBG, idBG);
    }

    public int GetBackground()
    {
        return PlayerPrefs.GetInt(indexBG, 0);
    }    
}
