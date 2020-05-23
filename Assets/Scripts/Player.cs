using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
	public bool canTripleShot;
	public bool canSpeedBoost;
	public bool shieldsActive;
	public bool screenEdgeWrap;
	public int playerHealth = 3;
	
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _speedBoostMultiplier = 1.5f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private int _totalAmmo = 15;
    [SerializeField] private float _laserVerticalOffset = 1.044f ;
    [SerializeField] private GameObject _singleLaserPrefab;
    [SerializeField] private GameObject _tripleLaserPrefab;
    [SerializeField] private GameObject _playerDestroyed;
    [SerializeField] private GameObject _shields;
    [SerializeField] private int _shieldHealth = 3;
    [SerializeField] private GameObject[] _engineFire;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioClip _laserShot;
    [SerializeField] private AudioClip _laserError;
    
    private float _topPositionLimit = 0.0f;
    private float _bottomPositionLimit = -4.0f;
    private float _sidePositionLimit = 9.78f;
    
    private float _nextFire = 0.0f;
    private GameManager _gameManager;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    private int _shieldHealthNow;
    [SerializeField] private int ammoNow;


    private void Start() {
	    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	    _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
	    _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
	    
	    if (!_spawnManager) { 
		    Debug.Log("Missing GameObject: Spawn Manager");
	    }

	    if (!_spriteRenderer) {
		    Debug.Log("Player::_spriteRenderer component missing");
	    }
	    
	    _spriteRenderer.color = new Color(1, 1, 1, 0.5f);
	    
	    if (_uiManager != null)
		    _uiManager.UpdateLives(playerHealth);

	    ResetAmmo();
		  
	    _audioSource = GetComponent<AudioSource>();
	}

    void Update()
    {
	    if (Input.GetKey(KeyCode.LeftShift)) {
		    canSpeedBoost = true;
	    } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
		    canSpeedBoost = false;
	    }
	    
        CalculateMovement();

        if (canTripleShot) {
	        FireLaser(_tripleLaserPrefab);
        }
        else {
	        FireLaser(_singleLaserPrefab);
        }
    }

    private void FireLaser(GameObject laserPrefab) {
	    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) && Time.time > _nextFire) {
		    if (ammoNow > 0) {
			    _nextFire = Time.time + _fireRate;
			    _audioSource.clip = _laserShot;
			    _audioSource.Play();
			    Instantiate(laserPrefab, transform.position + new Vector3(0, _laserVerticalOffset, 0),
				    Quaternion.identity);
			    ammoNow--;
		    }
		    else {
			    _audioSource.clip = _laserError;
			    _audioSource.Play();
		    }
	    }
	    
    }
    
    private void CalculateMovement() {
	    float _horizontalInput = Input.GetAxis("Horizontal");
	    float _verticalInput = Input.GetAxis("Vertical");

    	/* THIS SETS THE MOVEMENT SPEED AS WELL AS SPEED BOOST POWERUP */
    	if (canSpeedBoost) {
	        transform.Translate(new Vector3(_horizontalInput,_verticalInput,0) * 
	                            (_speedBoostMultiplier * _speed * Time.deltaTime));
    	}
    	else {
	        transform.Translate(new Vector3(_horizontalInput,_verticalInput,0) * 
	                            (_speed * Time.deltaTime));
    	}

    	/* THIS SETS THE MOVEMENT BOUNDARIES FOR THE SHIP */
        transform.position = new Vector3(transform.position.x, 
	        Mathf.Clamp(transform.position.y, _bottomPositionLimit, _topPositionLimit), 0);

        /* THIS CONTROLS IF THE SHIP WILL WRAP AROUND THE SIDE EDGE */
        if (screenEdgeWrap) {
	        //This will make the player wrap to the opposite x position defined by sidePositionLimit
	        if (transform.position.x < -_sidePositionLimit) {
		        transform.position = new Vector3(_sidePositionLimit, transform.position.y, 0);
	        }
	        else if (transform.position.x > _sidePositionLimit) {
		        transform.position = new Vector3(-_sidePositionLimit, transform.position.y, 0);
	        }
        }
        else {
	        //This will stop the player at the edge of the edge screen defined by sidePositionLimit
	        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-_sidePositionLimit,_sidePositionLimit),
		        transform.position.y,0);
        }
    }
    
    public void Damage() {
	    if (shieldsActive) {
		    _shieldHealthNow--;
		    if (_shieldHealthNow == 2) {
			    _spriteRenderer.color = new Color(1,1,1,0.25f);
		    }
		    else if (_shieldHealthNow == 1){
			    _spriteRenderer.color = new Color(1,1,1,0.1f);
		    }
		    else {
				shieldsActive = false;
				_shields.SetActive(false);
		    }
	    }
	    else {
		    UpdateLives(-1);
	    }

	    if (playerHealth == 0) {
			_spawnManager.StopSpawning();
		    Instantiate(_playerDestroyed, transform.position, Quaternion.identity);
		    _uiManager.GameOver();
		    Destroy(gameObject.GetComponent<Collider2D>());
		    Destroy(gameObject,0.5f);
	    }
    }

    public void TripleShotPowerUpOn() {
	    canTripleShot = true;
	    StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine() {
	    //Todo: Find a way to add time when collecting a second power up.
	    yield return new WaitForSeconds(5.0f);
	    canTripleShot = false;
    }

    public void SpeedBoostPowerUpOn() {
	    canSpeedBoost = true;
	    StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator SpeedBoostPowerDownRoutine() {
	    yield return new WaitForSeconds(5.0f);
	    canSpeedBoost = false;
    }

    public void ShieldPowerUpOn() {
	    shieldsActive = true;
	    _shieldHealthNow = _shieldHealth;
	    _spriteRenderer.color = new Color(1, 1, 1, 0.5f);
	    _shields.SetActive(true);
    }

    public void ResetAmmo() {
	    ammoNow = _totalAmmo;
    }

    public void AddLife() {
	    UpdateLives(1);
    }

    private void UpdateLives(int life) {
	    playerHealth += life;
	    if (playerHealth > 3) {
		    playerHealth = 3;
	    }
	    _uiManager.UpdateLives(playerHealth);
	    if (playerHealth == 3) {
		    _engineFire[0].SetActive(false);
	    }
	    if (playerHealth == 2) {
		    _engineFire[0].SetActive(true);
		    _engineFire[1].SetActive(false);
	    }
	    else if (playerHealth == 1) {
		    _engineFire[1].SetActive(true);
	    }
    }
}

