// ---------------------------------------------------------  
// GoalDecision.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class GoalDecision : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private Rigidbody _rigidbody = default;

    [SerializeField]
    private GameManeger _gameManeger = default;

    private string _goalTag = default;

    private int goalId = 0;

    #endregion

    #region プロパティ  

    public int GoalId { get => goalId; set => goalId = value; }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start ()
     {
        _goalTag = this.gameObject.tag;
     }

    private void OnTriggerEnter(Collider other)
    {
        if(_goalTag == "PlayerGoal" && other.tag == "Pack")
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);
            other.transform.localPosition = new Vector3(0, 0.1f,0);
            
            _gameManeger.Goal(1);
        }
        else if (_goalTag == "EnemyGoal" && other.tag == "Pack")
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);
            other.transform.localPosition = new Vector3(0, 0.1f, 0);
            _gameManeger.Goal(2);
        }
    }

    #endregion
}
