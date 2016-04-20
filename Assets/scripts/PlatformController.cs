using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool flip = false;
	public float moveForce = 100f;
	public float jumpForce = 500f;
	public float minSpeed = 10f;
	public float maxSpeed = 100f;


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

		if (death) {
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

		float vel_x = Mathf.Clamp(rb2d.velocity.x, -1f, 1f) * Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), minSpeed, maxSpeed);

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