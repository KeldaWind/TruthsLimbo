using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerCursorManager
{
    [SerializeField] Image cursorImage;
    [Header("Cursors Sprites")]
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite interactibleSprite;
    [SerializeField] Sprite canUseLampSprite;
    [SerializeField] Sprite canRemoveLampSprite;

    public void UpdateCursorSprite(bool canInteract, bool canLampOn, bool canLampOff)
    {
        if (canLampOff)
            cursorImage.sprite = canRemoveLampSprite;
        else if(canLampOn)
            cursorImage.sprite = canUseLampSprite;
        else if(canInteract)
            cursorImage.sprite = interactibleSprite;
        else
            cursorImage.sprite = normalSprite;

    }
}
