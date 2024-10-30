using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Model;
using System;
namespace DiceKnightArchitecture
{
    public class DiceKnight : Architecture<DiceKnight>
    {
        protected override void Init()
        {
            this.RegisterModel<IPlayerModel>(new PlayerModel());
            this.RegisterModel<IEnemyRefreshModel>(new EnemyRefreshModel());
            this.RegisterModel<IEnemyDataConfigurationModel>(new EnemyDataConfigurationModel());
            this.RegisterModel<IDiceItemDataConfigurationModel>(new DiceItemDataConfigurationModel());
            this.RegisterModel<IDiceItemExcuteDataConfigurationModel>(new DiceItemExcuteDataConfigurationModel());
            
            this.RegisterSystem<IInputSystem>(new InputSystem());
            this.RegisterSystem<IDiceItemExcuteSystem>(new DiceItemExcuteSystem());
            this.RegisterSystem<IShopSystem>(new ShopSystem());
        }
    }
}
