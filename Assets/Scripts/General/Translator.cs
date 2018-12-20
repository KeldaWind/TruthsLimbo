using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour {

    public Transform targetPosition;
    public float smoothTime = 1;

    [Header("DEBUG")]
    public bool debugTranslate = false;
    bool translate = false;
    Vector3 refVelocity;

	void Update ()
    {
        if (translate)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref refVelocity, smoothTime);
        }

        if (debugTranslate)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref refVelocity, smoothTime);
        }
	}

    public void StartTranslate()
    {
        translate = true;
    }
}
