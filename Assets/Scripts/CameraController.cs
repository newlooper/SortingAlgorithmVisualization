using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform mainCamera;

    #region 相机移动参数

    public float moveSpeed   = 1.0f;
    public float rotateSpeed = 90.0f;
    public float shiftRate   = 2.0f; // 按住Shift加速
    public float minDistance = 0.5f; // 相机离不可穿过的表面的最小距离（小于等于0时可穿透任何表面）

    #endregion

    #region 运动速度和其每个方向的速度分量

    private Vector3 _direction = Vector3.zero;
    private Vector3 _speedForward;
    private Vector3 _speedBack;
    private Vector3 _speedLeft;
    private Vector3 _speedRight;
    private Vector3 _speedUp;
    private Vector3 _speedDown;

    #endregion

    void Start()
    {
        if ( mainCamera == null ) mainCamera = gameObject.transform;
        // 防止相机边缘穿透
        //if (tourCamera.GetComponent<Camera>().nearClipPlane > minDistance / 3)
        //｛
        //    tourCamera.GetComponent<Camera>().nearClipPlane /= 3;
        //｝
    }

    void Update()
    {
        GetDirection();
        // 检测是否离不可穿透表面过近
        while ( Physics.Raycast( mainCamera.position, _direction, out var hit, minDistance ) )
        {
            // 消去垂直于不可穿透表面的运动速度分量
            var angel     = Vector3.Angle( _direction, hit.normal );
            var magnitude = Vector3.Magnitude( _direction ) * Mathf.Cos( Mathf.Deg2Rad * ( 180 - angel ) );
            _direction += hit.normal * magnitude;
        }

        mainCamera.Translate( _direction * moveSpeed * Time.unscaledDeltaTime, Space.World );
    }

    private void GetDirection()
    {
        #region 加速移动

        if ( Input.GetKeyDown( KeyCode.LeftShift ) ) moveSpeed *= shiftRate;
        if ( Input.GetKeyUp( KeyCode.LeftShift ) ) moveSpeed /= shiftRate;

        #endregion

        #region 键盘移动

        // 复位
        _speedForward = Vector3.zero;
        _speedBack = Vector3.zero;
        _speedLeft = Vector3.zero;
        _speedRight = Vector3.zero;
        _speedUp = Vector3.zero;
        _speedDown = Vector3.zero;
        // 获取按键输入
        if ( Input.GetKey( KeyCode.W ) ) _speedForward = mainCamera.forward;
        if ( Input.GetKey( KeyCode.S ) ) _speedBack = -mainCamera.forward;
        if ( Input.GetKey( KeyCode.A ) ) _speedLeft = -mainCamera.right;
        if ( Input.GetKey( KeyCode.D ) ) _speedRight = mainCamera.right;
        if ( Input.GetKey( KeyCode.E ) ) _speedUp = Vector3.up;
        if ( Input.GetKey( KeyCode.Q ) ) _speedDown = Vector3.down;
        _direction = _speedForward + _speedBack + _speedLeft + _speedRight + _speedUp + _speedDown;

        #endregion

        #region 鼠标旋转

        if ( Input.GetMouseButton( 1 ) )
        {
            // 转相机朝向
            mainCamera.RotateAround( mainCamera.position, Vector3.up, Input.GetAxis( "Mouse X" ) * rotateSpeed * Time.unscaledDeltaTime );
            mainCamera.RotateAround( mainCamera.position, mainCamera.right, -Input.GetAxis( "Mouse Y" ) * rotateSpeed * Time.unscaledDeltaTime );
            // 转运动速度方向
            _direction = V3RotateAround( _direction, Vector3.up, Input.GetAxis( "Mouse X" ) * rotateSpeed * Time.unscaledDeltaTime );
            _direction = V3RotateAround( _direction, mainCamera.right, -Input.GetAxis( "Mouse Y" ) * rotateSpeed * Time.unscaledDeltaTime );
        }

        #endregion
    }

    /// <summary>
    /// 计算一个Vector3绕旋转中心旋转指定角度后所得到的向量
    /// </summary>
    /// <param name="source">旋转前的源Vector3</param>
    /// <param name="axis">旋转轴</param>
    /// <param name="angle">旋转角度</param>
    /// <returns>旋转后得到的新Vector3</returns>
    public Vector3 V3RotateAround( Vector3 source, Vector3 axis, float angle )
    {
        var q = Quaternion.AngleAxis( angle, axis ); // 旋转系数
        return q * source; // 返回目标点
    }
}