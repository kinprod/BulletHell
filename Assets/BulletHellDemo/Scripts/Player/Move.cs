using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    private InputAction _moveAction;

    private void Awake()
    {
        _moveAction = InputManager.Instance.Actions.Gameplay.Move;
    }

    private void FixedUpdate()
    {
        MoveAction();
    }

    private void MoveAction()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, direction.y) * _speed;
    }
}
