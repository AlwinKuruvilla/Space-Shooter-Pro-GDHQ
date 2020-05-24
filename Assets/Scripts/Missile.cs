using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class Missile : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private Transform _targetEnemy;
    private GameObject[] _firstEnemy;
    
    [SerializeField] private float rotateSpeed = 250f;
    [SerializeField] private GameObject missileDestroyed;
    [SerializeField] private float missileSpeed = 10.0f;

    private void Start() {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        SearchEnemy();

        StartCoroutine(AutoDestruct());
    }

    private void SearchEnemy() {
        _firstEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (_firstEnemy.Length != 0) {
            _targetEnemy = _firstEnemy[0].transform;
        }
        else {
            Debug.Log("No enemy to target");
        }
    }

    void FixedUpdate () {
        if (_firstEnemy.Length != 0) {
            if (_targetEnemy != null) {
                Vector2 direction = (Vector2) _targetEnemy.position - _rigidBody2D.position;
                direction.Normalize();
                float rotateAmount = Vector3.Cross(direction, transform.up).z;
                _rigidBody2D.angularVelocity = -rotateAmount * rotateSpeed;
            }
            else {
                SearchEnemy();
            }
        }
        _rigidBody2D.velocity = transform.up * missileSpeed;
    }

    IEnumerator AutoDestruct() {
        yield return new WaitForSeconds(5.0f);
        Instantiate(missileDestroyed, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
