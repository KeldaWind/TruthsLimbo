using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet d'afficher ou non l'ennemi à l'écran
/// </summary>
public class EnemyVisibility : MonoBehaviour {

    public Renderer[] renderers;
    private List<Color> initialColors =  new List<Color>();
    
	void Start ()
    {
        //Get all colors
        for (int i = 0; i < renderers.Length; i++)
        {
            initialColors.Add(renderers[i].material.color);
        }	
	}

    public void SetVisible()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = initialColors[i];
        }
    }

    public void SetInvisible()
    {

        for (int i = 0; i < renderers.Length; i++)
        {
            Color c = new Color(initialColors[i].r, initialColors[i].g, initialColors[i].b, 0);
            renderers[i].material.color = c;
        }
    }


}
