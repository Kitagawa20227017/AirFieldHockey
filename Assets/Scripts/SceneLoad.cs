// ---------------------------------------------------------  
// Scene.cs  
//   
//
//
// 作成日: 
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class SceneLoad : MonoBehaviour
{

    #region 変数  

    // 上級をロードしたか
    private bool isAdvancedLoad = false;

    // 中級をロードしたか
    private bool isIntermediateLoad = false;

    // 初級をロードしたか
    private bool isElementarLoad = false;

    #endregion

    #region プロパティ  

    public bool IsAdvancedLoad 
    { 
        get => isAdvancedLoad; 
        set => isAdvancedLoad = value; 
    }

    public bool IsIntermediateLoad 
    {
        get => isIntermediateLoad; 
        set => isIntermediateLoad = value; 
    }

    public bool IsElementarLoad 
    {
        get => isElementarLoad; 
        set => isElementarLoad = value; 
    }

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
        DontDestroyOnLoad(gameObject);
    }
  
    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {
    }

    #endregion

}
