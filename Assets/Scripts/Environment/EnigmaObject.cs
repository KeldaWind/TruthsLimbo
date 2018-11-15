using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaObject : MonoBehaviour {
    [SerializeField] bool isReal;
    public bool IsReal
    {
        get
        {
            return isReal;
        }
    }
    [SerializeField] Collider objectCollider;
    [SerializeField] Renderer objectRenderer;

    private void Start()
    {
        CheckWorld();
    }

    public void MoveToTheOtherWorld()
    {
        isReal = !isReal;
        CheckWorld();
    }

    public void CheckWorld()
    {
        if (objectCollider == null)
            objectCollider = GetComponent<Collider>();

        if (objectCollider != null)
            objectCollider.isTrigger = !isReal;


        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();

        gameObject.layer = isReal ? 9 : 10;
    }
}
