using UnityEngine;
using Jibril;
using UnityEngine.UI;

namespace Jibril
{
    /// <summary>
    /// 设置UI控制
    /// </summary>
    public class SettingsUIController : MonoBehaviour
    {
        #region about
        /// <summary>
        /// 关于游戏对象
        /// </summary>
        public GameObject about;
        /// <summary>
        /// 设置关于
        /// </summary>
        /// <param name="active">激活</param>
        public void SetAbout(bool active)
        {
            about.SetActive(active);
        }
        #endregion

        /// <summary>
        /// 随机宠物平均距离设置
        /// </summary>
        public TextSliderController distance;
        /// <summary>
        /// 随机宠物数量设置
        /// </summary>
        public TextSliderController number;
        /// <summary>
        /// 返回按钮
        /// </summary>
        public Button btnBack;
        /// <summary>
        /// 选择按钮
        /// </summary>
        private SelectButtonController selectButton;

        /// <summary>
        /// 应用退出
        /// </summary>
        public void AppExit()
        {
            MainManager.Instance.AppExit();
        }
        /// <summary>
        /// 返回
        /// </summary>
        public void Back()
        {
            MainManager.Instance.catchSceneName = selectButton.value;
            MainManager.Instance.StartGame();
        }
        /// <summary>
        /// 重新开始
        /// </summary>
        public void Restart()
        {
            MainManager.Instance.catchSceneName = selectButton.value;
            MainManager.Instance.StartGame(
                distance.value,
                int.Parse(number.value.ToString()));
        }

        private void Start()
        {
            SetAbout(false);

            //设置选择按钮
            selectButton = FindObjectOfType<SelectButtonController>();
            if (MainManager.Instance.IsSupportARCore())
            {
                selectButton.OnClicked("ARCore");
            }
            else
            {
                selectButton.SetButton("ARCore", false);
                selectButton.OnClicked("Normal");
            }

            btnBack.interactable = MainManager.Instance.gameInitialization;
        }
    }
}
