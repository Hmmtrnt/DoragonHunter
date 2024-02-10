/*クエストリストのアニメーション*/

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class QuestListAnim : MonoBehaviour
{
    // アニメーションするUIの項目数.
    enum UIAnimNum
    {
        PARCHMENT_ONE,          // クエスト用紙.
        PARCHMENT_TWO,          // クエスト用紙(下から二番目).
        PARCHMENT_THREE,        // クエスト用紙(下から三番目).
        SELECT_QUEST_NORMAL,    // 普通の難易度の項目.
        NORMAL_STRING,          // 普通の難易度の項目の文字.
        SELECT_QUEST_HARD,      // 難しいの難易度の項目.
        HARD_STRING,            // 難しいの難易度の項目の文字.
        SELECTED_UI,            // 選択しているUI.
        EXPLANATION,            // 説明テキスト.
        MAX_NUM                 // 最大数.
    }

    // 各UI.
    public GameObject[] _ui;
    // 各UIの座標.
    private RectTransform[] _rectTransforms = new RectTransform[(int)UIAnimNum.MAX_NUM];
    // 各UIの色.
    private Image[] _images = new Image[(int)UIAnimNum.MAX_NUM];
    // SE.
    private SEManager _seManager;
    // ゲームパッドの入力情報.
    private ControllerManager _controllerManager;
    // 各UIの透明度.
    private byte[] _uiColorA = new byte[(int)UIAnimNum.MAX_NUM];

    // 各UIの表示非表示.
    private bool[] _uiDisplay = new bool[(int)UIAnimNum.MAX_NUM];

    // クエストを開いてからの経過時間．
    private int _questOpenCount = 0;


    void Start()
    {
        for(int UINum = 0; UINum < (int)UIAnimNum.MAX_NUM; UINum++)
        {
            _rectTransforms[UINum] = _ui[UINum].GetComponent<RectTransform>();
            _images[UINum] = _ui[UINum].GetComponent<Image>();
            _uiDisplay[UINum] = false;
            _uiColorA[UINum] = 0;

            //_images[UINum].color = new Color32(255, 255, 255, _uiColorA[UINum]);
            
        }
        _questOpenCount = 0;
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
    }

    void Update()
    {
        //DisableUpdate();
        Pos();
    }

    private void FixedUpdate()
    {
        OpenCount();
        Anim();
        UIColor();
        


        //Debug.Log(_rectTransforms[(int)UIAnimNum.PARCHMENT_ONE].anchoredPosition);
    }

    private void OnDisable()
    {
        // 非表示にするときに初期位置に設定.
        //UIDisable();

        
    }

    private void OnEnable()
    {
        UIDisable();
    }

    // 非表示になる瞬間の処理.
    private void UIDisable()
    {

        _questOpenCount = 0;

        // 座標の初期化.
        for(int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _rectTransforms[UINum].anchoredPosition = new Vector3(-414.0f, 0.0f, 0.0f);
            _rectTransforms[UINum].eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            _uiColorA[UINum] = 0;
            _rectTransforms[UINum].DORestart();
            Debug.Assert(_rectTransforms[UINum] == null);
        }

        
    }

    // UIの色を代入.
    private void UIColor()
    {
        for (int UINum = 0; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _images[UINum].color = new Color32(255, 255, 255, _uiColorA[UINum]);
        }
    }

    // UIが非表示の時に起こす処理.
    private void DisableUpdate()
    {
        if(!_controllerManager._BButtonDown) { return; }

        _questOpenCount = 0;

        // 座標の初期化.
        for (int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _rectTransforms[UINum].anchoredPosition = new Vector3(-414.0f, 0.0f, 0.0f);
            _rectTransforms[UINum].eulerAngles = new Vector3(0.0f,0.0f, 0.0f);
            _rectTransforms[UINum].DORestart();
        }
    }

    // クエストリストを開いたときに時間経過をさせる.
    private void OpenCount()
    {
        _questOpenCount++;
    }

    // 全体のアニメーション.
    private void Anim()
    {
        QuestPaperAnim();
    }

    //座標確認.
    private void Pos()
    {
        if (_controllerManager._BButtonDown)
        //if (_controllerManager._XButtonDown)
        {
            for (int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
            {
                _rectTransforms[UINum].anchoredPosition = new Vector3(-414.0f, 0.0f, 0.0f);
                _rectTransforms[UINum].eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                //_uiColorA[UINum] = 0;
                _rectTransforms[UINum].DORestart();
                //Debug.Log("to");
            }
        }
    }

    // クエストの用紙のアニメーション.
    private void QuestPaperAnim()
    {
        // アニメーションを開始するタイミング指定.
        if (_questOpenCount >= 0)
        {
            //QuestPaperOneAnim();
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_ONE, -10.0f);
        }
        if (_questOpenCount >= 5)
        {
            //QuestPaperTwoAnim();
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_TWO, 0.0f);
        }
        if (_questOpenCount >= 10)
        {
            //QuestPaperThreeAnim();
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_THREE, 10.0f);
        }
    }

    // クエスト用紙のアニメーション.
    private void QuestPaperAnim(int num, float RotateZ)
    {
        _rectTransforms[num].DOAnchorPos(new Vector3(414.0f, 0.0f, 0.0f), 0.5f).SetEase(Ease.OutQuad);
        _rectTransforms[num].DORotate(new Vector3(0.0f, 0.0f, RotateZ), 0.5f).SetEase(Ease.OutQuad);

        if (_uiColorA[num] < 255)
        {
            _uiColorA[num] += 5;
        }
    }

    // クエストの項目のアニメーション.


    // クエストの説明のアニメーション.
}
