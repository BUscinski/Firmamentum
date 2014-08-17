using UnityEngine;
using System.Collections;

public class CreateParticles : MonoBehaviour {
	private ParticleSystem[] playersParticles;
	// Use this for initialization
	void Start () {
		playersParticles = gameObject.GetComponentsInChildren<ParticleSystem>();
		for(int i = 0; i < playersParticles.Length; i++)
		{
			playersParticles[i].startColor = Color.yellow;
			playersParticles[i].startSpeed = 0.3f;
			playersParticles[i].startSize = 12f;
			playersParticles[i].startLifetime = 0.2f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
