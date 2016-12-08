# Unity3d Social Sharing plugin for Android and iOS

## 1. Description
This plugin allows you to use the native sharing window of your mobile device.

* Works on Unity3d 5.0+
* Works on Android, version 2.3.3 and higher (probably 2.2 as well).
* Works on iOS6 and up.

## 2. Screenshots

iOS 7 (iPhone)

![ScreenShot](https://raw.githubusercontent.com/shlapkoff/unity3d-social-sharing/master/screenshots/screenshot-ios7-share.png)

Sharing options are based on what has been setup in the device settings

![ScreenShot](https://raw.githubusercontent.com/shlapkoff/unity3d-social-sharing/master/screenshots/screenshots-ios7-shareconfig.png)

iOS 6 (iPhone)

![ScreenShot](https://raw.githubusercontent.com/shlapkoff/unity3d-social-sharing/master/screenshots/screenshot-ios6-share.png)

Android

![ScreenShot](https://raw.githubusercontent.com/shlapkoff/unity3d-social-sharing/master/screenshots/screenshot-android-share.png)

## 3. Installation
Unitypackage: [Download](https://github.com/shlapkoff/unity3d-social-sharing/releases/download/v.1.0.2/social-sharing.unitypackage)

## 4. Usage on iOS and Android
```
public class Example : MonoBehaviour
{
    [SerializeField]
    private SocialSharing socialSharing;
    [SerializeField]
    private string text;
    [SerializeField]
    private Texture2D texture2D;

    public void Click()
    {
        //Share text+screenshot
        socialSharing.ShareScreenshot(text);

        //Share text+texture2D
        socialSharing.ShareTexture2D(text, texture2D);
    }
}
```


