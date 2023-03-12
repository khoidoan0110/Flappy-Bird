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

    // public override void CheckCollision(GameObject upperPipe, GameObject lowerPipe)
    // {
    //     // Check PipeTop
    //     float TopPipeX = upperPipe.transform.position.x;
    //     float TopPipeY = upperPipe.transform.position.y;

    //     float topXLeft = TopPipeX - 0.5f;
    //     float topXRight = TopPipeX + 0.5f;
    //     float topYoffset = TopPipeY - 0.3f;

    //     // Check PipeBot
    //     float BotPipeX = lowerPipe.transform.position.x;
    //     float BotPipeY = lowerPipe.transform.position.y;

    //     float botXLeft = BotPipeX - 0.5f;
    //     float botXRight = BotPipeX + 0.5f;
    //     float botYoffset = BotPipeY + 0.3f;

    //     if (transform.position.y >= topYoffset)
    //     {
    //         if (transform.position.x + 0.2f > topXLeft && transform.position.x - 0.2f < topXRight)
    //         {
    //             if (upperPipe.activeSelf)
    //             {
    //                 if (!isDead)
    //                 {
    //                     isDead = true;
    //                     GameManager.instance.GameOver();
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             isDead = false;
    //         }
    //     }
    //     else if (transform.position.y <= botYoffset)
    //     {
    //         if (transform.position.x + 0.2f > topXLeft && transform.position.x - 0.2f < topXRight)
    //         {
    //             if (lowerPipe.activeSelf)
    //             {
    //                 if (!isDead)
    //                 {
    //                     isDead = true;
    //                     GameManager.instance.GameOver();
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             isDead = false;
    //         }
    //     }
    //     else if (transform.position.y < topYoffset && transform.position.y > botYoffset)
    //     {
    //         if (transform.position.x - 0.4f > topXLeft && transform.position.x + 0.4f < topXRight)
    //         {
    //             if (timeSinceLastScored >= scoreInterval)
    //             {
    //                 GameManager.instance.IncreaseScore();
    //                 timeSinceLastScored = 0.0f;
    //             }
    //         }
    //     }
    //     timeSinceLastScored += Time.deltaTime;
    //     soundPlayed = false;
    // }
    public void CheckBulletCollision(GameObject upperPipe, GameObject lowerPipe)
    {
        // Check PipeTop
        float TopPipeX = upperPipe.transform.position.x;
        float TopPipeY = upperPipe.transform.position.y;

        float topXLeft = TopPipeX - 0.5f;
        float topXRight = TopPipeX + 0.5f;
        float topYoffset = TopPipeY - 0.3f;

        // Check PipeBot
        float BotPipeX = lowerPipe.transform.position.x;
        float BotPipeY = lowerPipe.transform.position.y;

        float botXLeft = BotPipeX - 0.5f;
        float botXRight = BotPipeX + 0.5f;
        float botYoffset = BotPipeY + 0.3f;

        if (bullet != null && bullet.activeSelf && bullet.transform.position.y >= topYoffset)
        {
            if (bullet.transform.position.x + 0.2f > topXLeft && bullet.transform.position.x - 0.2f < topXRight)
            {
                bullet.gameObject.SetActive(false);
                upperPipe.gameObject.SetActive(false);
                AudioManager.instance.PlaySFX("PipeDestroyed");
            }
        }
        else if (bullet != null && bullet.activeSelf && bullet.transform.position.y <= botYoffset)
        {
            if (bullet.transform.position.x + 0.2f > topXLeft && bullet.transform.position.x - 0.2f < topXRight)
            {
                bullet.gameObject.SetActive(false);
                lowerPipe.gameObject.SetActive(false);
                AudioManager.instance.PlaySFX("PipeDestroyed");
            }
        }
    }
}
