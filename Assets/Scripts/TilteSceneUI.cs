// ---------------------------------------------------------  
// TilteSceneUI.cs  
//   
// タイトルシーン処理
//
// 作成日: 2024/6/7
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TilteSceneUI : MonoBehaviour
{

    #region 変数  

    [SerializeField,Header("NowLoadingオブジェクト")]
    private GameObject _nowLoadingObj = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  
  
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start ()
    {
        // 非アクティブ化
        _nowLoadingObj.SetActive(false);
    }

    /// <summary>
    /// 上級へ遷移
    /// </summary>
    public void AdvancedLevel()
    {
        _nowLoadingObj.SetActive(true);
        SceneManager.LoadScene("AdvancedLevel");
    }

    /// <summary>
    /// 中級へ遷移
    /// </summary>
    public void IntermediateLevel()
    {
        _nowLoadingObj.SetActive(true);
        SceneManager.LoadScene("IntermediateLevel");
    }

    /// <summary>
    /// 初級へ遷移
    /// </summary>
    public void ElementaryLevel()
    {
        _nowLoadingObj.SetActive(true);
        SceneManager.LoadScene("ElementaryLevel");
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void Fin()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();//ゲームプレイ終了
        #endif
    }

    #endregion

}
