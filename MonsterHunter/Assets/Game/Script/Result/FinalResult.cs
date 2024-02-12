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
        MONSTER_ICON,
        MAX_NUM
    }

    private HuntingEnd _huntingEnd;
    // ���ʕ\���̂��߂�UI.
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

    // �N���A�����s���ŏ����𕪊�.
    private void BranchUpdate(bool QuestResult)
    {
        _ui[(int)UIKinds.MONSTER_ICON].SetActive(true);
        // ������.
        if(QuestResult) 
        {
            _ui[(int)UIKinds.CLEAR].SetActive(true);
            _ui[(int)UIKinds.FAILED].SetActive(false);
        }
        // ���s��.
        else 
        {
            _ui[(int)UIKinds.FAILED].SetActive(true);
            _ui[(int)UIKinds.CLEAR].SetActive(false);
        }
    }
}
