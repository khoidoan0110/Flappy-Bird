using System.Collections;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float pipeSpeed = 3f;
    private PlayerMovement player;
    private PlayerMovement1 player1;
    private PlayerMovement2 player2;
    // private Bullet bullet;
    private bool pipeDashing = false;
    private bool bulletActive = false;

    [SerializeField] private GameObject topPipe;
    [SerializeField] private GameObject botPipe;
    private float leftCameraEdge;

    private void Start()
    {
        if (SelectionController.instance.CharIndex == 0)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
        else if (SelectionController.instance.CharIndex == 1)
        {
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement1>();
        }
        else if (SelectionController.instance.CharIndex == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();
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
            player1.CheckCollision(topPipe, botPipe);
            if (player1.isDashing)
            {
                StartCoroutine(DashPipe());
            }
            // if (pipeDashing == false)
            // {

            // }
        }
        if (player2 != null)
        {
            //player2.CheckCollision(topPipe, botPipe);
            player2.CheckBulletCollision(topPipe, botPipe);
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
