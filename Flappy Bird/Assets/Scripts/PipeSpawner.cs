using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 0.5f;
    public float minHeight = -1f;
    public float maxHeight = 2f;

    private void OnEnable()
    {
        InvokeRepeating("Spawn", 0f, spawnRate);
        //Invoke("Spawn", spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke("Spawn");
    }

    public void Spawn()
    {
        GameObject pipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);
        pipe.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
