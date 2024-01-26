/*�t�@�C�A�[�{�[���̐���*/

using UnityEngine;
using UnityEngine.VFX;

public class FireBall : MonoBehaviour
{
    [Header("�X�s�[�h")]
    [SerializeField] private float _speed;

    // �u���X�̕���������.
    private Vector3 _direction;

    private GameObject _monster;
    private Rigidbody _rigidbody;
    private VisualEffect _effect;

    // �Ȃ�̒l���킩���ĂȂ���VFX�m�[�h�̈�ԍŏ��ɂ���bool�^�Ȃ̂͂킩����
    private const string _fireballTrailsActiveString = "FireballTrailsActive";

    // ���ł��鎞��
    private int _destroyTime = 100;

    void Start()
    {
        _monster = GameObject.FindWithTag("Monster");

        _direction = _monster.transform.forward;

        _rigidbody = GetComponent<Rigidbody>();
        _effect = GetComponent<VisualEffect>();

        // ������^����
        _rigidbody.AddForce(_direction * _speed);

        if(_effect != null )
        {
            _effect.SetBool(_fireballTrailsActiveString, true);
        }
        
    }

    private void FixedUpdate()
    {
        // ���R����
        _destroyTime--;
        if(_destroyTime == 0)
        {
            Destroy(gameObject);
            _destroyTime = 100;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.tag == "Player")
        //{
        //    Destroy(gameObject);
        //}
    }
}
