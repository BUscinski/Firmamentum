using UnityEngine;
using System.Collections;

public class MoveTentacles : MonoBehaviour {
	private float multiplier;
	// Use this for initialization
	void Start () {
		multiplier = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.UpArrow))
		{
			rigidbody.AddForce (Vector3.up * multiplier);
		}
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			rigidbody.AddForce(Vector3.left * multiplier);
		}
		if(Input.GetKey (KeyCode.DownArrow))
		{
			rigidbody.AddForce (Vector3.down * multiplier);
		}
		if(Input.GetKey (KeyCode.RightArrow))
		{
			rigidbody.AddForce(Vector3.right * multiplier);
		}
	}
}
