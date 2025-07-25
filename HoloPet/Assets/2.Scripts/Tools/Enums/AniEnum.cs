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
            BotanWalk,
            BotanPunch
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
            BotanFaceTired,           
        }
    }
    public class Watame
    {
        public enum Main
        {
            WatameFall,
            WatameGrab,
            WatameIdle,
            WatameMount,
            WatameRun,
            WatameSleepy,
            WatameWalk,
            WatamePunch
        }
        public enum Face
        {
            WatameFaceCalm,
            WatameFaceExciting,
            WatameFaceHappy,
            WatameFaceHit,
            WatameFaceNormal,
            WatameFaceRoar,
            WatameFaceSad,
            WatameFaceShock,
            WatameFaceSleepy,
            WatameFaceTired
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

