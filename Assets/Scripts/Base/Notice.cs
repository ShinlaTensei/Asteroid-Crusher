using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Base
{
    public class Notice : MonoBehaviour
    {
        [SerializeField] private Text messageText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button cancelButton;

        private UnityAction acceptAction;
        private UnityAction cancleAction;

        public void Init(string message, UnityAction acceptCallback = null, UnityAction cancelCalback = null)
        {
            messageText.text = message;
            if (acceptCallback != null)
            {
                acceptButton.gameObject.SetActive(true);
                acceptButton.onClick.RemoveAllListeners();
                acceptAction += acceptCallback;
                acceptAction += OnClickAccept;
                acceptButton.onClick.AddListener(acceptAction);
            }
            else
            {
                acceptButton.onClick.RemoveAllListeners();
                acceptAction = null;
                acceptAction += OnClickAccept;
                acceptButton.onClick.AddListener(acceptAction);
            }

            if (cancelCalback != null)
            {
                cancelButton.onClick.RemoveAllListeners();
                cancleAction += cancelCalback;
                cancleAction += OnClickCancle;
                cancelButton.onClick.AddListener(cancleAction);
            }
            else
            {
                cancelButton.onClick.RemoveAllListeners();
                cancleAction = null;
                cancleAction += OnClickCancle;
                cancelButton.onClick.AddListener(cancleAction);
            }
        }

        public void OnClickAccept()
        {
            Destroy(gameObject);
        }

        public void OnClickCancle()
        {
            Destroy(gameObject);
        }
    }
}