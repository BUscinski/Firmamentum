using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {
	private bool finalPointDone = false;
	public GameObject Alcove;
	public GameObject Mirror;
	private int braneHits;
	public Renderer darkPlane;
	public GameObject allBranes;
	private Renderer[] Branes;

	// Use this for initialization
	void Start () {
		darkPlane.enabled = false;
		Branes = allBranes.GetComponentsInChildren<Renderer>();
		for(int i = 0; i < Branes.Length; i++)
		{
			Branes[i].renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(finalPointDone == true)
		{
			for(int i = 0; i < Branes.Length; i++)
			{
				Branes[i].renderer.enabled = false;
			}
			GetComponent<BoxCollider>().enabled = false;
			Alcove.SetActive (true);
			Mirror.renderer.enabled = true;
			DisableBarrier ();
		}
	}
	void OnTriggerEnter(Collider col){
		Debug.Log ("YOU ENTERING?");
		if(col.name == "BarrierEntry")
		{
			EnableBarrier ();
			Alcove.SetActive (false);
			Mirror.renderer.enabled = false;
		}
		if(col.name == "Brane")
		{
			if(braneHits == 0){
				Debug.Log ("Entering Brane");
				for(int i = 0; i < Branes.Length; i++)
				{
					Branes[i].renderer.enabled = true;
				}
				DisableBarrier ();
				braneHits++;
			}
			else if (braneHits >= 1)
			{
				Debug.Log ("Its getting here");
				EnableBarrier ();
			}
		}

	}
	public void SetFinalPointBool(bool value)
	{
		finalPointDone = value;
	}
	public void EnableBarrier()
	{
		renderer.enabled = true;
		darkPlane.enabled = true;
	}
	public void DisableBarrier()
	{
		renderer.enabled = false;
		darkPlane.enabled = false;
	}
	
}
