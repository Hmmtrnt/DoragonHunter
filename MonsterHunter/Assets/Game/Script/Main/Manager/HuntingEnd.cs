/*��I�����̏���*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HuntingEnd : MonoBehaviour
{
    // �����X�^�[�̏��.
    private MonsterState _monsterState;
    // �n���^�[�̏��.
    private PlayerState _playerState;
    // �V�[���J��.
    private SceneTransitionManager _sceneTransitionManager;

    // �����������.
    private bool _QuestClear = false;

    // ��������ăV�[���J�ڂ��s���܂ł̎���.
    private int _startSceneTransitionCount = 0;

    void Start()
    {
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _startSceneTransitionCount = 1000;
    }

    private void FixedUpdate()
    {
        HuntingEndBranch();


        SceneTransition();
    }

    // �V�[���J�ڂ��s��.
    private void SceneTransition()
    {
        // �f�o�b�O�p�V�[���J��.
        if (_monsterState.GetHitPoint() == 0 || _playerState.GetHitPoint() == 0)
        {
            // �V�[���؂�ւ����ɃC�x���g�o�^.
            SceneManager.sceneLoaded += GameSceneLoaded;

            // �V�[���؂�ւ�.
            _sceneTransitionManager.ResultScene();
        }
    }

    // �V�[���J�ڎ��ɍs������.
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // �V�[���؂�ւ���̃X�N���v�g�ǉ�.
        ResultBranch resultBranch = GameObject.Find("GameManager").GetComponent<ResultBranch>();

        // �N�G�X�g�I�����̃N�G�X�g��Ԃ���.
        resultBranch._questClear = _QuestClear;


        SceneManager.sceneLoaded -= GameSceneLoaded;
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
    }

    // �J�E���g�J�n.
    private void CountStart()
    {
        // �N�G�X�g���N���A���Ă��Ȃ��Ƃ��ɃX�L�b�v.
        if (!_QuestClear) return;
        _startSceneTransitionCount--;
    }
}
