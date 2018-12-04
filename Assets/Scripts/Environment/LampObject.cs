using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHitbox player = other.GetComponent<PlayerHitbox>();
        if (player != null)
        {
            player.relatedPlayer.GainLamp();
            Destroy(gameObject);
        }
    }
}
