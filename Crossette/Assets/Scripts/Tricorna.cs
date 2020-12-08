using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tricorna : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector3 movement;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    public void OnJump()
    {
        Debug.Log("JUMP");
    }

    public void OnMove(InputValue input)
    {
        float value = input.Get<float>();
        movement.x = value;

        // direction
        if (value > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (value < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
        }

        // animator
        animator.SetFloat("AnimState", value != 0 ? 1 : 0);
    }
}
