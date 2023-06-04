using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float targetDiff = 0.1f;
    private Vector3 targetPosition;
    private void Update()
    {
        if (Vector3.Distance(targetPosition, transform.position) > targetDiff)
        {
            var moveDir = (targetPosition - transform.position).normalized;
            transform.position += moveDir * movementSpeed * Time.deltaTime;

            var lookPos = this.targetPosition - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
        }

    }
    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
