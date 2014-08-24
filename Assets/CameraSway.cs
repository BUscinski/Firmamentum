using UnityEngine;
using System.Collections;

public class CameraSway : MonoBehaviour {
	public CameraMovement theCameraMovement;
	public Transform theSwayLocation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(theCameraMovement.iterator > 4)
		{
			Debug.Log ("WHAT THE FUUUUCK");
			iTween.MoveBy(gameObject, iTween.Hash("x", 5, "time", 3f, "easeType", iTween.EaseType.easeInOutSine, "loopType", "pingPong", "delay", .1, "islocal", true));
		}
	}
}