using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableObject : MonoBehaviour, IInteracible
{
    [SerializeField] Sprite relatedImage;
    public void Interact(Player player)
    {
        player.PlayerDocumentReadability.OpenDocument(relatedImage);
    }
}
