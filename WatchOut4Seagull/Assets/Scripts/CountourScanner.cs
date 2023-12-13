using System;
using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Demo;
using UnityEngine;
using Rect = OpenCvSharp.Rect;

public class CountourScanner : WebCamera
{
    private Mat image;
    //import the player gameobject and control it using any yellow blob that appears on the output, if there is no blobs, the player will not move
    public GameObject player;
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);

        Cv2.CvtColor(image, image, ColorConversionCodes.BGR2GRAY);
        Cv2.Threshold(image, image, 127, 255, ThresholdTypes.Binary);
        Cv2.FindContours(image, out var contours, out var hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
        CascadeClassifier handCascade = new CascadeClassifier("hand.xml");
        Mat frame = OpenCvSharp.Unity.TextureToMat(input);
        Mat gray = new Mat();
        Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
        Rect[] hands = handCascade.DetectMultiScale(gray, 1.1, 2, HaarDetectionType.ScaleImage, new Size(30, 30));

        float handX = hands[0].X + hands[0].Width / 2;
        float handY = hands[0].Y + hands[0].Height / 2;

        float playerX = handX / frame.Width * Screen.width;
        float playerY = handY / frame.Height * Screen.height;

        player.transform.position = new Vector2(playerX, playerY);
        Cv2.DrawContours(image, contours, -1, Scalar.Red, 2);
        Cv2.ImShow("output", image);


        if (output == null)
        {
            output = OpenCvSharp.Unity.MatToTexture(image);
        }
        else
        {
            OpenCvSharp.Unity.MatToTexture(image, output);
        }

        return true;
    }
}
