using System.Collections;
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

    void Start() {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartSpawning() {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerUps());
        StartCoroutine(SpawnAmmo());
    }


    public void StopSpawning() {
        StopCoroutine(SpawnEnemies());
        StopCoroutine(SpawnPowerUps());
        StopCoroutine(SpawnAmmo());
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
            if (_uiManager != null) {
                if (_uiManager.score > 100) {
                    _spawnTime = 500.0f / _uiManager.score;
                }
            }
            else {
                Debug.Log("UI is empty");
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
}
