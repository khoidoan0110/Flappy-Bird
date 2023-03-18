using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : PlayerMovement
{
    private bool canDash = true;
    private bool canJump = true;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private PipeSpawner pipeSpawner;

    public override void Update()
    {
        if (!isDashing) // only apply gravity if not dashing
        {
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;

            if (transform.position.y > maxY)
            {
                transform.position = new Vector2(transform.position.x, maxY);
            }
            if (transform.position.y < minY)
            {
                GameManager.instance.GameOver();
            }

            if (isDead && !soundPlayed)
            {
                GameManager.instance.GameOver();
                soundPlayed = true;
            }
        }

        GetInput();
    }

    public void CheckPointPerGap(GameObject upperPipe, GameObject lowerPipe)
    {
        // Check PipeTop
        float TopPipeX = upperPipe.transform.position.x;
        float TopPipeY = upperPipe.transform.position.y;

        float topXLeft = TopPipeX - 0.5f;
        float topXRight = TopPipeX + 0.5f;

        // Check PipeBot
        float BotPipeX = lowerPipe.transform.position.x;
        float BotPipeY = lowerPipe.transform.position.y;

        if (transform.position.y < TopPipeY && transform.position.y > BotPipeY)
        {
            if (transform.position.x - 0.4f > topXLeft && transform.position.x + 0.4f < topXRight)
            {
                if (timeSinceLastScored >= scoreInterval)
                {
                    GameManager.instance.IncreaseScore();
                    timeSinceLastScored = 0.0f;
                }
            }
        }
        timeSinceLastScored += Time.deltaTime;
        soundPlayed = false;
    }
    protected override void GetInput()
    {
        if (isDashing)
        {
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && canJump)
        {
            AudioManager.instance.PlaySFX("Wing");
            direction = Vector3.up * 5f;
            //Flap animation
            animator.SetTrigger("Flap");
        }

        if (Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        direction.y = 0;
        scoreInterval = 0.1f;

        float dashingTimer = 0f;
        while (dashingTimer < dashingTime)
        {
            dashingTimer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        scoreInterval = 0.5f;
        canJump = true;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
