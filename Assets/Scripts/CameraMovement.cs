using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject[] Waypoints;
	public GameObject Orientation;
	public BarrierController theBarrier;
//	private bool[] CompletedWaypoints;
	int iterator = 0;
	public float maxDistanceDelta = 10f;
	public float switchDistance = 10f;
	public float speed = 0.05f;
	public Transform lookTarget;
	private bool moving;
	// Use this for initialization
	void Start () 
	{
		moving = false;
		//		CompletedWaypoints = new bool[Waypoints.Length];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) || moving == true){
			moving = true;
			Orientation.transform.rotation = Quaternion.Slerp (Orientation.transform.rotation, Quaternion.LookRotation (lookTarget.position - Orientation.transform.position), Time.deltaTime);
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, Waypoints[iterator].transform.position, Time.deltaTime * speed);
			if((gameObject.transform.position - Waypoints[iterator].transform.position).magnitude <= switchDistance)
			{
				iterator++;
				//moving = false;
				//Waypoints[iterator].GetComponentInChildren<ParticleSystem>().enableEmission = true;
	
			}
			else if(iterator >= Waypoints.Length - 1)
			{
				theBarrier.SetFinalPointBool (true);
				Debug.Log ("THIS IS DONE");
			//	iterator = 0;

			}


		//		if(Input.GetKey (KeyCode.Space)){
//			transform.Rotate(0, 1, 0);
//		}
//		if(Input.GetKey (KeyCode.DownArrow))
//		{	
//			mainCamera.transform.Translate (0, 0, -1, Space.Self);
//		}
//		if(Input.GetKey (KeyCode.UpArrow))
//		{
//			mainCamera.transform.Translate(0, 0, 1, Space.Self);
//		}
		}
	}
}
