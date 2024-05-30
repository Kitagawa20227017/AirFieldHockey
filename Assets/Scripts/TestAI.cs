// ---------------------------------------------------------  
// TestAI.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Editor;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class TestAI : Agent
{

    #region 変数  

    [SerializeField]
    private GameObject a = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    private void Start()
    {
        
    }
    
    public override void OnEpisodeBegin()
    {
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
       
    }

    #endregion

}
