namespace Public
{
    //动画方式
    public enum AnimatorType
    {
        play,
        stop,
        playback,
    }
    //破碎物体状态效果
    public enum BrokenEffObjType
    {
        DefaultState,
        RustState,
    }
    //物品
    public enum GameObjectType
    {
        TestTube,   //试管
        Dish,       //盘子
        DusterCloth, //抹布
        IronSupport,  //铁架台
        Thermometer,  //温度计
        Asbestos,     //石棉网
        Beaker,       //烧杯
        SbBeaker,      //破烧杯
        Lamp,         //酒精灯
        LampCap,         //酒精灯
        Balance,         //电子秤
        Auncel,         //天平
        Keg,              //桶
        AirBall,          //气球
        BalanceBall,      //皮球
        Pulse,           //豆子
        pumup,            //打气筒
        BallNeedle,          //球针
        CoTestTube,    //放置大理石的试管
        WoodCube,      //木块
        WoodPlank,     //木板
        WildMouthBottle,  //广口瓶
        IMedicineSpoon,    //药勺
        Tripod,          //三脚架台
        Clip,          //架子
        Funnel,        //漏斗
        Bag,          //塑料袋
        WaterTank,  //水槽
        LMatch,    //左火柴
        RMatch,    //右火柴
        MatchBox, //火柴盒
        CottonBall, //棉花球
        GlassSheet, //盖玻片
        Lid,       //盖子
        KMn04,     //高锰酸钾
        RubberStopper,//橡皮塞
        TubeRack,//试管架
        Tweezers,//镊子
        TestTubeBrush,//试管刷
        MetalBlock,   //金属块
        Weight,     //砝码
    }
    //液体类型
    public enum LiquidType
    {
        noState,
        WhiteVinegar,   //白醋
        Saccharose,     //蔗糖
        Water,          //清水
        Iodine,         //碘酒
        HydrochloricAcid,  //盐酸
    }
    //吸管状态
    public enum DropperStateType
    {
        defaultState,
        /// <summary>
        /// 吸水
        /// </summary>
        AbsorbWater,   //吸水
        /// <summary>
        /// 滴水
        /// </summary>
        DripWater,     //滴水
        /// <summary>
        /// 满水
        /// </summary>
        fullWater,     //满水
        /// <summary>
        /// 没水
        /// </summary>
        nullWater,     //没水
    }
    //显微镜部件
    public enum ScrewType
    {
        CoarseFocusingScrew,  //粗准焦螺旋
        FineFocusingScrew,    //细准焦螺旋
        noEvent,              //镜臂
        ObjectGlass,          //物镜（转换器上的）
        Eyepiece,             //目镜
        DimmerLight,             //透光镜
        RefractionMirror,         //反光镜
    }
    //显微镜物镜倍数
    public enum ObjectiveLensMutiple
    {
        x4,
        x8,
        x10,
    }
    //拨片类型
    public enum SlideType
    {
        GlassSlide,  //载玻片
        Coverslip,   //盖玻片
    }
    //方向类型
    public enum DirectionType
    {
        Left,
        Right,
        Center,
    }
    //水的沸腾实验顺序
    public enum BoilingOfWaterStep
    {
        Asbestos,     //石棉网
        Beaker,       //烧杯
        Lamp,         //酒精灯
        Fire,         //点燃酒精灯
        Thermometer,  //温度计
        SBBeaker,
    }
    public enum WaterBoilinStep
    {
        Asbestos,     //石棉网
        Beaker,       //烧杯
        Lamp,         //酒精灯
        Fire,         //点燃酒精灯
        Thermometer,  //温度计
     //   Clip,          //架子
        Funnel,        //漏斗
      //  Bag,          //塑料袋
    }
    public enum PeriscopeType   //潜望镜
    {
        Down,
        Left,
        Right,
        Square,
        Front,
        Up,
        Circle,
        TopGlass,
        BottomGlass,
        Glass,
    }
    public enum BoxType
    {
        Normal,
        flat,
        Vertical,
        Oblique,

    }
    public enum HeatBeakType
    {
        Oil,   //油
        Weater,//水
    }
    public enum WoodType
    {
        Null,            //平
        Bevel,           //斜
        Bevell,          //斜斜
        Bevelll,          //斜斜斜
    }

    //空气有质量吗？
    public enum AirMassAuncelStep
    {
        ClaeanAuncel,     //电子秤清零
        Ball,       //烧杯
        Lamp,         //酒精灯
        Fire,         //点燃酒精灯
        Thermometer,  //温度计
    }
    public enum AirMassBalancelStep
    {
        LeftBucket,        //左桶
        RightBucket,       //右桶
        InPutBall,         //放入球
        InputBeans,        //放入气球
        KeepBalance,       //保持平衡
        InflationBall,      //气球打气
    }
    public enum AuncelPinot    //天平挂点
    {
        LeftPoint,    //左挂点
        RightPoint,    //右挂点
    }
    public enum Drug   //化学用品/容器中的物品
    {
        Salt,//盐
        Flour,//面粉
        Sand,//沙子
        KMnO4,//高锰酸钾
    }
}

