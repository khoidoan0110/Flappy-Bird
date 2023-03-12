using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    Vector3 direction;
    void FixedUpdate()
    {
        direction = new Vector3(bulletSpeed * Time.deltaTime, 0);
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    public void CheckBulletCollision(GameObject upperPipe, GameObject lowerPipe)
    {
        float TopPipeY = upperPipe.transform.position.y;
        float BotPipeY = lowerPipe.transform.position.y;
        if (transform.position.y >= TopPipeY)
        {
            gameObject.SetActive(false);
            Destroy(upperPipe.gameObject); 
        }
        else if (transform.position.y <= BotPipeY)
        {
            gameObject.SetActive(false);
            Destroy(lowerPipe.gameObject); 
        }
    }

}
