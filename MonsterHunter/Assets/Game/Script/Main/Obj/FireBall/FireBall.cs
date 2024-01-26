/*�t�@�C�A�[�{�[���̐���*/

using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.UI.GridLayoutGroup;

public class FireBall : MonoBehaviour
{
    [Header("�X�s�[�h")]
    [SerializeField] private float _speed;

    // �u���X�̕���������.
    private Vector3 _direction;

    // �����X�^�[�̏��.
    private GameObject _monster;
    // ���e�n�_�̔����G�t�F�N�g.
    private GameObject _fireExplosion;
    private Rigidbody _rigidbody;
    private VisualEffect _effect;

    // �Ȃ�̒l���킩���ĂȂ���VFX�m�[�h�̈�ԍŏ��ɂ���bool�^�Ȃ̂͂킩����
    private const string _fireballTrailsActiveString = "FireballTrailsActive";

    // ���ł��鎞��
    private int _destroyTime = 1000;

    void Start()
    {
        _monster = GameObject.FindWithTag("Monster");

        _direction = _monster.transform.forward;
        _fireExplosion = (GameObject)Resources.Load("FireExplosion");
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
            _destroyTime = 1000;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "Player")
        //{
        //    Instantiate(_fireExplosion, transform.position, Quaternion.identity);

        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // �����G�t�F�N�g�𐶐�.
            Instantiate(_fireExplosion, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
