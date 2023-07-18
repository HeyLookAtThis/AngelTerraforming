using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ACPlayer
{
    public static class Params
    {
        public const string Speed = nameof(Speed);
        public const string IsOnCloud = nameof(IsOnCloud);
    }

    public static class Stats
    {
        public const string Idle = nameof(Idle);
        public const string Run = nameof(Run);
        public const string Sitting = nameof(Sitting);
    }
}
