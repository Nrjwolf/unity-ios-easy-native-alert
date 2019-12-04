#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

public class IOSNativeAlert
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport ( "__Internal" )] private static extern void _IOSShowAlertMsg (string title, string message);
    [DllImport ( "__Internal" )] private static extern void _IOSShowToast (string message, bool isLongDuration);
    public static void ShowAlertMessage (string title, string message) => _IOSShowAlertMsg (title, message);
    public static void ShowToast (string text, bool isLongDuration = false) => _IOSShowToast (text, isLongDuration);
#endif
}
