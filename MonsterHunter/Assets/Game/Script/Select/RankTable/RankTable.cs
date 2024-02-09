/*ランク表*/

using UnityEngine;

public class RankTable : MonoBehaviour
{
    // パッドの入力情報.
    private ControllerManager _controllerManager;
    // ランク表.
    public GameObject _rankTable;
    // ボタンガイド.
    public GameObject _guiedButton;

    // 範囲内にいるかどうか.
    private bool _collisionStay = false;
    // UIの開閉.
    private bool _UIOpenAndClose = false;

    void Start()
    {
        _controllerManager = GameObject.Find("GameManager").GetComponent<ControllerManager>();
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

    // UIの描画.
    private void UIDraw()
    {
        _guiedButton.SetActive(_collisionStay);
        _rankTable.SetActive(_UIOpenAndClose);
    }

    // タイトル画面へ戻るUIの開閉.
    private void SceneTransitionUIOpenAndClose()
    {
        if (!_collisionStay) { return; }

        if (_controllerManager._AButtonDown)
        {
            _UIOpenAndClose = true;
        }
        else if (_controllerManager._BButtonDown)
        {
            _UIOpenAndClose = false;
        }

    }

    public void SetSceneTransitionUIOpen(bool flag) { _UIOpenAndClose = flag; }

    public bool GetSceneTransitionUIOpen() { return _UIOpenAndClose; }
}
