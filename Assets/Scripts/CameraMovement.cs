using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public Transform[] Waypoints;
	public Transform[] initialWaypoints; // 5-12
	public Transform[] waypointsAfterWaiting; // 12 - 21
	public GameObject Orientation;
	public float WaitTime;
	private bool Wait;
	private bool pausing = false;
	public BarrierController theBarrier;
//	private bool[] CompletedWaypoints;
	public int iterator = 0;
	public float maxDistanceDelta;
	public float switchDistance;
	public float speed;
	public Transform lookTarget;
	private bool moving;
	private bool tweening;

	// Use this for initialization
	void Start () 
	{
		moving = false;
		tweening = false;
		//		CompletedWaypoints = new bool[Waypoints.Length];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) || moving == true){
			if(iterator < Waypoints.Length)
			{
				Debug.Log ("Iterator " + iterator);
				moving = true;
				if(iterator <= 15){
					Orientation.transform.rotation = Quaternion.Lerp (Orientation.transform.rotation, Quaternion.LookRotation (lookTarget.position - Orientation.transform.position), Time.deltaTime * 3);
				}
				if(iterator == 20)
				{
					Orientation.transform.rotation = Quaternion.Lerp (Orientation.transform.rotation, Quaternion.LookRotation (lookTarget.position - Orientation.transform.position), Time.deltaTime * 3);
				}
				if(iterator == 5 && tweening == false)// && iterator != 12)
				{
					//transform.position = Vector3.MoveTowards(transform.position, Waypoints[iterator].transform.position, Time.deltaTime * speed);
					//iTween.MoveTo (gameObject, iTween.Hash ("position", Waypoints[iterator].transform, "time", 5.0f, "easetype", iTween.EaseType.easeInOutQuart)); 
					tweening = true;
					iTween.MoveTo (gameObject, iTween.Hash ("path", initialWaypoints, "time", 60f, "easeType", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.none, "delay", 5.0f));
				}
				if(iterator == 6)
				{
					tweening = false;
				}
				if(iterator == 12 && tweening == false)
				{
					tweening = true;
					iTween.MoveTo (gameObject, iTween.Hash ("path", waypointsAfterWaiting, "time", 60f, "easeType", iTween.EaseType.easeInOutSine, "loopType", iTween.LoopType.none, "delay", 5.0f));
				}
				else if (iterator < 5)
				{
					transform.position = Vector3.Lerp (transform.position, Waypoints[iterator].transform.position, Time.deltaTime * 0.4f);
				}
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
