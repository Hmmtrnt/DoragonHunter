using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPosition
{
    /// <summary>
    /// ���W���Œ�.
    /// </summary>
    /// <param name="targetPosition">�Œ肷��Ώۂ̍��W</param>
    /// <param name="fixPosition"></param>
    public void FixPosition(Vector3 targetPosition, Vector3 fixPosition)
    {
        targetPosition = fixPosition;
    }
}
