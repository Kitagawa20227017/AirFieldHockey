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

    [SerializeField,Header("Playerオブジェクト")]
    private GameObject Player = default;

    [SerializeField, Header("レイヤー指定")]
    private LayerMask _groundLayer;

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

    // プレイヤーID
    private enum PLAYER_ID
    {
        Player1 = 1,
        Player2 = 2,
    };

    [SerializeField,Header("プレイヤーID")]
    private PLAYER_ID _playerId = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        Cursor.visible = false;
        _playerRi = Player.gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void FixedUpdate()
    {
        // マウス座標取得
        _mousePos = Input.mousePosition;
        
        // マウス座標をワールド座標に変換
        _worldPos = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10f));
        
        // ワールド座標をローカル座標に変換
        _localPos = transform.InverseTransformPoint(_worldPos);
        
        // 移動量を代入
        _playerPos = new Vector3(_localPos.x, Player.transform.localPosition.y, _localPos.z);
        
        // 範囲外に出ないように補正
        _playerPos.x = Mathf.Clamp(_playerPos.x, -0.01f, 9.53f);
        _playerPos.z = Mathf.Clamp(_playerPos.z, -4.54f, 4.54f);
        
        // 移動
        _playerRi.MovePosition(_playerPos);
    }

    #endregion
}
