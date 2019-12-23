#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
#endif

public class IOSNativeAlert
{
    #if UNITY_IOS && !UNITY_EDITOR
    public class AllertButton
    {
        public string Title;
        public Action Callback;
        internal string m_Id;

        public AllertButton(string title, Action callback)
        {
            Title = title;
            Callback = callback;
        }
    }

    // button callback delegate
    private delegate void MonoPAllertButtonDelegate(string buttonId);
    [DllImport("__Internal")] private static extern void IOSRegisterMessageHandler(MonoPAllertButtonDelegate onButtonClick);
    [DllImport("__Internal")] private static extern void _IOSShowAlertMsg(string title, string message, string[] buttons, int buttonsLength);
    [DllImport("__Internal")] private static extern void _IOSShowToast(string message, bool isLongDuration);

    private static AllertButton[] m_CurrentAllertButtons;

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize() => IOSRegisterMessageHandler(OnAlertButtonClick);

    [AOT.MonoPInvokeCallback(typeof(MonoPAllertButtonDelegate))]
    public static void OnAlertButtonClick(string buttonId)
    {
        if (m_CurrentAllertButtons == null || m_CurrentAllertButtons.Length == 0) return;

        Debug.Log($"Clicked {buttonId}");
        foreach (var allertButton in m_CurrentAllertButtons)
        {
            if (allertButton.m_Id == buttonId)
            {
                allertButton.Callback?.Invoke();
                break;
            }
        }
    }

    public static void ShowAlertMessage(string title, string message) => ShowAlertMessage(title, message, new AllertButton("Ok", null));
    public static void ShowAlertMessage(string title, string message, params AllertButton[] buttons)
    {
        // creating unique id for buttons
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].m_Id = buttons[i].Title + i;

        // cache current allert buttons 
        m_CurrentAllertButtons = buttons;

        // call native ios function
        _IOSShowAlertMsg(title, message, buttons.Select(x => x.Title).ToArray(), buttons.Length);
    }

    public static void ShowToast(string text, bool isLongDuration = false) => _IOSShowToast(text, isLongDuration);

    #endif
}