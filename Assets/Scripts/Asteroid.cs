using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
	
	[SerializeField] private float rotationSpeed = 3.0f;
	[SerializeField] private GameObject asteroidDestroyed;
	[SerializeField] private AudioClip explosionSound;
	private SpawnManager _spawnManager;

	private void Start() {
		_spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
	}

	private void Update() {
		transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Instantiate(asteroidDestroyed, transform.position, Quaternion.identity);
		_spawnManager.StartSpawning();
		Destroy(other.gameObject);
		AudioSource.PlayClipAtPoint(explosionSound,Camera.main.transform.position);
		Destroy(gameObject,0.5f);
	}
}
