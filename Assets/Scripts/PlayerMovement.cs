using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll; 
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround; 
    
    private float dirX = 0;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jumpForce = 14;

    private enum MovementState {idle, running, jumping, falling}
    


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
           rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimnationState();

       
    }

    private void UpdateAnimnationState()
    {
        MovementState state; 

        if (dirX > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }

        else if (dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true; 
        }

        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -0.1)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int) state);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.1f, jumpableGround);
    }
}
