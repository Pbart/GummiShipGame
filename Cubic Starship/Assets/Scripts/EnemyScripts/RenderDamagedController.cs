using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//This class flashes the damaged material when the enemy is hit.
public class RenderDamagedController : MonoBehaviour 
{
	public Material damagedMaterial;
	public Renderer[] renderersToChange;
	private List<Material> originalMaterials;
	
	// Use this for initialization
	void Start () 
	{
		originalMaterials = new List<Material>();

		if(renderersToChange.Length > 0)
		{
			for(int i = 0; i < renderersToChange.Length; i++)
			{
				originalMaterials.Add(renderersToChange[i].material);
			}
		}
	}

	//switches out the current material of all the renderers to the damaged materials
	public void ShowDamagedMaterial()
	{
		for(int i = 0; i < renderersToChange.Length; i++)
		{
			renderersToChange[i].material = damagedMaterial;
		}
	}

	//switches the current material of all the renderers back to their original material
	public void ShowOriginalMaterial()
	{
		for(int i = 0; i < renderersToChange.Length; i++)
		{
			renderersToChange[i].material = originalMaterials[i];
		}
	}
}
