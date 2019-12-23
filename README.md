# IOS Easy Alert

### Instalation

Copypaste ```IOSNativeAlert``` in ```Plugins``` folder

---

``` c#
#if UNITY_IOS && !UNITY_EDITOR
  // Show alert
  IOSNativeAlert.ShowAlertMessage("No internet connection", "Sorry, but you can't to rate without internet access :(");
  // Show toast for 1 sec
  IOSNativeAlert.ShowToast("No internet connection", true);
#endif
```

![](https://github.com/Nrjwolf/unity-ios-easy-native-alert/blob/master/images/AlertOk.jpg "Alert") </br>
Alert

![](https://github.com/Nrjwolf/unity-ios-easy-native-alert/blob/master/images/Toast.jpg "Toast") </br>
Toast (disappears after .5 or 1 sec)  

>I'm on [reddit](https://www.reddit.com/r/Nrjwolf/)  
>Мой [телеграм канал](https://t.me/nrjwolf_live)
