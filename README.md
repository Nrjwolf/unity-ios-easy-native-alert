# IOS Easy Alert

### Install

Add this as a package to your project by adding the below as an entry to the dependencies in the `/Packages/manifest.json` file:

```json
"nrjwolf.games.iosnativealerts": "git+https://github.com/Nrjwolf/unity-ios-easy-native-alert"
```
For more information on adding git repositories as a package see the [Git support on Package Manager](https://docs.unity3d.com/Manual/upm-git.html) in the Unity Documentation.

---

Example
``` c#
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
```

![](https://github.com/Nrjwolf/unity-ios-easy-native-alert/blob/master/.github/sceenshot_ios_alerts.png "") </br>

For *Android* check another my plugin https://github.com/Nrjwolf/unity-android-easy-native-alerts <br>
Do not forgot to ⭐️ it.

>I'm on [reddit](https://www.reddit.com/r/Nrjwolf/)
>Мой [телеграм канал](https://t.me/nrjwolf_games)
