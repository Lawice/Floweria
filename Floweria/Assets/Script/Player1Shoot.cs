using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour {
    [SerializeField] private InputActionReference Shot, MousePos, Scroll;
    private Vector2 mouse_pos; 
    [SerializeField] private GameObject Projectile;
    [SerializeField] public float force = 0f;
    private bool myturn = false;
    Transform Player;
    Powerbar powerbar;
    
    void Start() {
        Player = transform;
    }

    void Update (){
        Turn_check();
        if (!myturn) {
            force = 0; 
        }
    }
    
    public void Charging(InputAction.CallbackContext ctx){
        Turn_check();
        if (!myturn) {
            return;
        } else {
            float scroll_amount = ctx.ReadValue<float>();
            int scrolling = 0;
            if (scroll_amount > 0) {
                scrolling = 1;
            } else if (scroll_amount < 0) {
                scrolling = -1;
            }
            force += scrolling;
            force = Mathf.Clamp(force,0, 50);
        }
    }
    
    public void Shooting(InputAction.CallbackContext ctx) {
        Turn_check();
        if (!myturn) {
            return; 
        } else if (ctx.performed) {
            GameObject newprojectile = Instantiate(Projectile, Player.position, Quaternion.identity);
            newprojectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D tempbody);
            tempbody.velocity += (mouse_pos - (Vector2)Player.position).normalized * force;
        }
    }

    public void gatherDirection(InputAction.CallbackContext ctx) {
        mouse_pos = ctx.ReadValue<Vector2>();
        mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
    }

    private void Turn_check(){
        myturn = (GameObject.Find("Game").GetComponent<GameInstance>().member == this.gameObject);
    }
}