using GoogleARCore;
using UnityEngine;

namespace Jibril
{
    /// <summary>
    /// arcore捕捉场景控制
    /// </summary>
    public class ARCoreController : MonoBehaviour
    {
        /// <summary>
        /// 是否生成宠物状态
        /// </summary>
        private bool status;
        /// <summary>
        /// camera
        /// </summary>
        public Transform cam;
        /// <summary>
        /// 固定位置宠物预制件
        /// </summary>
        public Transform presetPrefab;
        /// <summary>
        /// 随机宠物预制件
        /// </summary>
        public Transform randomPrefab;
        /// <summary>
        /// 生成目标位置
        /// </summary>
        private Vector3 targetPosition;

        void Start()
        {
            status = false;
            CreateTarget();
        }
        /// <summary>
        /// 生成目标
        /// </summary>
        private void CreateTarget()
        {
            targetPosition = MainManager.Instance.pokemonPosition;
            targetPosition = new Vector3(targetPosition.x * 10, -1.5f, targetPosition.z * 10);
        }

        void Update()
        {
            if (!status)
            {
                FindPlace();
            }
        }
        /// <summary>
        /// 查找目标方向的平面
        /// </summary>
        private void FindPlace()
        {
            //是否面向目标方向
            if (Vector3.Dot((targetPosition - cam.position).normalized, cam.forward) > 0.65f)
            {
                //是否有平面
                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                    TrackableHitFlags.FeaturePointWithSurfaceNormal;
                if (Frame.Raycast(0.5f, 0.5f, raycastFilter, out hit))
                {
                    //是否在平面正面
                    if ((hit.Trackable is DetectedPlane) &&
                        Vector3.Dot(cam.position - hit.Pose.position,
                            hit.Pose.rotation * Vector3.up) > 0)
                    {
                        //有识别平面，进入等待状态
                        status = true;
                        CreatePokemon(hit.Pose.position);
                    }
                }
            }
        }

        /// <summary>
        /// 生成宠物
        /// </summary>
        /// <param name="position">位置</param>
        private void CreatePokemon(Vector3 position)
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
            //生成宠物
            Transform tf = Instantiate(prefab);
            tf.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            tf.position = position;
            //宠物面向camrea
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

