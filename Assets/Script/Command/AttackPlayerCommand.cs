using Model;
using QFramework;
using UnityEngine;
using System;
namespace Command
{
    public class AttackPlayerCommand : AbstractCommand
    {
        private int atk;
        public AttackPlayerCommand(int atk)
        {
            this.atk = atk;
        }
        public void Init(int atk)
        {
            this.atk = atk;
        }
        protected override void OnExecute()
        {
            IPlayerModel playerModel = this.GetModel<IPlayerModel>();
            if (playerModel.injured == false)
            {
                this.GetModel<IPlayerModel>().CurrentHealth.Value -= atk;
            }
        }
    }
}