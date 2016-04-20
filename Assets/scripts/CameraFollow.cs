using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject follow;

	void Update () {
		Vector3 position = follow.transform.position;
		position.z = -20;
		this.transform.position = position;
	}
}
