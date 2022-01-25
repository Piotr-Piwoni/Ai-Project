using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Veriables

        private InputManager _inputManager;
        private Vector3 _cameraFollowVelocity = Vector3.zero;
        private Vector3 _cameraVectorPosition;

        public LayerMask collisionLayers;
        
        public Transform targetTransform;
        public Transform cameraPivot;
        public Transform cameraTransform;

        public float cameraCollisionRadius = 2f;
        public float cameraCollisionOffset = 2f;
        public float minCollisionOffset = 0.5f;
        public float defaultPosition;
        [Tooltip("The lower the value the faster it follows")]
        public float cameraFollowSpeed = 0.1f;
        [Range(0,1)]
        public float cameraLookSpeed = 0.2f;
        [Range(0,1)]
        public float cameraPivotSpeed = 0.2f;
        public float lookAngle;
        public float pivotAngle;
        public float minPivotAngle = -35f; 
        public float maxPivotAngle = 35f; 
            
        #endregion
        private void Awake()
        {
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            _inputManager = FindObjectOfType<InputManager>();
            cameraTransform = Camera.main.transform;
            defaultPosition = cameraTransform.localPosition.z;
        }

        public void HandleAllCameraMovement()
        {
            FollowTarget();
            RotateCamera();
            HandleCameraCollisions();
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref _cameraFollowVelocity, cameraFollowSpeed);

            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            Vector3  rotation;
            Quaternion targetRotation;
            
            lookAngle += (_inputManager.cameraInputX * cameraLookSpeed);
            pivotAngle+= (_inputManager.cameraInputY * cameraPivotSpeed);
            pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);
            
            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;
            
            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
            
        }

        private void HandleCameraCollisions()
        {
            float targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivot.position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
            {
                float distance = Vector3.Distance(cameraPivot.position, hit.point);
                targetPosition += (distance - cameraCollisionOffset);
            }

            if (Mathf.Abs(targetPosition) < minCollisionOffset)
            {
                targetPosition -= minCollisionOffset;
            }

            _cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
            cameraTransform.localPosition = _cameraVectorPosition;
        }
    }
}
