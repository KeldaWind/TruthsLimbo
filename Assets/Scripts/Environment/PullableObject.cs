using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableObject : MonoBehaviour, IInteracible {
    public void Interact(Player player)
    {
        player.PlrPullability.TakeObject(transform, player);
    }
}
