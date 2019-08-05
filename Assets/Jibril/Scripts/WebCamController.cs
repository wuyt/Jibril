using System.Collections;
using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 摄像头控制
    /// </summary>
    public class WebCamController : MonoBehaviour
    {
        /// <summary>
        /// 摄像头纹理
        /// </summary>
        private WebCamTexture webcamTexture;
        IEnumerator Start()
        {
            webcamTexture = new WebCamTexture();
            //等待授权
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                CallWebCam();
            }
        }
        /// <summary>
        /// 调用摄像头
        /// </summary>
        private void CallWebCam()
        {
            //如果有后置摄像头，调用后置摄像头  
            for (int i = 0; i < WebCamTexture.devices.Length; i++)
            {
                if (!WebCamTexture.devices[i].isFrontFacing)
                {
                    webcamTexture.deviceName = WebCamTexture.devices[i].name;
                    break;
                }
            }
            //获得并设置平面的纹理
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = webcamTexture;
            //显示摄像头内容
            webcamTexture.Play();
            //调整内容角度
            transform.localRotation = 
                Quaternion.Euler(0f, webcamTexture.videoRotationAngle, 0f);
        }
        /// <summary>
        /// 销毁时候，停止摄像头
        /// </summary>
        private void OnDestroy()
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }
        }
    }
}

