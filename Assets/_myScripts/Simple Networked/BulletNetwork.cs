using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNetwork : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		var hit = collision.gameObject;
		var health = hit.GetComponent<PlayerHealthNetwork>();
		if(health != null){
			health.TakeDamage(10);
		}
		Destroy(gameObject);
	}
}
