# IOS Easy Alert

### Install

Copypaste ```IOSNativeAlert``` in ```Plugins``` folder

---

``` c#
#if UNITY_IOS && !UNITY_EDITOR
  // Show simple alert with 'Ok' button
  IOSNativeAlert.ShowAlertMessage("No internet connection", "Sorry, but you can't to rate without internet access :(");

  // Alert with custom buttons
  IOSNativeAlert.ShowAlertMessage(
                  "My title?", 
                  "My message?",
                  new IOSNativeAlert.AlertButton("Cancel", () => IOSNativeAlert.ShowToast("Cancel")), // show 'toast' as callback 
                  new IOSNativeAlert.AlertButton("Ok", () => IOSNativeAlert.ShowToast("Ok"))
                  );

  // Show toast for 1 sec
  IOSNativeAlert.ShowToast("No internet connection", true);
#endif
```

![](https://github.com/Nrjwolf/unity-ios-easy-native-alert/blob/master/images/AlertOk.jpg "Alert") </br>
Simple alert

![](https://github.com/Nrjwolf/unity-ios-easy-native-alert/blob/master/images/CustomButtons.jpg "Custom buttons") </br>
Alert with custom buttons

![](https://github.com/Nrjwolf/unity-ios-easy-native-alert/blob/master/images/Toast.jpg "Toast") </br>
Toast (disappears after .5 or 1 sec)  

>I'm on [reddit](https://www.reddit.com/r/Nrjwolf/)  
>Мой [телеграм канал](https://t.me/nrjwolf_live)
