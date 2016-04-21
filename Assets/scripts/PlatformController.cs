using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlatformController : MonoBehaviour {

	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool flip = false;
	public float moveForce = 100f;
	public float jumpForce = 700f;
	public float minSpeed = 10f;
	public float maxSpeed = 100f;
	public float deathSpeed = 2f;
	public int maxJumps = 2;

	private Rigidbody2D rb2d;
	private new Collider2D collider;
	private bool dead;
	private int jumps;


	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = new Vector2(minSpeed, 0);
		collider = GetComponent<Collider2D>();
		dead = false;
	}

	// Update is called once per frame
	void Update () {
		if (dead) {
			return;
		}

		bool death = Physics2D.IsTouchingLayers(collider, LayerMask.GetMask("Death"));

		if (death || Mathf.Abs(rb2d.velocity.x) < 2f) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			dead = true;
		}

		bool grounded = Physics2D.IsTouchingLayers(collider, LayerMask.GetMask("Ground"));

		if (grounded && jumps != maxJumps) {
			jumps = maxJumps;
		}

		if (Input.GetButtonDown("Jump") && jumps > 0) {
			jumps--;
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			flip = true;
		}
	}

	void FixedUpdate() {
		if (dead) {
			return;
		}

		float h = Input.GetAxis("Horizontal");
		int sign = rb2d.velocity.x > 0 ? 1 : -1;
        h *= sign;
		Vector2 v_base = sign == 1? Vector2.right: Vector2.left;
		rb2d.AddForce(v_base * h * moveForce);
		float vel_x = sign * Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), minSpeed, maxSpeed);

		if (flip) {
			vel_x *= -1f;
			flip = false;
		}

		rb2d.velocity = new Vector2(vel_x, rb2d.velocity.y);

		if (jump) {
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
}