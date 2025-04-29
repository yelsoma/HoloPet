using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEnum
{
    //holomem
    public class Basic
    {
        public enum Hand
        {
            HalfHand,
            HaveHand,
            NoHand
        }
    }
    public class Botan
    {
        public enum Main
        {
            BotanFall,
            BotanGrab,
            BotanIdle,
            BotanMount,
            BotanRun,
            BotanSleepy,
            BotanWalk
        }
        public enum Face
        {
            BotanFaceCalm,
            BotanFaceExciting,
            BotanFaceHappy,
            BotanFaceHit,
            BotanFaceNormal,
            BotanFaceRoar,
            BotanFaceSad,
            BotanFaceShock,
            BotanFaceSleepy,
            BotanFaceTired
        }
    }
    public class Cart
    {
        public enum Main
        {
            CartIdle,
            CartDash,
            CartDashMaxSpeed,
            CartMounted
        }
    }
}

