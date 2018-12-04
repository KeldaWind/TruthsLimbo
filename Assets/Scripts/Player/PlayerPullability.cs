using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPullability {
    Transform currenltyPulledObject;
    public void CheckForTakeOrRelease(InputManager inputManager, Player player, Camera mainCamera)
    {
        if (inputManager.GetInteractDown)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0));
            RaycastHit hit = new RaycastHit();

            /*if (Physics.Raycast(ray, out hit, 3))
            {
                PullableObject pullableObject = hit.collider.GetComponent<PullableObject>();
                if (pullableObject != null)
                {
                    TakeObject(pullableObject.transform, player);
                }
            }*/
        }
        if (inputManager.GetInteractUp)
            ReleaseObject(player);
    }

    public void TakeObject(Transform objectToTake, Player player)
    {
        objectToTake.parent = player.transform;
        currenltyPulledObject = objectToTake;
    }
    public void ReleaseObject(Player player)
    {
        if(currenltyPulledObject != null)
            currenltyPulledObject.parent = null;
    }
}
