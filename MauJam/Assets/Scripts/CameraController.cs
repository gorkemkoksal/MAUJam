using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = 1;
        }

        var moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;

        var rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
}
