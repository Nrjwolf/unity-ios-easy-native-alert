#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
#endif

public class IOSNativeAlert
{
    #if UNITY_IOS && !UNITY_EDITOR
    public class AlertButton
    {
        public string Title;
        public Action Callback;
        internal string m_Id;

        public AlertButton(string title, Action callback)
        {
            Title = title;
            Callback = callback;
        }
    }

    // button callback delegate
    private delegate void MonoPAlertButtonDelegate(string buttonId);
    [DllImport("__Internal")] private static extern void IOSRegisterMessageHandler(MonoPAlertButtonDelegate onButtonClick);
    [DllImport("__Internal")] private static extern void _IOSShowAlertMsg(string title, string message, string[] buttons, int buttonsLength);
    [DllImport("__Internal")] private static extern void _IOSShowToast(string message, bool isLongDuration);

    private static AlertButton[] m_CurrentAlertButtons;

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize() => IOSRegisterMessageHandler(OnAlertButtonClick);

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

    public static void ShowAlertMessage(string title, string message) => ShowAlertMessage(title, message, new AlertButton("Ok", null));
    public static void ShowAlertMessage(string title, string message, params AlertButton[] buttons)
    {
        // creating unique id for buttons
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].m_Id = buttons[i].Title + i;

        // cache current alert buttons 
        m_CurrentAlertButtons = buttons;

        // call native ios function
        _IOSShowAlertMsg(title, message, buttons.Select(x => x.Title).ToArray(), buttons.Length);
    }

    public static void ShowToast(string text, bool isLongDuration = false) => _IOSShowToast(text, isLongDuration);

    #endif
}