using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPosition
{
    /// <summary>
    /// À•W‚ğŒÅ’è.
    /// </summary>
    /// <param name="targetPosition">ŒÅ’è‚·‚é‘ÎÛ‚ÌÀ•W</param>
    /// <param name="fixPosition"></param>
    public void FixPosition(Vector3 targetPosition, Vector3 fixPosition)
    {
        targetPosition = fixPosition;
    }
}
