using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointController : MonoBehaviour
{

    [SerializeField]
    private float sensitivity = 1;
    // [SerializeField]
    // private Transform cameraPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        float horizontal = Input.GetAxis("Mouse X") * sensitivity;
        transform.Translate(sensitivity * Vector3.right * horizontal + Vector3.up * vertical * Time.deltaTime);
    }
}
