using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEnum
{
    public enum Common
    {
        Fall,
        Grab,
        Idle,
        Jump,
        Mount,
        Dead,
        BasicAttack,
        Hit
    }
    public class Humanoid
    {
        public enum Main
        {
            Fall,
            Grab,
            Idle,
            Mount,
            Run,
            Sleepy,
            Walk,
            Punch,
            Jump
        }
        public enum Face
        {
            FaceCalm,
            FaceExciting,
            FaceHappy,
            FaceHit,
            FaceNormal,
            FaceRoar,
            FaceSad,
            FaceShock,
            FaceSleepy,
            FaceTired,
            FaceSmile
        }
        public enum Hand
        {
            HalfHand,
            HaveHand,
            NoHand
        }
        public enum Fx
        {
            DeadFlash
        }
    }
    public class Cart
    {
        public enum Main
        {
            Idle,
            Dash,
            DashMaxSpeed,
            Mounted,
            Break
        }
    }
}

