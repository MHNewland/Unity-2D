using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject followObject;

    Vector3 followDistance = new Vector3(0, 0, 10);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followObject.transform.position - followDistance;
    }
}
