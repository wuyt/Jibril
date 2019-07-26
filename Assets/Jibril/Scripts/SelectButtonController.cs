using UnityEngine;
using UnityEngine.UI;

namespace Jibril
{
    /// <summary>
    /// 选择按钮
    /// </summary>
    public class SelectButtonController : MonoBehaviour
    {
        /// <summary>
        /// 值
        /// </summary>
        [HideInInspector]
        public string value;
        /// <summary>
        /// 选中按钮颜色
        /// </summary>
        public Color selectedColor;
        /// <summary>
        /// 未选中按钮颜色
        /// </summary>
        public Color normalColor;
        /// <summary>
        /// 按钮数组
        /// </summary>
        private Button[] buttons;

        void Awake()
        {
            buttons = GetComponentsInChildren<Button>();
        }

        /// <summary>
        /// 设置按钮禁用启用
        /// </summary>
        /// <param name="key">按钮名字</param>
        /// <param name="enable">是否启用（true：启用；false：禁用）</param>
        public void SetButton(string key,bool enable)
        {
            //遍历按钮
            foreach (var btn in buttons)
            {
                //获取名称
                string temp = btn.GetComponentInChildren<Text>().text;
                ColorBlock colorBlock = ColorBlock.defaultColorBlock;
                if (temp.Equals(key))   //如果名称符合
                {
                    colorBlock.normalColor = normalColor;
                    btn.interactable = enable;
                    break;
                }
            }
        }
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="key">按钮名称</param>
        public void OnClicked(string key)
        {
            //遍历按钮
            foreach (var btn in buttons)
            {
                //获取名称
                string temp = btn.GetComponentInChildren<Text>().text;
                ColorBlock colorBlock = ColorBlock.defaultColorBlock;
                if (temp.Equals(key))   //如果名称符合
                {
                    colorBlock.highlightedColor = selectedColor;
                    colorBlock.normalColor = selectedColor;
                    value = key;
                }
                else
                {
                    colorBlock.highlightedColor = normalColor;
                    colorBlock.normalColor = normalColor;
                }
                btn.colors = colorBlock;
            }
        }
    }
}

