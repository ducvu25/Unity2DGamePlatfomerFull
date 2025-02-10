using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScollBG : MonoBehaviour
{
    [SerializeField] float speed;
    Material material;

    private void Awake()
    {
        material = transform.GetComponent<SpriteRenderer>().material;
    }
    // Start is called before the first frame update
    public void SetMaterial(float x, float y)
    {
        material.mainTextureOffset += new Vector2(x * speed, y * speed)*Time.deltaTime;
    }
}
