// ---------------------------------------------------------  
// PackMove.cs  
//   
// パックの移動処理
//
// 作成日: 2024/5/13
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PackMove : MonoBehaviour
{

    #region 変数  

    #region 定数

    // 最大スピード
    private const int MAX_SPEED = 20;
    private const int MIN_SPEED = -20;

    // RayCastの位置
    private const float RAY_POS_ADVENT_SUITED = 0.3f;

    // RayCastの長さ
    private const float RAY_LENGTH = 0.1f;

    #endregion

    [SerializeField, Header("レイヤー指定")]
    private LayerMask _groundLayer;

    // Rigidbody格納用
    private Rigidbody _rigidbody = default;

    // Ray格納用
    private RaycastHit _rightRay = default;
    private RaycastHit _leftRay = default;
    private RaycastHit _upWardRay = default;
    private RaycastHit _downWardRay = default;

    #endregion
  
    #region メソッド
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        // Rigidbody取得 
        _rigidbody = this.GetComponent<Rigidbody>();
     }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void FixedUpdate()
    {
        // X軸の速度制限
        if (_rigidbody.velocity.x > MAX_SPEED)
        {
            _rigidbody.velocity = new Vector3(MAX_SPEED, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }
        else if (_rigidbody.velocity.x < MIN_SPEED)
        {
            _rigidbody.velocity = new Vector3(MIN_SPEED, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }

        // Z軸の速度制限
        if (_rigidbody.velocity.z > MAX_SPEED)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, MAX_SPEED);
        }
        else if (_rigidbody.velocity.x < MIN_SPEED)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, MIN_SPEED);
        }

        // 左上の角のとき
        if (UpLeftWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x + 1f, _rigidbody.velocity.y, _rigidbody.velocity.z + -1f);
        }

        // 左下の角のとき
        if (DownLeftWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x + 1f, _rigidbody.velocity.y, _rigidbody.velocity.z + 1f);
        }

        // 右上の角のとき
        if (UpRightWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.z - 1f, _rigidbody.velocity.y, _rigidbody.velocity.z - 1f);
        }

        // 右下の角のとき
        if (DownRightWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.y - 1f, _rigidbody.velocity.y, _rigidbody.velocity.z + 1f);
        }
    }

    /// <summary>
    /// 左上の角の詰み防止
    /// </summary>
    /// <returns>左上の角かどうか</returns>
    private bool UpRightWard()
    {
        bool isMove = false;

        Vector3 upWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + RAY_POS_ADVENT_SUITED);
        Vector3 rightWardRayPos = new Vector3(transform.position.x + RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);
        Ray upAngle = new Ray(upWardRayPos, new Vector3(0, 0, 1));
        Ray rightAngle = new Ray(rightWardRayPos, new Vector3(1, 0, 0));

        if (Physics.Raycast(upAngle, out _upWardRay, RAY_LENGTH, _groundLayer) && Physics.Raycast(rightAngle, out _upWardRay, RAY_LENGTH, _groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }

    /// <summary>
    /// 左下の角の詰み防止
    /// </summary>
    /// <returns>左下の角かどうか</returns>
    private bool DownRightWard()
    {
        bool isMove = false;

        Vector3 downWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - RAY_POS_ADVENT_SUITED);
        Vector3 rightWardRayPos = new Vector3(transform.position.x + RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);
        Ray downAngle = new Ray(downWardRayPos, new Vector3(0, 0, -1));
        Ray rightAngle = new Ray(rightWardRayPos, new Vector3(1, 0, 0));

        if (Physics.Raycast(downAngle, out _downWardRay, RAY_LENGTH, _groundLayer) && Physics.Raycast(rightAngle, out _downWardRay, RAY_LENGTH, _groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }

    /// <summary>
    /// 右上の角の詰み防止
    /// </summary>
    /// <returns>右上の角かどうか</returns>
    private bool UpLeftWard()
    {
        bool isMove = false;

        Vector3 upWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + RAY_POS_ADVENT_SUITED);
        Vector3 rightWardRayPos = new Vector3(transform.position.x - RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);
        Ray upAngle = new Ray(upWardRayPos, new Vector3(0, 0, 1));
        Ray rightAngle = new Ray(rightWardRayPos, new Vector3(-1, 0, 0));

        if (Physics.Raycast(rightAngle, out _rightRay, RAY_LENGTH, _groundLayer) && Physics.Raycast(upAngle, out _rightRay, RAY_LENGTH, _groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }

    /// <summary>
    /// 右下の角の詰み防止
    /// </summary>
    /// <returns>右下の角かどうか</returns>
    private bool DownLeftWard()
    {
        bool isMove = false;

        Vector3 downWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - RAY_POS_ADVENT_SUITED);
        Vector3 leftWardRayPos = new Vector3(transform.position.x - RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);
        Ray downAngle = new Ray(downWardRayPos, new Vector3(0, 0, -1));
        Ray leftAngle = new Ray(leftWardRayPos, new Vector3(-1, 0, 0));

        if (Physics.Raycast(leftAngle, out _leftRay, RAY_LENGTH, _groundLayer) && Physics.Raycast(downAngle, out _leftRay, RAY_LENGTH, _groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }
    #endregion

}
