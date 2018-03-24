using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCollider : MonoBehaviour {
	
	public void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "bowlingBall")          //Destroy ball on hitti 
		{

			Destroy (GameObject.FindWithTag ("bowlingBall"));


		}
        if(collision.gameObject.tag == "Pin")
        {
            Destroy(GameObject.FindWithTag("Pin"));
        }
	}
}
