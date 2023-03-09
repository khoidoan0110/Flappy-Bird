using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private float timeSinceLastScored = 0.0f;
    public float scoreInterval = 0.5f;
    protected Vector3 direction;
    public float gravity = -9.8f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private float minY = -3f;
    public Animator animator;

    private bool isDead = false;
    private bool soundPlayed = false;

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
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

    public void CheckCollision(GameObject upperPipe, GameObject lowerPipe)
    {
        // Check PipeTop
        float dirXTop = upperPipe.transform.position.x;
        float dirYTop = upperPipe.transform.position.y;

        float topXLeft = dirXTop - 0.5f;

        float topXRight = dirXTop + 0.5f;
        float topY = dirYTop - 0.3f;

        // Check PipeBot
        float dirXBot = lowerPipe.transform.position.x;
        float dirYBot = lowerPipe.transform.position.y;

        float botXLeft = dirXBot - 0.5f;
        float botY = dirYBot + 0.3f;

        float botXRight = dirXBot + 0.5f;

        if (transform.position.y >= topY)
        {
            if (transform.position.x + 0.2f > topXLeft && transform.position.x - 0.2f < topXRight)
            {
                if (!isDead)
                {
                    isDead = true;
                    GameManager.instance.GameOver();
                }
            }
            else
            {
                isDead = false;
            }
        }
        else if (transform.position.y <= botY)
        {
            if (transform.position.x + 0.2f > topXLeft && transform.position.x - 0.2f < topXRight)
            {
                if (!isDead)
                {
                    isDead = true;
                    GameManager.instance.GameOver();
                }
            }
            else
            {
                isDead = false;
            }
        }
        else if (transform.position.y < topY && transform.position.y > botY)
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