using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointsManager  {

    Checkpoint currentCheckpoint;

	public void SetUp()
    {

    }

    public void ChangeCheckpoint(Checkpoint newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    public void Respawn(Player playerToRespawn)
    {
        playerToRespawn.transform.position = currentCheckpoint.GetRespawnPosition();
    }
}
