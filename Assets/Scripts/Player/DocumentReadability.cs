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

    public void OpenDocument(ReadableDocument document)
    {
        documentImage.gameObject.SetActive(true);
        documentImage.sprite = GameManager.gameManager.LensActivated ? document.truthImage : document.normalImage;
        documentImage.SetNativeSize();
        currentDocument = document;
        opened = true;
    }

    public void CloseDocument()
    {
        documentImage.gameObject.SetActive(false);
        opened = false;
    }

    public void SwitchDocumentImage()
    {
        documentImage.sprite = GameManager.gameManager.LensActivated ? currentDocument.truthImage : currentDocument.normalImage;
    }
}

[System.Serializable]
public struct ReadableDocument
{
    public Sprite normalImage;
    public Sprite truthImage;
}
