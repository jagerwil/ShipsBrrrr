using System;
using UnityEngine;

namespace Ducksten.ShipsBrrrr {
    public class CameraController : MonoBehaviour {
        [SerializeField] private Transform _player;

        private Vector3 _deltaPosition;
        
        private void Awake() {
            _deltaPosition = transform.position - _player.position;
        }

        private void Update() {
            transform.position = _player.position + _deltaPosition;
        }
    }
}
