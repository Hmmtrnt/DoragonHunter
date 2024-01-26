/*ファイアーボールの制御*/

using UnityEngine;
using UnityEngine.VFX;

public class FireBall : MonoBehaviour
{
    [Header("スピード")]
    [SerializeField] private float _speed;

    // ブレスの放たれる方向.
    private Vector3 _direction;

    private GameObject _monster;
    private Rigidbody _rigidbody;
    private VisualEffect _effect;

    // なんの値かわかってないがVFXノードの一番最初にあるbool型なのはわかった
    private const string _fireballTrailsActiveString = "FireballTrailsActive";

    // 消滅する時間
    private int _destroyTime = 100;

    void Start()
    {
        _monster = GameObject.FindWithTag("Monster");

        _direction = _monster.transform.forward;

        _rigidbody = GetComponent<Rigidbody>();
        _effect = GetComponent<VisualEffect>();

        // 初速を与える
        _rigidbody.AddForce(_direction * _speed);

        if(_effect != null )
        {
            _effect.SetBool(_fireballTrailsActiveString, true);
        }
        
    }

    private void FixedUpdate()
    {
        // 自然消滅
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
