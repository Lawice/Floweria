using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour{
    [Header("GUI")]
    private GameObject GUI;

    [Header("Players & Turn")]
    public int nb_player;
    public GameObject member;
    private string member_turn = "Red Bush";

    [Header("Timer")]
    public float turn_time;
    [SerializeField] private float max_time = 10;

    [Header("Caméra")]
    private GameObject following_member;
    private bool following = false;
    private Camera cam;
    private float cam_z;

    [Header("Wind & Gravity")]
    [SerializeField] public Vector2 wind;
    [SerializeField] public Vector2 gravity;

    void Start(){
        GUI = GameObject.Find("GUI");
        cam = Camera.main;
        cam_z = cam.transform.position.z;
        Random_wind();
        Random_gravity();
        End_turn();
    }



// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||Forces||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    private void Random_wind() {
        float wind_force = UnityEngine.Random.Range(-10, 10);
        wind = new Vector2(wind_force,0);
    }

    private void Random_gravity() {
        float gravity_force = UnityEngine.Random.Range(-2, 4);
        gravity = new Vector2(0,gravity_force);
    }

// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||Timer||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    private void Restart_timer() {
        turn_time = max_time;
    }

    private void Run_timer() {
        turn_time -= 1 * Time.fixedDeltaTime;
        if (turn_time <=0){
            End_turn();
        }
    }

// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||Turns||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    private void End_turn(GameObject follow_that = null){
        member = null;
        if (follow_that != null) {
            following = true;
            following_member = follow_that;
            return;
        }
        new WaitForSeconds(3f);
        Next_turn();
    }

    private void Next_turn (){
        Random_wind();
        Random_gravity();
        Restart_timer();
        if (Get_member()==null){
            Change_member();
        }
    }
    
    private void Change_member(){
        switch(member_turn) {
            case "Red Bush":
                member_turn = "Blue Bush";
                break;
            case "Blue Bush":
                member_turn = "Red Bush";
                break;
        }
    }

    private GameObject Get_member() {
        return member = GameObject.Find(member_turn);
    }

// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||Camera||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    private void Camera_follow(GameObject follow) {
        cam.transform.position = Vector3.Lerp(cam.transform.position,follow.transform.position, 4f * Time.fixedDeltaTime);
        cam.transform.position += new Vector3(0,0,-cam.transform.position.z + cam_z);
    }

    private void FixedUpdate (){
        if (following_member != null){
            Camera_follow(following_member);
        } else if(following) {
            following = false;
            new WaitForSeconds(3f);
            Next_turn();
        }
        if (member != null){
            Camera_follow(member);
            Run_timer();
        }
    }
}