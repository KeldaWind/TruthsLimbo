using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LensManager {

    public void Equip(bool equip)
    {
        GameManager.gameManager.NormalCamera.gameObject.SetActive(!equip);
        GameManager.gameManager.LensCamera.gameObject.SetActive(equip);
    }

    public bool Equiped
    {
        get
        {
            return GameManager.gameManager.LensCamera.gameObject.activeInHierarchy;
        }
    }	
}
