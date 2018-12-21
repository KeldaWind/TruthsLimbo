using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableObject : MonoBehaviour, IInteracible
{
    [SerializeField] ReadableDocument document;

    public void Interact(Player player)
    {
        player.PlayerDocumentReadability.OpenDocument(document);
    }
}
