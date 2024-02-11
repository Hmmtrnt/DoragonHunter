/*‹¤’Êˆ—*/

using DG.Tweening;
using UnityEngine;

public static class Common 
{
    public static Sequence CreateReusableSequence(GameObject Target)
    {
        return DOTween.Sequence()
            .Pause()
            .SetAutoKill(false)
            .SetLink(Target);
    }
}
