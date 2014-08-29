using UnityEngine;
using System.Collections;

public class BarrierBreathing : MonoBehaviour {
	public Breathing breathCalc;
	private float min;
	private float max;
	private float newValue;
	private float newBreathValue;
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
			breathSensorInit = true;
			newValue = newBreathValue;
			if(newValue > max)
			{
				max = newValue;
			}
			if(newValue < min && newValue != 0)
			{
				min = newValue;
			}
			//	Debug.Log (newValue);
			//Debug.Log (breathCalc.GetMovementValue ());
			if(renderer.material.GetFloat ("_HiPower") <= 10.0f && Mathf.Abs (newValue) > ((max + min)/2))
			{
				renderer.material.SetFloat ("_HiPower", Mathf.Lerp (renderer.material.GetFloat ("_HiPower"), 10.0f, Time.deltaTime * 0.4f));
			}
			else if(renderer.material.GetFloat ("_HiPower") >=1.5f && Mathf.Abs(newValue) <= ((max + min)/2))
			{
				renderer.material.SetFloat ("_HiPower", Mathf.Lerp (renderer.material.GetFloat ("_HiPower"), 1.5f, Time.deltaTime * 0.4f));
			}
		}
	}
}	