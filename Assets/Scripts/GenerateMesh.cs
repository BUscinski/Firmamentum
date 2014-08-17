using UnityEngine;
using System.Collections;

public class GenerateMesh : MonoBehaviour {
	private Mesh newMesh;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		newMesh.MarkDynamic();
	}
}
