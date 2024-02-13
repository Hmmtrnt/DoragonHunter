/*��t�J�n*/

using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ReceptionFlag : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;

    // ��t���.
    public GameObject _ReceptionistList;
    // �{�^��UI.
    public GameObject _buttonUI;
    // �t�F�[�h.
    private Fade _fade;

    // ��t�̑O�ɂ���.
    private bool _receptionistFlag = false;
    // �N�G�X�g���X�g�̊J��.
    private bool _openAndClose = false;
    

    void Start()
    {
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _receptionistFlag = false;
        _openAndClose = false;
        _ReceptionistList.SetActive(_openAndClose);
        _buttonUI.SetActive(false);
        _fade = GameObject.Find("Fade").GetComponent<Fade>();
    }

    void Update()
    {
        QuestListOpenAndClose();
    }

    private void FixedUpdate()
    {
        QuestListDraw();

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _receptionistFlag = true;
            _buttonUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _receptionistFlag = false;
            _buttonUI.SetActive(false);
        }
    }

    // �N�G�X�g���X�g�̕\����\��.
    private void QuestListDraw()
    {
        _ReceptionistList.SetActive(_openAndClose);
    }

    // �N�G�X�g���X�g���J��.
    private void QuestListOpenAndClose()
    {
        if(!_receptionistFlag) { return; }

        // �J��.
        if(!_openAndClose)
        {
            if(_controllerManager._AButtonDown)
            {
                _openAndClose = true;
            }
        }
        else
        {
            if(_controllerManager._BButtonDown && _fade._isFading)
            {
                _openAndClose = false;
            }
        }
    }

    // �N�G�X�g���X�g���J���Ă��邩�ǂ���.
    public bool GetOpenQuestList() {  return _openAndClose; } 
}
