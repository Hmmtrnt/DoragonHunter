/*���U���g���ʂ̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUpdate : MonoBehaviour
{
    enum UIKinds
    {
        // �e�\���̔w�i.
        TITLE, 
        RANK,
        RANK_TABLE,
        CLEAR_TIME,
        GUIDE

    }

    // �N�G�X�g�I�����̏��.
    private HuntingEnd _huntingEnd;
    // ���U���g��ʂ�UI.
    public GameObject[] _ui;


    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        

        
    }

    void Update()
    {
        
    }

    


}
