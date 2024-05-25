using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Share_Score : MonoBehaviour
{
    string appName;
    string message;

    public void ShareScore()
    {
        appName = "Street Racing 2D";
        message = "I scored a new HighScore of " + ScoreManager.Ins.bestScore.ToString() + " in " + appName;

        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");

        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject(appName).SetText(message).Share();
    }
}