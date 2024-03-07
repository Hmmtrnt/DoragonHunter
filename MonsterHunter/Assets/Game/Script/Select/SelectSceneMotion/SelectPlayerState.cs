/*選択画面のハンターの処理*/

using UnityEngine;

public partial class SelectPlayerState : MonoBehaviour
{
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
        _currentState.OnChangeState(this);
    }


    private void FixedUpdate()
    {
        SubstituteVariableFixedUpdate();
        _currentState.OnFixedUpdate(this);
    }

}
