using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float animSpeed;
    [SerializeField] private SpriteRenderer sr;
    void Update(){
        Material material = sr.material;
        Vector2 offset = material.mainTextureOffset;
        offset.x += Time.deltaTime * animSpeed;
        material.mainTextureOffset = offset;
    }
}
