using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampObject : MonoBehaviour, IInteracible
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

    public void Interact(Player player)
    {
        player.GainLamp();
        Destroy(gameObject);
    }
}
