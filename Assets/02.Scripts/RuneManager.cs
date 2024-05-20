using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour 
{
    public List<(Rune, bool)> runeList;

    public (Rune, bool) this[int i]
    {
        get => runeList[i];
        set => runeList[i] = value;
    }

    public void Init()
    {

    }
}
