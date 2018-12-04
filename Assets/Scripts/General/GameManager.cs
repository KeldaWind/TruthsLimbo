using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    [SerializeField] Camera normalCamera;
    public Player player;
    public LensManager lensManager;
    public CheckpointsManager checkpointsManager;

    [Header("Player Infos")]
    bool lensActivated;
    public bool LensActivated
    {
        get
        {
            return lensActivated;
        }
    }

    bool lampActivated;
    public bool LampActivated
    {
        get
        {
            return lampActivated;
        }
    }

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
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("espace");
	}

    public void SetLensActive(bool active)
    {
        lensActivated = active;
    }

    public void SetLampActive(bool active)
    {
        lampActivated = active;
    }
}
