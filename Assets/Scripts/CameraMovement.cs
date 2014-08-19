using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject[] Waypoints;
	public GameObject Orientation;
	public float WaitTime;
	private bool Wait;
	private bool pausing = false;
	public BarrierController theBarrier;
//	private bool[] CompletedWaypoints;
	int iterator = 0;
	public float maxDistanceDelta = 10f;
	public float switchDistance = 10f;
	public float speed = 0.05f;
	public Transform lookTarget;
	public Transform lookTargetTwo;
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
			if(iterator < Waypoints.Length)
			{
				moving = true;
				if(iterator <= 15){
					Orientation.transform.rotation = Quaternion.Lerp (Orientation.transform.rotation, Quaternion.LookRotation (lookTarget.position - Orientation.transform.position), Time.deltaTime * 3);
				}
				transform.position = Vector3.MoveTowards(transform.position, Waypoints[iterator].transform.position, Time.deltaTime * speed);
				if((gameObject.transform.position - Waypoints[iterator].transform.position).magnitude <= switchDistance)
				{
					if(iterator == 2 && Wait == false)
					{
						Debug.Log ("Should be waiting");
						StartCoroutine (PauseForSeconds());
					}
					if(Wait == false)
					{
						iterator++;
					}
						//moving = false;
					//Waypoints[iterator].GetComponentInChildren<ParticleSystem>().enableEmission = true;
		
				}
				else if(iterator == Waypoints.Length - 1)
				{
				//	theBarrier.SetFinalPointBool (true);
					theBarrier.EnableBarrier ();
					Debug.Log ("THIS IS DONE");
			//	iterator = 0;
				}

		}
		else
		{
			theBarrier.SetFinalPointBool (true);
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
	private IEnumerator PauseForSeconds()
	{
		Wait = true;
		yield return new WaitForSeconds(WaitTime);
		iterator++;
		Wait = false;
	}
}
