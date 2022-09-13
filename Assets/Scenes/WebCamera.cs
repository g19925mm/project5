using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Webカメラ
public class WebCamera : MonoBehaviour
{
    private static int INPUT_SIZE = 256;
    private static int FPS = 30;

    // UI
    RawImage rawImage;
    WebCamTexture webCamTexture;

    // スタート時に呼ばれる
    void Start ()
    {
        // Webカメラの開始
        this.rawImage = GetComponent<RawImage>();
        this.webCamTexture = new WebCamTexture(INPUT_SIZE, INPUT_SIZE, FPS);
        this.rawImage.texture = this.webCamTexture;
        this.webCamTexture.Play();
        Invoke(nameof(TakeShot), 10f);
    }

    void TakeShot()
    {
        Texture tex = this.rawImage.texture;
        int w = tex.width;
        int h = tex.height;

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture rt = new RenderTexture(w, h, 32);

        Graphics.Blit(tex, rt);
        RenderTexture.active = rt;

        Texture2D result = new Texture2D(w, h, TextureFormat.RGBA32, false);
        result.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        result.Apply();
        RenderTexture.active = currentRT;

        GetComponent<MeshRenderer>().material.mainTexture = result;
    }

}
