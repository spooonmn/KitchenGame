using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractLocation;

    private void Update()
    {
        HandleMovement();
        HandleOnInteraction();

    }

    public bool IsWalking()
    {
        return isWalking;
    }


    private void HandleOnInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactDistance = 2f;
        if ( moveDir != Vector3.zero)
        {
            lastInteractLocation = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractLocation, out RaycastHit raycastHit, interactDistance, countersLayerMask ))
        {
           if (raycastHit.collider.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }


    }
    private void HandleMovement() 
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            //cannot move towards MoveDir

            //attempy only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can only move on x axis
                moveDir = moveDirX;
            }
            else
            {
                //attempt only z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can only move on x axis
                    moveDir = moveDirZ;
                }
            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }


}
