using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LensManager {

    [SerializeField] bool ownsLens;
    public bool HasLens
    {
        get
        {
            return ownsLens;
        }
    }

    public void Equip(bool equip)
    {
        GameManager.gameManager.NormalCamera.gameObject.SetActive(!equip);
        GameManager.gameManager.LensCamera.gameObject.SetActive(equip);
        GameManager.gameManager.SetLensActive(equip);
    }

    public bool Equiped
    {
        get
        {
            return GameManager.gameManager.LensCamera.gameObject.activeInHierarchy;
        }
    }	

    public void GainLens()
    {
        ownsLens = true;
    }
}
