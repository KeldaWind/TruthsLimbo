using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealAfterLamp : MonoBehaviour {

    public static RevealAfterLamp staticReveal;
    public GameObject grandPont;
    public GameObject petitPont;
    public GameObject salleTransfomée;
    public GameObject player;

    private void Start()
    {
        staticReveal = this;
    }

    private void FixedUpdate()
    {
        if(player != null)
        if(player.GetComponent<Player>().HasLamp == true)
        {
            Debug.Log("Choppe la lampe");
            grandPont.SetActive(false);
                petitPont.SetActive(false);
            salleTransfomée.SetActive(true);
        }
    }


}
