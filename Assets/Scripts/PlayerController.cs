using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource playerAudio;
    public AudioClip jumpAudio;
    public AudioClip coinAudio;

    private Rigidbody playerRB;
    private Animator playerAnimator;

    // Move
    private float speed = 5.0f;
    private float jumpForce = 1500.0f;

    private bool isGround = true;
    private bool isJumpPressed = false;
    private float horizontalInput = 0.0f;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumpPressed && isGround && Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
            playerAnimator.SetBool("Jump_b", true);
            playerAudio.PlayOneShot(jumpAudio);
        }

        AnimatorStateInfo jumpInfo = playerAnimator.GetCurrentAnimatorStateInfo(3);

        if (jumpInfo.IsTag("Jump") && jumpInfo.normalizedTime > 1.0f)
        {
            playerAnimator.SetBool("Jump_b", false);
        }

        playerAnimator.SetFloat("Speed_f", Mathf.Abs(horizontalInput));

        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        if(isJumpPressed)
        {
            isJumpPressed = false;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        float horizontalSpeed = horizontalInput * speed;
        float playerAngle = 180.0f - 90.0f * horizontalInput;

        Quaternion playerRotation = Quaternion.Euler(0.0f, playerAngle, 0.0f);
        playerRB.MoveRotation(playerRotation);

        Vector3 vel = playerRB.velocity;
        vel.x = horizontalSpeed;
        playerRB.velocity = vel;

    }

    private void OnCollisionStay(Collision collision)
    {
        bool contactBottom = collision.contacts[0].normal.y > 0.99f;

        if (contactBottom)
        {
            isGround = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerAnimator.SetBool("Jump_b", false);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            bool stepOn = collision.contacts[0].normal.y > 0.7f;
            if (stepOn)
            {
                EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
                enemy.Eliminate();
                playerRB.AddForce(Vector3.up * jumpForce / 2, ForceMode.Impulse);
                playerAnimator.SetBool("Jump_b", true);
                playerAudio.PlayOneShot(jumpAudio);
            }
            else
            {
                GameOver();
            }
        }
        if (collision.gameObject.CompareTag("Rock"))
        {
            GameOver();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            CollectCoin coinScript = other.gameObject.GetComponent<CollectCoin>();
            if (!coinScript.collected)
            {
                playerAudio.PlayOneShot(coinAudio);
                coinScript.Collect();
            }
        }

        if (other.gameObject.CompareTag("Explosion"))
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        playerRB.freezeRotation = true;
        playerAnimator.SetInteger("DeathType_int", 1);
        playerAnimator.SetBool("Death_b", true);
        enabled = false;
    }
}
