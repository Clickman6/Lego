using System;
using UnityEngine;

namespace SaveLoad {
    [Serializable]
    public class Block {
        public int Id;
        
        public float PositionX;
        public float PositionY;
        public float PositionZ;

        public float RotationX;
        public float RotationY;
        public float RotationZ;
        
        public int Material;

        public Block(int id, Transform transform, Material material) {
            Id = id;
            
            PositionX = transform.localPosition.x;
            PositionY = transform.localPosition.y;
            PositionZ = transform.localPosition.z;

            RotationX = transform.localEulerAngles.x;
            RotationY = transform.localEulerAngles.y;
            RotationZ = transform.localEulerAngles.z;

            if (material) {
                Material = material.GetInstanceID();
            }
        }

        public Vector3 Position() {
            return new Vector3(PositionX, PositionY, PositionZ);
        }
        
        public Quaternion Rotation() {
            return Quaternion.Euler(RotationX, RotationY, RotationZ);
        }
    }
}
