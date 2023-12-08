using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    public float midpoint = 0.5f;

    private float yRotation;
    private float xRotation;

    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            bobbingSpeed = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(bobbingSpeed);
            bobbingSpeed = bobbingSpeed + bobbingAmount;
            if (bobbingSpeed > Mathf.PI * 2)
            {
                bobbingSpeed = bobbingSpeed - (Mathf.PI * 2);
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = midpoint + translateChange;
        }
        else
        {
            cSharpConversion.y = midpoint;
        }

        transform.localPosition = cSharpConversion;

        float moveX = horizontal * speed * Time.deltaTime;
        float moveZ = vertical * speed * Time.deltaTime;

        transform.Translate(moveX, 0, moveZ);

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yRotation -= mouseY;
        xRotation += mouseX;

        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
    }
}
