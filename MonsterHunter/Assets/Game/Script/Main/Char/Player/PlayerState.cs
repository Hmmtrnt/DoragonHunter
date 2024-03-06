/*プレイヤー行動全体の管理*/

using UnityEngine;

public partial class PlayerState : MonoBehaviour
{
    void Start()
    {
        VariableInitialization();
        _currentState.OnEnter(this, null);
    }

    void Update()
    {
        StateTransitionFlag();

        GetStickInput();
        AnimTransition();

        _currentState.OnUpdate(this);
        if(!_mainSceneManager.GetPauseStop())
        {
            _currentState.OnChangeState(this);
        }
        viewAngle();
        StateFlameManager();
        StateTime();
    }

    private void FixedUpdate()
    {
        
        SubstituteVariableFixedUpdate();
        _currentState.OnFixedUpdate(this);

        // スタミナ.
        LimitStop(ref _stamina, _maxStamina);
        // 練気ゲージ.
        LimitStop(ref _currentRenkiGauge, _maxRenkiGauge);
        // 練気ゲージ赤.
        LimitStop(ref _currentRedRenkiGauge, _maxRedRenkiGauge);

        // 体力が0以下の時.
        if(_hitPoint <= 0)
        {
            OnDead();
        }

        // スタミナを回復させるタイミング指定.
        if(_autoRecaveryStaminaFlag)
        {
            AutoRecoveryStamina();
        }

        RenkiNaturalConsume();
        MaintainElapsedTimeRenkiGauge();
        ApplyRedRenkiGauge();
        OpenMenu();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // モンスターに当たっても浮かないようにする.
        if (collision.transform.tag == "Monster")
        {
            _transform.position = new Vector3 (_transform.position.x, 0.1f, _transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ダメージを受けつける.
        if(other.gameObject.tag == "MonsterAtCol" && _currentState != _damage)
        {
            // カウンターの受付をしていない時はダメージを受ける.
            if (!_counterValid)
            {
                OnDamage();
            }
            else if(_counterValid)
            {
                _counterSuccess = true;
            }
            
        }
    }
}