/*�I����ʑS�̂̏���*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectUpdate : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �V�[���J�ڊǗ�.
    private SceneTransitionManager _sceneTransitionManager;
    // �I������UI.
    private SelectSceneSelectUi _SelectUi;
    // ��Փx�����̃e�L�X�g.
    private Text _explanationText;
    // ����{�^�������������ǂ���.
    private bool _decidePush = false;
    // �N�G�X�g�̓�Փx.
    private bool _hard = false;

    void Start()
    {
        _controllerManager = GetComponent<ControllerManager>();
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        _SelectUi = GameObject.Find("SelectDraw").GetComponent<SelectSceneSelectUi>();
        _explanationText = GameObject.Find("DifficultText").GetComponent<Text>();
    }

    void Update()
    {
        DecidePush();
        TitleTransitionScene();
    }

    private void FixedUpdate()
    {
        if(_decidePush)
        {
            SceneTransition();
        }
        Difficulty();
        ExplanationDraw();
    }

    // ��Փx�̐ݒ�.
    private void Difficulty()
    {
        if(_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.EASY)
        {
            _hard = false;
        }
        else if(_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.HATD)
        {
            _hard = true;
        }
    }


    // ���肵�����̏���.
    private void DecidePush()
    {
        if(_controllerManager._AButtonDown)
        {
            _decidePush = true;
        }
    }

    // �^�C�g���V�[���ɖ߂�.
    private void TitleTransitionScene()
    {
        if (_controllerManager._BButtonDown)
        {
            _sceneTransitionManager.TitleScene();
        }
    }

    // �V�[���J�ڂ��s��.
    private void SceneTransition()
    {
        if (!_decidePush) return;

        // �V�[���J��
        SceneManager.sceneLoaded += SceneTransitionUpdate;

        _sceneTransitionManager.MainScene();
    }

    // �V�[���J�ڎ��ɍs������.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // �V�[���J�ڐ�ɂ���X�N���v�g�ǉ�.
        MonsterState monsterState�@= GameObject.Find("Dragon").GetComponent<MonsterState>();

        // ��Փx��I������������.
        monsterState._HitPointMany = _hard;

        SceneManager.sceneLoaded -= SceneTransitionUpdate;
    }

    // ��Փx�����̕\�L(�f�o�b�O�p)
    private void ExplanationDraw()
    {
        if(_SelectUi.GetSelectNumber()==(int)SelectSceneSelectUi.SelectItem.EASY)
        {
            _explanationText.text = "�����X�^�[�̗̑͂����Ȃ�\n�Z���ԂŋC�y�Ƀv���C�ł��܂��B";
        }
        else if(_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.HATD)
        {
            _explanationText.text = "�����X�^�[�̗̑͂�����\n�����Ԃ�����ƃv���C�ł��܂��B";
        }
        
    }
}
