using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charapter : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;
    private float horizontal = 0f;
    private Rigidbody2D rb;
    public Animator animator;

    private bool isGround = false;
    private bool isJump = false;
    private bool isFacingRight = true;
    private bool isCrystal = false;
    private float waitTime;
    //private bool isFishish = false;

    public bool isGrunded
    {
        get => isGround;
    }

    const float speedXMult = 50f;

    private GameObject crystal;

    private GameObject finish;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        crystal = GameObject.FindGameObjectWithTag("Crystal");
        finish = GameObject.FindGameObjectWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("speedX", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;
        }

        if (isCrystal)
        {
            crystal.SetActive(false);
            finish.GetComponent<Renderer>().material.color = Color.red;
        }

        animator.SetBool("IsJump", !isGround);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speedX * speedXMult * Time.fixedDeltaTime, rb.velocity.y);

        if (isJump)
        {
            rb.AddForce(new Vector2(0f, 270f));
            isGround = false;
            isJump = false;
        }

        if (horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Crystal"))
        {
            isCrystal = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crystal"))
        {
            isCrystal = true;
        }
    }
}