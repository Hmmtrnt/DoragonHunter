using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPosition
{
    /// <summary>
    /// 座標を固定.
    /// </summary>
    /// <param name="targetPosition">固定する対象の座標</param>
    /// <param name="fixPosition"></param>
    public void FixPosition(Vector3 targetPosition, Vector3 fixPosition)
    {
        targetPosition = fixPosition;
    }
}
