using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoleManager : MonoBehaviour {

	List<MoleController> moles = new List<MoleController>();
	bool generate;
	public AnimationCurve maxMoles;

	public TextMeshProUGUI Debug;

	// Use this for initialization
	void Start () 
	{
		
	}

	public void GetMoles()
    {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Mole");
		Debug.text = gos.Length.ToString();

		foreach (GameObject go in gos)
		{
			moles.Add(go.GetComponent<MoleController>());
		}

		this.generate = false;
	}

	public void StartGenerate()
	{
		StartCoroutine ("Generate");
	}

	public void StopGenerate()
	{
		this.generate = false;
	}
		
	IEnumerator Generate()
	{
		this.generate = true;

		while (this.generate) 
		{
			// wait to generate next group
			yield return new WaitForSeconds (1.0f);

			int n = moles.Count;
			int maxNum = (int)this.maxMoles.Evaluate ( GameManager.time );

			for (int i = 0; i < maxNum; i++) 
			{
				// select mole to up
				this.moles [Random.Range (0, n)].Up ();
								
				yield return new WaitForSeconds (0.3f);
			}
		}
	}
}
