using UnityEngine;
using System.Collections;

public class ClearPrefs : MonoBehaviour {

	void Start () {
		PlayerPrefs.SetInt("deaths", 0);
		PlayerPrefs.SetFloat("time", 0f);
        PlayerPrefs.SetInt("cleanRun", -1);
	}
}
