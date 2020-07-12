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
        public const string NotEnoughMoney = "You don't have enough money to di this !!!";
        public const string DontOwn = "You don't have this item in stock !!!";
    }

    public struct IndexScene
    {
        public const int LoadingScene = 0;
        public const int HomeScene = 1;
        public const int MainScene = 2;
    }

    public struct Path
    {
        public const string saveFileName = "data.bin";
    }

    public struct URL
    {
        public const string Google = "http://google.com";
    }
}
