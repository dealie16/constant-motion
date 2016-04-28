using UnityEngine;
using System.Collections;

public class CleanRun : MonoBehaviour {

	// Use this for initialization
	public void CleanRunStatus () {
        PlayerPrefs.SetInt("cleanRun", 1);
	}
}
