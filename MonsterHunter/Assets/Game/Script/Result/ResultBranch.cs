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

    void Start()
    {
        _clearBranch = GameObject.Find("ClearResult").gameObject;
        _resultBranch = GameObject.Find("FailedResult").gameObject;
    }

    void Update()
    {
        
    }
}
