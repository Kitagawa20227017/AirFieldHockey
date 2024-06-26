// ---------------------------------------------------------  
// EnemyAI.cs  
//   
// エージェントの行動決定処理
//
// 作成日: 2024/5/15
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class EnemyAI : Agent
{

    #region 変数  

    #region 定数

    // X軸の移動範囲
    private const float MOVE_POS_LIMIT_X = 9.20f;

    // X軸の移動可能位置(センターライン)
    private const float MOVE_POS_CENTER_X = 9.19f;

    // X軸の移動可能位置(壁側)
    private const float MOVE_POS_WALL_X = 0.19f;

    // Z軸の移動範囲
    private const float MOVE_POS_LIMIT_Z = 4.325f;

    // Z軸の移動可能位置
    private const float MOVE_POS_Z = 4.324f;

    // RayCastの位置
    private const float RAY_POS_ADVENT_SUITED = 0.45f;

    // RayCastの長さ
    private const float RAY_LENGTH = 0.1f;

    #endregion

    [SerializeField,Header("Packオブジェクト")]
    private GameObject _packObj = default;

    [SerializeField, Header("対戦相手")]
    private GameObject _enemyObj = default;

    [SerializeField, Header("PlayerGoalオブジェクト")]
    private GameObject _playerGoal = default;

    [SerializeField, Header("EnemyGoalオブジェクト")]
    private GameObject _enemyGoal = default;

    [SerializeField, Header("レイヤー指定")]
    private LayerMask _groundLayer;

    [SerializeField, Header("GameManegerスクリプト")]
    private GameManeger _gameManeger = default;

    // パックのRigidbody
    private Rigidbody _puckRb = default;

    // AIのRigidbody
    private Rigidbody _thisRb = default;

    // 移動スピード
    private float _moveSpeed = 12.5f;

    // プレイヤー操作かどうか
    private bool _isPlayer = false;

    // プレイヤー識別ID
    private enum PLAYER_ID
    {
        Player1 = 1,
        Player2 = 2,
    };

    // 指定出来るようにする
    [SerializeField]
    private PLAYER_ID _playerId = default;

    #endregion

    #region プロパティ  

    // プレイヤー操作かどうか
    public bool IsPlayer 
    { 
        get => _isPlayer; 
    }

    #endregion

    #region メソッド

    /// <summary>
    /// 更新前処理 
    /// </summary>
    public override void Initialize()
    {
        // パックのデータ取得
        _thisRb = this.gameObject.GetComponent<Rigidbody>();
        _puckRb = _packObj.GetComponent<Rigidbody>();
        _isPlayer = false;
    }

    /// <summary>
    /// エピソードが始まる前に呼ばれる(初期化処理)
    /// </summary>
    public override void OnEpisodeBegin()
    {
        // プレイヤーIDが2のときは反転させる
        float dir = ((int)_playerId == 2) ? 1.0f : -1.0f;

        // パックの位置を初期化
        if((int)_playerId == 1)
        {
            _gameManeger.Reset();
        }

        // プレイヤーの位置を初期化
        this.transform.localPosition = new Vector3(5 * dir, transform.localPosition.y, 0);
    }


    /// <summary>
    /// 学習するAIが見てるデータ
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        // プレイヤーIDが2のときは反転させる
        float dir = ((int)_playerId == 2) ? 1.0f : -1.0f;

        // パックの速度
        sensor.AddObservation(this._puckRb.velocity.x * dir);
        sensor.AddObservation(this._puckRb.velocity.y);
        sensor.AddObservation(this._puckRb.velocity.z * dir);

        // パックの位置
        sensor.AddObservation(this._packObj.transform.localPosition.x * dir);
        sensor.AddObservation(this._packObj.transform.localPosition.y);
        sensor.AddObservation(this._packObj.transform.localPosition.z * dir);

        // 自分の位置
        sensor.AddObservation(this.transform.localPosition.x * dir);
        sensor.AddObservation(this.transform.localPosition.y);
        sensor.AddObservation(this.transform.localPosition.z * dir);

        // 相手の位置
        sensor.AddObservation(this._enemyObj.transform.localPosition.x * dir);
        sensor.AddObservation(this._enemyObj.transform.localPosition.y);
        sensor.AddObservation(this._enemyObj.transform.localPosition.z * dir);

        // 自分のゴールの位置
        sensor.AddObservation(this._playerGoal.transform.localPosition.x * dir);
        sensor.AddObservation(this._playerGoal.transform.localPosition.y);
        sensor.AddObservation(this._playerGoal.transform.localPosition.z * dir);

        // 相手のゴールの位置
        sensor.AddObservation(this._enemyGoal.transform.localPosition.x * dir);
        sensor.AddObservation(this._enemyGoal.transform.localPosition.y);
        sensor.AddObservation(this._enemyGoal.transform.localPosition.z * dir);

    }

    /// <summary>
    /// AIの行動決定
    /// 更新頻度は、FixedUpdateと同じ(実質FixedUpdateとして使用できる)  
    /// </summary>
    /// <param name="actions">AIの行動</param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        // プレイヤーIDが2のときは反転させる
        float dir = ((int)_playerId == 2) ? 1.0f : -1.0f;

        // 左右の移動入力取得
        int action1 = actions.DiscreteActions[0];

        // 上下の移動入力取得
        int action2 = actions.DiscreteActions[1];

        // スピードUPの入力取得
        int action3 = actions.DiscreteActions[2];

        // 左右の移動量
        float movePosX = 0f;

        // 上下の移動量
        float movePosZ = 0f;

        // 移動量合計
        Vector3 movePos = default;
 
        // 加速の倍速    
        float speed = 1f;

        if (action1 == 1 && !RightWard())
        {
            movePosX = _moveSpeed;
        }
        else if (action1 == 2 && !LeftWard())
        {
            movePosX = -_moveSpeed;
        }

        if (action2 == 1 && !UpWard())
        {
            movePosZ = _moveSpeed;
        }
        else if (action2 == 2 && !DownWard())
        {
            movePosZ = -_moveSpeed;
        }

        if (action3 == 1)
        {
            speed = 1.5f;
        }

        // 移動量を求めて代入
        movePos = new Vector3(this.transform.position.x + movePosX * speed * Time.deltaTime, this.transform.position.y, this.transform.position.z + movePosZ * speed * Time.deltaTime);
        
        // 移動処理
        this._thisRb.MovePosition(movePos);

        // X軸の移動制限
        if (transform.localPosition.x * dir >= MOVE_POS_LIMIT_X)
        {
            transform.localPosition = new Vector3(MOVE_POS_CENTER_X * dir, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x * dir <= 0f)
        {
            transform.localPosition = new Vector3(MOVE_POS_WALL_X * dir, transform.localPosition.y, transform.localPosition.z);
        }

        // Z軸の移動制限
        if (transform.localPosition.z >= MOVE_POS_LIMIT_Z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, MOVE_POS_Z);
        }
        else if (transform.localPosition.z <= -MOVE_POS_LIMIT_Z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MOVE_POS_Z);
        }
    }


    /// <summary>
    /// プレイヤー操作
    /// </summary>
    /// <param name="actionsOut"></param>
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        _isPlayer = true;
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">当たったオブジェクト</param>
    private void OnCollisionEnter(Collision collision)
    {
        // パックに触れたとき報酬を与える
        if (collision.gameObject.tag == "Pack")
        {
            this.SetReward(0.1f);
        }
    }

    /// <summary>
    /// 上方向のめり込み防止処理
    /// </summary>
    /// <returns>壁かどうか</returns>
    private bool UpWard()
    {
        // 壁かどうか
        bool isWall = false;

        // Rayの出す位置
        Vector3 upWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + RAY_POS_ADVENT_SUITED);
        
        // Rayの角度指定
        Ray upAngle = new Ray(upWardRayPos, new Vector3(0, 0, 1));
        
        // Rayの格納
        RaycastHit upWardRay = default;

        // Rayが壁び当たったとき
        if (Physics.Raycast(upAngle, out upWardRay, RAY_LENGTH, _groundLayer))
        {
            isWall = true;
        }
        return isWall;
    }

    /// <summary>
    /// 下方向のめり込み防止処理
    /// </summary>
    /// <returns>壁かどうか</returns>
    private bool DownWard()
    {
        // 壁かどうか
        bool isWall = false;

        // Rayの出す位置
        Vector3 downWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - RAY_POS_ADVENT_SUITED);

        // Rayの出す位置
        Ray downAngle = new Ray(downWardRayPos, new Vector3(0, 0, -1));

        // Rayの格納
        RaycastHit downWardRay = default;

        // Rayが壁び当たったとき
        if (Physics.Raycast(downAngle, out downWardRay, RAY_LENGTH, _groundLayer))
        {
            isWall = true;
        }
        return isWall;
    }


    /// <summary>
    /// 右方向のめり込み防止処理
    /// </summary>
    /// <returns>壁かどうか</returns>
    private bool RightWard()
    {
        // 壁かどうか
        bool isWall = false;

        // Rayの出す位置
        RaycastHit rightRay = default;

        // Rayの出す位置
        Vector3 rightWardRayPos = new Vector3(transform.position.x + RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);

        // Rayの格納
        Ray rightAngle = new Ray(rightWardRayPos, new Vector3(1, 0, 0));

        // Rayが壁び当たったとき
        if (Physics.Raycast(rightAngle, out rightRay, RAY_LENGTH, _groundLayer))
        {
            isWall = true;
        }
        return isWall;
    }

    /// <summary>
    /// 左方向のめり込み防止処理
    /// </summary>
    /// <returns>壁かどうか</returns>
    private bool LeftWard()
    {
        // 壁かどうか
        bool isWall = false;

        // Rayの出す位置
        Vector3 leftWardRayPos = new Vector3(transform.position.x - RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);

        // Rayの出す位置
        Ray leftAngle = new Ray(leftWardRayPos, new Vector3(-1, 0, 0));

        // Rayの格納
        RaycastHit leftRay = default;

        // Rayが壁び当たったとき
        if (Physics.Raycast(leftAngle, out leftRay, RAY_LENGTH, _groundLayer))
        {
            isWall = true;
        }
        return isWall;
    }

    #endregion
}
