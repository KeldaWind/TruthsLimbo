using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensObject : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        PlayerHitbox player = other.GetComponent<PlayerHitbox>();
        if(player != null)
        {
            player.relatedPlayer.GainLens();
            Destroy(gameObject);
        }
    }
}
