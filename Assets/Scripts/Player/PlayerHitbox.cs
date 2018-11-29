using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour {
    public Player relatedPlayer;

    public void Die()
    {
        relatedPlayer.transform.position = Vector3.zero;
    }
}
