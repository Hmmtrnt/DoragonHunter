/*選択画面のプレイヤーの関数まとめ*/

using UnityEngine;

public partial class SelectPlayerState
{
    // 初期化.
    private void Initialization()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _receptionFlag = GameObject.Find("ReceptioFlag").GetComponent<ReceptionFlag>();
        _titleGuide = GameObject.Find("TitleGuide").GetComponent<TitleGuide>();
        _rankTable = GameObject.Find("RankTableGuide").GetComponent<RankTable>();
        _input = GameObject.FindWithTag("Manager").GetComponent<ControllerManager>();
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _transform = transform;
    }

    /// <summary>
    /// 状態遷移
    /// </summary>
    /// <param name="nextState">次の変更する状態</param>
    private void ChangeState(StateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    // アニメーション遷移.
    private void AnimTransition()
    {
        if(_animator == null) { return; }

        _animator.SetFloat("Speed", _currentRunSpeed);

        _animator.SetBool("Idle", _idleMotion);
        _animator.SetBool("Run", _runMotion);
        _animator.SetBool("Dash", _dashMotion);
    }

    // 常に情報を代入.
    private void SubstituteVariableFixedUpdate()
    {
        // カメラの正面.
        _cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1.0f,0.0f, 1.0f)).normalized;

        /*カメラの向きから移動方向取得*/
        // 正面.
        Vector3 moveForward = _cameraForward * _leftStickVertical;
        // 横.
        Vector3 moveSide = _camera.transform.right * _leftStickHorizontal;
        // 速度の代入,
        _moveVelocity = moveForward + moveSide;

        // リストを開いているかどうか代入.
        _openMenu = _receptionFlag.GetOpenQuestList() || 
            _titleGuide.GetSceneTransitionUIOpen() || 
            _rankTable.GetRankTableUI();
    }

    // 左スティックの入力情報取得.
    private void GetStickInput()
    {
        // 入力情報代入.
        _leftStickHorizontal = _input._LeftStickHorizontal;
        _leftStickVertical = _input._LeftStickVertical;
    }

    // 移動時の回転処理.
    private void RotateDirection()
    {
        _transform.forward = Vector3.Slerp(_transform.forward, _moveVelocity, Time.deltaTime * _rotateSpeed);
    }
}