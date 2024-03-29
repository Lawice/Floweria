using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueHP : MonoBehaviour{
    private Animator animator;
    int ennemy_hp = 3;

    void Start(){
        animator = GameObject.Find("Heart2").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Redflow")) {
            Destroy(collision.gameObject);
            ennemy_hp -= 1;
            if(ennemy_hp < 0)
            {
                SceneManager.LoadSceneAsync("EndMenuRed");
            }
            animator.SetInteger("Life2", ennemy_hp);
        }
    }
}
