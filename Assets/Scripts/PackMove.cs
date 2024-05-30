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

    #region const定数

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
        if (_rigidbody.velocity.x > 20f)
        {
            _rigidbody.velocity = new Vector3(20f, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }
        else if (_rigidbody.velocity.x < -20f)
        {
            _rigidbody.velocity = new Vector3(-20f, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }

        if (_rigidbody.velocity.z > 20f)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, 20f);
        }
        else if (_rigidbody.velocity.x < -20f)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, -20f);
        }

        if (UpLeftWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x + 1f, _rigidbody.velocity.y, _rigidbody.velocity.z + -1f);
        }

        if (DownLeftWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x + 1f, _rigidbody.velocity.y, _rigidbody.velocity.z + 1f);
        }

        if (UpRightWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.z - 1f, _rigidbody.velocity.y, _rigidbody.velocity.z - 1f);
        }

        if (DownRightWard())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.y - 1f, _rigidbody.velocity.y, _rigidbody.velocity.z + 1f);
        }
    }


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
