using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHitbox : MonoBehaviour, IPlayer
{
    public Player relatedPlayer;
    public static PlayerHitbox instance;
    public event Action OnDeath = delegate { };

    void Awake()
    {
        instance = this;
    }

    public void Die()
    {
        GameManager.gameManager.checkpointsManager.Respawn(relatedPlayer);
        OnDeath();
    }

    public void HitPlayer()
    {
        Die();
    }
}
