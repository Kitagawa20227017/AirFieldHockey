// ---------------------------------------------------------  
// GoalDecision.cs  
//   
// ゴール判定処理
//
// 作成日: 2024/5/14
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class GoalDecision : MonoBehaviour
{

    #region 変数  

    [SerializeField,Header("Packオブジェクト")]
    private Rigidbody _rigidbody = default;

    [SerializeField,Header("Manegerオブジェクト")]
    private GameManeger _gameManeger = default;

    // ゴールID
    private int _goalId = 0;

    // オブジェクトのタグ格納用
    private string _goalTag = default;

    #endregion

    #region プロパティ  

    public int GoalId 
    { 
        get => _goalId; 
        set => _goalId = value; 
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _goalTag = this.gameObject.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_goalTag == "PlayerGoal" && other.tag == "Pack")
        {
            // パックを初期化にする
            _rigidbody.velocity = new Vector3(0, 0, 0);
            other.transform.localPosition = new Vector3(0, 0.1f, 0);

            // 勝ったプレイヤーを伝える
            _gameManeger.Goal(1);
        }
        else if (_goalTag == "EnemyGoal" && other.tag == "Pack")
        {
            // パックを初期化にする   
            _rigidbody.velocity = new Vector3(0, 0, 0);
            other.transform.localPosition = new Vector3(0, 0.1f, 0);

            // 勝ったプレイヤーを伝える
            _gameManeger.Goal(2);
        }
    }

    #endregion
}