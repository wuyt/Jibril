using Mapbox.Unity.Map;
using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// 地图抓捕控制
    /// </summary>
    public class MapController : MonoBehaviour
    {
        /// <summary>
        /// 地图
        /// </summary>
        private AbstractMap map;
        /// <summary>
        /// 玩家
        /// </summary>
        public Transform player;
        /// <summary>
        /// 随机宠物预置位置
        /// </summary>
        public Transform randomPlace;
        /// <summary>
        /// 随机宠物预制件
        /// </summary>
        public PokemonController prefab;


        void Start()
        {
            //添加地图初始化完成事件
            map = FindObjectOfType<AbstractMap>();
            map.OnInitialized += MapInitialized;
        }
        /// <summary>
        /// 地图初始化完成
        /// </summary>
        void MapInitialized()
        {
            Invoke("InitializeRandomPokemon", 1f);
        }
        /// <summary>
        /// 初始化随机宠物
        /// </summary>
        private void InitializeRandomPokemon()
        {
            if (MainManager.Instance.gameInitialization)
            {
                //之前初始化过则直接使用之前的数据
                randomPlace.position = MainManager.Instance.startPosition;
            }
            else
            {
                //否则，将随机宠物位置中心设置在玩家处
                randomPlace.position = player.position;
                MainManager.Instance.startPosition = randomPlace.position;
                MainManager.Instance.gameInitialization = true;
            }

            Vector2[] randomPositions = MainManager.Instance.randomPositions;
            //遍历并生成宠物
            for (int i = 0; i < randomPositions.Length; i++)
            {
                PokemonController pokemon = Instantiate<PokemonController>(prefab, randomPlace);
                pokemon.pokemonID = i;
                pokemon.pokemonType = false;
                pokemon.transform.localPosition =
                    new Vector3(randomPositions[i].x, 0f, randomPositions[i].y);
            }
        }
        /// <summary>
        /// 销毁
        /// </summary>
        private void OnDestroy()
        {
            //销毁时注销事件
            map.OnInitialized -= MapInitialized;
        }

        void Update()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    HitPokemon(ray);
                }
            }
            else if (Application.platform == RuntimePlatform.Android 
                || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                        HitPokemon(ray);
                    }
                }
            }
        }
        /// <summary>
        /// 点击宠物
        /// </summary>
        /// <param name="ray">射线</param>
        private void HitPokemon(Ray ray)
        {
            //指定Layer9
            int layerMask = 1 << 9;
            //如果射线碰到物体
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
            {
                PokemonController pokemon = hitInfo.transform.GetComponent<PokemonController>();
                if (pokemon.catching)
                {
                    pokemon.transform.parent = player;
                    MainManager.Instance.ClickedPokemon(
                        pokemon.pokemonID,
                        pokemon.pokemonType,
                        pokemon.transform.localPosition);
                }
            }
        }
    }
}

