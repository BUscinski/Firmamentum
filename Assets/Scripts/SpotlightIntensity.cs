using UnityEngine;
using System.Collections;

public class SpotlightIntensity : MonoBehaviour {
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
			if(light.intensity <= 0.5f && Mathf.Abs (newValue) > ((max + min)/2))
			{
//				Debug.Log (light.intensity);
				light.intensity = Mathf.Lerp (light.intensity, 1.0f, Time.deltaTime *0.4f);
			}
			else if(light.intensity >=0.0f && Mathf.Abs(newValue) <= ((max + min)/2))
			{
				light.intensity = Mathf.Lerp (light.intensity, 0.0f, Time.deltaTime *0.4f);
			}
		}
	}
}	