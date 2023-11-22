using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    private Rigidbody2D CC;

     [SerializeField] private InputActionReference Left, Right, Up;
    void Start() {
        CC = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Left.action.inProgress) {
            CC.velocity = new Vector2(-10, CC.velocity.y);
        }
        if (Right.action.inProgress) {
            CC.velocity = new Vector2(10, CC.velocity.y);
        }
        if (Up.action.inProgress) {
            CC.velocity = new Vector2(CC.velocity.x, 5);
        }
    }
}
