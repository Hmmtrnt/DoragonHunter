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
    // SE.
    private SEManager _seManager;
    // �t�F�[�h.
    private Fade _fade;
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
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
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
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.QUEST_START);
            _decidePush = true;
            _fade._isFading = false;
        }
    }

    // �^�C�g���V�[���ɖ߂�.
    private void TitleTransitionScene()
    {
        // ����{�^�����������Ƃ��̓X�L�b�v.
        if (_decidePush) return;

        if (_controllerManager._BButtonDown)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _fade._isFading = false;
        }
        if(_fade._fadeEnd) 
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
        if(_fade._fadeEnd)
        {
            _sceneTransitionManager.MainScene();
        }
    }

    // �V�[���J�ڎ��ɍs������.
    private void SceneTransitionUpdate(Scene next, LoadSceneMode mode)
    {
        // �V�[���J�ڐ�ɂ���X�N���v�g�ǉ�.
        MainSceneManager mainSceneManager = GameObject.Find("GameManager").GetComponent<MainSceneManager>();

        // ��Փx��I������������.
        mainSceneManager._hitPointMany = _hard;

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
