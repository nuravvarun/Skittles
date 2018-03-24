using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textureChange : MonoBehaviour {

	//public Material material1;
	//public Material material2;
	public float duration = 1.0F;
	public Renderer rend;
	public Vector3 pos;
	public float smoothness=0.02f;
	public Texture[] textures;
	public float changeInterval = 0.33F;
	Color tempColor;

	void Start() {
		rend = GetComponent<Renderer>();
		//rend.material = material1;
		rend.material.mainTexture = textures[0];
	
	}



	void Update()
	{

		Collider[] hitColliders = Physics.OverlapSphere(pos,0.1f);

		if (hitColliders.Length > 0.1) {
			Debug.Log ("Spawn Occupied");

			rend.material.mainTexture = textures [1];
			//tempColor = rend.material.color;
			//tempColor.a = 124;
			//rend.material.color = tempColor;
		
		} else {
			rend.material.mainTexture = textures [0];
		}


		}
	

	
		
}
