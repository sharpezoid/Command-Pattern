using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Transform RedCameraTransform;
    public Transform BlueCameraTransform;
    
    public float LerpSpeed = 3f;
    Transform Target;

    private void Start()
    {
        Target = RedCameraTransform;
    }

    public void SetRedCameraTarget()
    {
        Target = RedCameraTransform;
    }
    public void SetBlueCameraTarget()
    {
        Target = BlueCameraTransform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.transform.position, Time.deltaTime * LerpSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Target.transform.rotation, Time.deltaTime * LerpSpeed);
    }
}
