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
    // �N�G�X�g���Ԏ擾.
    private QuestTime _questTime;
    // ���U���g�X�V����.
    private ResultUpdate _resultUpdate;
    // SE.
    private SEManager _seManager;
    // �t�F�[�h.
    private Fade _fade;

    // �����������.
    private bool _QuestClear = false;
    // ����s������.
    private bool _QuestFailed = false;
    // �N�G�X�g���I�������ǂ���.
    public bool _questEnd = false;
    // �Z���N�g��ʂɖ߂邩�ǂ���.
    private bool _selectSceneRemove = false;

    // �N�G�X�g�̎��Ԃ�ۑ�.
    public int _Minute = 0;
    public int _Second = 0;

    // ���݂̃����N.
    private int _currentRank = 0;

    // ��������ăV�[���J�ڂ��s���܂ł̎���.
    private int _startSceneTransitionCount = 0;

    void Start()
    {
        _mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _controllerManager = GetComponent<ControllerManager>();
        _questTime = GameObject.Find("LongTimeShaft").GetComponent<QuestTime>();
        _resultUpdate = GameObject.Find("ResultBackGround").GetComponent<ResultUpdate>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
        _startSceneTransitionCount = 1000;
        _selectSceneRemove = false;
    }

    private void Update()
    {
        SceneTransition();
    }

    private void FixedUpdate()
    {
        RankDown();
        HuntingEndBranch();
        
        QuestResult();
    }

    // �V�[���J�ڂ��s��.
    private void SceneTransition()
    {
        // �t�F�[�h�J�n.
        if(_resultUpdate.GetAnimEnd())
        {
            // �I�����.
            if (_controllerManager._AButtonDown)
            {
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
                _selectSceneRemove = true;
                _fade._isFading = false;
            }
            // �^�C�g�����.
            else if (_controllerManager._BButtonDown)
            {
                _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
                _selectSceneRemove = false;
                _fade._isFading = false;
            }
        }
        // �V�[���J��.
        if(_fade._fadeEnd && _selectSceneRemove)
        {
            _sceneTransitionManager.SelectScene();
        }
        else if(_fade._fadeEnd && !_selectSceneRemove) 
        {
            _sceneTransitionManager.TitleScene();
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
            _Minute = _questTime.GetMinutes();
            _Second = _questTime.GetSecond();
        }
    }

    // ���Ԍo�߂Ń����N�_�E��.
    private void RankDown()
    {
        if(_Minute >= 6)
        {
            _currentRank = 1;
        }
        else if(_Minute >= 12)
        {
            _currentRank = 2;
        }
        else if( _Minute >= 20)
        { 
            _currentRank = 3;
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
    // �N�G�X�g�I���̃����N���擾.
    public int GetRank() { return _currentRank; }

}
