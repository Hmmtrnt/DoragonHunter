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
    public GameObject[] _UI;
    // 各UIの座標.
    private RectTransform[] _rectTransforms = new RectTransform[(int)UIAnimNum.MAX_NUM];
    // SE.
    private SEManager _seManager;

    // 各UIの表示非表示.
    private bool[] _uiDisplay = new bool[(int)UIAnimNum.MAX_NUM];

    // クエストを開いてからの経過時間．
    private int _questOpenCount = 0;


    void Start()
    {
        for(int UINum = 0; UINum < (int)UIAnimNum.MAX_NUM; UINum++)
        {
            _rectTransforms[UINum] = _UI[UINum].GetComponent<RectTransform>();
            _uiDisplay[UINum] = false;
        }
        _questOpenCount = 0;
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        OpenCount();
    }

    private void OnDisable()
    {
        // 非表示にするときに初期位置に設定.
        UIDisable();
    }

    // 非表示になる瞬間の処理.
    private void UIDisable()
    {
        _questOpenCount = 0;
    }

    // クエストリストを開いたときに時間経過をさせる.
    private void OpenCount()
    {
        _questOpenCount++;
    }

    // 全体のアニメーション.
    private void Anim(int layerIndex)
    {
        
    }

    // クエストの用紙のアニメーション.
    private void QuestPaperAnim()
    {
        if(_questOpenCount == 0 )
        {
            _rectTransforms[(int)UIAnimNum.PARCHMENT_ONE].DOAnchorPos(new Vector3(414.0f, 0.0f, 0.0f), 0.5f).SetEase(Ease.OutQuad);

        }
    }

    // クエストの項目のアニメーション.

    // クエストの説明のアニメーション.
}
