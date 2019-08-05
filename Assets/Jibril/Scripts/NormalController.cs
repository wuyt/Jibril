using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 普通抓捕场景控制
    /// </summary>
    public class NormalController : MonoBehaviour
    {
        /// <summary>
        /// 固定位置宠物预制件
        /// </summary>
        public Transform presetPrefab;
        /// <summary>
        /// 随机宠物预制件
        /// </summary>
        public Transform randomPrefab;
        /// <summary>
        /// camera
        /// </summary>
        public Transform cam;

        void Start()
        {
            CreatePokemon();
        }
        /// <summary>
        /// 生成宠物
        /// </summary>
        private void CreatePokemon()
        {
            //判断类型
            Transform prefab;
            if (MainManager.Instance.pokemonType)
            {
                prefab = presetPrefab;
            }
            else
            {
                prefab = randomPrefab;
            }
            //设置位置
            Vector3 position = new Vector3(
                    MainManager.Instance.pokemonPosition.x + 1,
                    -1.5f,
                    MainManager.Instance.pokemonPosition.z + 1);
            //生成宠物
            Transform tf = Instantiate(prefab);
            tf.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            tf.position = position;
            //宠物面向camrea方向
            GameObject go = new GameObject();
            go.transform.position = new Vector3(cam.transform.position.x, tf.position.y, cam.transform.position.z);
            tf.LookAt(go.transform);
            Destroy(go);
            //设置id和类型
            PokemonController pokemon = tf.GetComponent<PokemonController>();
            pokemon.pokemonID = MainManager.Instance.pokemonID;
            pokemon.pokemonType = MainManager.Instance.pokemonType;
        }
    }
}

