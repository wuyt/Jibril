using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jibril
{
    /// <summary>
    /// 抓捕控制
    /// </summary>
    public class CatchController : MonoBehaviour
    {
        /// <summary>
        /// 特效预制件
        /// </summary>
        public Transform effectPrefab;

        void Update()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    CatchPokemon(ray);
                }
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                        CatchPokemon(ray);
                    }
                }
            }
        }
        /// <summary>
        /// 抓捕宠物
        /// </summary>
        /// <param name="ray">射线</param>
        private void CatchPokemon(Ray ray)
        {
            //设置层，只检查第9层
            int layerMask = 1 << 9;
            //射线检测
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
            {
                //生成特效
                Instantiate(effectPrefab, hitInfo.transform.position, Quaternion.identity);
                //获取宠物信息并更新字典
                PokemonController pokemon = hitInfo.transform.GetComponent<PokemonController>();
                MainManager.Instance.UpdatePokemon(pokemon.pokemonType, pokemon.pokemonID);
                //删除宠物
                Destroy(pokemon.gameObject);
                Invoke("CatchEnd", 5);
            }
        }
        /// <summary>
        /// 抓捕结束
        /// </summary>
        private void CatchEnd()
        {
            MainManager.Instance.CatchEnd();
        }
    }
}

