// ---------------------------------------------------------  
// Scene.cs  
//   
// １回ロードしたかに処理
//
// 作成日: 2024/6/11
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
}
