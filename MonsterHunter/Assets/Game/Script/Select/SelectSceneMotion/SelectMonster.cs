/*セレクト画面モンスターの処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMonster : MonoBehaviour
{
    private Animator _animator;

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
