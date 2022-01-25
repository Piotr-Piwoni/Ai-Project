using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Veriables

        private PlayerControls _playerControls;
        
        public Vector2 movementInput;
        public Vector2 cameraInput;
        
        public float verticalInput;
        public float horizontalInput;
        public float cameraInputX;
        public float cameraInputY;

        #endregion
        private void OnEnable()
        {
            if (_playerControls == null)
            {
                _playerControls = new PlayerControls();

                _playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                _playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }
        
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
        
        public void HandleAllInputs()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            cameraInputX = cameraInput.x;
            cameraInputY = cameraInput.y;
        }
    }
}
