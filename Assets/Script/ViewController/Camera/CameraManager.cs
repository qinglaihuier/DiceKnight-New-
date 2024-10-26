using Model;
using QFramework;
using UnityEngine;
namespace ViewController
{
    public class CameraManager : AbstractViewController
    {
        #region Model
        private IPlayerModel playerModel;
        #endregion
        private float positionZ;
        private void Awake()
        {
            positionZ = transform.position.z;
            playerModel = this.GetModel<IPlayerModel>();
        }
        private void LateUpdate()
        {
            Vector2 positionXY = playerModel.PlayerPosition;
            transform.position = new Vector3(positionXY.x, positionXY.y, positionZ);
        }
    }
}