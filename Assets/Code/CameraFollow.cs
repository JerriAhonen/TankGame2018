using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CameraFollow : MonoBehaviour, ICameraFollow
    {
        public Transform target;

        public float distance;
        public float angle;

        Vector3 offset;

        // Update is called once per frame
        void Update()
        {
            //Calculate the offset position of the Camera relative to the player's position.
            offset = target.position - transform.forward * distance;
            transform.position = offset;

            //Rotate the Camera according to the player, and the set angle.
            //90 - angle, so that we get the desired angle to the inspector. 
            //(when we insert 90, the camera is on the ground, and when 0 the camera is on top of the player.)
            Quaternion localRotation = Quaternion.Euler(90.0f - angle, target.rotation.eulerAngles.y, 0.0f);
            transform.rotation = localRotation;
        }
        
        public void SetAngle(float angle)
        {
            this.angle = angle;
        }

        public void SetDistance(float distance)
        {
            this.distance = distance;
        }

        public void SetTarget(Transform targetTransform)
        {
            target = targetTransform;
        }
    }
}