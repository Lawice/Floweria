using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour {
    [SerializeField] private InputActionReference Shot, MousePos, Scroll;
    private Vector2 mouse_pos; 
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float force = 0f;
    Transform Player;
    void Start() {
        Player = transform;
    }

    public void Charging(InputAction.CallbackContext ctx){
        float scroll_amount = ctx.ReadValue<float>();
        int scrolling = 0;
        if (scroll_amount > 0) {
            scrolling = 1;
        } else if (scroll_amount < 0) {
            scrolling = -1;
        }
        force += scrolling;
        force=Mathf.Clamp(force,0, 50);
        print(force);
    }
    
    public void Shooting(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            GameObject newprojectile = Instantiate(Projectile, Player.position, Quaternion.identity);
            newprojectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D tempbody);
            tempbody.velocity += (mouse_pos - (Vector2)Player.position).normalized * force;
        }
    }

    public void gatherDirection(InputAction.CallbackContext ctx) {
        mouse_pos = ctx.ReadValue<Vector2>();
        mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
    }
}