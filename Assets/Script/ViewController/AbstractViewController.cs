using DiceKnightArchitecture;
using QFramework;
using UnityEngine;
namespace ViewController
{
    public abstract class AbstractViewController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return DiceKnight.Interface;
        }
    }
}