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

        #region ARCoreTest
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


        /// <summary>
        /// 应用退出
        /// </summary>
        public void AppExit()
        {
            Application.Quit();
        }
        /// <summary>
        /// 是否支持ARCore
        /// </summary>
        /// <returns>是否支持,false:不支持，true:支持</returns>
        public bool IsSupportARCore()
        {
            int isSupport = PlayerPrefs.GetInt("SupportARCore", -1);
            if (isSupport ==1)
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
        /// 抓捕场景设置
        /// </summary>
        /// <param name="sceneName">抓捕场景名称</param>
        public void SetCatchScene(string sceneName)
        {
            catchSceneName = sceneName;
        }
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
        public void StartGame(float distance,int number)
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
            //todo
        }
        /// <summary>
        /// 游戏初始化状态
        /// </summary>
        /// <returns>true：已经初始化；false：未初始化</returns>
        public bool GameInitialization()
        {
            //todo
            return false;
        }

    }
}


