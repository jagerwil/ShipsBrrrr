using UnityEngine;

namespace Ducksten.ShipsBrrrr {
    public class ShipMovement : MonoBehaviour {
        [SerializeField] private float _maxMoveForwardSpeed = 5f;
        [SerializeField] private float _maxMoveBackSpeed = 2f;
        [SerializeField] private float _moveAcceleration = 5f;
        [SerializeField] private float _moveDeceleration = 1f;
        [Space]
        [SerializeField] private float _maxRotationSpeed = 45f;
        [SerializeField] private float _rotationAcceleration = 45f;
        [SerializeField] private float _rotationDeceleration = 45f;
        //#TODO: Add Curve that connects rotation speed with movement speed (aka rotation is slow when ship is standing/fast, rotation is fast when ship is going at avg max speed or something)

        private Vector2 _movementVector;
        private float _currentMoveSpeed;
        private float _currentRotationSpeed;

        private void OnEnable() {
            PlayerInput.onMoveVectorChanged += MoveVectorChanged;
        }

        private void OnDisable() {
            PlayerInput.onMoveVectorChanged -= MoveVectorChanged;
        }

        private void MoveVectorChanged(Vector2 vector) {
            _movementVector = vector;
        }

        private void Update() {
            var deltaTime = Time.deltaTime;
            UpdateMovementSpeed(deltaTime);
            UpdateRotationSpeed(deltaTime);

            transform.Translate(0f, 0f, _currentMoveSpeed * deltaTime, Space.Self);;
            transform.Rotate(transform.up, _currentRotationSpeed * deltaTime, Space.Self);
        }

        private void UpdateMovementSpeed(float deltaTime) {
            UpdateSpeed(ref _currentMoveSpeed, _movementVector.y, _maxMoveForwardSpeed, _maxMoveBackSpeed, _moveAcceleration, _moveDeceleration, deltaTime);
        }

        private void UpdateRotationSpeed(float deltaTime) {
            UpdateSpeed(ref _currentRotationSpeed, _movementVector.x, _maxRotationSpeed, _maxRotationSpeed, _rotationAcceleration, _rotationDeceleration, deltaTime);
        }

        private void UpdateSpeed(ref float curSpeed, float inputVector, float forwardMaxSpeed, float backMaxSpeed, 
                                 float acceleration, float deceleration, float deltaTime) {
            //accelerate
            if (!inputVector.IsApproxZero()) { 
                curSpeed += inputVector * acceleration * deltaTime;
                curSpeed = Mathf.Clamp(curSpeed, -1f * backMaxSpeed, forwardMaxSpeed);
                return;
            }

            //decelerate
            if (curSpeed.IsApproxZero())
                return;

            var moveDirection = (curSpeed > 0f) ? 1f : -1f;
            var deltaMoveSpeed = -1f * moveDirection * deceleration * deltaTime;
            if (Mathf.Abs(curSpeed) <= Mathf.Abs(deltaMoveSpeed)) {
                curSpeed = 0f;
                return;
            }
            curSpeed += deltaMoveSpeed;
        }
    }
}
