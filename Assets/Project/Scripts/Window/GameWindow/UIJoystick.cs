using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace StikerBombing.UI
{
    public class UIJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public static Action DragBeginned = delegate { };
		public static Action<Vector3> Dragged = delegate { };
		public static Action DragEnded = delegate { };

		public static UIJoystick Instance = null;

		private Vector2 _swipeDelta = Vector2.zero;

		public bool IsDragging
		{
			get;
			private set;
		}

		private void Awake()
        {
			Instance = this;
		}

		public void OnBeginDrag(PointerEventData data)
		{
			IsDragging = true;

			DragBeginned();
		}

		public void OnDrag(PointerEventData data)
		{
			_swipeDelta += data.delta;

			Dragged(_swipeDelta);
		}

		public void OnEndDrag(PointerEventData data)
		{
			IsDragging = false;

			_swipeDelta = Vector2.zero;

			DragEnded();
		}
    }
}