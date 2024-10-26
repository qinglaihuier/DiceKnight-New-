using UnityEngine;
using QFramework;
using UnityEngine.AddressableAssets;
using ScriptableObjectData;

namespace Model
{
    public interface IPlayerModel : IModel
    {
        public BindableProperty<int> CurrentHealth { get; }
        public BindableProperty<int> MaxHealth { get; }
        public BindableProperty<int> PlayerSpeedSize { get; }
        public BindableProperty<float> DiceSpeedSize { get; }
        public BindableProperty<int> PAtk { get; }
        public BindableProperty<int> MAtk { get; }
        public BindableProperty<float> InvincibleTime { get; }
        public bool injured { get; set; }
        public Vector3 PlayerPosition{get; set;}
    }
    public class PlayerModel : AbstractModel, IPlayerModel
    {
        private const string PLAYER_DATA = "PlayerData";

        public BindableProperty<int> CurrentHealth { get; private set; }
        public BindableProperty<int> MaxHealth { get; private set; }
        public BindableProperty<int> PlayerSpeedSize { get; private set; }
        public BindableProperty<float> DiceSpeedSize { get; private set; }
        public BindableProperty<int> PAtk { get; private set; }
        public BindableProperty<int> MAtk { get; private set; }
        public BindableProperty<float> InvincibleTime { get; private set; }
        public bool injured { get; set; } = false;
        public Vector3 PlayerPosition { get; set; }

        protected override void OnInit()
        {
            //同步加载数据
            //TODO : 游戏开始时显示加载界面，异步加载数据
            //初始化
            var handle = Addressables.LoadAssetAsync<PlayerData>(PLAYER_DATA);

            PlayerData playerData = handle.WaitForCompletion();

            CurrentHealth = new BindableProperty<int>(playerData.health);
            MaxHealth = new BindableProperty<int>(playerData.health);
            PlayerSpeedSize = new BindableProperty<int>(playerData.playerSpeedSize);
            DiceSpeedSize = new BindableProperty<float>(playerData.diceSpeedSize);
            PAtk = new BindableProperty<int>(playerData.pAtk);
            MAtk = new BindableProperty<int>(playerData.mAtk);
            InvincibleTime = new BindableProperty<float>(playerData.invincibleTime);
        }
    }
}