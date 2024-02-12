/*最終結果の処理*/

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
    // 結果表示のためのUI.
    public GameObject[] _ui;


    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        for(int UINum = 0;  UINum < (int)UIKinds.MAX_NUM; UINum++)
        {
            _ui[UINum].SetActive(false);
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        BranchUpdate(_huntingEnd.GetQuestClear());
    }

    // クリアか失敗かで処理を分岐.
    private void BranchUpdate(bool QuestResult)
    {
        _ui[(int)UIKinds.MONSTER_ICON].SetActive(true);
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
}
