/// <summary>
/// Stateの抽象クラス
/// </summary>

public abstract class StateBase
{
    /*プレイヤー*/
    /// <summary>
    /// ステート開始時呼び出し
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    /// <param name="prevState">ひとつ前の状態</param>
    public virtual void OnEnter(PlayerState owner, StateBase prevState) { }
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    public virtual void OnUpdate(PlayerState owner) { }
    /// <summary>
    /// FixedUpdate
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    public virtual void OnFixedUpdate(PlayerState owner) { }
    /// <summary>
    /// ステート終了時呼び出し
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    /// <param name="nextState">次の状態</param>
    public virtual void OnExit(PlayerState owner, StateBase nextState) { }
    /// <summary>
    /// ステート遷移の呼び出し
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    public virtual void OnChangeState(PlayerState owner) { }


    /*モンスター*/
    /// <summary>
    /// ステート開始時呼び出し
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    /// <param name="prevState">ひとつ前の状態</param>
    public virtual void OnEnter(MonsterState owner, StateBase prevState) { }
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    public virtual void OnUpdate(MonsterState owner) { }
    /// <summary>
    /// FixedUpdate
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    public virtual void OnFixedUpdate(MonsterState owner) { }
    /// <summary>
    /// ステート終了時呼び出し
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    /// <param name="nextState">次の状態</param>
    public virtual void OnExit(MonsterState owner, StateBase nextState) { }
    /// <summary>
    /// ステート遷移の呼び出し
    /// </summary>
    /// <param name="owner">アクセスするための参照</param>
    public virtual void OnChangeState(MonsterState owner) { }
}
