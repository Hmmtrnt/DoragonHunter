/*�^�C�g����ʂ�*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGuide : MonoBehaviour
{
    // �{�^���K�C�h.
    public GameObject _guiedButton;

    // �͈͓��ɂ��邩�ǂ���.
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

    // �{�^����UI�̕`��.
    private void ButtonUIDraw()
    {
        _guiedButton.SetActive(_collisionStay);
    }
}
