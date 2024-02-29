/*�_���[�W�\�L*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private Text _damageText;
    private PlayerState _player;

    private RectTransform _rectTransform;

    private Transform _DamageUIWorldPos;

    private Camera _targetCamera;

    private Vector3 _screenPosition;

    // �t�F�[�h�A�E�g����X�s�[�h.
    public float _fadeOutSpeed = 1f;
    // ��Ɉړ��n.
    public float _moveSpeed = 0.4f;
    // ��x������ʂ�����ʂ�Ȃ��悤�ɂ���.
    public bool _isProcess = false;
    // �_���[�W�̒l���󂯎��^�C�~���O�����炷���߂̕ϐ�.
    private int _getDamageTiming = 0;


    void Start()
    {
        _damageText = GetComponentInChildren<Text>();
        _player = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _rectTransform = GetComponent<RectTransform>();
        _DamageUIWorldPos = GameObject.Find("EffectSpawnPosition").GetComponent<Transform>();
        _targetCamera = GameObject.Find("Camera").GetComponent<Camera>();
        _isProcess = false;

        _screenPosition = _targetCamera.WorldToScreenPoint(_DamageUIWorldPos.position);


        // TODO:�Ȃ����l��0�ɂȂ�
        //float screenwidth = 800 / 1920;
        //float screenLength = 450 / 1080;

        _screenPosition = new Vector3(_screenPosition.x * 0.416f, _screenPosition.y * 0.416f, _screenPosition.z);

        transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

        _getDamageTiming = 0;
    }

    void Update()
    {
        if(_getDamageTiming == 1)
        {
            GetDamage();
        }

        _getDamageTiming++;

        //Debug.Log(_player.GetHunterAttack());

        //transform.LookAt(Camera.main.transform);
        //transform.position += Vector3.up * _moveSpeed * Time.deltaTime;

        Vector3 pos = Vector3.up * _moveSpeed * Time.deltaTime;

        _rectTransform.anchoredPosition = _screenPosition;

        

        _damageText.color =
            Color.Lerp(_damageText.color,
            new Color(1.0f, 200.0f / 255.0f, 0.0f, 0.0f),
            _fadeOutSpeed * Time.deltaTime);

        if (_damageText.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �_���[�W�ʂ��擾.
    /// </summary>
    public void GetDamage()
    {
        if (!_isProcess)
        {
            _damageText.text = Convert.ToString((int)_player.GetHunterAttack());
            _isProcess = true;
        }
    }
}
