using System.Collections;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float pipeSpeed = 3f;
    private PlayerMovement player;
    private PlayerMovement1 player1;
    private PlayerMovement2 player2;
    private PlayerMovement3 player3;
    // private Bullet bullet;
    private bool pipeDashing = false;
    private bool bulletActive = false;

    [SerializeField] private GameObject topPipe;
    [SerializeField] private GameObject botPipe;
    [SerializeField] private GameObject rock;
    private float leftCameraEdge;

    private void Start()
    {
        if (SelectionController.instance.CharIndex == 0)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            rock.SetActive(false);
        }
        else if (SelectionController.instance.CharIndex == 1)
        {
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement1>();
            rock.SetActive(false);
        }
        else if (SelectionController.instance.CharIndex == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();
            rock.SetActive(true);
        }
        else if (SelectionController.instance.CharIndex == 3)
        {
            player3 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement3>();
            rock.SetActive(false);
        }

        leftCameraEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        if (player != null)
        {
            player.CheckCollision(topPipe, botPipe);
        }

        if (player1 != null)
        {
            if (player1.isDashing)
            {
                StartCoroutine(DashPipe());
            }
            if (pipeDashing == false)
            {
                player1.CheckCollision(topPipe, botPipe);
            }
        }
        if (player2 != null)
        {
            player2.CheckCollision(topPipe, botPipe, rock);
            player2.CheckBulletCollision(rock);
        }

        if (player3 != null)
        {
            player3.CheckCollision(topPipe, botPipe);
        }

        transform.position += Vector3.left * pipeSpeed * Time.deltaTime;
        if (transform.position.x < leftCameraEdge)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DashPipe()
    {
        pipeDashing = true;
        pipeSpeed += 0.5f;
        yield return new WaitForSecondsRealtime(0.3f);
        pipeDashing = false;
        pipeSpeed -= 0.5f;
    }

}
