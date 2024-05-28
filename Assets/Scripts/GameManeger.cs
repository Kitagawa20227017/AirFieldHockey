// ---------------------------------------------------------  
// GamaMene.cs  
//   
// ゲームマネージャー
//
// 作成日: 2024/5/13
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using Unity.MLAgents;

public class GameManeger : MonoBehaviour
{

    #region 変数  

    [SerializeField,Header("ゴール判定")]
    private GoalDecision[] _goalDecisions = default;

    [SerializeField, Header("Packオブジェクト")]
    private GameObject _pack;

    [SerializeField,Header("エージェント0")]
    private Agent[] _agent = default;

    // Rigidbody格納用
    private Rigidbody _rigidbody;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
    {
        // Rigidbody取得
        _rigidbody = _pack.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ゴールした後の初期化処理
    /// </summary>
    public void Reset()
    {
        // パックを止める
        this._rigidbody.velocity = new Vector3(0, 0, 0);
        
        // センターのランダムな座標Zにパックを配置
        float random = Random.Range(0, 4.5f);
        if (Random.Range(0, 2) == 0)
        {
            random = random * -1;
        }
        this._pack.transform.localPosition = new Vector3(0, 0.1f, random);
    }
    
    /// <summary>
    /// ゴール処理
    /// </summary>
    /// <param name="Id">プレイヤーID</param>
    public void Goal(int Id)
    {
        // それぞれのエージェントに報酬を与える
        if (Id == 2)
        {
            this._agent[0].AddReward(-1.2f);
            this._agent[1].AddReward(1f);
        }
        else if (Id == 1)
        {
            this._agent[0].AddReward(1f);
            this._agent[1].AddReward(-1.2f);
        }

        // それぞれエージェントのエピソードを終了する
        this._agent[0].EndEpisode();
        this._agent[1].EndEpisode();

        // 初期化
        Reset();
    }

    #endregion
}
