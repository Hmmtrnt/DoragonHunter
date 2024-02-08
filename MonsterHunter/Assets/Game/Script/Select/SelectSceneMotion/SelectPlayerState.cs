/*選択画面のハンターの処理*/

using UnityEngine;

public partial class SelectPlayerState : MonoBehaviour
{
    
    private static readonly SelectStateIdle _idle = new();      // アイドル.
    private static readonly SelectStateRunning _running = new();// 走る.
    private static readonly SelectStateDash _dash = new();      // ダッシュ.

    private StateBase _currentState = _idle;// 現在の状態.

    void Start()
    {
        Initialization();
        _currentState.OnEnter(this, null);
    }

    private void Update()
    {
        GetStickInput();
        AnimTransition();


        _currentState.OnUpdate(this);

        //if(!_openMenu)
        //{
        //    _currentState.OnChangeState(this);
        //}
        //else
        //{
        //    _currentState = _idle;
        //}
        _currentState.OnChangeState(this);
    }


    private void FixedUpdate()
    {
        StateFlameManager();
        SubstituteVariableFixedUpdate();
        _currentState.OnFixedUpdate(this);
    }

}
