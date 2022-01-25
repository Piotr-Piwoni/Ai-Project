using System;
using UnityEngine;

namespace Enemy
{
    public class CanvasScript : MonoBehaviour
    {
        private Canvas _enemyCanvas;
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            _enemyCanvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            _enemyCanvas.worldCamera = _camera;
        }

        private void Update()
        {
            _enemyCanvas.transform.rotation = _camera.transform.rotation;
        }
    }
}
