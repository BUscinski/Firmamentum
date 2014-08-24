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
	public GameObject finalEntry;
	public DisplayColor kinectPointCloud;
	public GameObject thePointCloud;


	// Use this for initialization
	void Start () {
		darkPlane.enabled = false;
		Branes = allBranes.GetComponentsInChildren<Renderer>();
		finalEntry.SetActive (false);
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
			thePointCloud.SetActive (true);
			finalPointDone = false;
		}
	}
	void OnTriggerEnter(Collider col){
		Debug.Log ("YOU ENTERING?");
		if(col.name == "BarrierEntry")
		{
			EnableBarrier ();
			Alcove.SetActive (false);
			Mirror.renderer.enabled = false;
			kinectPointCloud.DisableAllRenderers();
			StartCoroutine (ExpandBody());
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
				finalEntry.SetActive (true);
				braneHits++;
			}

		}
		if(col.name == finalEntry.name)
		{
			Debug.Log ("ENTERING FINAL ENTRY POINT");
			for(int i = 0; i < Branes.Length; i++)
			{
				Branes[i].renderer.enabled = false;
			}
			EnableBarrier ();

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
	private IEnumerator ExpandBody()
	{
		float timeToExpand = 0.0f;
		while(timeToExpand < 5.0f)
		{
			yield return null;
			timeToExpand += Time.deltaTime;
			kinectPointCloud.PushBody ();
		}
		thePointCloud.SetActive (false);

	}
	
}
