using System.Collections;
using System.Collections.Generic;
using DiceKnightArchitecture;
using UnityEngine;
using ViewController;

/// <summary>
/// 游戏初始化，加载必须的数据等
/// </summary>
public class GameInit : AbstractViewController
{
    private void Awake()
    {
        //初始化框架
        //TDDO : 显示加载界面，数据在后台异步
        DiceKnight.CreateArchitecture();
    }
}


