using UnityEngine;
using UnityEngine.EventSystems;

using BattleTank.Scripts.Runtime.Contracts.Interfaces;

namespace BattleTank.Scripts.Runtime.Input.Virtual
{
    public class VirtualStick : MonoBehaviour, IStick, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _bg;
        [SerializeField] private RectTransform _handle;

        [SerializeField] private float _radius = 100f;
        [SerializeField, Range(0f, 1f)] private float _deadZone = 0.2f;

        private Vector2 _value;
        private bool _isPressed;

        public Vector2 Value => _value;
        public bool IsPressed => _isPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            UpdateStick(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isPressed) return;
            UpdateStick(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            _value = Vector2.zero;

            if (_handle != null)
                _handle.anchoredPosition = Vector2.zero;
        }

        private void UpdateStick(PointerEventData eventData)
        {
            if (_bg == null || _handle == null) return;

           
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _bg,
                    eventData.position,
                    eventData.pressEventCamera,  
                    out var localPoint))
            {
                return;
            }

          
            var radius = Mathf.Max(1f, _radius);

            
            var clamped = Vector2.ClampMagnitude(localPoint, radius);

         
            var normalized = clamped / radius; 

          
            if (normalized.magnitude < _deadZone)
                normalized = Vector2.zero;

            _value = normalized;
            _handle.anchoredPosition = normalized * radius;
        }
    }
}
