﻿using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject[] powerUps;

    private GameManager _gameManager;
    [SerializeField] private UIManager _uiManager;
    private float _spawnTime = 5.0f;
    private Player _player;
    private bool _isUiManagerNotNull;

    void Start() {
        if (!_uiManager) {
            Debug.LogError("SpawnManager needs a UIManager reference"); 
        }
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (!_gameManager) {
            Debug.LogError("SpawnManager could not find the GameManager");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (!_player) {
            Debug.LogError("SpawnManager could not find the Player");
        }
    }

    public void StartSpawning() {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerUps());
        StartCoroutine(SpawnAmmo());
        StartCoroutine(SpawnLife());
        StartCoroutine(SpawnMissile());
    }


    public void StopSpawning() {
        StopCoroutine(SpawnEnemies());
        StopCoroutine(SpawnPowerUps());
        StopCoroutine(SpawnAmmo());
        StopCoroutine(SpawnLife());
        StopCoroutine(SpawnMissile());
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
            if (_uiManager.score > 100) {
                _spawnTime = 500.0f / _uiManager.score;
            }
            yield return new WaitForSeconds(_spawnTime);
            Instantiate(enemyShipPrefab,enemyContainer.transform);
        }
    }
	
    private IEnumerator SpawnPowerUps() {
        while (true) {
            yield return new WaitForSeconds(5.0f);
            Instantiate(powerUps[Random.Range(0,3)],transform.position,Quaternion.identity);
        }
    }
    
    private IEnumerator SpawnAmmo() {
        while (true) {
            yield return new WaitForSeconds(3.0f);
            Instantiate(powerUps[3],transform.position,Quaternion.identity);
        }
    }
    
    private IEnumerator SpawnLife() {
        while (true) {
            yield return new WaitForSeconds(15.0f);
            Instantiate(powerUps[4],transform.position,Quaternion.identity);
        }
    }
    
    private IEnumerator SpawnMissile() {
        while (true) {
            yield return new WaitForSeconds(30.0f);
            Instantiate(powerUps[5],transform.position,Quaternion.identity);
        }
    }
}
