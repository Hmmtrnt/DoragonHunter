/*最終結果の処理*/

using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.UI;

public class FinalResult : MonoBehaviour
{
    // 最終結果のスタンプ.
    enum UIKinds
    {
        CLEAR,
        FAILED,
        MONSTER_ICON,
        MAX_NUM
    }

    private HuntingEnd _huntingEnd;
    private ResultUpdate _resultUpdate;
    // 結果表示のためのUI.
    public GameObject[] _ui;
    private RectTransform[] _rectTransform = new RectTransform[(int)UIKinds.MAX_NUM];
    private SEManager _seManager;
    // 時間.
    private int _countUp = 0;
    // SEを一度しか鳴らさない.
    private bool _playSEFlag = true;

    void Start()
    {
        _resultUpdate = GameObject.Find("ResultBackGround").GetComponent<ResultUpdate>();
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        for(int UINum = 0;  UINum < (int)UIKinds.MAX_NUM; UINum++)
        {
            _rectTransform[UINum] = _ui[UINum].GetComponent<RectTransform>();
            _ui[UINum].SetActive(false);
        }
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        CountUp();
        _ui[(int)UIKinds.MONSTER_ICON].SetActive(true);

        

        if(_resultUpdate._animEnd)
        {
            if (_huntingEnd.GetQuestClear())
            {
                AnimEnd((int)UIKinds.CLEAR);
            }
            else
            {
                AnimEnd((int)UIKinds.FAILED);
            }
            BranchUpdate(_huntingEnd.GetQuestClear());
            
        }
        else
        {
            if (_countUp >= 350)
            {
                if (_huntingEnd.GetQuestClear())
                {
                    StampAnim((int)UIKinds.CLEAR);
                }
                else
                {
                    StampAnim((int)UIKinds.FAILED);
                }
                BranchUpdate(_huntingEnd.GetQuestClear());

            }
        }
    }

    // 時間経過.
    private void CountUp()
    {
        _countUp++;
    }

    // クリアか失敗かで処理を分岐.
    private void BranchUpdate(bool QuestResult)
    {
        
        // 成功時.
        if(QuestResult) 
        {
            _ui[(int)UIKinds.CLEAR].SetActive(true);
            _ui[(int)UIKinds.FAILED].SetActive(false);
        }
        // 失敗時.
        else 
        {
            _ui[(int)UIKinds.FAILED].SetActive(true);
            _ui[(int)UIKinds.CLEAR].SetActive(false);
        }
    }

    // スタンプロゴのアニメーション.
    private void StampAnim(int num)
    {
        _rectTransform[num].DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f).SetEase(Ease.OutElastic);
        if (!_playSEFlag) return;

        _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.STAMP_PUSH);
        _playSEFlag = false;
    }

    private void AnimEnd(int num)
    {
        _rectTransform[num].localScale = new Vector3(0.4f, 0.4f, 0.4f);
        _playSEFlag = false;
    }
}
