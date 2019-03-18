using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("LightOn");
        Debug.Log("Entered Light Trigger " + gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetTrigger("LightOff");
        Debug.Log("Exited Light Trigger " + gameObject.name);
    }
}
