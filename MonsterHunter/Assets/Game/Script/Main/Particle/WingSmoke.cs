/*翼煙の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingSmoke : MonoBehaviour
{
    // 生存時間
    private int _countLife;
    // モンスター情報.
    private MonsterState _monsterState;

    void Start()
    {
        _monsterState = GameObject.Find("Dragon").GetComponent<MonsterState>();

    }

    private void FixedUpdate()
    {
        _countLife++;

        // パーティクルを消去する.
        if (_countLife == 50)
        {
            Destroy(gameObject);
        }

        ParticlePositionDesignation();
    }

    // パーティクルの座標指定.
    private void ParticlePositionDesignation()
    {
        // それぞれの状態の情報によってパーティクルをモデルのメッシュに固定.
        if (_monsterState._currentWingAttackLeft)
        {
            transform.position = _monsterState._footSmokePosition[1].transform.position;
        }
        if (_monsterState._currentWingAttackRight)
        {
            transform.position = _monsterState._footSmokePosition[0].transform.position;
        }
    }
}
