using System.Collections;
using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 宠物控制
    /// </summary>
    public class PokemonController : MonoBehaviour
    {
        /// <summary>
        /// 动作
        /// </summary>
        private Animator animator;
        /// <summary>
        /// 是否能捕捉
        /// </summary>
        public bool catching;

        void Start()
        {
            animator = GetComponent<Animator>();
            StartCoroutine("WaitAction");
            catching = false;
        }
        /// <summary>
        /// 等待时随机动作
        /// </summary>
        IEnumerator WaitAction()
        {
            float time = Random.Range(2f, 15f);
            yield return new WaitForSeconds(time);
            animator.SetTrigger("Pose");
            StartCoroutine("WaitAction");
        }
        /// <summary>
        /// 玩家进入捕捉范围
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 9)
            {
                catching = true;
                StopCoroutine("WaitAction");
                animator.SetTrigger("Look");
            }
        }
        /// <summary>
        /// 玩家离开捕捉范围
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 9)
            {
                catching = false;
                StartCoroutine("WaitAction");
            }
        }
    }
}