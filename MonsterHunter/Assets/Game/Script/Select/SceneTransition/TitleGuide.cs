/*タイトル画面へ*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGuide : MonoBehaviour
{
    // ボタンガイド.
    public GameObject _guiedButton;

    // 範囲内にいるかどうか.
    private bool _collisionStay = false;

    void Start()
    {
        _collisionStay = false;
        _guiedButton.SetActive(_collisionStay);
    }

    private void FixedUpdate()
    {
        ButtonUIDraw();
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

    // ボタンのUIの描画.
    private void ButtonUIDraw()
    {
        _guiedButton.SetActive(_collisionStay);
    }
}
