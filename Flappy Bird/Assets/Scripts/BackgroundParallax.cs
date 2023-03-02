using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float animSpeed;
    private void Update(){
        meshRenderer.material.mainTextureOffset += new Vector2(animSpeed * Time.deltaTime, 0);
    }
}
