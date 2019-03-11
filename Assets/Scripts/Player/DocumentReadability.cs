using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DocumentReadability {
    [HideInInspector]
    public bool opened;
    [SerializeField] Image documentImage;
    ReadableDocument currentDocument;

    bool currentDocEvolved;

    public void OpenDocument(ReadableDocument document, bool evolved)
    {
        documentImage.gameObject.SetActive(true);
        documentImage.sprite = GameManager.gameManager.LensActivated ? document.truthImage : document.normalImage;
        documentImage.SetNativeSize();
        currentDocument = document;
        opened = true;

        currentDocEvolved = evolved;
    }

    public void CloseDocument()
    {
        documentImage.gameObject.SetActive(false);
        opened = false;
    }

    public void SwitchDocumentImage()
    {
        documentImage.sprite = GameManager.gameManager.LensActivated ? (currentDocEvolved ? currentDocument.evolvedImage : currentDocument.truthImage) : currentDocument.normalImage;
    }
}

[System.Serializable]
public struct ReadableDocument
{
    public Sprite normalImage;
    public Sprite truthImage;
    public Sprite evolvedImage;
}
