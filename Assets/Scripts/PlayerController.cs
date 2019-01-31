using System.Collections;
using System.Collections.Generic;               
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin,xMax,zMin,zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;
	public float fireRate;
	private float nextFire;
	public Boundary boundary;
	Rigidbody rigid;
	public GameObject shot;
	public Transform shotSpawn;
	// Update is called once per frame

	void Start(){
		rigid = GetComponent<Rigidbody>();
	}

	void Update(){
		if(Input.GetButton("Fire1") && Time.time>nextFire){
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigid.velocity = movement * speed;

		rigid.position = new Vector3 (
			Mathf.Clamp (rigid.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigid.position.z, boundary.zMin, boundary.zMax)
		);

		rigid.rotation = Quaternion.Euler (0.0f, 0.0f, rigid.position.x*-tilt);

	}
}
