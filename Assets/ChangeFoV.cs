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
	private bool breathSensorInit = false;
	// Use this for initialization
	void Start () {
		min = 10000;
		max = 1;
		newValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		newBreathValue = breathCalc.GetMovementValue ();
		if(newBreathValue != 0 || breathSensorInit == true)
		{
			Debug.Log ("Its getting through here");
			breathSensorInit = true;
			newValue = newBreathValue;
			if(newValue > max)
			{
				max = newValue;
				Debug.Log ("NewMaxValue");
			}
			if(newValue < min && newValue != 0)
			{
				min = newValue;
				Debug.Log ("newMinValue");
			}
		//	Debug.Log (newValue);
			//Debug.Log (breathCalc.GetMovementValue ());
			if(theController.VerticalFOV <= 180 && Mathf.Abs (newValue) > ((max + min)/2))
			{
				theController.VerticalFOV = Mathf.Lerp(theController.VerticalFOV, 150f, Time.deltaTime * 0.4f);
			}
			else if(theController.VerticalFOV >=70 && Mathf.Abs(newValue) <= ((max + min)/2))
			{
				theController.VerticalFOV = Mathf.Lerp(theController.VerticalFOV, 70f, Time.deltaTime * 0.4f);
			}
		}
	}
}	