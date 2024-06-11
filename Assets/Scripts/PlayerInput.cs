using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ducksten.ShipsBrrrr {
    public class PlayerInput : MonoBehaviour {
        private PlayerInputActions _inputActions;

        public static event Action<Vector2> onMoveVectorChanged;

        private void OnEnable() {
            if (_inputActions == null) {
                _inputActions = new PlayerInputActions();
            }
            _inputActions.Input.Movement.performed += MovementPerformed;
            _inputActions.Input.Movement.canceled += MovementCanceled;
            _inputActions.Enable();
        }

        private void OnDisable() {
            _inputActions.Disable();
            _inputActions.Input.Movement.performed -= MovementPerformed;
            _inputActions.Input.Movement.canceled -= MovementCanceled;
        }

        private void MovementPerformed(InputAction.CallbackContext ctx) {
            onMoveVectorChanged?.Invoke(ctx.ReadValue<Vector2>());
        }

        private void MovementCanceled(InputAction.CallbackContext ctx) {
            onMoveVectorChanged?.Invoke(Vector2.zero);
        }
    }
}

