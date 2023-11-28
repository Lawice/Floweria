using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worlddestroy : MonoBehaviour{  
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Ground")) {
            print("touch");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
    private void Awake() {
        Destroy(gameObject, 5);
    }
}