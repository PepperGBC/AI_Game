using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 2.0f;
    public float sensitivity = 2.5f;
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.03f;
    public float minFov = 15f;
    public float maxFov = 90f;
    public float fovSpeed = 10f;
    public float lerpSpeed = 0.1f; // Lerp speed for smooth transitionss
    public float midpoint = 2.0f;

    private float yRotation;
    private float xRotation;

    void Start()
    {
        // Initialize rotation
        transform.localRotation = Quaternion.identity;

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        // Midpoint control with smoothing
        float targetMidpoint = Input.GetKey(KeyCode.LeftControl) ? 1.0f : 2.0f;
        midpoint = Mathf.Lerp(midpoint, targetMidpoint, lerpSpeed);

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

        // Speed control
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 4.0f;
            bobbingAmount = 0.1f;
        }
        else
        {
            speed = 2.0f;
            bobbingAmount = 0.03f;
        }

        float moveX = horizontal * speed * Time.deltaTime;
        float moveZ = vertical * speed * Time.deltaTime;

        transform.Translate(moveX, 0, moveZ);

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yRotation -= mouseY;
        xRotation += mouseX;

        yRotation = Mathf.Clamp(yRotation, -180f, 180f);
        xRotation = Mathf.Clamp(xRotation, -180f, 180f);

        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);

        // FOV control
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -fovSpeed;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
