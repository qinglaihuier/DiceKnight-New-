using System;
using System.Collections;
using InputConfigurationSpace;
using Model;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using ViewController;

namespace ViewController
{
    public struct GetPlayerInjureStateStruct
    {

    }
    public class PlayerViewController : AbstractViewController
    {
        #region 依赖组件
        new private Rigidbody2D rigidbody2D;
        private Animator animator;
        private Dice dice;
        #endregion

        #region Model
        private IPlayerModel playerModel;
        #endregion

        #region  System
        private IInputSystem inputSystem;
        #endregion

        #region  缓存
        private InputConfiguration.GamePlayActions gamePlayActions;
        private Vector3 diceLocalPosition;

        private int diceLayer;

        private int injuredAnimHash;
        #endregion
        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            dice = GetComponentInChildren<Dice>();
            animator = GetComponent<Animator>();

            diceLocalPosition = dice.transform.localPosition;
            diceLayer = dice.gameObject.layer;

            playerModel = this.GetModel<IPlayerModel>();

            inputSystem = this.GetSystem<IInputSystem>();
            gamePlayActions = inputSystem.GetGamePlayActions();

            injuredAnimHash = Animator.StringToHash("injured");
        }
        private void OnEnable()
        {
            //TODO : 考虑自动化注册的取消
            gamePlayActions.ThrowDice.performed += ThrowDice;
            playerModel.CurrentHealth.Register(BeAttacked).UnRegisterWhenGameObjectDestroyed(gameObject); //自动取消注册
        }
        private void OnDisable()
        {
            gamePlayActions.ThrowDice.performed -= ThrowDice;
        }

        private void Update()
        {
            float inputValueX = gamePlayActions.Move.ReadValue<Vector2>().x;
            if (Mathf.Abs(inputValueX) > .1f)
            {
                Vector3 scale = Vector3.one;
                scale.x = gamePlayActions.Move.ReadValue<Vector2>().x > 0 ? -1 : 1;
                transform.localScale = scale;
            }
            playerModel.PlayerPosition = transform.position;
        }
        private void FixedUpdate()
        {
            //考虑是否可以再优化输入的获取？
            rigidbody2D.velocity = gamePlayActions.Move.ReadValue<Vector2>() * playerModel.PlayerSpeedSize;
            playerModel.PlayerPosition = transform.position;
        }
        private void ThrowDice(InputAction.CallbackContext context)
        {
            if (dice && !playerModel.injured)
            {
                Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
                //TODO : 投掷骰子逻辑
                dice.Throw(playerModel.PAtk, mousePosInWorld, playerModel.DiceSpeedSize);
                dice = null;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == diceLayer)
            {
                if (other.TryGetComponent<Dice>(out Dice d))
                {
                    dice = d;
                    dice.BePickedUp(transform, diceLocalPosition);
                }
            }
        }

        private void BeAttacked(int i)
        {
            if (playerModel.injured) return;
            StartCoroutine(InvincibleTimeCoroutine());
            Debug.Log("BeAttack");
        }
        private IEnumerator InvincibleTimeCoroutine()
        {
            playerModel.injured = true;
            animator.SetBool(injuredAnimHash, playerModel.injured);
            yield return new WaitForSeconds(playerModel.InvincibleTime.Value);
            playerModel.injured = false;
            animator.SetBool(injuredAnimHash, playerModel.injured);
        }
       
    }

}
