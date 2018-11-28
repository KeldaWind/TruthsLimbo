using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeCorridorBlock : MonoBehaviour {
    [SerializeField] bool clockward;
    [SerializeField] float rotatingSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * (clockward ? 1 : -1) * rotatingSpeed, 0, 0));
    }
}
