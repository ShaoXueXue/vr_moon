using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class LevleManager : MonoBehaviour
{

    //不同实验的预制体列表
    public List<IBaseScene> levlePrefabs = new List<IBaseScene>();

    //当前的场景
    private GameObject presentLevle = null;

    public Transform nolo = null;

    //public MirrorFlipCamera mirrorFlipCamera=null;
    private void Awake()
    {
        //#if UNITY_EDITOR
        //        mirrorFlipCamera.enabled = false;
        //#else
        //        mirrorFlipCamera.enabled = true;
        //#endif
    }
    private void Start()
    {
        ConnectionManager.Instance.ReceiveFromAndroid += ReceiveFromAndroid;
        //OnSceneLoad("水沸腾了");


    }
    private void OnDestroy()
    {
        ConnectionManager.Instance.ReceiveFromAndroid -= ReceiveFromAndroid;
    }

    private void Update()
    {
        //#if UNITY_EDITOR
        /*以下为前期开发实验*/
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnSceneLoad("molecule");   //分子动画
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            OnSceneLoad("地球的四季变化");
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            OnSceneLoad("模拟昼夜交替");
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            OnSceneLoad("日食月食");
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            OnSceneLoad("观察鱼");
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            OnSceneLoad("铁生锈的快慢");
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            OnSceneLoad("测量水的温度");
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            OnSceneLoad("水的沸腾");
        }
        /*以下为小学实验-上册18个实验*/
        else if (Input.GetKeyUp(KeyCode.A))
        {
            OnSceneLoad("观察叶子");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            OnSceneLoad("观察蚯蚓在土壤中的活动");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            OnSceneLoad("观察月相");
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            OnSceneLoad("水沸腾了");
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            OnSceneLoad("空气有质量吗");
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            OnSceneLoad("测量气温");
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            OnSceneLoad("观察食盐,沙,面粉等物质在水中的溶解情况");
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            OnSceneLoad("分离食盐与水的方法");
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            OnSceneLoad("骨骼、关节和肌肉");
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            OnSceneLoad("运动与呼吸");
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            OnSceneLoad("运动与心跳");
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            OnSceneLoad("吸热和散热");
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            OnSceneLoad("潜望镜");
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            OnSceneLoad("摩擦力的秘密");
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            OnSceneLoad("杠杆");
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            OnSceneLoad("滑轮");
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            OnSceneLoad("斜面的作用");
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            OnSceneLoad("电磁铁的磁力");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            OnSceneLoad("太阳系");
        }
        //#endif
        /*初中实验*/
        else if (Input.GetKeyUp(KeyCode.U))
        {
            OnSceneLoad("实验室制取二氧化碳");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            OnSceneLoad("观察洋葱表皮细胞");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            OnSceneLoad("用温度计测量水的温度");
        }
        //else if (Input.GetKeyUp(KeyCode.Alpha4))
        //{
        //    OnSceneLoad("探究凸透镜的成像规律");
        //}
    }
    #region//在编辑器内切换场景
    public enum Scene
    {
        功能案例,
        none,
        /*小学实验*/
        观察叶子,
        观察蚯蚓在土壤中的活动,
        观察月相,
        水沸腾了,
        空气有质量吗,
        测量气温,
        观察食盐沙面粉等物质在水中的溶解情况,
        分离食盐与水的方法,
        骨骼, 关节和肌肉,
        运动与呼吸,
        运动与心跳,
        吸热和散热,
        潜望镜,
        摩擦力的秘密,
        杠杆,
        滑轮,
        斜面的作用,
        电磁铁的磁力,
        /*初中实验*/
     //   观察洋葱表皮细胞,
        测金属块的密度,
        设计调光台灯的原理电路,
        探究凸透镜成像的规律,
        探究并联电路中的电流规律,
        测量小灯泡额定功率,
        用温度计测量水的温度,
        变更电路,
        用刻度尺测量硬币直径,
        用天平测量金属块质量,
        地球的七大洲和四大洋,
        地球的运动之地球的公转,
        地球和地球仪,
        观察种子的结构,
        显微镜的使用,
        制作和观察洋葱表皮细胞临时装片,
        用组装好的装置制取二氧化碳并检验石灰水,
        过滤浑浊天然水,
        组装用高锰酸钾制取氧气并用排水法收集的装置,

    }
    public Scene cScene = Scene.none;
    public void ChangeSceneByEditor()
    {
        OnSceneLoad(cScene.ToString());
    }
    #endregion

    public void ReceiveFromAndroid(string value)
    {
        Experience exp = JsonMapper.ToObject<Experience>(value);
        // ConnectionManager.Instance.textReceive.text = "从安卓端收到Json消息：\n" + value;
        if (exp.experienceName != "")
        {
            #region//打开使用ab加载
            //  Debug.Log("加载场景" + exp.experienceName);
            ////TODO:从assetBundle加载
            //   GetComponent<TestLoad>().StartLoadScene(exp.experienceName);
            #endregion
            #region//使用本地预制体加载
            OnSceneLoad(exp.experienceName);
            // ConnectionManager.Instance.textReceive.text = "开始加载场景：" + exp.experienceName;
            #endregion
        }
        else
        {
            // ConnectionManager.Instance.showText.text = "传来的场景名称是空的！！！";
        }
    }

    private const float fadeOutTime = 0.2f;
    private const float fadeInTime = 0.2f;
    public void OnSceneLoad(string name)
    {
        if (name.Equals("none"))
            return;
        if (presentLevle == null)
        {
            StartCoroutine(StartScene(name));
        }
        else
        {
            StartCoroutine(SwitchFade(name));
        }
    }

    //首次打开
    private IEnumerator StartScene(string name)
    {
        CamFade.Instance.StartFade(fadeInTime, false);
        yield return new WaitForSeconds(fadeInTime);
        OnLoadScene(name);
    }

    private IEnumerator SwitchFade(string name)
    {
        //淡出
        CamFade.Instance.StartFade(fadeInTime, true);
        yield return new WaitForSeconds(fadeOutTime);
        OnLoadScene(name);
        //等0.02s
        yield return new WaitForSeconds(fadeInTime);
        //加载实验
        //开始淡入
        CamFade.Instance.StartFade(fadeOutTime, false);
        //yield return new WaitForSeconds(fadeInTime);

    }

    private void OnLoadScene(string name)
    {
        Debug.Log("打开实验:" + name);
        if (name == "")
        {

            return;
        }
        else
        {
            if (presentLevle != null && presentLevle.GetComponent<IBaseScene>() != null)
            {
                if (presentLevle.GetComponent<IBaseScene>().experienceName == name)
                {
                    return;
                }
            }

            if (presentLevle != null)
            {
                IBaseScene script = presentLevle.GetComponent<IBaseScene>();
                if (script == null)
                {
                    Debug.Log("事件注销失败");
                }
                else
                    script.OnDestaryThisLab();
                Destroy(presentLevle);

            }
            for (int i = 0; i < levlePrefabs.Count; i++)
            {
                if (levlePrefabs[i].experienceName == name)
                {
                    //实例化实验
                    presentLevle = Instantiate(levlePrefabs[i].gameObject);
                    //调用IBaseScene上的OnStartSceneEnter方法
                    presentLevle.GetComponent<IBaseScene>().OnStartSceneEnter();
                    //ConnectionManager.Instance.sendText.text = "加载实验成功："+ name;
                }
#if UNITY_EDITOR
                nolo.gameObject.SetActive(true);
#endif
#if !UNITY_EDITOR
                if (name == "molecule")
                {
                    nolo.gameObject.SetActive(false);
                }
                else
                {
                    nolo.gameObject.SetActive(true);
                }
#endif

            }
        }
    }
}