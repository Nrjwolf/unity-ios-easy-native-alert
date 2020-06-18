using UnityEngine;

namespace Nrjwolf.Tools
{
    public class IOSNativeAlertsExample : MonoBehaviour
    {
        private void OnGUI()
        {
#if UNITY_IOS
            GUI.matrix = Matrix4x4.Scale(new Vector3(3.5f, 3.5f, 3.5f));
            if (GUILayout.Button("Simple Allert"))
            {
                IOSNativeAlert.ShowAlertMessage("Simple alert", "Press ok, if you're ok");
            }
            if (GUILayout.Button("Cancel/Ok"))
            {
                IOSNativeAlert.ShowAlertMessage(
                    "Check out my github",
                    "You can find another great plugins for unity on my github account",
                    new IOSNativeAlert.AlertButton("Cancel", null, ButtonStyle.Cancel),
                    new IOSNativeAlert.AlertButton("Github", () => Application.OpenURL("https://github.com/Nrjwolf"))
                    );
            }
            if (GUILayout.Button("Sheet"))
            {
                IOSNativeAlert.ShowSheetMessage(
                    "Do you want to reset your phone?",
                    "Just kidding, I can't do it :)",
                    new IOSNativeAlert.AlertButton("Cancel", null, ButtonStyle.Cancel),
                    new IOSNativeAlert.AlertButton("Let's do it", () => IOSNativeAlert.ShowToast("Reseting..."), ButtonStyle.Destructive)
                    );
            }
#endif
        }
    }
}
