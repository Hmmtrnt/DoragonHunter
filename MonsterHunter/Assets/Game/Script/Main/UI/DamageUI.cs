using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private Text _damageText;

    [Header("フェードするスピード")]
    [SerializeField]private float _fadeSpeed = 1f;

    [Header("表記された際の移動速度")]
    [SerializeField] private float _moveSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        _damageText = GetComponent<Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position += Vector3.up * _moveSpeed * Time.deltaTime;

        _damageText.color = Color.Lerp(_damageText.color, new Color(1f,0f,0f,0f), _fadeSpeed * Time.deltaTime);

        if(_damageText.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
