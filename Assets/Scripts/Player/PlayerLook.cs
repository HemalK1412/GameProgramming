using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerLook : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 100f;

    float xAxis = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }   

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxis -= mouseY;
        xAxis = Mathf.Clamp(xAxis, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xAxis, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}