using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace net.indigobunting.SocialSharing
{
    public class ScreenshotHandler : MonoBehaviour
    {
#if UNITY_IOS
        public static event Action<string> ImageFinishedSaving;

        [DllImport("__Internal")]
        private static extern bool saveToGallery (string path);

        public static IEnumerator SaveExisting(string filePath, bool callback = false)
        {
            yield return 0;
            bool photoSaved = false;
        
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                while (!photoSaved) 
                {
                    photoSaved = saveToGallery(filePath);
                    yield return new WaitForSeconds (.5f);
                }
                
                UnityEngine.iOS.Device.SetNoBackupFlag(filePath);
            }

            if (callback) ImageFinishedSaving(filePath);
        }
#endif
    }
}

