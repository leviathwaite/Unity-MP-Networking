using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFolllowNetwork : NetworkBehaviour {

	Transform cameraNode;

	[SerializeField]
	bool lookAt;

	Camera localCamera;
	[SerializeField]
	Vector3 offsetPosition;

	[SerializeField]
	Vector3 offsetRotation;


	  public override void OnStartLocalPlayer()
     {
		
     }

	// Use this for initialization
	void Start () {
		if(!isLocalPlayer){
			Destroy(this);
			return;
		}

		 // Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);

		 Debug.Log("Network Start");
         localCamera = Camera.main;

		bool foundCameraNode = false;
		//This looks for cameraNode in the child objects
 		foreach (Transform child in transform) {
             if (child.CompareTag ("CameraNode")) {
				 foundCameraNode = true;
                 cameraNode = child;
             }
 		}

		// TODO if cameraNode is null throw error or delete script
		if(!foundCameraNode){
			Debug.Log("CameraFollowNetwork, could not find cameraNode in children");
		}
	}
	
	// 
	void LateUpdate () {

		if(localCamera == null){
			Debug.Log("Where's my camera");
			localCamera = Camera.main;
		}

		// Update Camera postion
		localCamera.transform.position = new Vector3(
			cameraNode.position.x + offsetPosition.x,
			cameraNode.position.y + offsetPosition.y,
			cameraNode.position.z + offsetPosition.z
		);

		// Update rotation
		UpdateRotation();

		// if enabled, look at player
		if(lookAt){
			LookAt();
		}else{
			LookAtWithOffset();
		}
	}

	// uppdate rotation by looking at transform of object that has this script
	void LookAt(){
		// Update Camera Rotation
		localCamera.transform.LookAt(transform);
		
		/* 
		localCamera.transform.rotation = Quaternion.LookRotation(transform.position) *
		 	Quaternion.Euler(offsetRotation.x, offsetRotation.y, offsetRotation.z);
		*/

		// transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 0, PartPlacement.rotation);
	
	}

	void LookAtWithOffset(){
		Vector3 cameraDir = -(localCamera.transform.position - transform.position).normalized;
		// Update Camera Rotation
		localCamera.transform.rotation = Quaternion.LookRotation(cameraDir) *
			Quaternion.Euler(offsetRotation.x, offsetRotation.y, offsetRotation.z);
		
		/* 
		localCamera.transform.rotation = Quaternion.LookRotation(transform.position) *
		 	Quaternion.Euler(offsetRotation.x, offsetRotation.y, offsetRotation.z);
		*/

		// transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 0, PartPlacement.rotation);
	
	}

	void UpdateRotation(){
		// Update Camera Rotation
		localCamera.transform.rotation = Quaternion.Euler(offsetRotation.x, offsetRotation.y, offsetRotation.z);

	}
}
