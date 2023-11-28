using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour {

    [Header("Shoot")]
    private Vector2 _mouse_pos; 
    [SerializeField] private GameObject Projectile;
    [SerializeField] private InputActionReference Shot, MousePos;
    Transform Player;


    void Start() {
        Player = transform;
    }
    public void Shooting(InputAction.CallbackContext ctx) {
        if(ctx.performed) {
            GameObject newprojectile = Instantiate(Projectile, Player.position, Quaternion.identity);
            newprojectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D tempbody);
            tempbody.velocity += (_mouse_pos - (Vector2)Player.position);
        }
    }

    public void gatherDirection(InputAction.CallbackContext ctx) {
        _mouse_pos = ctx.ReadValue<Vector2>();
        _mouse_pos = Camera.main.ScreenToWorldPoint(_mouse_pos);
    }
}