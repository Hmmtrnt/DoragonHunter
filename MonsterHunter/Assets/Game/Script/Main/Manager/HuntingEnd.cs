/*��I�����̏���*/

using UnityEngine;

public class HuntingEnd : MonoBehaviour
{
    // �����N�̔ԍ�.
    enum RankNumber
    {
        S = 0,
        A,
        B,
        C,
        MAXNUM
    }

    // �����N�_�E�������鎞�ԑ�.
    // �m�[�}�����[�h.
    private int[] _rankDownNormalTime = new int[(int)RankNumber.MAXNUM];
    // �n�[�h���[�h.
    private int[] _rankDownHardTime = new int[(int)RankNumber.MAXNUM];

    // ����V�[���̏��.
    private HuntingSceneManager _huntingSceneManager;

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

    // �N�G�X�g���s�ɂȂ鎞��.
    private const int _minuteFailed = 50;

    // ���݂̃����N.
    private int _currentRank = 0;

    void Start()
    {
        _rankDownNormalTime[0] = 3;
        _rankDownNormalTime[1] = 6;
        _rankDownNormalTime[2] = 10;

        _rankDownHardTime[0] = 5;
        _rankDownHardTime[1] = 9;
        _rankDownHardTime[2] = 13;

        _huntingSceneManager = GameObject.Find("GameManager").GetComponent<HuntingSceneManager>();
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();
        _playerState = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _controllerManager = GetComponent<ControllerManager>();
        _questTime = GameObject.Find("LongTimeShaft").GetComponent<QuestTime>();
        _resultUpdate = GameObject.Find("ResultBackGround").GetComponent<ResultUpdate>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
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

    // �����X�^�[���A�v���C���[�̗̑͂�0�ɂȂ�����.�N�G�X�g�̃N���A�ǂ����ύX.
    private void HuntingEndBranch()
    {
        // �G�̗̑͂��s������N���A.
        if(_monsterState.GetHitPoint() == 0)
        {
            _QuestClear = true;
        }
        // �n���^�[�̗̑͂��s����A�܂����Ԃ��߂����玸�s.
        else if(_playerState.GetHitPoint() == 0 || _Minute >= _minuteFailed)
        {
            _QuestClear= false;
        }


        if (_playerState.GetHitPoint()==0 || _monsterState.GetHitPoint() == 0 || _Minute >= _minuteFailed)
        {
            _questEnd = true;
            _Minute = _questTime.GetMinutes();
            _Second = _questTime.GetSecond();
        }
    }

    // ���Ԍo�߂Ń����N�_�E��.
    private void RankDown()
    {
        // �n�[�h���[�h�̃^�C��.
        if (_huntingSceneManager._hitPointMany)
        {
            if (_Minute >= _rankDownHardTime[0])
            {
                _currentRank = (int)RankNumber.A;
            }
            else if (_Minute >= _rankDownHardTime[1])
            {
                _currentRank = (int)RankNumber.B;
            }
            else if (_Minute >= _rankDownHardTime[2])
            {
                _currentRank = (int)RankNumber.C;
            }
        }
        // �m�[�}�����[�h�̃^�C��.
        else if (!_huntingSceneManager._hitPointMany)
        {
            if (_Minute >= _rankDownNormalTime[0])
            {
                _currentRank = (int)RankNumber.A;
            }
            else if (_Minute >= _rankDownNormalTime[1])
            {
                _currentRank = (int)RankNumber.B;
            }
            else if (_Minute >= _rankDownNormalTime[2])
            {
                _currentRank = (int)RankNumber.C;
            }
        }
    }

    // �N�G�X�g�̌��ʂ�����.
    private void QuestResult()
    {
        if(_playerState.GetHitPoint()<=0)
        {
            _QuestFailed = true;
            _huntingSceneManager._openGamePlayUi = false;
        }
        else if(_monsterState.GetHitPoint()<=0) 
        {
            _QuestClear = true;
            _huntingSceneManager._openGamePlayUi = false;
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
