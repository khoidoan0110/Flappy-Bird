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
    [SerializeField] private float dashingTime = 0.5f;


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
        direction.y = 0;
        direction.x += dashingPower;
        transform.position += direction * Time.deltaTime;
        dustTrail.emitting = true;
        yield return new WaitForSecondsRealtime(dashingTime);
        dustTrail.emitting = false;
        isDashing = false;
        yield return new WaitForSecondsRealtime(dashingCooldown);
        canDash = true;
    }
}
