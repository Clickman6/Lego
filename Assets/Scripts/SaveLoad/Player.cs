using System;
using UnityEngine;

namespace SaveLoad {
    [Serializable]
    public class Player {
        public float PositionX;
        public float PositionY;
        public float PositionZ;

        public float RotationX;
        public float RotationY;
        public float RotationZ;
        
        public Player(Transform transform) {
            PositionX = transform.position.x;
            PositionY = transform.position.y;
            PositionZ = transform.position.z;

            RotationX = transform.eulerAngles.x;
            RotationY = transform.eulerAngles.y;
            RotationZ = transform.eulerAngles.z;
        }

        public Vector3 Position() {
            return new Vector3(PositionX, PositionY, PositionZ);
        }
        
        public Quaternion Rotation() {
            return Quaternion.Euler(RotationX, RotationY, RotationZ);
        }
    }
}
