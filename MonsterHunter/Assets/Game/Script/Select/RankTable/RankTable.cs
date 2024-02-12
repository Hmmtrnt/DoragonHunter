/*�����N�\*/

using UnityEngine;

public class RankTable : MonoBehaviour
{
    // �p�b�h�̓��͏��.
    private ControllerManager _controllerManager;
    // �����N�\.
    public GameObject _rankTable;
    // �{�^���K�C�h.
    public GameObject _guiedButton;
    // ���ʉ�.
    private SEManager _seManager;

    // �͈͓��ɂ��邩�ǂ���.
    private bool _collisionStay = false;
    // UI�̊J��.
    private bool _UIOpenAndClose = false;

    void Start()
    {
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        _collisionStay = false;
        _UIOpenAndClose = false;
        _guiedButton.SetActive(_collisionStay);
        _rankTable.SetActive(_UIOpenAndClose);
    }

    void Update()
    {
        SceneTransitionUIOpenAndClose();
    }

    private void FixedUpdate()
    {
        UIDraw();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _collisionStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _collisionStay = false;
        }
    }

    // UI�̕`��.
    private void UIDraw()
    {
        _guiedButton.SetActive(_collisionStay);
        _rankTable.SetActive(_UIOpenAndClose);
    }

    // �^�C�g����ʂ֖߂�UI�̊J��.
    private void SceneTransitionUIOpenAndClose()
    {
        if (!_collisionStay) { return; }

        if (_controllerManager._AButtonDown)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.DECISION);
            _UIOpenAndClose = true;
        }
        else if (_controllerManager._BButtonDown)
        {
            _seManager.UIPlaySE((int)SEManager.AudioNumber.AUDIO2D, (int)SEManager.UISE.REMOVE_PUSH);
            _UIOpenAndClose = false;
        }

    }

    public void SetSceneTransitionUIOpen(bool flag) { _UIOpenAndClose = flag; }

    public bool GetRankTableUI() { return _UIOpenAndClose; }
}