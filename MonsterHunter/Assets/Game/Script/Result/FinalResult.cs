/*�ŏI���ʂ̏���*/

using UnityEngine;
using UnityEngine.UI;

public class FinalResult : MonoBehaviour
{
    // �ŏI���ʂ̃X�^���v.
    enum UIKinds
    {
        CLEAR,
        FAILED,
        STRING,
        MAX_NUM
    }

    // �����̉摜.
    enum ResultStringImage
    {
        SUCCESS,
        FAILED,
        MAX_NUM
    }

    private HuntingEnd _huntingEnd;
    // ���ʕ\���̂��߂�UI.
    public GameObject[] _ui;
    // �����̉摜�̏��.
    private Image _stringImage;
    // �����̃X�v���C�g.
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

    // �N���A�����s���ŏ����𕪊�.
    private void BranchUpdate(bool QuestResult)
    {
        //_ui[(int)UIKinds.STRING].SetActive(true);
        // ������.
        if(QuestResult) 
        {
            _ui[(int)UIKinds.CLEAR].SetActive(true);
            _ui[(int)UIKinds.FAILED].SetActive(false);
            _stringImage.sprite = _string[(int)ResultStringImage.SUCCESS];
        }
        // ���s��.
        else 
        {
            _ui[(int)UIKinds.FAILED].SetActive(true);
            _ui[(int)UIKinds.CLEAR].SetActive(false);
            _stringImage.sprite = _string[(int)ResultStringImage.FAILED];
        }
    }
}
