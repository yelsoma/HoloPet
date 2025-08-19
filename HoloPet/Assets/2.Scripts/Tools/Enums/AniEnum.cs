using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEnum
{
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
            Dead
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
            FaceSmile,
            FaceDead
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
    public class Botan
    {
    }
    public class Watame
    {
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

