using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private Camera followTargetCamera;
    [SerializeField] private float cameraZPosition = -10;
    [SerializeField] private Transform target;

    public void SetFollowTarget(Transform followTarget)
    {
        target = followTarget;
        JumpToPosition();
    }

    public void UpdateCamera()
    {
        JumpToPosition();
    }

    /// <summary>
    /// Instantly move the camera to the player's position
    /// </summary>
    public void JumpToPosition()
    {
        followTargetCamera.transform.position = new Vector3(target.position.x, target.position.y, cameraZPosition);
    }
}
