using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private int waveNumber = 5;
    
    
    private GameManager _gameManager;
    private UIManager _uiManager;
    private float _enemySpawnTime = 2.0f;
    private Player _player;
    private bool _isUiManagerNotNull;
    public Queue<GameObject> currentWave = new Queue<GameObject>();
    private bool waveSpawned;
    [SerializeField] private int waveMultiplier;
    [SerializeField] private int initialEnemies;

    void Start() {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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

    private void Update() {
        //Spawn wave 1
        //Wait until all enemies have been defeated
        //Spawn wave 2

        if (waveNumber == 0) {
            _uiManager.GameOver();
        } else if (waveNumber > 0 && !waveSpawned) {
            SpawnWave(waveNumber);
            waveSpawned = true;
        }

        if (currentWave.Count == 0 & !_uiManager.gameOver) {
            waveSpawned = false;
            waveNumber--;
        }
    }

    public void StartSpawning() {
        StartCoroutine(SpawnPowerUps());
        StartCoroutine(SpawnAmmo());
        StartCoroutine(SpawnLife());
        StartCoroutine(SpawnMissile());
    }


    public void StopSpawning() {
        // StopCoroutine(SpawnEnemies());
        // StopCoroutine(SpawnPowerUps());
        // StopCoroutine(SpawnAmmo());
        // StopCoroutine(SpawnLife());
        // StopCoroutine(SpawnMissile());
        StopAllCoroutines();
    }

    private void SpawnWave(int wave) {
        int numEnemies = initialEnemies + (wave * waveMultiplier);
        StartCoroutine(SpawnEnemies(numEnemies));
    }

    private IEnumerator SpawnEnemies(int numEnemies) {
        for (int i = 0; i < numEnemies; i++) {
            GameObject enemy = Instantiate(enemyShipPrefab,enemyContainer.transform);
            currentWave.Enqueue(enemy);
            yield return new WaitForSeconds(_enemySpawnTime);
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
