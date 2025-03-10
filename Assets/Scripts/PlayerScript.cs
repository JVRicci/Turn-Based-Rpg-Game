using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    private Vector2 movement;
    private Rigidbody2D rig;
    private CapsuleCollider2D capsule_col;
    private Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        capsule_col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move(){
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        rig.linearVelocity = movement * speed;

        // Reseta os estados da animação antes de definir um novo
        anim.SetBool("down", false);
        anim.SetBool("up", false);
        anim.SetBool("side", false);
        anim.SetBool("walking", false);

        // Define se o personagem está andando
        bool isWalking = movement.sqrMagnitude > 0f; // Mais eficiente que verificar x e y separadamente
        anim.SetBool("walking", isWalking);

        // Define a direção da animação
        if (movement.y != 0f)
        {
            anim.SetBool("down", movement.y < 0f);
            anim.SetBool("up", movement.y > 0f);
        }

        else if (movement.x != 0f){
            anim.SetBool("side", true);
            
            // Ajusta a rotação apenas se necessário
            float newRotationY = movement.x > 0f ? 0f : 180f;
            if (transform.eulerAngles.y != newRotationY)
            {
                transform.eulerAngles = new Vector3(0f, newRotationY, 0f);
            }
        }
    }

    private void  OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy"){
            SceneManager.LoadScene("BattleScene");
        }
    }
}
