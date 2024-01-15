/*プレイヤーの攻撃判定*/

using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    private PlayerState _state;
    private PlayerHitStopManager _hitStop;
    private SEManager _seManager;
    private SEManager.SE _se;
    // 攻撃ヒットエフェクトのプレハブ取得.
    GameObject _HitEffectObject;
    // 攻撃ヒットエフェクトの生成位置.
    GameObject _HitEffectPosition;

    void Start()
    {
        _state = GameObject.Find("Hunter").GetComponent<PlayerState>();
        _hitStop = GameObject.Find("HitStopManager").GetComponent<PlayerHitStopManager>();
        _seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        // 攻撃ヒットエフェクトのプレハブ取得.
        _HitEffectObject = (GameObject)Resources.Load("HunterHitEffect");
        _HitEffectPosition = GameObject.Find("EffectSpawnPosition");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MonsterHead")
        {
            CauseDamageUpdate(1.2f);
        }
        else if(other.gameObject.tag == "MonsterBody")
        {
            CauseDamageUpdate(1.0f);
        }
        else if (other.gameObject.tag == "MonsterWingRight")
        {
            CauseDamageUpdate(0.9f);
        }
        else if( other.gameObject.tag == "MonsterWingLeft")
        {
            CauseDamageUpdate(0.9f);
        }
        else if(other.gameObject.tag == "MonsterTail")
        {
            CauseDamageUpdate(1.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    // ダメージを与えた瞬間、肉質を変化やヒットストップ追加.
    // TODO:変数名が決まってない.
    /// <summary>
    /// ダメージを与えた瞬間に更新する
    /// </summary>
    /// <param name="monsterFleshy">モンスターの肉質</param>
    private void CauseDamageUpdate(float monsterFleshy)
    {
        _state._MonsterFleshy = monsterFleshy;
        _state.RenkiGaugeFluctuation();
        _state._weaponActive = false;
        // プレイヤーのヒットストップ.
        _hitStop.StartHitStop(_state._hitStopTime);

        SEPlay();
        AttackEffectSpawn();

        // インスタンス生成.
        //Instantiate(_HitEffectObject, _HitEffectPosition.transform);
    }

    // 攻撃SEを流す.
    private void SEPlay()
    {
        // 気大回転刃斬りの時音を変更.
        if (_state.GetRoundSlash())
        {
            _seManager.PlaySE((int)SEManager.SE.RoundSlash);
        }
        else
        {
            _seManager.PlaySE((int)SEManager.SE.Slash);
        }
    }

    // 攻撃ヒット時エフェクトを生成.
    private void AttackEffectSpawn()
    {
        
        // 生成.
        Instantiate(_HitEffectObject, _HitEffectPosition.transform.position, Quaternion.identity);
    }
}
