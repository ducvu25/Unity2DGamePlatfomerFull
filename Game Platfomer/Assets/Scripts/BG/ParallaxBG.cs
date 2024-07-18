using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    Transform cam;
    [SerializeField] float parallaxEffect;
    [SerializeField] float smoth;

    float length;
    float xPositon;
    ScollBG scollBG;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").transform;

        scollBG = GetComponentInChildren<ScollBG>();
        xPositon = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.position.x *(1 - parallaxEffect);
        float distanceToMove = cam.position.x * parallaxEffect;
        scollBG.SetMaterial(xPositon + distanceToMove - transform.position.x, 0);
        transform.position = Vector3.Lerp(transform.position, new Vector3(xPositon + distanceToMove, transform.position.y, 0), smoth*Time.deltaTime);
        
        if(distanceMoved > xPositon + length)
        {
            xPositon += length;
        }else if(distanceMoved < xPositon - length)
        {
            xPositon -= length;
        }
    }
}
