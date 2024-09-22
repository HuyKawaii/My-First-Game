using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float smoothFactor;
    Vector3 offset = new Vector3(0, 0, -10);
    float cameraLeftLimit = -10;
    float cameraRightLimit;
    [SerializeField] float cameraRightLimitOffset = 5.0f; 
    GameObject goal;
    private void Start()
    {
        goal = GameObject.Find("Goal");
        if (goal != null)
        {
            cameraRightLimit = goal.transform.position.x - cameraRightLimitOffset;
        }
        else
        {
            cameraRightLimit = Mathf.Infinity;
        }
    }
    private void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 playerPos = playerTransform.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, playerPos, smoothFactor * Time.deltaTime);
        transform.position = smoothFollow;
        if (transform.position.x < cameraLeftLimit)
        {
            transform.position = new Vector3(cameraLeftLimit, transform.position.y, transform.position.z);
        }else if (transform.position.x > cameraRightLimit)
        {
            transform.position = new Vector3(cameraRightLimit, transform.position.y, transform.position.z);
        }
    }
}
