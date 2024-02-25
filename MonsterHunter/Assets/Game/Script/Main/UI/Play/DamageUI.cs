/*�_���[�W�\�L*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private Text _damageText;
    private Player _player;
    // �t�F�[�h�A�E�g����X�s�[�h.
    public float _fadeOutSpeed = 1f;
    // ��Ɉړ��n.
    public float _moveSpeed = 0.4f;
    // ��x������ʂ�����ʂ�Ȃ��悤�ɂ���.
    private bool _isProcess = false;

    void Start()
    {
        _damageText = GetComponentInChildren<Text>();
        _player = GameObject.Find("Hunter").GetComponent<Player>();
        
        _isProcess = false;
    }

    void Update()
    {
        if (!_isProcess)
        {
            _damageText.text = Convert.ToString((int)_player.GetHunterAttack());
            _isProcess = true;
        }

        transform.LookAt(Camera.main.transform);
        transform.position += Vector3.up * _moveSpeed * Time.deltaTime;

        _damageText.color = 
            Color.Lerp(_damageText.color, 
            new Color(1.0f, 200.0f / 255.0f, 0.0f, 0.0f), 
            _fadeOutSpeed * Time.deltaTime);

        if(_damageText.color.a <= 0.1f )
        {
            Destroy(gameObject);
        }
    }
}
