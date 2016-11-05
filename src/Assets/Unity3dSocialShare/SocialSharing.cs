using UnityEngine;
using System.Collections;
using System.IO;

namespace net.indigobunting.SocialSharing
{
    public class SocialSharing : MonoBehaviour
    {
        #pragma warning disable 0414

        //NSPhotoLibraryUsageDescription

        [SerializeField]
        private string appStoreUrl = "";
        [SerializeField]
        private string googlePlayUrl = "";
        [SerializeField]
        private string text = "";

        void OnEnable()
        {
#if UNITY_IOS
            ScreenshotHandler.ImageFinishedSaving += OnScreenshotSaved;
#endif
        }

        void OnDisable()
        {
#if UNITY_IOS
            ScreenshotHandler.ImageFinishedSaving -= OnScreenshotSaved;
#endif
        }

        public void Share()
        {
            StartCoroutine(ShareScreenshot());
        }

        private IEnumerator ShareScreenshot()
        {
            yield return new WaitForEndOfFrame();
            Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
            screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
            screenTexture.Apply();

            string filename = System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            string pathToImage = Path.Combine(Application.persistentDataPath, filename + ".png");
            byte[] dataToSave = screenTexture.EncodeToPNG();
            File.WriteAllBytes(pathToImage, dataToSave);

#if UNITY_ANDROID
            string shareText = text;
            shareText += googlePlayUrl;

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + pathToImage);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);
#endif

#if UNITY_IOS
            StartCoroutine(ScreenshotHandler.SaveExisting(pathToImage, true));
#endif
        }

        private void OnScreenshotSaved(string path)
        {
#if UNITY_IOS
            string shareText = text;
            shareText += appStoreUrl;

            GeneralSharingiOSBridge.ShareTextWithImage(path, shareText);
#endif
        }
    }
}
