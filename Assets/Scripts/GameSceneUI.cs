// ---------------------------------------------------------  
// GameSceneUI.cs  
//   
// ゲームシーンのUI処理
//
// 作成日: 2024/6/3
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSceneUI : MonoBehaviour
{

    #region 変数  

    [SerializeField, Header("Scoreオブジェクト")]
    private TextMeshProUGUI[] _scoreUI = default;

    [SerializeField, Header("Outcomeオブジェクト")]
    private TextMeshProUGUI _outCome = default;

    [SerializeField, Header("Resultオブジェクト")]
    private GameObject _result = default;

    [SerializeField, Header("Conutオブジェクト")]
    private GameObject _conutObj = default;

    private SceneLoad _sceneLoad = default;

    private TextMeshProUGUI _conutText = default;

    // カウントダウン
    private float _conut = 0f;

    // カウントダウン用のフラグ
    private bool _isStart = true;
    private bool _isUI = false;

    // 現在のシーン名
    private string _sceneName = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start ()
    {
        // 初期設定
        _sceneLoad = GameObject.Find("SceneLoad").GetComponent<SceneLoad>();
        _conutText = _conutObj.GetComponent<TextMeshProUGUI>();
        _sceneName = SceneManager.GetActiveScene().name;
        _conutText.text = _conut.ToString();
        _conutObj.SetActive(true);
        _conut = 3f;
        Load();
        _scoreUI[0].text = "0";
        _scoreUI[1].text = "0";
        _outCome.text = "";
        _result.SetActive(false);
    }

    private void Update()
    {
        // カウントダウン処理
        if (_isStart)
        {
            // カウントダウン
            _conut -= Time.unscaledDeltaTime;

            // 表示する
            int tem = (int)_conut;
            _conutText.text = tem.ToString();
            
            // カウントが0になったとき
            if (_conut < 0)
            {
                _isStart = false;
                _isUI = true;
                _conutText.text = "Start!!";
            }
        }

        // ゲームをスタートさせる
        if (!_isStart && _isUI)
        {
            _conut -= Time.unscaledDeltaTime;
            if (_conut < -0.5f)
            {
                _conutObj.SetActive(false);
                Time.timeScale = 1;
                _isUI = false;
            }
        }
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

    /// <summary>
    /// 結果表示
    /// </summary>
    /// <param name="playerId">勝ったプレイヤー</param>
    public void Outcome(int playerId)
    {
        _result.SetActive(true);
        if(playerId == 1)
        {
            _outCome.text = "W i n ! !";
        }
        else if(playerId == 2)
        {
            _outCome.text = " L o s s . . .";
        }
    }

    /// <summary>
    /// リスタート処理
    /// </summary>
    public void ReStart()
    {
        SceneManager.LoadScene(_sceneName);
    }

    /// <summary>
    /// タイトルシーン遷移処理
    /// </summary>
    public void Tilte()
    {
        SceneManager.LoadScene("TilteScene");
    }

    private void Load()
    {
        Debug.Log(_sceneName);
        if(_sceneName == "AdvancedLevel" && !_sceneLoad.IsAdvancedLoad)
        {
            Debug.Log("A");
            _conut = 11f;
            _sceneLoad.IsAdvancedLoad = true;
        }

        if (_sceneName == "IntermediateLevel" && !_sceneLoad.IsIntermediateLoad)
        {
            Debug.Log("A");
            _conut = 11f;
            _sceneLoad.IsIntermediateLoad = true;
        }

        if (_sceneName == "ElementaryLevel" && !_sceneLoad.IsElementarLoad)
        {
            Debug.Log("A");
            _conut = 11f;
            _sceneLoad.IsElementarLoad = true;
        }
    }

    #endregion

}
