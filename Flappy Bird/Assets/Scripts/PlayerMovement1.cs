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

    private void Update()
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

        float dashingTimer = 0f;
        while (dashingTimer < dashingTime)
        {
            dashingTimer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        canJump = true;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
