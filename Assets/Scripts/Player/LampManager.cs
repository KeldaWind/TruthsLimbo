using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LampManager {
    [SerializeField] bool ownsLamp;
    public bool HasLamp
    {
        get
        {
            return ownsLamp;
        }
    }

    [SerializeField] Image gameCursor;
    [SerializeField] Sprite normalGameCursorImage;
    [SerializeField] Sprite lampActiveGameCursorImage;
    [SerializeField] Sprite interactibleGameCursorImage;
    EnigmaObject activeEnigmaObject;

    public EnigmaObject CheckForLookedObject(Camera mainCamera)
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0));
        RaycastHit hit = new RaycastHit();
        bool enigmaTouched = false;

        if (Physics.Raycast(ray, out hit, 10))
        {
            EnigmaObject enigmaObject = hit.collider.GetComponent<EnigmaObject>();
            if (enigmaObject != null)
            {
                if (!enigmaObject.IsReal)
                {
                    gameCursor.sprite = interactibleGameCursorImage;
                    enigmaTouched = true;
                    return enigmaObject;
                }
            }
        }
        if (!enigmaTouched)
        {
            if (activeEnigmaObject != null)
                gameCursor.sprite = lampActiveGameCursorImage;
            else
                gameCursor.sprite = normalGameCursorImage;
        }

        return null;
    }

    public void ActiveLamp(EnigmaObject objectToActive)
    {
        if(activeEnigmaObject != null)
            activeEnigmaObject.MoveToTheOtherWorld();

        objectToActive.MoveToTheOtherWorld();
        activeEnigmaObject = objectToActive;

        GameManager.gameManager.SetLampActive(true);
    }

    public void DesactiveLamp()
    {
        if(activeEnigmaObject != null)
            activeEnigmaObject.MoveToTheOtherWorld();
        activeEnigmaObject = null;

        GameManager.gameManager.SetLampActive(false);
    }

    public void GainLamp()
    {
        ownsLamp = true;
    }
}
