/*�^�C�g����ʂ֖߂邩�̃��j���[���*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class TitleTransitionMenu : MonoBehaviour
{
    // �I�����ڐ�.
    enum SelectItem
    {
        NO, 
        YES,
        MAX_NUM
    }

    // �^�C�g����ʂɖ߂�UI.
    private TitleGuide _titleGuide;

    // UI�̍��W
    private RectTransform _rectTransform;
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �I������UI�̊֐�.
    private Menu _menu;
    // SE.
    private SEManager _seManager;
    // �t�F�[�h.
    private Fade _fade;
    // �V�[���J�ڊǗ�.
    private SceneTransitionManager _sceneTransitionManager;
    // 

    // ���ݑI�΂�Ă���I��ԍ�.
    private int _selectNum = (int)SelectItem.NO;

    void Start()
    {
        _titleGuide = GameObject.Find("TitleGuide").GetComponent<TitleGuide>();
        _rectTransform = GetComponent<RectTransform>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _menu = GameObject.Find("GameManager").GetComponent<Menu>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
        _sceneTransitionManager = GameObject.Find("GameManager").GetComponent<SceneTransitionManager>();
    }

    void Update()
    {
        _menu.SelectMove(_controllerManager._RightLeftCrossKey, ref _selectNum);
        _menu.CrossKeyPushFlameCount(_controllerManager._RightLeftCrossKey);
        _menu.CrossKeyNoPush(_controllerManager._RightLeftCrossKey);
        TitleSceneTransition();
        UINoDecide();
    }

    private void FixedUpdate()
    {
        _menu.SelectNumLimit(ref _selectNum, (int)SelectItem.MAX_NUM);
        SelectPosition();
    }

    // �I������Ă��鍀�ڂ̍��W����.
    private void SelectPosition()
    {
        if(_selectNum == (int)SelectItem.NO)
        {
            _rectTransform.anchoredPosition = new Vector3(-280.0f,-60.0f,0.0f);
        }
        else if(_selectNum == (int)SelectItem.YES)
        {
            _rectTransform.anchoredPosition = new Vector3(280.0f, -60.0f, 0.0f);
        }
    }

    // �͂����������Ƃ��̃V�[���J�ڏ���.
    private void TitleSceneTransition()
    {
        if (_controllerManager._AButtonDown && 
            _selectNum == (int)SelectItem.YES)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _fade._isFading = false;
        }
        if (_fade._fadeEnd)
        {
            _sceneTransitionManager.TitleScene();
        }
    }

    // ���������������Ƃ��̏���.
    private void UINoDecide()
    {
        if(_controllerManager._AButtonDown &&
            _selectNum == (int)SelectItem.NO &&
            _fade._isFading)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _titleGuide.SetSceneTransitionUIOpen(false);
        }
    }

}
