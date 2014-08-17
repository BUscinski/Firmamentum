using UnityEngine;
using System.Collections;

public class BodyMovement : MonoBehaviour {
	public GameObject kinectPointPrefab;
	public GameObject leftHip;
	public GameObject rightHip;
	private Rigidbody[] BodyParts;
	private Collider[] originalPositions;
	// Use this for initialization
	void Start () {
		//BodyParts = kinectPointPrefab.GetComponentsInChildren<Rigidbody>();
		originalPositions = new Collider[BodyParts.Length];
		for(int i = 0; i < BodyParts.Length; i++)
		{
			originalPositions[i] = BodyParts[i].GetComponentInParent<Collider>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (leftHip.transform.position + rightHip.transform.position) * 0.5f;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			PushBody();
		}
		if(Input.GetKeyUp (KeyCode.UpArrow))
		{
			StopBody ();
		}
		if(Input.GetKey (KeyCode.DownArrow))
		{
			PullBody();
		}
		if(Input.GetKeyUp (KeyCode.DownArrow))
		{
			StopBody ();
		}
	}
	public void PushBody()
	{
		for(int i = 0; i < BodyParts.Length; i++)
		{
			if((BodyParts[i].transform.position - transform.position).magnitude < 2.0f){
				Vector3 pushDirection = (BodyParts[i].transform.position - transform.position).normalized;
				BodyParts[i].AddForce (pushDirection);
			}
			else
			{
			//	Debug.Log ("Setting this shit to 0");
				BodyParts[i].velocity = Vector3.zero;
			}
		}
	}
	public void PullBody()
	{
		for(int i = 0; i < BodyParts.Length; i++)
		{
//			Debug.Log ("BodyPart[i]: " + BodyParts[i].transform.position);
//			Debug.Log ("originalPositions[i]: " + originalPositions[i].transform.position);
		//	Debug.Log (originalPositions[i].transform.position - BodyParts[i].transform.position);
			if((BodyParts[i].transform.position - originalPositions[i].transform.position).magnitude > 0.05f)
			{
			//	Debug.Log ("It should be pulling");
				Vector3 pullDirection = (BodyParts[i].transform.position - originalPositions[i].transform.position).normalized;
				BodyParts[i].AddForce (-pullDirection);
			}
			else
			{
			//	Debug.Log ("Setting this shit to 0");
				BodyParts[i].velocity = Vector3.zero;
			}
		}
	}
	public void StopBody()
	{
		for(int i = 0; i < BodyParts.Length; i++)
		{
			BodyParts[i].velocity = Vector3.zero;
		}
	}
}
