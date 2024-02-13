/*受付開始*/

using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ReceptionFlag : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;

    // 受付画面.
    public GameObject _ReceptionistList;
    // ボタンUI.
    public GameObject _buttonUI;
    // フェード.
    private Fade _fade;

    // 受付の前にいる.
    private bool _receptionistFlag = false;
    // クエストリストの開閉.
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

    // クエストリストの表示非表示.
    private void QuestListDraw()
    {
        _ReceptionistList.SetActive(_openAndClose);
    }

    // クエストリストを開閉.
    private void QuestListOpenAndClose()
    {
        if(!_receptionistFlag) { return; }

        // 開く.
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

    // クエストリストを開いているかどうか.
    public bool GetOpenQuestList() {  return _openAndClose; } 
}
