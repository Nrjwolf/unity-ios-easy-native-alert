#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
#if !UNITY_EDITOR
using System.Linq;
#endif
using UnityEngine;
#endif

namespace Nrjwolf.Tools
{
    public enum ButtonStyle
    {
        Default,
        Cancel,
        Destructive,
    }

    public class IOSNativeAlert
    {
#if UNITY_IOS
        private enum AlertStyle
        {
            Sheet,
            Alert,
        }

        public class AlertButton
        {
            public ButtonStyle Style;
            public string Title;
            public Action Callback;
            internal string m_Id;

            public AlertButton(string title, Action callback, ButtonStyle style = ButtonStyle.Default)
            {
                Title = title;
                Callback = callback;
                Style = style;
            }
        }

        // button callback delegate
        private delegate void MonoPAlertButtonDelegate(string buttonId);
        [DllImport("__Internal")] private static extern void IOSRegisterMessageHandler(MonoPAlertButtonDelegate onButtonClick);
        [DllImport("__Internal")] private static extern void _IOSShowAlertMsg(int alertStyle, string title, string message, string[] buttons, int[] buttonsStyle, int buttonsLength);
        [DllImport("__Internal")] private static extern void _IOSShowToast(string message, bool isLongDuration);

        private static AlertButton[] m_CurrentAlertButtons;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
#if !UNITY_EDITOR
            IOSRegisterMessageHandler(OnAlertButtonClick);
#endif
        }

        [AOT.MonoPInvokeCallback(typeof(MonoPAlertButtonDelegate))]
        public static void OnAlertButtonClick(string buttonId)
        {
            if (m_CurrentAlertButtons == null || m_CurrentAlertButtons.Length == 0) return;

            Debug.Log($"Clicked {buttonId}");
            foreach (var alertButton in m_CurrentAlertButtons)
            {
                if (alertButton.m_Id == buttonId)
                {
                    alertButton.Callback?.Invoke();
                    break;
                }
            }
        }

        public static void ShowSheetMessage(string title, string message) => ShowAlertMessage(title, message, new AlertButton("Ok", null));
        public static void ShowSheetMessage(string title, string message, params AlertButton[] buttons) => CallNativeAlertMessage(AlertStyle.Sheet, title, message, buttons);

        public static void ShowAlertMessage(string title, string message) => ShowAlertMessage(title, message, new AlertButton("Ok", null));
        public static void ShowAlertMessage(string title, string message, params AlertButton[] buttons) => CallNativeAlertMessage(AlertStyle.Alert, title, message, buttons);

        private static void CallNativeAlertMessage(AlertStyle alertStyle, string title, string message, params AlertButton[] buttons)
        {
            // creating unique id for buttons
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].m_Id = buttons[i].Title + i;

            // cache current alert buttons 
            m_CurrentAlertButtons = buttons;

#if !UNITY_EDITOR
            // call native ios function
            _IOSShowAlertMsg((int)alertStyle, title, message, buttons.Select(x => x.Title).ToArray(),  buttons.Select(x => (int)x.Style).ToArray(), buttons.Length);
#endif
        }

        public static void ShowToast(string text, bool isLongDuration = false)
        {
#if !UNITY_EDITOR
            _IOSShowToast(text, isLongDuration);
#endif
        }

#endif
    }
}
