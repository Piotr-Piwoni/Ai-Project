using System;
using Managers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerLocomotion : MonoBehaviour
    {
        private InputManager _inputManager; 
        private Vector3 _moveDirection;
        private Rigidbody _playerRb;
        private Transform _cameraObject;
        
        public float movementSpeed = 7f;
        public float rotationSpeed = 7f;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
            _playerRb = GetComponent<Rigidbody>();
            _cameraObject = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleMovement();
            HandleRotation();
        }
        
        private void HandleMovement()
        {
            _moveDirection = _cameraObject.forward * _inputManager.verticalInput;
            _moveDirection += _cameraObject.right * _inputManager.horizontalInput;
            _moveDirection.Normalize();
            _moveDirection.y = 0f;
            _moveDirection *= movementSpeed; 

            Vector3 movementVelocity = _moveDirection;
            _playerRb.velocity = movementVelocity;
        }

        private void HandleRotation()
        {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = _cameraObject.forward * _inputManager.verticalInput;
            targetDirection += _cameraObject.right * _inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0f;
            
            if (targetDirection == Vector3.zero) 
                targetDirection = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
    }
}
