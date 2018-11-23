using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    [SerializeField] Camera normalCamera;
    public Player player;

    public Camera NormalCamera
    {
        get
        {
            return normalCamera;
        }
    }
    [SerializeField] Camera lensCamera;
    public Camera LensCamera
    {
        get
        {
            return lensCamera;
        }
    }

    private void Awake()
    {
        gameManager = this;
    }

    void Start () {
		
	}
	
	void Update ()
    {
		
	}
}
