// ---------------------------------------------------------  
// PlayerInput.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{

    #region 変数  

    #region const定数

    // RayCastの位置
    private const float RAY_POS_ADVENT_SUITED = 0.45f;

    // RayCastの長さ
    private const float RAY_LENGTH = 0.025f;

    #endregion


    [SerializeField, Header("レイヤー指定")]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _moveSpeed = 0.1f;

    private Rigidbody _rigidbody = default;
    private Vector3 _movePos = default;

    private LayerMask _wall;

    private RaycastHit _rightRay = default;
    private RaycastHit _leftRay = default;

    private RaycastHit _upWardRay = default;
    private RaycastHit _downWardRay = default;


    private float _speed = 1f;

    private float _movePosX = default;
    private float _movePosZ = default;

    private enum PLAYER_ID
    {
        Plaer1 = 1,
        Plaey2 = 2,
    };

    [SerializeField]
    private PLAYER_ID _playerId = default;

    #endregion

    #region プロパティ  

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
    void Start()
    {
        
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float dir = ((int)_playerId == 1) ? 1.0f : -1.0f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 2f;
        }
        else
        {
            _speed = 1;
        }

        if (Input.GetKey(KeyCode.D) && !RightWard())
        {
            _movePosX = _moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A) && !LeftWard())
        {
            _movePosX = -_moveSpeed;
        }
        else
        {
            _movePosX = 0;
        }

        if(Input.GetKey(KeyCode.W) && !UpWard())
        {
            _movePosZ = _moveSpeed;
        }
        else if(Input.GetKey(KeyCode.S) && !DownWard())
        {
            _movePosZ = -_moveSpeed;
        }
        else
        {
            _movePosZ = 0;
        }

        //_movePos = new Vector3(transform.localPosition.x + _movePosX * _speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z + _movePosZ * _speed * Time.deltaTime);
        _movePos = new Vector3(transform.position.x + _movePosX * _speed * Time.deltaTime, transform.position.y, transform.position.z + _movePosZ * _speed * Time.deltaTime);
        _rigidbody.MovePosition(_movePos);

        if (transform.localPosition.x * dir >= 9.5f)
        {
            transform.localPosition = new Vector3(9.49f * dir, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x * dir <= 0f)
        {
            transform.localPosition = new Vector3(-0.01f * dir, transform.localPosition.y, transform.localPosition.z);
        }

        if (transform.localPosition.z >= 4.55f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 4.54f);
        }
        else if (transform.localPosition.z <= -4.55f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -4.54f);
        }
    }

    private bool UpWard()
    {
        bool isMove = false;

        Vector3 upWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + RAY_POS_ADVENT_SUITED);
        Ray upAngle = new Ray(upWardRayPos, new Vector3(0, 0, 1));

        if (Physics.Raycast(upAngle, out _upWardRay,0.05f,_groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }

    private bool DownWard()
    {
        bool isMove = false;

        Vector3 downWardRayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - RAY_POS_ADVENT_SUITED);
        Ray downAngle = new Ray(downWardRayPos, new Vector3(0, 0, -1));

        if (Physics.Raycast(downAngle, out _downWardRay, 0.05f, _groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }

    private bool RightWard()
    {
        bool isMove = false;

        Vector3 rightWardRayPos = new Vector3(transform.position.x + RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);
        Ray rightAngle = new Ray(rightWardRayPos, new Vector3(1, 0, 0));

        if (Physics.Raycast(rightAngle, out _rightRay, 0.1f, _groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }


    private bool LeftWard()
    {
        bool isMove = false;

        Vector3 leftWardRayPos = new Vector3(transform.position.x - RAY_POS_ADVENT_SUITED, transform.position.y, transform.position.z);
        Ray leftAngle = new Ray(leftWardRayPos, new Vector3(-1, 0, 0));

        if (Physics.Raycast(leftAngle, out _leftRay, 0.1f, _groundLayer))
        {
            isMove = true;
        }
        return isMove;
    }


    #endregion
}
