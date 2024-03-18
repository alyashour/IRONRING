using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Sets the position of the box collider, and sets it to be active only when the player is attacking
    Author - Aiden
*/

public class AttackBehaviour : MonoBehaviour
{
    // Component references
    [SerializeField] BoxCollider2D swordCollider;

    [SerializeField] float damage = 0.05f;


    // Values for the sizes and positions of the collider, based on the direction of the attack
    private Vector2 rightPosition = new Vector2(0.11f, -0.02f);
    private Vector2 leftPosition = new Vector2(-0.11f, -0.02f);
    private Vector2 sideSize = new Vector2(0.22f, 0.25f);

    private Vector2 upPosition = new Vector2(0, 0.078f);
    private Vector2 downPosition = new Vector2(0, -0.078f);
    private Vector2 upDownSize = new Vector2(0.275f, 0.17f);

    // Defines a enum to hold the directions possible, defines a reference to it
    public enum AttackDirection
    {
        left, right, up, down
    }
    public AttackDirection direction;

    // Determines the appropriate attack direction method to call
    public void Attack()
    {
        switch(direction)
        {
            case AttackDirection.left:
                AttackLeft(); 
                break;
            case AttackDirection.right:
                AttackRight(); 
                break;
            case AttackDirection.up:
                AttackUp();
                break;
            case AttackDirection.down:
                AttackDown();
                break;
        }
    }

    // Sets the collider as active, applies the positions and sizes
    private void AttackRight()
    {
        swordCollider.enabled = true;

        swordCollider.offset = rightPosition;
        swordCollider.size = sideSize;
    }
    private void AttackLeft() 
    {
        swordCollider.enabled = true;

        swordCollider.offset = leftPosition;
        swordCollider.size = sideSize;
    }
    private void AttackUp()
    {
        swordCollider.enabled = true;

        swordCollider.offset = upPosition;
        swordCollider.size = upDownSize;
    }
    private void AttackDown()
    {
        swordCollider.enabled = true;

        swordCollider.offset = downPosition;
        swordCollider.size = upDownSize;
    }

    // Disables the collider
    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    // Called when the attack collides with an object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks the collision is with an attackable object
        if (collision.tag == "Attackable")
        {
            // Calls the OnHit method from the collided attackable object, passes the player position
            collision.SendMessage("OnHit", transform.parent.position);
            StopAttack();
        }
        
    }

}