using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 玩家控制
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// 动作
        /// </summary>
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("Walk", true);
        }
    }
}

