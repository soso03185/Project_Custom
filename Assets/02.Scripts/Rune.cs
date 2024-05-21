using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
    public enum RuneGrade
    {
        Normal,
        Rare,
        Epic,
        Unique,
        Legend
    };

    public enum RuneShape
    {
        Circle,
        Rectangle,
        Triangle
    };

    public enum RuneEffect
    {
        Atk,
        AtkPercent,
        AtkCount,
        CastingTime,
        CriticalChance,
        CriticalMultiplier,
        AtkRange,
        TargetChange,
    }

    private List<(RuneEffect, float)> runeEffectList;

    private RuneGrade grade;

    public RuneGrade Grade
    {
        get { return grade; }
        set { grade = value; }
    }

    private RuneShape shape;

    public RuneShape Shape
    {
        get { return shape;}
        set { shape = value; }
    }

    private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

}
