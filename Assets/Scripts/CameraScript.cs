using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 1.0f;
    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private Movement player;


    private float rotX = 0 ;
    private float rotY = 180;
    private bool activ = true;

    private void Awake()
    {
        LockCursor();
    }

    private void Start()
    {
        mouseSensitivity = TransferData.Instance.mouseSensitivity;
    }

    private void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        CameraRotation();
    }

    public void UpdateSensitivity(float value) {
        mouseSensitivity = value;
    }

    public void setRotX(float value) {
        rotX = value;
        transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
    }
    public void setRotY(float value)
    {
        rotY = value;
        playerBody.localRotation = Quaternion.Euler(0, rotY, 0);
    }

    public float GetRotX() {
        return rotX;
    }

    public float GetRotY() {
        return rotY;
    }

    public void setActive(bool value) {
        activ = value;
    }

    private void CameraRotation()
    {
        if (activ)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            rotX += mouseY;
            rotY += mouseX;
            if (rotY > 360)
            {
                rotY -= 360;
            }
            else if (rotY < 0)
            {
                rotY += 360;
            }

            if (rotX > 90)
            {
                rotX = 90;
            }
            else if (rotX < -90)
            {
                rotX = -90;
            }
            transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
            if (!player.getPushing())
            //rotate players body
            {
                playerBody.localRotation = Quaternion.Euler(0, rotY, 0);
            }
            else
            //allow player to rotate to look around
            {
                float angle = player.GetComponent<Movement>().angleYWhenPushPress();
                if (angle >= 135 && angle <= 225)
                {
                    if (rotY < 120)
                    {
                        rotY = 120;
                    }
                    else if (rotY > 240)
                    {
                        rotY = 240;
                    }
                }
                else if (angle >= 225 && angle <= 315)
                {
                    if (rotY < 210)
                    {
                        rotY = 210;
                    }
                    else if (rotY > 330)
                    {
                        rotY = 330;
                    }
                }
                else if (angle >= 315 || angle <= 45)
                {
                    if (rotY > 60 && rotY < 200)
                    {
                        rotY = 60;
                    }
                    else if (rotY < 300 && rotY > 200)
                    {
                        rotY = 300;
                    }
                }
                else if (angle > 45 && angle < 135)
                {
                    if (rotY < 30)
                    {
                        rotY = 30;
                    }
                    else if (rotY > 150)
                    {
                        rotY = 150;
                    }

                }
                transform.localRotation = Quaternion.Euler(-rotX, rotY - playerBody.transform.rotation.eulerAngles.y, 0);
            }
        }
    }
}
