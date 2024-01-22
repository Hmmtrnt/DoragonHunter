/*足煙の処理*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSmoke : MonoBehaviour
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

        if (_countLife == 50)
        {
            Destroy(gameObject);
        }

        if(_monsterState._currentRotateAttack)
        {
            transform.position = _monsterState._footSmokePosition[2].transform.position;
        }
    }
}
