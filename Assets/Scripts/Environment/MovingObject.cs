using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    [SerializeField] bool moved;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] float movingSpeed;

	void Start () {
		
	}
	
	void Update () {
        if (moved && transform.position != endPosition)
            transform.position = Vector3.Lerp(transform.position, endPosition, movingSpeed);

        if (!moved && transform.position != startPosition)
            transform.position = Vector3.Lerp(transform.position, startPosition, movingSpeed);
	}

    public void MoveObject(bool needLens)
    {
        if (needLens && !GameManager.gameManager.player.HasLens)
            return;

        moved = true;
    }

    public void UnmoveObject(bool needLens)
    {
        if (needLens && !GameManager.gameManager.player.HasLens)
            return;

        moved = false;

    }
}
