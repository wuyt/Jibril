using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jibril
{
    /// <summary>
    /// 主要逻辑管理
    /// </summary>
    public class MainManager : MonoBehaviour
    {
        #region singleton 
        /// <summary>
        /// 单实例所在游戏对象名称
        /// </summary>
        private const string goName = "GameMaster";
        /// <summary>
        /// 实例
        /// </summary>
        private static MainManager instance;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static readonly object _lock = new object();
        /// <summary>
        /// 是否关闭
        /// </summary>
        private static bool isShuttingDown = false;

        /// <summary>
        /// 获取实例
        /// </summary>
        public static MainManager Instance
        {
            get
            {
                if (isShuttingDown) //如果已经关闭则返回空
                {

                    return null;
                }

                lock (_lock)    //锁定避免同时创建
                {
                    if (instance == null)   //当前实例为空
                    {
                        //在场景中查找对象
                        instance = FindObjectOfType<MainManager>();
                        if (instance == null)   //场景中没对象
                        {
                            //查找游戏对象
                            GameObject go = GameObject.Find(goName);
                            if (go == null) //游戏对象为空则创建
                            {
                                go = new GameObject(goName);
                            }
                            //为游戏对象添加组件
                            instance = go.AddComponent<MainManager>();
                            //不在场景转换时删除
                            DontDestroyOnLoad(go);
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 程序关闭时
        /// </summary>
        private void OnApplicationQuit()
        {
            isShuttingDown = true;
        }

        /// <summary>
        /// 消除对象时
        /// </summary>
        private void OnDestroy()
        {
            isShuttingDown = true;
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                int index = SceneManager.GetActiveScene().buildIndex;
                if (index > 3)
                {
                    SceneManager.LoadScene("Map");
                }else if (index == 3)
                {
                    SceneManager.LoadScene("Settings");
                }
            }
        }

        #region Loading
        /// <summary>
        /// Loading场景结束时需要加载的场景
        /// </summary>
        /// <returns>场景名称</returns>
        public string LoadingEnd()
        {
            string sceneName = "ARCoreTest";
            int isSupport = PlayerPrefs.GetInt("SupportARCore", -1);
            if (isSupport > -1)
            {
                sceneName = "Settings";
            }
            return sceneName;
        }
        #endregion

        #region ARCoreTest
        /// <summary>
        /// ARCore支持
        /// </summary>
        /// <param name="status">是否支持,0:不可用，1:可用</param>
        public void ARCoreStatus(int status)
        {
            PlayerPrefs.SetInt("SupportARCore", status);
            SceneManager.LoadScene("Settings");
        }
        #endregion

        #region Settings
        /// <summary>
        /// 应用退出
        /// </summary>
        public void AppExit()
        {
            PlayerPrefs.SetInt("SupportARCore", -2);//测试用
            Application.Quit();
        }
        /// <summary>
        /// 是否支持ARCore
        /// </summary>
        /// <returns>是否支持,false:不支持，true:支持</returns>
        public bool IsSupportARCore()
        {
            int isSupport = PlayerPrefs.GetInt("SupportARCore", -1);
            if (isSupport == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 抓捕场景名称
        /// </summary>
        public string catchSceneName;
        /// <summary>
        /// 随机宠物位置信息
        /// </summary>
        public Vector2[] randomPositions;
        /// <summary>
        /// 是否初始化过
        /// </summary>
        public bool gameInitialization;
        /// <summary>
        /// 开始游戏
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene("Map");
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="distance">距离</param>
        /// <param name="number">数量</param>
        public void StartGame(float distance, int number)
        {
            InitializeGame(distance, number);
            StartGame();
        }
        /// <summary>
        /// 初始化游戏
        /// </summary>
        /// <param name="distance">距离</param>
        /// <param name="number">数量</param>
        private void InitializeGame(float distance, int number)
        {
            randomPositions = new Vector2[number];
            for(int i = 0; i < randomPositions.Length; i++)
            {
                randomPositions[i] = Random.insideUnitCircle * distance;
            }
        }
        #endregion

        #region Map
        /// <summary>
        /// 预置宠物
        /// </summary>
        private Dictionary<int, bool> presetPokemon = new Dictionary<int, bool>();
        /// <summary>
        /// 随机宠物
        /// </summary>
        private Dictionary<int, bool> randomPokemon = new Dictionary<int, bool>();
        /// <summary>
        /// 宠物ID
        /// </summary>
        public int pokemonID;
        /// <summary>
        /// 宠物类型
        /// </summary>
        public bool pokemonType;
        /// <summary>
        /// 宠物位置
        /// </summary>
        public Vector3 pokemonPosition;
        /// <summary>
        /// 随机宠物起始位置
        /// </summary>
        public Vector3 startPosition;
        /// <summary>
        /// 检查预置宠物
        /// </summary>
        /// <param name="id">宠物ID</param>
        /// <returns>是否显示</returns>
        public bool CheckPreset(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            if (presetPokemon.ContainsKey(id))
            {
                presetPokemon.TryGetValue(id, out bool visiable);
                return visiable;
            }
            else
            {
                presetPokemon.Add(id, true);
                return true;
            }
        }
        /// <summary>
        /// 检查随机宠物
        /// </summary>
        /// <param name="id">宠物ID</param>
        /// <returns>是否显示</returns>
        public bool CheckRandom(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            if (randomPokemon.ContainsKey(id))
            {
                randomPokemon.TryGetValue(id, out bool visiable);
                return visiable;
            }
            else
            {
                randomPokemon.Add(id, true);
                return true;
            }
        }
        /// <summary>
        /// 点击宠物
        /// </summary>
        /// <param name="clickedId">宠物ID</param>
        /// <param name="clickedType">宠物类型</param>
        /// <param name="position">宠物相对位置</param>
        public void ClickedPokemon(int clickedId, bool clickedType,Vector3 position)
        {
            pokemonID = clickedId;
            pokemonType = clickedType;
            pokemonPosition = position;
            SceneManager.LoadScene(catchSceneName);
        }
        #endregion
    }
}


