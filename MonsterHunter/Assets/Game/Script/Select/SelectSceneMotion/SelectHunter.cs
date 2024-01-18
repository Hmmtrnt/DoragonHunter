/*�I����ʂ̃n���^�[�̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHunter : MonoBehaviour
{
    // �A�j���[�V����.
    private Animator _animator;
    // �A�j���[�V�����̍Đ��J�n�ʒu.
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
