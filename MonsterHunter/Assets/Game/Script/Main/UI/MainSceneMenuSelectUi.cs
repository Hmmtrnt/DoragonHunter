/*���C���V�[���̑I��UI�̏���*/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneMenuSelectUi : MonoBehaviour
{
    public enum SelectItem
    {
        OPTION,     // �ݒ�.
        PAUSE,      // �ꎞ��~.
        RETIRE,     // ���^�C�A.
        MAXNUM      // ���ڐ�.
    }

    // UI�̍��W.
    private RectTransform _rectTransform;
    // �I������UI�̊֐�.
    private Menu _menu;
    // �v���C���[���.
    private PlayerState _playerState;
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // ���C���V�[���̏��.
    private MainSceneManager _mainSceneManager;


    // ���ݑI�΂�Ă���I��ԍ�.
    public int _selectNum = (int)SelectItem.OPTION;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _selectNum = (int)SelectItem.OPTION;
    }

    private void OnEnable()
    {
        _selectNum = (int)SelectItem.OPTION;
    }

    void Update()
    {
        // ���j���[��ʂ���Ă���܂��A�I�v�V������ʂ��J���Ă��鎞�ɃX�L�b�v����.
        if (!_mainSceneManager.GetOpenMenu() || _mainSceneManager.GetOpenOption() ||
            _mainSceneManager.GetOpenRetireConfirmation()) return;
        _menu.SelectMove(_controllerManager._UpDownCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._UpDownCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._UpDownCrossKey);
        OpenOption();
        OpenRetire();
    }

    private void FixedUpdate()
    {
        // ���j���[��ʂ���Ă���܂��A�I�v�V������ʂ��J���Ă��鎞�ɃX�L�b�v����.
        if (!_mainSceneManager.GetOpenMenu() || _mainSceneManager.GetOpenOption() ||
            _mainSceneManager.GetOpenRetireConfirmation()) return;
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAXNUM);
        SelectPosition();
        
    }

    // �I������Ă��鍀�ڂ̍��W����.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.OPTION)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -20.0f, 0.0f);
        }
        else if(_selectNum == (int)SelectItem.PAUSE)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -60.0f, 0.0f);
        }
        else if(_selectNum == (int)SelectItem.RETIRE)
        {
            _rectTransform.anchoredPosition = new Vector3(100.0f, -100.0f, 0.0f);
        }
    }

    // �ݒ��ʂ��J��.
    private void OpenOption()
    {
        // �ݒ��ʂ�I��Ō���.
        if(_controllerManager._AButtonDown && _selectNum == (int)SelectItem.OPTION)
        {
            _mainSceneManager._openOption = true;
        }
    }

    // ���^�C�A�m�F��ʂ��J��.
    private void OpenRetire()
    {
        if(_controllerManager._AButtonDown && _selectNum ==(int)SelectItem.RETIRE)
        {
            _mainSceneManager._openRetireConfirmation = true;
        }
    }


    // �I�����Ă��鍀�ڂ̔ԍ�.
    public int GetSelectNumber() { return _selectNum; }
}
