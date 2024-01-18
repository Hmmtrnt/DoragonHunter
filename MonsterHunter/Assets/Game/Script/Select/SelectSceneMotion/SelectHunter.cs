/*選択画面のハンターの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHunter : MonoBehaviour
{
    // アニメーション.
    private Animator _animator;
    // アニメーションの再生開始位置.
    [SerializeField, Range(0.0f, 1.0f)]
    private float _animMotion;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        _animator.Play(_animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, _animMotion);
        _animator.speed = 0;
    }
}
