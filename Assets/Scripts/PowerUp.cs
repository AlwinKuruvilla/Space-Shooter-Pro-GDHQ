using UnityEngine;

public class PowerUp : MonoBehaviour {
	[SerializeField] private float travelSpeed = 3.0f;
	[SerializeField] private int powerUpId; //0 = Triple shot, 1 = Speed Boost, 2 = Shields
	[SerializeField] private AudioClip clip;

	private const float BottomPositionLimit = -6.0f;

	private void Start() {
		float randomX = Random.Range(-8.33f, 8.33f);
		transform.position = new Vector3(randomX, transform.position.y, 0);
	}

	void Update() {
		transform.Translate(Vector3.down * (Time.deltaTime * travelSpeed));
		if (transform.position.y < BottomPositionLimit) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Player")) {
			Player player = col.GetComponent<Player>();

			if (player != null) {
				switch (powerUpId) {
					case 0:
						player.TripleShotPowerUpOn();
						break;
					case 1:
						player.SpeedBoostPowerUpOn();
						break;
					case 2:	
						player.ShieldPowerUpOn();
						break;	
					case 3:
						player.ResetAmmo();
						break;
				}
			}

			AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
			Destroy(gameObject);
		}
	}
}