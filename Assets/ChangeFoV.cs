using UnityEngine;
using System.Collections;

public class ChangeFoV : MonoBehaviour {
	public OVRCameraController theController;
	public Breathing breathCalc;
	private float min;
	private float max;
	private float newValue;
	private float newBreathValue;
	private bool currentlyLerping = false;
	// Use this for initialization
	void Start () {
		min = 10000;
		max = 1;
		newValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		newBreathValue = breathCalc.GetMovementValue ();
		if(newValue >= newBreathValue || newValue <= newBreathValue - 10)
		{
			Debug.Log ("IS IT BEING RESET EVERY FRAME????");
			newValue = newBreathValue;
		}
		if(newValue > max)
		{
			max = newValue;
		}
		if(newValue < min && newValue != 0)
		{
			min = newValue;
		}
		Debug.Log (newValue);
		//Debug.Log (breathCalc.GetMovementValue ());
		if(theController.VerticalFOV <= 180 && newValue > 0)
		{
			theController.VerticalFOV = Mathf.Lerp(theController.VerticalFOV, 180f, Time.deltaTime * 0.4f);
		}
		else if(theController.VerticalFOV >=45 && newValue < 0)
		{
			theController.VerticalFOV = Mathf.Lerp(theController.VerticalFOV, 70f, Time.deltaTime * 0.4f);
		}
	}
}
