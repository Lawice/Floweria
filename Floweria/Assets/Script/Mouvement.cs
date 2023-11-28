using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    private bool grounded;

    [SerializeField] private InputActionReference Left, Right, Up;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Left.action.inProgress) {
            body.velocity = new Vector2(-speed, body.velocity.y);
        }
        if (Right.action.inProgress) {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
        if (Up.action.inProgress && grounded) {
            Jump();
        }
    }

    private void Jump() {
        body.velocity = new Vector2(body.velocity.x, jump);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }
}

