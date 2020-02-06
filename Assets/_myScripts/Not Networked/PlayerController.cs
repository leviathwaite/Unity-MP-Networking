using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The first piece of game-like functionality in this example will be to move the Player GameObject in the scene. 
We will do this with a new script called “PlayerController”.

To begin with the PlayerController script will be written without any Networking code
 so it will only work in a single-player environment.
 */

public class PlayerController : MonoBehaviour
{

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

		if(Input.GetKeyDown(KeyCode.Space)){
			Fire();
		}

    }

	void Fire(){
		// create Bullet from bulletPrefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab, 
			bulletSpawn.position,
			bulletSpawn.rotation);

			// Add Velocity
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

			// Destroy the bullet after 2 seconds
			Destroy(bullet, 2.0f);
	}

}