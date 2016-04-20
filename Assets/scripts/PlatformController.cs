using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool flip = false;
	public float moveForce = 100f;
	public float jumpForce = 700f;
	public float minSpeed = 10f;
	public float maxSpeed = 100f;
	public float deathSpeed = 2f;


	private Rigidbody2D rb2d;
	private new Collider2D collider;


	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
		collider = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update () {
		bool death = Physics2D.IsTouchingLayers(collider, LayerMask.GetMask("Death"));

		if (death || Mathf.Abs(rb2d.velocity.x) < 2f) {
			Debug.Log("You Died");
		}

		Vector2 ground = transform.position;
		ground.y -= 1f;
		bool grounded = Physics2D.IsTouchingLayers(collider, LayerMask.GetMask("Ground"));

		if (Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			flip = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");

		rb2d.AddForce(Vector2.right * h * moveForce);
		int sign = rb2d.velocity.x > 0 ? 1 : -1;
		float vel_x = sign * Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), minSpeed, maxSpeed);

		if (flip) {
			vel_x *= -1;
			flip = false;
		}

		rb2d.velocity = new Vector2(vel_x, rb2d.velocity.y);

		if (jump) {
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
}