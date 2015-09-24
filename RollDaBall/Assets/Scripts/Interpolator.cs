using System;
using UnityEngine;

namespace Assets.Scripts
{
    public struct PositionBehavior
    {
        public Single xPosition;
        public Single yPosition;
        public Single xSpeed;
        public Single ySpeed;

        public PositionBehavior(float xPosition, float yPosition, float xSpeed, float ySpeed)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }
    }

    public class Interpolator
    {
        public PositionBehavior newData;
        public PositionBehavior lastData;

        public float interpolationTime = .25f;

        public Interpolator(PositionBehavior data)
        {
            lastData = newData;
            newData = data;
        }


        public void CalculateNewInterpolation(PositionBehavior input)
        {
            lastData = this.newData;
            this.newData = input;
        }

        private float XPositionAfterTimeElapsed(float time)
        {
            return (lastData.xPosition + (lastData.xSpeed * time) + ((lastData.xSpeed - newData.xSpeed) * time * time));
        }

        private float YPositionAfterTimeElapsed(float time)
        {
            return (lastData.yPosition + (lastData.ySpeed * time) + ((newData.ySpeed - lastData.ySpeed) * time * time));
        }

        public Vector3 PositionAfterTime(float time)
        {
            return new Vector3(XPositionAfterTimeElapsed(time), 0, YPositionAfterTimeElapsed(time));
        }
    }

}
