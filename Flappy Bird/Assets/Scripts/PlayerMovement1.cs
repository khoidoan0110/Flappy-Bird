using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : PlayerMovement
{
    [SerializeField] private TrailRenderer dustTrail;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 5f;
    [SerializeField] private float dashingCooldown = 0.5f;
    [SerializeField] private float dashingTime = 0.2f;

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
        base.GetInput();

        if (Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dustTrail.emitting = true;
        direction.y = 0;
        float dashingTimer = 0f;
        while (dashingTimer < dashingTime)
        {
            dashingTimer += Time.deltaTime;
            transform.position += transform.right * dashingPower * Time.deltaTime;
            yield return null;
        }
        dustTrail.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
