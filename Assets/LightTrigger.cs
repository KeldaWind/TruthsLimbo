using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        if (GetComponent<Animator>() != null)
            anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (anim != null)
            {
                anim.SetTrigger("LightOn");

            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            Debug.Log(other.gameObject.name + " Entered Light Trigger " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (anim != null)
            {
                anim.SetTrigger("LightOff");
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            Debug.Log(other.gameObject.name + " Exited Light Trigger " + gameObject.name);
        }
    }
}
