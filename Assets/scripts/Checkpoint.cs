using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public bool achieved = false;

	public void Achieved() {
		achieved = true;	
	}

	public bool isAchieved() {
		return achieved;
	}
}
