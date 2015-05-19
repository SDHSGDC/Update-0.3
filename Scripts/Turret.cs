using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public GameObject bullet;
	public float BPS;
	public float bulletSpeed;
	public float bulletLife;

	// Use this for initialization
	void Start () {

		StartCoroutine(ShootRate());

	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator ShootRate (){
		while (Application.isPlaying) {
			yield return new WaitForSeconds (60 / 60 / BPS);
			Shoot ();
			continue;
		}
	}

	void Shoot (){
		GameObject spawnBullet = Instantiate (bullet, transform.position, Quaternion.Euler(Vector3.forward)) as GameObject;
		spawnBullet.rigidbody.AddForce (transform.forward * bulletSpeed * 100);
		spawnBullet.GetComponent<Bullet> ().bulletLife = bulletLife;
	}
}
