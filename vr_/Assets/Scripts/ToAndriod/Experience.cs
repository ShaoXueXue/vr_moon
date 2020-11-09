
using System.Collections.Generic;

public class Experience
{
    //实验名称，unity端根据这个字段区分当前是哪个实验 //实验缩略图按钮名称，unity端根据这个字段确定是哪个实验，进行切换
    public string experienceName;
    //特殊按钮，unity端根据这个字段区分点击事件
    public List<int> buttonName;
    public List<string> step;

    public List<int> imgName;
    //保存实验按钮
    public string buttonSaveLab;
    //保存实验时的得分
    public int score;

    //星星功能
    public List<string> capacity; //2没星 1有星 0观察能力 1思维能力 2归纳能力 3空间能力 4实验比较能力

    //保存实验时保存的数据
    public string labData;

    //我的实验下的保存的实验的进入按钮
    public string recomeBotton;

    public string endanimation;   //加载实验  传入“LoadCallBack”值，传入方法“endAnimation”

    public string experimentEvent;    //按钮事件，小屏按钮是否可以开启

    public List<string> experimentSmallSteps;            //实验小步骤，存取 0/1
}

