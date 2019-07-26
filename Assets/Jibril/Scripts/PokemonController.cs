using System.Collections;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Interfaces;
using System.Collections.Generic;

namespace Jibril
{
    /// <summary>
    /// 宠物控制
    /// </summary>
    public class PokemonController : MonoBehaviour, IFeaturePropertySettable
    {
        #region base action
        /// <summary>
        /// 动作
        /// </summary>
        private Animator animator;

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
        #endregion

        /// <summary>
        /// 是否能捕捉
        /// </summary>
        public bool catching;
        /// <summary>
        /// 宠物ID
        /// </summary>
        public int pokemonID;
        /// <summary>
        /// 宠物类型，true：预置；false：随机
        /// </summary>
        public bool pokemonType;


        /// <summary>
        /// 设置变量
        /// </summary>
        /// <param name="props">mapbox自定义变量字典</param>
        public void Set(Dictionary<string, object> props)
        {
            if (props.ContainsKey("ID"))
            {
                pokemonID = int.Parse(props["ID"].ToString());
                pokemonType = true;
            }
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            StartCoroutine("WaitAction");
            catching = false;
            //修改模型大小
            transform.localScale = Vector3.one;

            if (pokemonType)
            {
                if (!MainManager.Instance.CheckPreset(pokemonID))
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (!MainManager.Instance.CheckRandom(pokemonID))
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// 玩家进入捕捉范围
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 10)
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
            if (other.gameObject.layer == 10)
            {
                catching = false;
                StartCoroutine("WaitAction");
            }
        }
    }
}