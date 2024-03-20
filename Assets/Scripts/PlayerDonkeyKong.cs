using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDonkeyKong : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 direction;
    public float moveSpeed = 1.0f;
    public float jumpStrength = 1.0f;
    private Collider2D[] results;
    private new Collider2D collider;
    private bool grounded;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
    }
    private void Update()
    {
        CheckCollision();
        if (Input.GetButtonDown("Jump"))
        {
            direction = Vector2.up * jumpStrength;
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;
        direction.y = Mathf.Max(direction.y, -1f); 
        if (direction.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        } else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }

    private void CheckCollision()
    {
        grounded = false;

        Vector2 size = collider.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;
        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;
            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.05f);
            }
        }
    }
}
