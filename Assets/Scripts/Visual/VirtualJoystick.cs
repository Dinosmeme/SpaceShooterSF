using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceShooter
{

    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image m_JoyBack;
        [SerializeField] private Image m_Joystick;

        public Vector3 Value { get; private set; }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            // Transform coordinates to match JoyBack area ((0.0) in bottom left and (400.400) in top right)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_JoyBack.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out position);

            // normalize coordinates tranformed above in recttransformutility
            position.x = (position.x / m_JoyBack.rectTransform.sizeDelta.x);
            position.y = (position.y / m_JoyBack.rectTransform.sizeDelta.y);

            // Shifts (0.0) to center 
            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            Value = new Vector3(position.x, position.y, 0);

            if (Value.magnitude > 1)
                Value = Value.normalized;

            float offsetX = m_JoyBack.rectTransform.sizeDelta.x / 2 - m_Joystick.rectTransform.sizeDelta.x / 2;
            float offsetY = m_JoyBack.rectTransform.sizeDelta.y / 2 - m_Joystick.rectTransform.sizeDelta.y / 2;


            m_Joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }

}
