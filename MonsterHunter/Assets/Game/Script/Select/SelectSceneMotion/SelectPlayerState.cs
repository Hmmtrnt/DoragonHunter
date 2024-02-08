/*�I����ʂ̃n���^�[�̏���*/

using UnityEngine;

public partial class SelectPlayerState : MonoBehaviour
{
    
    private static readonly SelectStateIdle _idle = new();      // �A�C�h��.
    private static readonly SelectStateRunning _running = new();// ����.
    private static readonly SelectStateDash _dash = new();      // �_�b�V��.

    private StateBase _currentState = _idle;// ���݂̏��.

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
