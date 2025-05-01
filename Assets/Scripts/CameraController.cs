using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speedT;
    public Vector3 offSet;
    public float camlerpx;
    private void Start()
    {    
        Camera.main.depthTextureMode = DepthTextureMode.Depth; // or DepthTextureMode.DepthNormals
        offSet = transform.position - target.transform.position;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x * camlerpx, transform.position.y, target.position.z+offSet.z), speedT);
      
    }
}
