using UnityEngine;
using System.Collections;

public class AttachTentacles : MonoBehaviour {
	HingeJoint[] AllTentacles;
	// Use this for initialization
	void Start () {
		AllTentacles = GetComponentsInChildren<HingeJoint>();
		for(int i = 0; i < AllTentacles.Length; i++)
		{
			if(AllTentacles[i].tag == "TentacleBase")
			{
				FixedJoint newJoint = gameObject.AddComponent<FixedJoint>();
				newJoint.connectedBody = AllTentacles[i].GetComponent<Rigidbody>();
			}
		}
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
