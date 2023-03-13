using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : PlayerMovement
{
    public float slowdownScale = 0.5f;
    public float slowdownLength = 1f;
    public float cooldown = 1.5f;
    float nextSlow = 0f;

    void Update()
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

        GetInput();

        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
    }

    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.PlaySFX("Wing");
            direction = Vector3.up * 5f;
        }

        if (Time.time > nextSlow)
        {
            if (Input.GetMouseButtonDown(1))
            {
                {
                    nextSlow = Time.time + cooldown;
                    Time.timeScale = slowdownScale;
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;
                }

            }
        }
    }
}
