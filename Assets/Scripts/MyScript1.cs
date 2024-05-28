// ---------------------------------------------------------  
// MyScript1.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using Unity.MLAgents;


public class MyScript1 : MonoBehaviour
{

    #region 変数

    [SerializeField]
    private Agent[] agents = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {
     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
  
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
       
     }

    private void OnTriggerStay(Collider other)
    {
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
