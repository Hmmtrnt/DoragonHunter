/*��I�����̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HuntingEnd : MonoBehaviour
{
    // ���C���V�[���̏��.
    private MainSceneManager _mainSceneManager;

    // �����X�^�[�̏��.
    private MonsterState _monsterState;
    // �n���^�[�̏��.
    private PlayerState _playerState;
    // �V�[���J��.
    private SceneTransitionManager _sceneTransitionManager;
    // �p�b�h���͏��.
    private ControllerManager _controllerManager;

    // �����������.
    private bool _QuestClear = false;
    // ����s������.
    private bool _QuestFailed = false;
    // �N�G�X�g���I�������ǂ���.
    public bool _questEnd = false;

    // �N�G�X�g�̎��Ԃ�ۑ�.
    //private int _

    // ��������ăV�[���J�ڂ��s���܂ł̎���.
    private int _startSceneTransitionCount = 0;

    void Start()
    {
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _controllerManager = GetComponent<ControllerManager>();
        _startSceneTransitionCount = 1000;
    }

    private void FixedUpdate()
    {
        HuntingEndBranch();
        SceneTransition();
        QuestResult();
    }

    // �V�[���J�ڂ��s��.
    private void SceneTransition()
    {
        // �f�o�b�O�p�V�[���J��.
        //if (_monsterState.GetHitPoint() == 0 || _playerState.GetHitPoint() == 0)
        //{
        //    // �V�[���؂�ւ����ɃC�x���g�o�^.
        //    SceneManager.sceneLoaded += SceneTransitionUpdate;

        //    // �V�[���؂�ւ�.
        //    _sceneTransitionManager.ResultScene();
        //}

        if(_monsterState.GetHitPoint() == 0 || _playerState.GetHitPoint() == 0)
        {
            //if(_controllerManager._AButtonDown)
            //{
            //    _sceneTransitionManager.SelectScene();
            //}
            //else if(_controllerManager._BButtonDown)
            //{
            //    _sceneTransitionManager.TitleScene();
            //}
        }
    }

    // �V�[���J�ڎ��ɍs������.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // �V�[���؂�ւ���̃X�N���v�g�ǉ�.
        ResultUpdate resultBranch = GameObject.Find("GameManager").GetComponent<ResultUpdate>();

        // �N�G�X�g�I�����̃N�G�X�g��Ԃ���.
        //resultBranch._questClear = _QuestClear;


        SceneManager.sceneLoaded -= SceneTransitionUpdate;
    }

    // �����X�^�[���A�v���C���[�̗̑͂�0�ɂȂ�����.�N�G�X�g�̃N���A�ǂ����ύX.
    // �f�o�b�O���.
    private void HuntingEndBranch()
    {
        if(_monsterState.GetHitPoint() == 0)
        {
            _QuestClear = true;
        }
        else if(_playerState.GetHitPoint() == 0)
        {
            _QuestClear= false;
        }

        if(_playerState.GetHitPoint()==0 || _monsterState.GetHitPoint() == 0)
        {
            _questEnd = true;
        }
    }

    // �J�E���g�J�n.
    private void CountStart()
    {
        // �N�G�X�g���N���A���Ă��Ȃ��Ƃ��ɃX�L�b�v.
        if (!_QuestClear) return;
        _startSceneTransitionCount--;
    }

    // �N�G�X�g�̌��ʂ�����.
    private void QuestResult()
    {
        if(_playerState.GetHitPoint()==0)
        {
            _QuestFailed = true;
            _mainSceneManager._openGamePlayUi = false;
        }
        else if(_monsterState.GetHitPoint()==0) 
        {
            _QuestClear = true;
            _mainSceneManager._openGamePlayUi = false;
        }
    }

    // �N�G�X�g���N���A�������ǂ���.
    public bool GetQuestClear() { return _QuestClear; }
    // �N�G�X�g�����s�������ǂ���.
    public bool GetQuestFailed() { return _QuestFailed; }
    // �N�G�X�g���I���������ǂ���.
    public bool GetQuestEnd() { return _questEnd; }
}
