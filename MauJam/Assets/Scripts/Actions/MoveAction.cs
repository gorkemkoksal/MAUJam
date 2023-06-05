using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private float targetDiff = 0.1f;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int maxMoveDistance;
    private Unit unit;
    //Animator
    private Vector3 targetPosition;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }
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

    }
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for(int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetgridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = offsetgridPosition + unitGridPosition;
            }
        }
        return validGridPositionList;
    }
}
