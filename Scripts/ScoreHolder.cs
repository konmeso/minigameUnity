using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : GenericSingleton<ScoreHolder>
{

    public int score;
    public string playerName;

    public override void Awake()
    {
        base.Awake();
    }
}
