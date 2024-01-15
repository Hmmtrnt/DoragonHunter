/*プレイヤーの攻撃判定*/

using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    private PlayerState _state;
    private HitStopManager _hitStop;

    void Start()
    {
        _state = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _hitStop = GameObject.Find("HitStopManager").GetComponent<HitStopManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MonsterHead")
        {
            FleshyChange(1.2f);

            // ヒットストップ.
            // NOTE:コレジャナイ感、世界全体が止まっている.
            _hitStop.StartHitStop(0.1f);
        }
        else if(other.gameObject.tag == "MonsterBody")
        {
            FleshyChange(1.0f);
        }
        else if (other.gameObject.tag == "MonsterWingRight")
        {
            FleshyChange(0.9f);
        }
        else if( other.gameObject.tag == "MonsterWingLeft")
        {
            FleshyChange(0.9f);
        }
        else if(other.gameObject.tag == "MonsterTail")
        {
            FleshyChange(1.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    // ダメージを与えた瞬間、肉質を変化.
    // TODO:変数名が決まってない.
    private void FleshyChange(float monsterFleshy)
    {
        _state._MonsterFleshy = monsterFleshy;
        _state.RenkiGaugeFluctuation();
        _state._weaponActive = false;
    }
}
