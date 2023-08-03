using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterLook : MonoBehaviour
{
    public float sensitivity = 200f;
    public Transform playerBody;
    public float verticalLookLimit = 90f;

    private float xRotation = 0f;
    private float yRotation = 0f;


    public float angle;

    public Transform radar;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);

        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f); // Yukarý ve aþaðý dönüþ
        playerBody.Rotate(Vector3.up * mouseX); // Saða ve sola dönüþ

        radar.transform.localRotation = Quaternion.Euler(90f, yRotation, 0f);
        radar.RotateAround(transform.position, Vector3.up, mouseX);
    }
}

