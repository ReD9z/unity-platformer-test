using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
 
    // Update is called once per frame
    void LateUpdate()
    {
        camTransform.position = new Vector3(transform.position.x, transform.position.y, camTransform.position.z);
    }
}
