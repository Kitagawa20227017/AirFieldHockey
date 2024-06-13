// ---------------------------------------------------------  
// PlayerInput.cs  
// 
// プレイヤー入力処理
//
// 作成日: 2024/5/13
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{

    #region 変数  

    #region 定数

    // X軸の行動制限
    private const float MOVE_X_POS_MAX = 9.19f;
    private const float MOVE_X_POS_MIN = 0.19f;

    // Z軸の行動制限
    private const float MOVE_Z_POS_MAX = 4.324f;
    private const float MOVE_Z_POS_MIN = -4.324f;

    #endregion

    [SerializeField,Header("Playerオブジェクト")]
    private GameObject Player = default;

    [SerializeField, Header("Playerオブジェクト")]
    private EnemyAI _enemyAI = default;

    // ローカル座標格納用
    Vector3 _localPos = default;

    // プレイヤー座標格納用
    Vector3 _playerPos = default;

    // マウス座標格納用
    Vector3 _mousePos; 

    // ワールド座標格納用
    Vector3 _worldPos;

    // Rigidbody格納用
    Rigidbody _playerRi = default;

    // ポーズ中かどうか
    private bool _isStop = false;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _playerRi = Player.gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>  
    /// 一定時間処理  
    /// </summary>  
    private void FixedUpdate()
    {

        // プレイヤー操作ではないときは実行しない
        if (!_enemyAI.IsPlayer)
        {
            return;
        }

        // マウス座標取得
        _mousePos = Input.mousePosition;

        // マウス座標をワールド座標に変換
        _worldPos = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 11f));
        
        // ワールド座標をローカル座標に変換
        _localPos = transform.InverseTransformPoint(_worldPos);

        // 移動量を代入
        _playerPos = new Vector3(_localPos.x, Player.transform.localPosition.y, _localPos.z);

        // 範囲外に出ないように補正
        _playerPos.x = Mathf.Clamp(_playerPos.x, MOVE_X_POS_MIN, MOVE_X_POS_MAX);
        _playerPos.z = Mathf.Clamp(_playerPos.z, MOVE_Z_POS_MIN, MOVE_Z_POS_MAX);

        // 移動
        _playerRi.MovePosition(_playerPos);
    }

    #endregion
}
