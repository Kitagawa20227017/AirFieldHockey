// ---------------------------------------------------------  
// aaaaaaa.cs  
//   
//
//
// 作成日: 
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class aaaaaaa : MonoBehaviour
{


    #region 変数  


    [SerializeField, Header("Playerオブジェクト")]
    private GameObject Player = default;

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
    /// 更新処理  
    /// </summary>
    private void Update()
    {


    }

    /// <summary>  
    /// 一定時間処理  
    /// </summary>  
    private void FixedUpdate()
    {



        // マウス座標取得
        _mousePos = Input.mousePosition;

        // マウス座標をワールド座標に変換
        _worldPos = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 11f));

        //// ワールド座標をローカル座標に変換
        _localPos = transform.InverseTransformPoint(_worldPos);

        //// 移動量を代入
        _playerPos = new Vector3(_localPos.x, Player.transform.localPosition.y, _localPos.z);

        Debug.Log(_playerPos);

        // 移動
        _playerRi.MovePosition(_playerPos);
       
    }

    #endregion

}
