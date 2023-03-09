using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 3f;
    private PlayerMovement player;
    [SerializeField] private GameObject topPipe;
    [SerializeField] private GameObject botPipe;
    private float leftCameraEdge;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        leftCameraEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < leftCameraEdge)
        {
            Destroy(gameObject); 
        }
    }

    void FixedUpdate(){
        player.CheckCollision(topPipe, botPipe);
    }

}
