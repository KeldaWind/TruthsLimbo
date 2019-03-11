using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableObject : MonoBehaviour, IInteracible
{
    [SerializeField] ReadableDocument document;
    [SerializeField] SpriteRenderer renderer;
    bool evolved;

    public void Interact(Player player)
    {
        player.PlayerDocumentReadability.OpenDocument(document, evolved);
    }

    public void Update()
    {
        if (GameManager.gameManager.player.IsUsingLens)
        {
            if (!evolved)
                renderer.sprite = document.truthImage;
            else
                renderer.sprite = document.evolvedImage;
        }
        else
            renderer.sprite = document.normalImage;
    }

    public void EvolveDoc(int neededProgression)
    {
        if (neededProgression == 1 && !GameManager.gameManager.player.HasLens)
            return;

        if (neededProgression == 2 && !GameManager.gameManager.player.HasLamp)
            return;

        evolved = true;
    }
}
