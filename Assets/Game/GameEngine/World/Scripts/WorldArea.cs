using System;
using UnityEditor;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class WorldArea : MonoBehaviour
    {
        [SerializeField]
        private float sizeX;

        [SerializeField]
        private float sizeZ;

        public WorldVector NormalizeVector(WorldVector vector)
        {
            var normalizedX = Mathf.Clamp01(vector.x / this.sizeX);
            var normalizedZ = Mathf.Clamp01(vector.z / this.sizeZ);
            return new WorldVector(normalizedX, normalizedZ);
        }

#if UNITY_EDITOR

        [SerializeField]
        private bool gizmosEnabled;

        private void OnDrawGizmos()
        {
            if (!this.gizmosEnabled)
            {
                return;
            }

            try
            {
                this.DrawArea();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void DrawArea()
        {
            
            Handles.color = Color.white;
            var startPosition = this.transform.position;
            var xpoint = new Vector3(startPosition.x + this.sizeX, 0, startPosition.z);
            var zPoint = new Vector3(startPosition.x, 0, startPosition.z + this.sizeZ);
            var xzPoint = new Vector3(startPosition.x + this.sizeX, 0, startPosition.z + this.sizeZ);

            const int thickness = 5;
            Handles.DrawLine(startPosition, xpoint, thickness);
            Handles.DrawLine(startPosition, zPoint, thickness);
            Handles.DrawLine(xzPoint, xpoint, thickness);
            Handles.DrawLine(xzPoint, zPoint, thickness);
        }

        private void OnValidate()
        {
            this.transform.position = new Vector3();
            this.transform.eulerAngles = new Vector3();
        }
#endif
    }
}