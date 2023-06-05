using System;
using System.Collections.Generic;
using System.Linq;
using Core._Common.Extensions;
using Core.Menu.Popup;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Popup
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private PopupSettings _popupSettings;
        private Stack<PopupQueueElement> _queueElements;
        private event Action ElementQueued;
        public event Action OnPopupsClosed;

        [SerializeField] private List<PopupObject> popupObjects;
        private Dictionary<PopupQueueElement, Popup> _popupMap;

        private Stack<Popup> _activePopupStack;
        [SerializeField] private Image blackOut;

        private void Awake()
        {
            _activePopupStack = new Stack<Popup>();
            _popupMap = popupObjects.ToDictionary(x => x.elementType, x => x.element);
            ElementQueued += DisplayQueuePopup;
        }

        private void OnDestroy()
        {
            ClearStackOfQue();
            ElementQueued -= DisplayQueuePopup;
        }

        private void Start()
        {
            if (_queueElements == null) return;
            DisplayQueuePopup();
            
        }

        /// <summary>
        /// Queue popups are popups that are queued from other systems. 
        /// </summary>
        private void DisplayQueuePopup()
        {
            if (_queueElements == null || _queueElements.Count == 0)
            {
                if (_activePopupStack.Count == 0) CloseBlackOut();
                return;
            }
            Popup popup = _popupMap[_queueElements.Pop()];
            DisplayPopup(popup);
        }

        /// <summary>
        /// Show the popup.
        /// </summary>
        /// <param name="target"></param>
        public void DisplayPopup(Popup target)
        {
            if(target.gameObject.activeSelf) return;
            if(target == null) Debug.Log("displayPopup target = null");
            target.transform.SetAsLastSibling();
            OpenPanel(target.Canvas);
            if (_activePopupStack.Count == 0 && !blackOut.gameObject.activeSelf)
            {
                OpenBlackOut();
            }
            _activePopupStack.Push(target);
        }

        /// <summary>
        /// Try to discard the currently uppermost popup. Checks whether popup is discardable.
        /// </summary>
        public void DiscardPopup()
        {
            if (_activePopupStack.Count == 0) return;
            if (!_activePopupStack.Peek().isDiscardable) return;
            HidePopup();
        }

        /// <summary>
        /// Try to hide the currently uppermost popup. Doesn't check whether popup is discardable.
        /// </summary>
        public void HidePopup()
        {
            if(_activePopupStack.Count == 0) return;
            ClosePanel(_activePopupStack.Pop());
        }

        /// <summary>
        /// Visually animates popup in.
        /// </summary>
        /// <param name="panel"></param>
        private void OpenPanel(CanvasGroup panel)
        {
            panel.gameObject.SetActive(true);
            AnimationValues<Vector2> scaleValues = _popupSettings.scale.show;
            AnimationValues<float> alphaValues = _popupSettings.alpha.show;
            panel.transform.localScale = scaleValues.initial;
            panel.alpha = alphaValues.initial;

            Sequence sequence = DOTween.Sequence();
            Tweener scaleTween = panel.transform.DOScale(scaleValues.final, scaleValues.duration)
                .SetEase(scaleValues.ease);
            Tweener alphaTween = DOTween.To(() => panel.alpha, x => panel.alpha = x,
                alphaValues.final,
                alphaValues.duration);
            sequence.Join(scaleTween);
            sequence.Join(alphaTween);
        }

        public void ClosePanel(Popup popup)
        {
            CanvasGroup canvas = popup.Canvas;
            AnimationValues<Vector2> scaleValues = _popupSettings.scale.hide;
            AnimationValues<float> alphaValues = _popupSettings.alpha.hide;
            Sequence sequence = DOTween.Sequence();
            Tweener scaleTween = popup.transform.DOScale(scaleValues.final, scaleValues.duration)
                .SetEase(scaleValues.ease);
            Tweener alphaTween = DOTween.To(() => canvas.alpha, x => canvas.alpha = x,
                alphaValues.final,
                alphaValues.duration);
            sequence.Join(scaleTween);
            sequence.Join(alphaTween);
            sequence.OnComplete(() =>
            {
                popup.gameObject.SetActive(false);
                OnPopupsClosed?.Invoke();
                DisplayQueuePopup();
            });
        }

        private void OpenBlackOut()
        {
            AnimationValues<float> values = _popupSettings.blackout.show;
            blackOut.gameObject.SetActive(true);
            blackOut.SetColor(a: values.initial);
            blackOut
                .DOColor(CreateColor(blackOut.color, values.final), values.duration)
                .SetEase(values.ease);
        }

        private void CloseBlackOut()
        {
            AnimationValues<float> values = _popupSettings.blackout.hide;
            blackOut.gameObject.SetActive(true);
            blackOut
                .DOColor(CreateColor(blackOut.color, values.final), values.duration)
                .SetEase(values.ease).OnComplete(() => { blackOut.gameObject.SetActive(false); });
        }

        private Color CreateColor(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        public void EnqueuePopup(PopupQueueElement element)
        {
            _queueElements ??= new Stack<PopupQueueElement>();
            _queueElements.Push(element);
            ElementQueued?.Invoke();
            
        }
        
        public void EnqueuePopupWithoutInvoke(PopupQueueElement element)
        {
            _queueElements ??= new Stack<PopupQueueElement>();
            _queueElements.Push(element);
        }

        public void ClearStackOfQue()
        {
            if(_queueElements == null) return;
            
            for (; 0 < _queueElements.Count ;)
            {
                _queueElements.Pop();
            }
        }
        
        public void OpenThePopup()
        {
            EnqueuePopup(PopupQueueElement.miniGame_1);
        }

    }
    
    public enum PopupQueueElement
    {
        miniGame_1,
    }

    [Serializable]
    public class PopupObject
    {
        public PopupQueueElement elementType;
        public Popup element;
    }
}