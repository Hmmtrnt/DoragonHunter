/*���U���g���ʂɂ���ď����𕪊�*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBranch : MonoBehaviour
{
    // �N���A���U���g.
    private GameObject _clearBranch;
    // �Q�[���I�[�o�[���U���g.
    private GameObject _resultBranch;
    // �N�G�X�g�N���A�������ǂ���.
    public bool _questClear = false;


    void Start()
    {
        _clearBranch = GameObject.Find("ClearResult").gameObject;
        _resultBranch = GameObject.Find("FailedResult").gameObject;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if( _questClear )
        {
            //_clearBranch.SetActive( true );
            _resultBranch.SetActive(false);
        }
        else
        {
            _clearBranch.SetActive(false);
            //_resultBranch.SetActive(false);
        }
    }
}
