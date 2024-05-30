// ---------------------------------------------------------  
// AreaReward.cs  
//   
// エリアによる報酬
//
// 作成日: 2024/5/20
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using Unity.MLAgents;


public class AreaReward : MonoBehaviour
{

    #region 変数

    [SerializeField,Header("エージェント")]
    private Agent[] agents = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// エリア内処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // 条件に合う場合報酬を減らしていく
        if(other.tag == "Pack" && this.tag == "PlayerArea")
        {
            agents[0].AddReward(-0.01f);
        }
        else if (other.tag == "Pack" && this.tag == "EnemyArea")
        {
            agents[1].AddReward(-0.01f);
        }
    }

    #endregion

}
