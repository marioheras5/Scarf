
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public Animator animator;
    public TrailRenderer tr;
    public Rigidbody2D rb;

    // Movimiento direccional
    public float moveSpeed = 5f;
    Vector2 movement;

    // Dash
    public float dashCooldown = 0.35f;
    public float dashTime = 0.15f;
    public float dashForce = 25f;
    bool canDash = true;
    bool isDashing = false;

    void Update()
    {
        if (rb == null || animator == null || isDashing /*|| !IsOwner*/) return;


        // Movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash && !(movement.x == 0 && movement.y == 0))
        {
            isDashing = true;
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {
        tr.emitting = true;
        canDash = false;
        rb.velocity = movement * dashForce;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    void FixedUpdate()
    {
        if (rb == null || animator == null || isDashing) return;

        // Cursor direcci�n
        Vector3 mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPoint.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Movimiento
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}
