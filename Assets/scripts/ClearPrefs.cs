using UnityEngine;
using System.Collections;

public class ClearPrefs : MonoBehaviour {

	void Start () {
		PlayerPrefs.SetInt("death", 0);
		PlayerPrefs.SetFloat("time", 0f);
	}
}
