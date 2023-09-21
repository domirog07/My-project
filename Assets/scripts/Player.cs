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

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator_.SetBool("isMove", false);
            animator_.SetBool("isJump", true);
            if (bjump != true)
            {
                //animator_.SetInteger("moveType", 2);
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                bjump = true;
                
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
            transform.localScale = new Vector3(3f, 3f, 1f);
            if (bjump != true) {
                animator_.SetBool("isMove", true);
            }
           
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
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
        if (rigid.velocity.x > maxSpeed)//오른쪽
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);//y값을 0으로 잡으면 공중에서 멈춰버림
        }
        else if (rigid.velocity.x < maxSpeed * (-1))//왼쪽
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator_.SetBool("isJump", false);
        bjump = false;
    }
}