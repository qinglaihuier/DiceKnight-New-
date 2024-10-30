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
            this.RegisterModel<IAllWaveInformationModel>(new AllWaveInformationModel());
            this.RegisterModel<IEnemyDataConfigurationModel>(new EnemyDataConfigurationModel());
            this.RegisterModel<IDicePropertyDataConfigurationModel>(new DicePropertyDataConfigurationModel());
            this.RegisterModel<IPropertyExcuteDataConfigurationModel>(new PropertyExcuteDataConfigurationModel());
            
            this.RegisterSystem<IInputSystem>(new InputSystem());
            this.RegisterSystem<IPropertyExcuteSystem>(new PropertyExcuteSystem());
            this.RegisterSystem<IShopSystem>(new ShopSystem());
        }
    }
}
