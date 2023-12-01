using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Mouvement : MonoBehaviour {
    private bool player2;
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    private bool grounded;
    [SerializeField] private InputActionReference Left, Right, Up;
    [SerializeField] private bool myturn = false;

    private void Awake() {
        player2 = (GameObject.Find("Game").GetComponent<GameInstance>().nb_player == 2);
        print(player2);
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if (player2) {
            Turn_check();
            if (!myturn) {
                print(myturn);
                return; 
            } else {
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

    private void Turn_check(){
        myturn = (GameObject.Find("Game").GetComponent<GameInstance>().member == this.gameObject);
    }
}
