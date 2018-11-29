using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeathZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        PlayerHitbox playerHitbox = other.GetComponent<PlayerHitbox>();
        if(playerHitbox != null)
        {
            playerHitbox.Die();
        }
    }
}
