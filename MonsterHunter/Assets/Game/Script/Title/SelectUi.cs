/*�I��UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUi : MonoBehaviour
{
    private RectTransform _rectTransform;
    // �^�C�g����ʂ̏���.
    private TitleUpdate _titleUpdate;
    // ���͏��.
    private ControllerManager _controllerManager;
    // �I������UI�������C���^�[�o��.
    private int _selectMoveInterval = 0;

    // ���ݑI�΂�Ă���I��ԍ�.
    // 1.�Q�[���X�^�[�g.
    // 2.�I�v�V����.
    private int _selectNum = 1;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        
    }

    void Update()
    {
        SelectUpdate();
        CrossKeyPushFlameCount()
    }

    private void FixedUpdate()
    {
        

        if(_selectNum == 1)
        {
            _rectTransform.anchoredPosition = new Vector3(500.0f, -200.0f, 0.0f);
        }
        else if(_selectNum == 2)
        {
            _rectTransform.anchoredPosition = new Vector3(500.0f, -300.0f, 0.0f);
        }

        //Debug.Log(_selectMoveTime);
    }

    // pressAnyButton����������I���ł���悤�ɂ���.
    private void SelectUpdate()
    {
        //if (!_titleUpdate._pressAnyPush) return;

        //if(_controllerManager._UpDownCrossKey == 1 && _selectNum == 2)
        //{
        //    _selectNum = 1;
        //}
        //else if(_controllerManager._UpDownCrossKey == -1 && _selectNum == 1)
        //{
        //    _selectNum = 2;
        //}


    }

    // �\���L�[�������Ă��鎞��.
    private void CrossKeyPushFlameCount()
    {
        // �\���L�[�������Ă���ԁA���Ԃ𑝂₷.
        if(_controllerManager._UpDownCrossKey != 0)
        {
            _selectMoveInterval++;
        }
    }
    
}
