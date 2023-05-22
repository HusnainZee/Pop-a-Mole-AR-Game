using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerController : MonoBehaviour {

	public GameObject particle;
	public AudioClip hitSE;

	AudioSource audio;

	void Start () {
		this.audio = GetComponent<AudioSource> ();	
	}

	IEnumerator Hit(Vector3 target)
	{
		// Hummer Down		
		transform.position = new Vector3(target.x, 0, target.z);

		// Impact
		//Instantiate (this.particle, new Vector3 (target.x, target.y, target.z + 5), Quaternion.identity);

		//Camera.main.GetComponent<CameraController>().Shake();

		this.audio.PlayOneShot (this.hitSE);

		yield return new WaitForSeconds (0.1f);

		// Hummer Up
		for (int i = 0; i < 6; i++) 
		{
			transform.Translate (0, 0, 1.0f);
			yield return null;
		}
	}

	void Update () 
	{
		if(Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			Vector3 inputPosition;

			if (Input.touchCount > 0)
				inputPosition = Input.GetTouch(0).position;
			else
				inputPosition = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay (inputPosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) 
			{
				GameObject mole = hit.collider.gameObject;
					
				bool isHit = mole.GetComponent<MoleController> ().Hit ();

				// if hit the mole, show hummer and effect
				if (isHit) 
				{
					StartCoroutine (Hit (mole.transform.position));
					Instantiate(this.particle, mole.transform.position, Quaternion.identity);

					ScoreManager.score += 5;
				}
			}
		}
	}
}
