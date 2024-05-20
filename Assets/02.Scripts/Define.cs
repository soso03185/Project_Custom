using GoogleSheet.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum MonsterState
    {
        spawn,
        move,
        attack,
        hit,
        dead
    };

    public enum MonsterName
    {
        Fox,
        Skeleton
    }

    public enum SpawnType
    {
        Noraml,
        Delay,
        Group
    }
}
