using UnityEngine;
using UnityEngine.UI;

namespace Jibril
{
    /// <summary>
    /// 文本滚动条控制
    /// </summary>
    public class TextSliderController : MonoBehaviour
    {
        /// <summary>
        /// 标题（滚动条值用{$}表示）
        /// </summary>
        public string title;
        /// <summary>
        /// 值
        /// </summary>
        [HideInInspector]
        public float value;
        /// <summary>
        /// 滚动条
        /// </summary>
        private Slider slider;
        /// <summary>
        /// 文本
        /// </summary>
        private Text text;

        void Start()
        {
            slider = GetComponentInChildren<Slider>();
            text = GetComponentInChildren<Text>();
            value = slider.value;
            UpdateTitle();
        }
        /// <summary>
        /// 更新标题
        /// </summary>
        public void UpdateTitle()
        {
            text.text = title.Replace("{$}", slider.value.ToString());
            value = slider.value;
        }
    }
}

