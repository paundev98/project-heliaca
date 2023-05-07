using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask ground;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Quaternion GetMouseRotationInWorld()
    {
        Vector2 mouseScreenPosition = playerInputActions.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _camera.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;

        Quaternion targetRotation = Quaternion.identity;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0f;

            // Rotate the character towards the mouse position
            targetRotation = Quaternion.LookRotation(targetDirection);
        }

        return targetRotation;
    }
}
