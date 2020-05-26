using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
 	[SerializeField] private float enemySpeed = 2.0f;
 	[SerializeField] private int enemyHealth = 5;
 	[SerializeField] GameObject enemyDestroyed;
 	[SerializeField] private AudioClip clip;
 	private UIManager _uiManager;
    private float randomX;
    private float _topPositionLimit;
    private float _bottomPositionLimit = -6.0f;
    private float _sidePositionLimit = 9.40f;
    private int movement;
 
 	
 	private void Start() {
	    randomX = Random.Range(-_sidePositionLimit, _sidePositionLimit);
 		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_topPositionLimit = transform.position.y; 
 		transform.position = new Vector3(randomX, _topPositionLimit, 0);
        movement = Random.Range(1, 4);
    }
 
 	void Update() {
	    Move(movement);

	    if (transform.position.y < _bottomPositionLimit && !_uiManager.gameOver) {
	        randomX = Random.Range(-_sidePositionLimit, _sidePositionLimit);
 			transform.position = new Vector3(randomX, _topPositionLimit, 0);
 		}
	    if (transform.position.x < -_sidePositionLimit) {
		    transform.position = new Vector3(_sidePositionLimit, transform.position.y, 0);
	    }
	    else if (transform.position.x > _sidePositionLimit) {
		    transform.position = new Vector3(-_sidePositionLimit, transform.position.y, 0);
	    }
    }

    private void Move(int movementType) {
	    switch (movementType) {
		    case 1:
				transform.Translate(Vector3.down * (enemySpeed * Time.deltaTime));
				break;
		    case 2:
			    transform.Translate(new Vector3(-1, -1, 0) * (enemySpeed * Time.deltaTime));
			    break;
		    case 3:
			    transform.Translate(new Vector3(1, -1, 0) * (enemySpeed * Time.deltaTime));
			    break;
	    }
    }

    private void OnTriggerEnter2D(Collider2D col) {
 		if (col.CompareTag("Player")) {
 			Player player = col.GetComponent<Player>();
 			if (player != null) player.Damage();
 			AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Destroy(gameObject);
 			Instantiate(enemyDestroyed, transform.position, Quaternion.identity);
 		}
  
 		if (col.CompareTag("Laser")) {
 			enemyHealth -= 1;
 			Destroy(col.gameObject);
        }
        
        if (col.CompareTag("Missile")) {
	        enemyHealth -= 5;
	        Destroy(col.gameObject);
        }
        
        if (enemyHealth < 1) {
	        if (_uiManager != null)
		        _uiManager.UpdateScore();
	        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
	        Destroy(gameObject);
	        Instantiate(enemyDestroyed, transform.position, Quaternion.identity);
        }
        
 	}
}
