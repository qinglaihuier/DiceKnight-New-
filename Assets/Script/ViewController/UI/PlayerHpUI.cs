using Model;
using QFramework;
using UnityEngine.UI;
using UnityEngine;

namespace ViewController
{
    public class PlayerHpUI : AbstractViewController
    {
        #region 依赖组件
        [SerializeField] private Image backHpPadding;
        [SerializeField] private Image frontHpPadding;
        #endregion

        #region  Model
        private IPlayerModel playerModel;
        #endregion
        private void Awake()
        {
            playerModel = this.GetModel<IPlayerModel>();
            playerModel.CurrentHealth.Register(OnPlayerHealthChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        // Update is called once per frame
        void Update()
        {
            if(backHpPadding.fillAmount > frontHpPadding.fillAmount)
            {
                backHpPadding.fillAmount -= Time.deltaTime;
                if(backHpPadding.fillAmount < frontHpPadding.fillAmount)
                {
                    backHpPadding.fillAmount = frontHpPadding.fillAmount;
                }
            }
        }
        private void OnPlayerHealthChanged(int currentHealth)
        {
            frontHpPadding.fillAmount = (float)playerModel.CurrentHealth.Value / playerModel.MaxHealth.Value; 
        }
    }
}

