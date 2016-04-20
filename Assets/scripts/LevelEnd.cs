using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	public string nextLevel = "";
	public Collider2D player;

	private Collider2D coll;

	void Start() {
		coll = this.GetComponent<Collider2D>();	
	}

	void Update () {
		if (coll.IsTouching(player)) {
			SceneManager.LoadScene(nextLevel);
		}
	}
}
