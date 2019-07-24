using Cinemachine;
using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 镜头控制
    /// </summary>
    public class CMController : MonoBehaviour
    {
        /// <summary>
        /// 多个相机组件
        /// </summary>
        private CinemachineClearShot clearShot;
        /// <summary>
        /// 当前序号
        /// </summary>
        private int num;

        void Start()
        {
            clearShot = GetComponent<CinemachineClearShot>();
            num = 0;
            SetCamera(num);
        }

        /// <summary>
        /// 设置当前镜头
        /// </summary>
        /// <param name="index">序号</param>
        private void SetCamera(int index)
        {
            for (int i = 0; i < clearShot.ChildCameras.Length; i++)
            {
                if (i == index)
                {
                    clearShot.ChildCameras[i].Priority = 11;
                }
                else
                {
                    clearShot.ChildCameras[i].Priority = 10;
                }
            }
        }
        /// <summary>
        /// 切换镜头
        /// </summary>
        public void ChangeVC()
        {
            if (num == 0)
            {
                num = 1;
            }
            else
            {
                num = 0;
            }
            SetCamera(num);
        }
    }
}

