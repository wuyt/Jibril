using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 陀螺控制摄像头
    /// </summary>
    public class GyroController : MonoBehaviour
    {
        /// <summary>
        /// 陀螺仪是否启用
        /// </summary>
        private bool gyroEnabled;
        /// <summary>
        /// 陀螺仪
        /// </summary>
        private Gyroscope gyro;
        /// <summary>
        /// Camera容器
        /// </summary>
        private GameObject cameraContainer;
        /// <summary>
        /// 四元数
        /// </summary>
        private Quaternion quaternion;

        void Start()
        {
            //添加容器
            cameraContainer = new GameObject("Camera Container");
            cameraContainer.transform.position = transform.position;
            transform.SetParent(cameraContainer.transform);

            gyroEnabled = EnableGyro();
            
        }
        /// <summary>
        /// 启用陀螺仪
        /// </summary>
        /// <returns>是否启用</returns>
        private bool EnableGyro()
        {
            if (SystemInfo.supportsGyroscope)
            {
                gyro = Input.gyro;
                gyro.enabled = true;

                cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
                quaternion = new Quaternion(0, 0, 1, 0);

                return true;
            }

            return false;
        }

        void Update()
        {
            if (gyroEnabled)
            {
                transform.localRotation = gyro.attitude * quaternion;
            }
        }
    }

}
