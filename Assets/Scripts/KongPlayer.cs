using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongPlayer : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 direction;
    public float moveSpeed = 1f;
    public float jumpStrength = 4f;
    private Collider2D[] results;
    private new Collider2D collider;
    private bool grounded;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
    }
    private void CheckCollison()
    {
        Debug.Log("checking collisions");
        grounded = false;
        Vector2 size = collider.bounds.size;
        Debug.Log(size.y);
        size.y += 0.1f;
        size.x /= 2f;
        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);
        
        for (int i = 0; i < amount; i++)
        {
            if (results[i].CompareTag("Platform"))
            {
                grounded = true; break;
            }
        }
    }
    private void Update()
    {
        CheckCollison();

        if (grounded && Input.GetButtonDown("Jump")){
            direction = Vector2.up * jumpStrength;
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;
        if (grounded)
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }

        if (direction.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        } else if (direction.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }
}
