using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathMove : MonoBehaviour {

	public List<Vector2> points;			
	public float speed = .1f;
	public bool loop = true;

	private int cur_point = 1;
	private int step = 1;

	void Start () {
		if (points.Count >= 1) {
			this.transform.position = points[0];	
		}
	}
	
	void Update () {
		if (points.Count <= 1) {
			return;
		}
		Vector2 moveTo = movement(speed);	
		Vector2 move = this.position2() - moveTo;
		if (speed - move.magnitude > speed / 10) {
			this.transform.position = new Vector3(moveTo.x, moveTo.y, 0);
			cur_point += step;
			if (cur_point >= points.Count) {
				if (loop) {
					cur_point = 0;
				} else {
					cur_point = points.Count - 1;
					step = -1;
				}
			}
			if (cur_point < 0) {
				cur_point = 1;
				step = 1;
			}
			moveTo = movement(speed - move.magnitude);
		}
		this.transform.position = new Vector3(moveTo.x, moveTo.y, 0);
	}

	Vector2 position2() {
		return new Vector2(this.transform.position.x, this.transform.position.y);
	}

	Vector2 movement(float max) {
		return Vector2.MoveTowards(this.position2(), points[cur_point], speed);
	}
}