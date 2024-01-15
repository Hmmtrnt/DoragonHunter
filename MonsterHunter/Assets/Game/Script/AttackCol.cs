/*プレイヤーの攻撃判定*/

using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    private PlayerState _state;

    void Start()
    {
        _state = GameObject.Find("Hunter").GetComponent<PlayerState>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(/*_state._isCauseDamage &&*/ other.gameObject.tag == "Monster")
        //{
        //    Debug.Log("当たった");

        //    _state._isCauseDamage = false;

        //}

        //Debug.Log(other.gameObject.tag);
        //Debug.Log(_state._MonsterFleshy);

        if (other.gameObject.tag == "MonsterHead")
        {
            _state._MonsterFleshy = 1.2f;
            RenkiGaugeManager();

            //if(_state.CurrentState() == new StateRoundSlash())
            //{
            //    _state
            //}  
            _state._weaponActive = false;
            // ヒットストップ.
            // NOTE:コレジャナイ感、世界全体が止まっている.
            //HitStopManager.instance.StartHitStop(0.1f);
        }
        else if(other.gameObject.tag == "MonsterBody")
        {
            _state._MonsterFleshy = 1.0f;
            _state.RenkiGaugeFluctuation();
            _state._weaponActive = false;
        }
        else if (other.gameObject.tag == "MonsterWingRight")
        {
            _state._MonsterFleshy = 0.9f;
            _state.RenkiGaugeFluctuation();
            _state._weaponActive = false;
        }
        else if( other.gameObject.tag == "MonsterWingLeft")
        {
            _state._MonsterFleshy = 0.9f;
            _state.RenkiGaugeFluctuation();
            _state._weaponActive = false;
        }
        else if(other.gameObject.tag == "MonsterTail")
        {
            _state._MonsterFleshy = 1.1f;
            _state.RenkiGaugeFluctuation();
            _state._weaponActive = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if(other.gameObject.tag == "Monster")
        //{
        //    Debug.Log("通る");
        //    _col.enabled = true;
        //}
    }


    // 錬気ゲージ管理.
    private void RenkiGaugeManager()
    {
        if (_state.GetIsCauseDamage())
        {
            _state._currentRenkiGauge += _state._increaseAmountRenkiGauge;
            _state._maintainTimeRenkiGauge = _state._maintainTime;
            if (_state.GetRoundSlash())
            {
                _state._currentRedRenkiGauge = 100;
            }
        }
    }

}
