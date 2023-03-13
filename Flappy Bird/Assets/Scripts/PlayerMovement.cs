using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float timeSinceLastScored = 0.0f;
    public float scoreInterval = 0.5f;
    protected Vector3 direction;
    public float gravity = -9.8f;
    public float maxY = 5f;
    public float minY = -3f;
    public Animator animator;

    protected bool isDead = false;
    protected bool soundPlayed = false;
    public bool isDashing;
    public bool isShooting;

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    public virtual void Update()
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

        GetInput();
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, direction.y * 5f);
    }

    protected virtual void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.PlaySFX("Wing");
            direction = Vector3.up * 5f;
            //Flap animation
            animator.SetTrigger("Flap");
        }
    }

    public virtual void CheckCollision(GameObject upperPipe, GameObject lowerPipe)
    {
        // Check PipeTop
        float TopPipeX = upperPipe.transform.position.x;
        float TopPipeY = upperPipe.transform.position.y;

        float topXLeft = TopPipeX - 0.5f;
        float topXRight = TopPipeX + 0.5f;

        // Check PipeBot
        float BotPipeX = lowerPipe.transform.position.x;
        float BotPipeY = lowerPipe.transform.position.y;

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
            if (transform.position.x + 0.2f> topXLeft && transform.position.x - 0.2f < topXRight)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
            }
        }
        else if (transform.position.y < TopPipeY && transform.position.y > BotPipeY)
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
}