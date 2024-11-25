using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    [SerializeField] private bool instant = false;
    [SerializeField] private float followSpeed = 5;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        Invoke("JumpToPosition", 0.1f);
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    /// <summary>
    /// Move the camera to the player's position
    /// </summary>
    private void FollowTarget()
    {
        if (target != null)
        {
            if (instant)
                JumpToPosition();
            else
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime * followSpeed);
        }
    }

    /// <summary>
    /// Instantly move the camera to the player's position
    /// </summary>
    public void JumpToPosition()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
