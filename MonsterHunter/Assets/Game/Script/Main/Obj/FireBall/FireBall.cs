/*ファイアーボールの制御*/

using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.UI.GridLayoutGroup;

public class FireBall : MonoBehaviour
{
    [Header("スピード")]
    [SerializeField] private float _speed;

    // ブレスの放たれる方向.
    private Vector3 _direction;

    // モンスターの情報.
    private GameObject _monster;
    // 着弾地点の爆発エフェクト.
    private GameObject _fireExplosion;
    private Rigidbody _rigidbody;
    private VisualEffect _effect;

    // なんの値かわかってないがVFXノードの一番最初にあるbool型なのはわかった
    private const string _fireballTrailsActiveString = "FireballTrailsActive";

    // 消滅する時間
    private int _destroyTime = 1000;

    void Start()
    {
        _monster = GameObject.FindWithTag("Monster");

        _direction = _monster.transform.forward;
        _fireExplosion = (GameObject)Resources.Load("FireExplosion");
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
            // 爆発エフェクトを生成.
            Instantiate(_fireExplosion, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
