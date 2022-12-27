using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Controls for sprite movement
    public float moveDist;
    public MoveAxis moveAxis;
    public iTween.EaseType easeType;
    public iTween.LoopType loopType;
    public float time;
    public enum MoveAxis
    {
        x,
        y,
    }

    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            iTween.MoveBy(gameObject,
                iTween.Hash(
                moveAxis.ToString(), moveDist,
                "easeType", easeType.ToString(),
                "loopType", loopType.ToString(),
                "delay", 0,
                "time", time,
                "ignoretimescale", true));
        }
    }
}
