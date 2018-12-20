using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DocumentReadability {
    [HideInInspector]
    public bool opened;
    [SerializeField] Image documentImage;

	public void OpenDocument(Sprite image)
    {
        documentImage.gameObject.SetActive(true);
        documentImage.sprite = image;
        documentImage.SetNativeSize();
        opened = true;
    }

    public void CloseDocument()
    {
        documentImage.gameObject.SetActive(false);
        opened = false;
    }
}
