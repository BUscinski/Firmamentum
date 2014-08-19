using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {
	private bool finalPointDone = false;
	public GameObject Alcove;
	private int braneHits;
	private Renderer darkPlane;
	// Use this for initialization
	void Start () {
		darkPlane = gameObject.GetComponentInChildren<Renderer>();
		darkPlane.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(finalPointDone == true)
		{
			GetComponent<BoxCollider>().enabled = false;
			Alcove.SetActive (true);
			DisableBarrier ();
		}
	}
	void OnTriggerEnter(Collider col){
		Debug.Log ("YOU ENTERING?");
		if(col.name == "BarrierEntry")
		{
			EnableBarrier ();
			Alcove.SetActive (false);
		}
		if(col.name == "Brane")
		{
			if(braneHits == 0){
				Debug.Log ("Entering Brane");
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
