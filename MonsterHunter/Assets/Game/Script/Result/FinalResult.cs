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
        STRING,
        MAX_NUM
    }

    // 文字の画像.
    enum ResultStringImage
    {
        SUCCESS,
        FAILED,
        MAX_NUM
    }

    private HuntingEnd _huntingEnd;
    // 結果表示のためのUI.
    public GameObject[] _ui;
    // 文字の画像の情報.
    private Image _stringImage;
    // 文字のスプライト.
    public Sprite[] _string;


    void Start()
    {
        _huntingEnd = GameObject.Find("GameManager").GetComponent<HuntingEnd>();
        for(int UINum = 0;  UINum < (int)UIKinds.MAX_NUM; UINum++)
        {
            _ui[UINum].SetActive(false);
        }
        _stringImage = _ui[(int)UIKinds.STRING].GetComponent<Image>();
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
        //_ui[(int)UIKinds.STRING].SetActive(true);
        // 成功時.
        if(QuestResult) 
        {
            _ui[(int)UIKinds.CLEAR].SetActive(true);
            _ui[(int)UIKinds.FAILED].SetActive(false);
            _stringImage.sprite = _string[(int)ResultStringImage.SUCCESS];
        }
        // 失敗時.
        else 
        {
            _ui[(int)UIKinds.FAILED].SetActive(true);
            _ui[(int)UIKinds.CLEAR].SetActive(false);
            _stringImage.sprite = _string[(int)ResultStringImage.FAILED];
        }
    }
}
