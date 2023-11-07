using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float maxSpeed;
    public float jumpPower;

    public bool bjump = false;
    private Animator animator_;
    Rigidbody2D rigid;
    int Takeepalwl = 0;
    private bool Moveable = true;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (bjump != true)
            {
                animator_.SetBool("isMove", false);
                animator_.SetBool("isJump", true);
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) && Moveable == true)
        {
            rigid.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
            transform.localScale = new Vector3(3f, 3f, 1f);
            if (bjump != true) {
                animator_.SetBool("isMove", true);
            }
           
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Moveable == true)
        {
            rigid.AddForce(Vector3.left * speed, ForceMode2D.Impulse); ;
            transform.localScale = new Vector3(-3f, 3f, 1f);
            if (bjump != true)
            {
                animator_.SetBool("isMove", true);
            }
        }
        else
        {
            animator_.SetBool("isMove", false);
        }
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.R)){
            transform.position = new Vector3(-1.75f, 2.0f, 0.0f);
        }
    }

    void TakeDamage()
    {
        Takeepalwl += 1;
        Debug.Log("Àå¾Ö¹°¿¡ ºÎµúÈù È½¼ö :" + Takeepalwl);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            animator_.SetBool("isJump", false);
            bjump = false;
            Moveable = true;
            animator_.SetBool("isDamage", false);
        }
        else bjump = true;
        if (other.collider.CompareTag("Damage"))
        {
            animator_.SetBool("isDamage", true);
            TakeDamage();
            Moveable = false;
            if (transform.localScale == new Vector3(3f, 3f, 1f))
            {
                rigid.velocity = new Vector2(-10, 10);
            }
            else if (transform.localScale == new Vector3(-3f, 3f, 1f))
            {
                rigid.velocity = new Vector2(10, 10);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D o)
    {
        if (o.collider.CompareTag("Ground"))
            bjump = true;
    }
}