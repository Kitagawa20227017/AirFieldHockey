// ---------------------------------------------------------  
// GameManeger.cs  
//   
// ゲーム管理
//
// 作成日: 2024/5/13
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using Unity.MLAgents;

public class GameManeger : MonoBehaviour
{

    #region 変数  

    [SerializeField, Header("Packオブジェクト")]
    private GameObject _pack = default;

    [SerializeField,Header("エージェント")]
    private Agent[] _agent = default;

    [SerializeField,Header("EnemyAIスクリプト")]
    private EnemyAI[] _enemyAI = default;

    [SerializeField, Header("勝利点数")]
    private int _goalConut = 5;

    [SerializeField,Header("GameSceneUIスクリプト")]
    private GameSceneUI _gameSceneUI = default;

    // Rigidbody格納用
    private Rigidbody _rigidbody;

    // プレイヤーの得点
    private int _playerGoalConut = 0;

    // 相手の得点
    private int _enemyGoalConut = 0;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
    {
        Time.timeScale = 1;

        // Rigidbody取得
        _rigidbody = _pack.GetComponent<Rigidbody>();
        _playerGoalConut = 0;
        _enemyGoalConut = 0;
    }

    /// <summary>
    /// ゴールした後の初期化処理
    /// </summary>
    public void Reset()
    {
        // パックを止める
        this._rigidbody.velocity = new Vector3(0, 0, 0);
        
        // センターのランダムな座標Zにパックを配置
        float random = Random.Range(-4.324f, 4.324f);
        this._pack.transform.localPosition = new Vector3(0, -1.15f, random);
    }
    
    /// <summary>
    /// ゴール処理
    /// </summary>
    /// <param name="Id">プレイヤーID</param>
    public void Goal(int Id)
    {
        // それぞれのエージェントに報酬を与えて得点を増やす
        if (Id == 1)
        {
            this._agent[0].AddReward(1f);

            // プレイヤー操作のとき
            if(!_enemyAI[1].IsPlayer)
            {
                this._agent[1].AddReward(-1.2f);
            }

            _enemyGoalConut++;
            _gameSceneUI.ScoreUpdata(1, _enemyGoalConut);
        }
        else if(Id == 2)
        {
            this._agent[0].AddReward(-1.2f);

            // プレイヤー操作のとき
            if (!_enemyAI[1].IsPlayer)
            {
                this._agent[1].AddReward(1f);
            }

            _playerGoalConut++;
            _gameSceneUI.ScoreUpdata(2, _playerGoalConut);
        }

        // それぞれエージェントのエピソードを終了する
        this._agent[0].EndEpisode();

        // プレイヤー操作のとき
        if (!_enemyAI[1].IsPlayer)
        {
            this._agent[1].EndEpisode();
        }

        // どっちかが勝利点数になったとき
        if (_playerGoalConut >= _goalConut)
        {
            _gameSceneUI.Outcome(1);
            Time.timeScale = 0;
        }
        else if (_enemyGoalConut >= _goalConut)
        {
            _gameSceneUI.Outcome(2);
            Time.timeScale = 0;
        }

        // 初期化
        Reset();
    }

    #endregion
}
