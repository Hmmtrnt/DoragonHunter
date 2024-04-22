/*クエストリストのアニメーション*/

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class QuestListAnim : MonoBehaviour
{
    // アニメーションするUIの項目.
    enum UIAnimNum
    {
        PARCHMENT_ONE,          // クエスト用紙.
        PARCHMENT_TWO,          // クエスト用紙(下から二番目).
        PARCHMENT_THREE,        // クエスト用紙(下から三番目).
        SELECT_QUEST_NORMAL,    // 普通の難易度の項目.
        NORMAL_STRING,          // 普通の難易度の項目の文字.
        SELECT_QUEST_HARD,      // 難しい難易度の項目.
        HARD_STRING,            // 難しい難易度の項目の文字.
        SELECT_QUEST_TUTORIAL,  // チュートリアルの項目.
        TUTORIAL_STRING,        // チュートリアルの項目の文字.
        SELECTED_UI,            // 選択しているUI.
        QUEST_TITLE,
        //EXPLANATION,            // 説明テキスト.
        MAX_NUM                 // 最大数.
    }

    enum QuestTitle
    {
        NORMAL,
        HARD,
        TUTORIAL,
        MAX_NUM
    }

    private Sequence _sequence;

    // 各UI.
    [SerializeField, EnumIndex(typeof(UIAnimNum))]
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

    [SerializeField, EnumIndex(typeof(QuestTitle))]
    public Sprite[] _questSprite;
    // 選択したUI.
    private SelectSceneSelectUi _SelectUi;

    // クエストタイトルのサイズの値.
    private float _questTitleSizeValueX = 0;
    private float _questTitleSizeValueY = 0;
    // クエストタイトルのサイズ,
    private Vector2 _questTitleSize = Vector2.zero;

    // クエストタイトルの大きさの値.
    private float _questTitleScaleValue = 0;
    // クエストタイトルの大きさ.
    private Vector3 _questTitleScale = Vector3.zero;

    void Start()
    {
        _sequence = DOTween.Sequence();

        for (int UINum = 0; UINum < (int)UIAnimNum.MAX_NUM; UINum++)
        {
            _rectTransforms[UINum] = _ui[UINum].GetComponent<RectTransform>();
            _images[UINum] = _ui[UINum].GetComponent<Image>();
            _uiDisplay[UINum] = false;
            //_uiColorA[UINum] = 0;

            _images[UINum].color = new Color32(255, 255, 255, _uiColorA[UINum]);
            
        }

        for (int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _uiColorA[UINum] = 0;
        }

        for (int UINum = (int)UIAnimNum.SELECT_QUEST_NORMAL; UINum < (int)UIAnimNum.MAX_NUM; UINum++)
        {
            _uiColorA[UINum] = 255;
        }

        _questOpenCount = 0;
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _SelectUi = GameObject.Find("SelectDraw").GetComponent<SelectSceneSelectUi>();
    }

    void Update()
    {
        ColorInit();
    }

    private void FixedUpdate()
    {
        OpenCount();
        Anim();
        UIColor();
        QuestTitleSprite();
    }

    private void OnDisable()
    {
        UIDisable();
    }

    // 色の透明度を初期化.
    private void ColorInit()
    {
        if(_controllerManager._BButtonDown)
        {
            for (int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
            {
                _uiColorA[UINum] = 0;
            }
        }
    }

    // 非表示になる瞬間の処理.
    private void UIDisable()
    {
        _questOpenCount = 0;

        // 座標の初期化.
        for (int UINum = (int)UIAnimNum.PARCHMENT_ONE; UINum < (int)UIAnimNum.PARCHMENT_THREE + 1; UINum++)
        {
            _uiColorA[UINum] = 0;
            _rectTransforms[UINum].DORewind();
        }
    }

    // UIの色を代入.
    private void UIColor()
    {
        for (int UINum = 0; UINum < (int)UIAnimNum.MAX_NUM; UINum++)
        {
            _images[UINum].color = new Color32(255, 255, 255, _uiColorA[UINum]);
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

    // クエストの用紙のアニメーション.
    private void QuestPaperAnim()
    {
        // 紙の音を鳴らす.
        if(_questOpenCount == 1)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.QUEST_LIST);
        }
        // アニメーションを開始するタイミング指定.
        if (_questOpenCount >= 0)
        {
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_ONE, -10.0f);
        }
        if (_questOpenCount >= 5)
        {
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_TWO, 0.0f);
        }
        if (_questOpenCount >= 10)
        {
            QuestPaperAnim((int)UIAnimNum.PARCHMENT_THREE, 10.0f);
        }
    }

    // クエスト用紙のアニメーションテスト.
    private void QuestPaperAnim(int num, float RotateZ)
    {
        _sequence.Append(_rectTransforms[num].DOAnchorPos(new Vector3(414.0f, 0.0f, 0.0f), 0.5f));
        _sequence.Join(_rectTransforms[num].DORotate(new Vector3(0.0f, 0.0f, RotateZ), 0.5f));

        _sequence.Play();
        if (_uiColorA[num] < 255)
        {
            _uiColorA[num] += 5;
        }
    }

    // クエストタイトルの変更.
    private void QuestTitleSprite()
    {
        if (_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.EASY)
        {
            _images[(int)UIAnimNum.QUEST_TITLE].sprite = _questSprite[(int)QuestTitle.NORMAL];
            _questTitleSizeValueX = 306;
            _questTitleSizeValueY = 66;
            _questTitleScaleValue = 1.5f;
        }
        else if (_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.HATD)
        {
            _images[(int)UIAnimNum.QUEST_TITLE].sprite = _questSprite[(int)QuestTitle.HARD];
            _questTitleSizeValueX = 605;
            _questTitleSizeValueY = 67;
            _questTitleScaleValue = 1.1f;
        }
        else if (_SelectUi.GetSelectNumber() == (int)SelectSceneSelectUi.SelectItem.TUTORIAL)
        {
            _images[(int)UIAnimNum.QUEST_TITLE].sprite = _questSprite[(int)QuestTitle.TUTORIAL];
            _questTitleSizeValueX = 127;
            _questTitleSizeValueY = 46;
            _questTitleScaleValue = 2.0f;
        }

        _questTitleSize = new Vector2(_questTitleSizeValueX, _questTitleSizeValueY);
        _questTitleScale = new Vector3(_questTitleScaleValue, _questTitleScaleValue, _questTitleScaleValue);
        _rectTransforms[(int)UIAnimNum.QUEST_TITLE].sizeDelta = _questTitleSize;
        _rectTransforms[(int)UIAnimNum.QUEST_TITLE].localScale = _questTitleScale;
    }
}
