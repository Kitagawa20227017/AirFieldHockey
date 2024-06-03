// ---------------------------------------------------------  
// GameSceneUI.cs  
//   
// ゲームシーンのUI処理
//
// 作成日: 2024/6/3
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using TMPro;
using System.Collections;

public class GameSceneUI : MonoBehaviour
{

    #region 変数  

    [SerializeField, Header("ScoreUI")]
    private TextMeshProUGUI[] _scoreUI = default;

    #endregion
  
    #region プロパティ  
  
    #endregion
  
    #region メソッド  
  
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start ()
    {
        _scoreUI[0].text = "0";
        _scoreUI[1].text = "0";
    }

    /// <summary>
    /// スコアUIの更新処理
    /// </summary>
    /// <param name="plauerId">ゴールしたプレイヤー</param>
    /// <param name="score">合計点数</param>
    public void ScoreUpdata(int plauerId,int score)
    {
        if(plauerId == 1)
        {
            _scoreUI[0].text = score.ToString();
        }
        else if (plauerId == 2)
        {
            _scoreUI[1].text = score.ToString();
        }
    }

    #endregion

}
