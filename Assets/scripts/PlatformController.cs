using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour {

	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool flip = false;
	public float moveForce = 100f;
	public float jumpForce = 700f;
	public float minSpeed = 10f;
	public float maxSpeed = 100f;
	public float deathSpeed = 2f;
	public int maxJumps = 2;
	public List<Checkpoint> checkpoints;

	private Rigidbody2D rb2d;
	private new Collider2D collider;
	private int deaths = -1;
	private int jumps;
    private float storedXVel, storedYVel, time;
    private bool paused;

    [SerializeField] private Text timer;
    [SerializeField] private Text deathCount;
    [SerializeField] private GameObject pauseScreen;


    // Use this for initialization
    void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
		collider = GetComponent<Collider2D>();
        pauseScreen.SetActive(false);
        time = 0;
		player_death();
	}

	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

		bool death = Physics2D.IsTouchingLayers(collider, LayerMask.GetMask("Death"));

		if (death || Mathf.Abs(rb2d.velocity.x) < 2f) {
			player_death();
		}

		foreach (Checkpoint checkpoint in checkpoints) {
			bool achieved = GetComponent<Renderer>().bounds.Intersects(checkpoint.GetComponent<Renderer>().bounds);
			if (achieved) {
				checkpoint.Achieved();
			}
		}

        if (Input.GetKeyDown("p")) {
            if (paused) {
                rb2d.velocity = new Vector2(storedXVel, storedYVel);
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
                paused = false;
            } else {
                storedXVel = rb2d.velocity.x;
                storedYVel = rb2d.velocity.y;
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
                paused = true;
            }
        }

        timer.text = GetTimeText();
        deathCount.text = deaths.ToString();

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

    string GetTimeText()
    {
        int m = (int)time / 60;
        int s = (int)time % 60;
        int f = (int)((time % 1) * 100);
        string text = string.Concat(f, "");
        if (text.Length < 2){
            text = text.Insert(0, "0");
        }
        text = string.Concat(s, ":", text);
        if (text.Length < 5) {
            text = text.Insert(0, "0");
        }
        text = string.Concat(m, ":", text);
        if (text.Length < 8) {
            text = text.Insert(0, "0");
        }

        return text;
    }

	void player_death() {
		deaths++;
		Checkpoint last_checkpoint = checkpoints[0];
		for (int i = 0; i < checkpoints.Count; i++) {
			if (checkpoints[i].isAchieved()) {
				last_checkpoint = checkpoints[i];
			}
		}
		this.transform.position = last_checkpoint.transform.position;
		float speed = minSpeed;
		if (last_checkpoint.spawnGoingLeft) {
			speed = speed * -1f;
		}
		rb2d.velocity = new Vector2(speed, 0);
	}
}