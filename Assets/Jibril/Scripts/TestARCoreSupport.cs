using GoogleARCore;
using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 测试ARCore支持情况
    /// </summary>
    public class TestARCoreSupport : MonoBehaviour
    {
        void Start()
        {
            //2秒后再运行
            Invoke("Test",2f);
        }
        /// <summary>
        /// 测试
        /// </summary>
        private void Test()
        {
            int status = 1;
            if (Session.Status.IsError())
            {
                status = 0;
            }
            MainManager.Instance.ARCoreStatus(status);
        }
    }
}
