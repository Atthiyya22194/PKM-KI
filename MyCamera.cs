using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public FixedTouchField touchField;
    public float Yaxis;
    public float Xaxis;
    public Transform target;
    public bool enableMobileInputs = false;
    float RotationSensitivity = 1f;
    float RotationMin = -30f;
    float RotationMax = 50f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (enableMobileInputs)
        {
            Xaxis += touchField.TouchDist.y * RotationSensitivity;
            Yaxis -= touchField.TouchDist.x * RotationSensitivity;
        }
        else
        {
            Xaxis += Input.GetAxis("Mouse Y") * RotationSensitivity;
            Yaxis -= Input.GetAxis("Mouse X") * RotationSensitivity;
        }     
        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);

        Vector3 targetRotation = new Vector3(Xaxis, Yaxis);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * 2f;
    }
}
