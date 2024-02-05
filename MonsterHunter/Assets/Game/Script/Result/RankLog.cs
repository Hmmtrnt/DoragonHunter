/*リザルト画面のロゴ*/

using UnityEngine;
using UnityEngine.UI;

public class RankLog : MonoBehaviour
{
    // ランクの種類.
    enum SpriteRank
    {
        S_RANK,
        A_RANK,
        B_RANK,
        C_RANK,
        NONE,
        MAX_RANK
    }

    // クエスト終了時の情報取得.
    private HuntingEnd _huntingEnd;
    // スプライト変更のために取得.
    private Image _rankLog;
    // 座標を取得.
    private RectTransform _rectTransform;
    // ランクのスプライト.
    public Sprite[] _rank;

    // ランクの段階(数が小さいほどランクが高い).
    private int _rankStep;


    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        _rankLog = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RankSpriteSubstitute();
    }

    // スプライトを代入.
    private void RankSpriteSubstitute()
    {
        if(_huntingEnd.GetQuestClear())
        {
            // ランクの結果を代入.
            _rankStep = _huntingEnd.GetRank();
            _rankLog.sprite = _rank[_rankStep];
        }
        else
        {
            // 失敗時に-を代入.
            _rankLog.sprite = _rank[(int)SpriteRank.NONE];
        }
        
        RankSizeChange();
    }

    // ランクによって描画の大きさを変更.
    private void RankSizeChange()
    {
        if(_rankLog.sprite == _rank[(int)SpriteRank.NONE])
        {
            _rectTransform.sizeDelta = new Vector2(56, 19);
        }
        else if(_huntingEnd.GetRank() == (int)SpriteRank.S_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(276, 319);
        }
        else if (_huntingEnd.GetRank() == (int)SpriteRank.A_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(210, 252);
        }
        else if (_huntingEnd.GetRank() == (int)SpriteRank.B_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(190, 241);
        }
        else if (_huntingEnd.GetRank() == (int)SpriteRank.C_RANK)
        {
            _rectTransform.sizeDelta = new Vector2(205, 200);
        }
    }
}
