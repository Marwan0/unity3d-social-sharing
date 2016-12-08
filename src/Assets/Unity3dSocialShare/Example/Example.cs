using UnityEngine;
using System.Collections;

namespace IndigoBunting.SocialSharing
{
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
}

