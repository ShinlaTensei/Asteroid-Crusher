using System;
using UnityEngine;

namespace Constant
{
    public struct ShipUpgrade
    {
        public const float OffsetAttributeLevel = 15f;
    }

    public struct Message
    {
        public const string NotEnoughMoney = "You don't have enough money to do this !!!";
        public const string DontOwn = "You don't have this item in stock !!!";
        public const string NetworkError = "You don't have internet connection or its being interupted. Try again later!";
        public const string LoginFbFailed = "Login failed. Please try again later!";
        public const string HaveNotLogin = "We cannot connect you to server, please login!!!";
    }

    public struct IndexScene
    {
        public const int LoadingScene = 0;
        public const int HomeScene = 1;
        public const int MainScene = 2;
    }

    public struct Path
    {
        public const string shipData = "data.bin";
        public const string playerData = "data2.bin";
        public const string settingFile = "setting.json";
    }

    public struct URL
    {
        public const string Google = "http://google.com";
    }

    public enum Powerup
    {
        SPEED, HEALTH, MISSLES, SHIELD, CLUSTER
    }

    public struct Const
    {
        public const float SPEED_BOOST = 20.0f;
    }

    public struct Projectile
    {
        public const string BLASTER_SMALL = "bullet_blaster_small_single";
        public const string CLUSTER_BOMB = "cluster_bomb";
    }
}
