/*�I��UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUi : MonoBehaviour
{
    private RectTransform _rectTransform;
    // �^�C�g����ʂ̏���.
    private TitleUpdate _titleUpdate;
    // ���ݑI�΂�Ă���I��ԍ�.
    // 1.�Q�[���X�^�[�g.
    // 2.�I�v�V����.
    private int _selectNum = 1;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _titleUpdate = GameObject.Find("GameManager").GetComponent<TitleUpdate>();
        Debug.Log(_rectTransform.anchoredPosition);
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(_selectNum == 1)
        {
            _rectTransform.anchoredPosition = new Vector3(0.0f, -230.0f, 0.0f);
        }
        else if(_selectNum == 2)
        {
            _rectTransform.anchoredPosition = new Vector3(0.0f, -330.0f, 0.0f);
        }
    }

    // pressAnyButton����������I���ł���悤�ɂ���.
    private void SelectUpdate()
    {
        if (!_titleUpdate._pressAnyPush) return;
    }
}
