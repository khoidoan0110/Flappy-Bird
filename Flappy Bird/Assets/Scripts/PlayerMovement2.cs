using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : PlayerMovement
{
    private bool canShoot = true;
    [SerializeField] private float shootingCooldown = 0.5f;
    [SerializeField] private Transform gunPoint;
    private GameObject bullet;

    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.PlaySFX("Wing");
            direction = Vector3.up * 5f;
            //Flap animation
            animator.SetTrigger("Flap");
        }

        if (Input.GetMouseButtonDown(1) && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        float nextFire = 0f;
        if (Time.time > nextFire)
        {
            nextFire = Time.time + shootingCooldown;
            bullet = ObjectPool.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = gunPoint.position;
                bullet.SetActive(true);
                AudioManager.instance.PlaySFX("Gun");
            }
        }
    }

    public void CheckCollision(GameObject upperPipe, GameObject lowerPipe, GameObject rock)
    {
        // Check PipeTop
        float TopPipeX = upperPipe.transform.position.x;
        float TopPipeY = upperPipe.transform.position.y;

        float topXLeft = TopPipeX - 0.5f;
        float topXRight = TopPipeX + 0.5f;

        // Check PipeBot
        float BotPipeX = lowerPipe.transform.position.x;
        float BotPipeY = lowerPipe.transform.position.y;

        // Check rock
        float rockX = rock.transform.position.x;
        float rockY = rock.transform.position.y;

        if (transform.position.y >= TopPipeY && transform.position.y < maxY)
        {
            if (transform.position.x + 0.2f > topXLeft && transform.position.x - 0.2f < topXRight)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
            }
        }
        else if (transform.position.y <= BotPipeY && transform.position.y > minY)
        {
            if (transform.position.x + 0.2f > topXLeft && transform.position.x - 0.2f < topXRight)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
            }
        }
        else if (transform.position.y < TopPipeY && transform.position.y > BotPipeY) //inside gap
        {
            if (rock.activeSelf) // rock is active
            {
                if (transform.position.y < rockY + 0.5f && transform.position.y > rockY - 0.5f)
                {
                    if (transform.position.x + 0.3f > rockX - 0.5f && transform.position.x - 0.3f < rockX + 0.5f)
                    {
                        isDead = true;
                    }
                }
            }
            else
            {
                isDead = false;
            }


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

    public void CheckBulletCollision(GameObject rock)
    {
        // Check rock
        float rockX = rock.transform.position.x;
        float rockY = rock.transform.position.y;

        float leftX = rockX - 0.5f;
        float topY = rockY + 0.5f;
        float botY = rockY - 0.5f;

        if (bullet != null && bullet.activeSelf && bullet.transform.position.y > botY && bullet.transform.position.y <= topY)
        {
            if (bullet.transform.position.x + 0.2f > leftX)
            {
                bullet.gameObject.SetActive(false);
                rock.gameObject.SetActive(false);
                AudioManager.instance.PlaySFX("PipeDestroyed");
            }
        }
    }
}
