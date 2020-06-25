using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public float speed = 3f;
    public float jumpHeight = 10f;
    public GameObject gameOverScreen;
    /*public TextMeshProUGUI HPLabelText;*/

    private float gravity = -9.81f;
    private bool isAlive;
    private CharacterController characterController;
    private Animator animator;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        /*HPLabelText.SetText($"HP: {health}");*/
        isAlive = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float deltaX = Input.GetAxisRaw("Horizontal");
        float deltaZ = Input.GetAxisRaw("Vertical");
        SetRunningAnimation(deltaX, deltaZ);

        var movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        Jump(movement);
        movement = transform.TransformDirection(movement);

        characterController.Move(movement * speed);
    }

    private void Jump(Vector3 movement)
    {
        if (Input.GetButtonDown("Jump"))
        {
            movement.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private void SetRunningAnimation(float x, float z)
    {
        var isRunning = x != 0 || z != 0;

        animator.SetBool("IsRunning", isRunning);
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            /*HPLabelText.SetText($"HP: {health}");*/
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isAlive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
    }
}
