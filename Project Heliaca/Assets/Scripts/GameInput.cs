using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask groundMask;
    private PlayerInputActions playerInputActions;

    private Plane groundPlane;
    private Vector3 clickPosition;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, new Vector3(0, 1.35f, 0));

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            clickPosition = hitPoint;
            return hitPoint;
        }
        return Vector3.zero;
    }
    public float IsShooting()
    {
        return playerInputActions.Player.Shooting.ReadValue<float>();
    }

    public float IsAiming()
    {
        return playerInputActions.Player.Aiming.ReadValue<float>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(clickPosition, 0.1f);
    }

}
