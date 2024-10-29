using UnityEngine;
using UnityEngine.EventSystems;

namespace ViewController
{
    public class PurchasedCommodity : AbstractViewController, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            string name = "Test";
        }
    }
}