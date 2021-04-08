using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class VehicleAI : AbstractBehaviour, IOnUpdate
    {
        [Inject] ISceneState sceneState = default;
        [Inject] public IVehicle vehicle = default;

        public float SensorFrontOffset;
        public float SensorSideOffset;
        public float SensorEndShift;
        public float SensorLength;
        public float SensorY;
        public LayerMask LayerMask;
        public float DistanceThreshold;

        public VehicleWaypoint prevWaypoint;
        public VehicleWaypoint nextWaypoint;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (nextWaypoint != null) {
                nextWaypoint.VehicleCount--;
                nextWaypoint = null;
            }

            if (prevWaypoint != null) {
                prevWaypoint.VehicleCount--;
                prevWaypoint = null;
            }
        }

        public void StartFromWaypoint(VehicleWaypoint waypoint)
        {
            nextWaypoint = waypoint;
            nextWaypoint.VehicleCount++;
        }

        Vector3 GetSensorStart(float dir)
        {
            return transform.position + -transform.forward * SensorFrontOffset + transform.right * SensorSideOffset * dir + SensorY * Vector3.up;
        }

        Vector3 GetSensorDir(float shift)
        {
            return -transform.forward * SensorLength + transform.right * shift * SensorEndShift + SensorY * Vector3.up;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(GetSensorStart( 0.0f), GetSensorDir( 0.0f)); // center
            Gizmos.DrawRay(GetSensorStart(-1.0f), GetSensorDir(-1.0f)); // left diagonal
            Gizmos.DrawRay(GetSensorStart(-1.0f), GetSensorDir( 0.0f)); // left direct
            Gizmos.DrawRay(GetSensorStart( 1.0f), GetSensorDir( 1.0f)); // right diagonal
            Gizmos.DrawRay(GetSensorStart( 1.0f), GetSensorDir( 0.0f)); // right direct
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (nextWaypoint == null)
                return;

            Vector3 target3D = nextWaypoint.transform.position;
            Vector3 target = new Vector3(target3D.x, transform.position.y, target3D.z);
            Vector3 vectorToTarget = transform.InverseTransformPoint(target);
            float distanceToTarget = vectorToTarget.magnitude;

            if (distanceToTarget <= DistanceThreshold) {
                if (prevWaypoint != null)
                    prevWaypoint.VehicleCount--;

                prevWaypoint = nextWaypoint;
                nextWaypoint = nextWaypoint.Next;
                nextWaypoint.VehicleCount++;
            }

            float forward = 1.0f;
            float steer = vectorToTarget.x / distanceToTarget;

            if (Physics.Raycast(GetSensorStart( 0.0f), GetSensorDir( 0.0f), SensorLength, LayerMask)) // center
                forward = -1.0f;

            if (Physics.Raycast(GetSensorStart(-1.0f), GetSensorDir(-1.0f), SensorLength, LayerMask)) // left diagonal
                steer = 1.0f;
            if (Physics.Raycast(GetSensorStart(-1.0f), GetSensorDir( 0.0f), SensorLength, LayerMask)) // left direct
                steer = 1.0f;

            if (Physics.Raycast(GetSensorStart( 1.0f), GetSensorDir( 0.0f), SensorLength, LayerMask)) // right diagonal
                steer = -1.0f;
            if (Physics.Raycast(GetSensorStart( 1.0f), GetSensorDir( 1.0f), SensorLength, LayerMask)) // right direct
                steer = -1.0f;

            if (nextWaypoint.TrafficLights != null && !nextWaypoint.TrafficLights.IsGreen) {
                if (vehicle.SpeedKmh > 0.0f)
                    forward = -1.0f;
                else if (vehicle.SpeedKmh < 0.0f)
                    forward = 1.0f;
                else
                    forward = 0.0f;
            }

            vehicle.Forward = forward;
            vehicle.Turn = -steer;
        }
    }
}
