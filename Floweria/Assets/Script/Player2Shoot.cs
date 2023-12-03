using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor;

public class Player2Shoot : MonoBehaviour {
    [Header("2nd Player")]
    private bool player2;

    [Header("Commun Vraibles")]
    private bool myturn = false;
    private Rigidbody2D body;
    [SerializeField] private GameObject flowers;
    Transform Player;

    [Header("CPU")]
    [SerializeField] private float angle = 0;
    private float force;
    
    [SerializeField] private int nb_simu = 60;
    private enum State {Idle, SimuShoot,Shoot, Move, Dig}
    private State state = State.Idle; 

    private GameObject target; //player

    private List<Vector2> simu_pos = new List<Vector2>();
    private int simu_itera = 200;

    [Header("CPU Forces")]
    private GameObject game;
    [SerializeField] private float max_force = 15;
    [SerializeField] private float rand_angle = 15;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Red Bush");
    }

    void Start() {
        game = GameObject.Find("Game");
        Player = transform;
    }

    private void Update(){
        
    }

    private void FixedUpdate() {
        if (player2) {
            Turn_check();
            if(!myturn){
                return;
            } else {

            }
        }
        else{
            Turn_check();
            if(!myturn){
                state = State.Idle;
                return;
            }

            switch (state) {
                case State.Idle:
                    Setup_shooting();
                    break;
                
                case State.SimuShoot :
                    print("shoot");
                    Simu_shooting(nb_simu);
                    break;
            }
        }
    }

    private void Setup_shooting() {
        Debug_shooting();
        new WaitForSeconds(1f);
        Start_simu_shoot();
    }

    private void Debug_shooting() {
        for (int u = 0; u < simu_pos.Count() -1; u++) {
            Debug.DrawLine(simu_pos[u], simu_pos[u+1], Color.green);
        }
    }

    private void Start_simu_shoot(){
        angle = (float)Math.PI/2;
        force = 5;
        state = State.SimuShoot;
    }

    private void Simu_shooting(int nb_corsimu) {
        float base_ofs_angle = 180 / nb_corsimu;
        force += 1;
        if (force >= max_force) {
            state = State.Move;
        }

        for (int u = 0; u <= nb_corsimu; u++) {
            float ofs_angle = 0;
            if (target.transform.position.x - Player.transform.position.x > 0) {
                ofs_angle = base_ofs_angle * u;
            } else {
                ofs_angle = base_ofs_angle * u + 180;
            }
            StartCoroutine(Simu_shooting(angle - (ofs_angle * Mathf.Deg2Rad)));
            if (state != State.SimuShoot) {
                break;
            }
        }
    }

    IEnumerator Simu_shooting(float simu_angle) {
        simu_pos.Clear();
        Vector2 simu_newpos;
        Vector2 simu_velocity;
        simu_newpos = body.position;
        Vector2 direction = new Vector2((float)Math.Cos(simu_angle), (float)Math.Sin(simu_angle));
        direction *= force;
        simu_velocity = direction;

        for (int u = 0; u < simu_itera; u++){
            simu_pos.Add(simu_newpos);
            Debug_shooting();
            simu_newpos += simu_velocity * Time.fixedDeltaTime;
            simu_velocity += game.GetComponent<GameInstance>().gravity *Time.fixedDeltaTime;
            simu_velocity += game.GetComponent<GameInstance>().wind *Time.fixedDeltaTime;

            var raycast = Physics2D.CircleCast(simu_newpos,0.5f,Vector2.zero,0.5f);

            if (raycast.collider !=null) {
                if (raycast.collider.GetComponent<BoxCollider2D>() != null && raycast.transform.tag == "Ground") {
                    break;  
                }
            }

            if(target.GetComponent<BoxCollider2D>().OverlapPoint(simu_newpos)) {
                float simulate_angle = UnityEngine.Random.Range(-rand_angle,rand_angle);
                float angle_random = simu_angle + (simulate_angle * Mathf.Deg2Rad);
                direction = new Vector2((float)Math.Cos(angle_random),(float)Math.Sin(angle_random));
                direction *= force;
                Shoot(direction);
                break;
            }
        }
        yield return null;
    }

    private void Shoot(Vector2 shootvector) {
        GameObject newprojectile = Instantiate(flowers, Player.position, Quaternion.identity);
        newprojectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D tempbody);
        tempbody.velocity = shootvector * 1.008f;
    }

    private void Turn_check(){
        myturn = (GameObject.Find("Game").GetComponent<GameInstance>().member == this.gameObject);
    }
}
