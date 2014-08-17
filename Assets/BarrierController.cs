using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {
	private Color originalColorFullAlpha;
	private Color originalColorNoAlpha;
	private bool finalPointDone = false;
	public GameObject Alcove;
	private int braneHits;
	// Use this for initialization
	void Start () {
		originalColorNoAlpha = renderer.material.color;
		originalColorFullAlpha = new Color(originalColorNoAlpha.r, originalColorNoAlpha.g, originalColorNoAlpha.b, 255);
	}
	
	// Update is called once per frame
	void Update () {
		if(finalPointDone == true)
		{
			renderer.material.color = originalColorNoAlpha;
			GetComponent<BoxCollider>().enabled = false;
			Alcove.SetActive (true);
		}
	}
	void OnTriggerEnter(Collider col){
		Debug.Log ("YOU ENTERING?");
		if(col.name == "BarrierEntry")
		{
			renderer.material.color = originalColorFullAlpha;
			Alcove.SetActive (false);
		}
		if(col.name == "Brane")
		{
			if(braneHits == 0){
				renderer.material.color = originalColorNoAlpha;
				braneHits++;
			}
			else if (braneHits == 1)
			{
				renderer.material.color = originalColorFullAlpha;
			}
		}

	}
	public void SetFinalPointBool(bool value)
	{
		finalPointDone = value;
	}
	
}
