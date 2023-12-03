using UnityEngine;
using UnityEngine.SceneManagement;

public class RedHP : MonoBehaviour{
    private Animator animator;
    int player_hp = 3;

    void Start(){
        animator = GameObject.Find("Heart").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Blueflow")) {
            Destroy(collision.gameObject);
            player_hp -= 1;
            if(player_hp < 0)
            {
                SceneManager.LoadSceneAsync("EndMenuBlue");
            }
            animator.SetInteger("Life",player_hp);
        }
    }
}
