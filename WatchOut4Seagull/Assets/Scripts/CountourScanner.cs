using System;
using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Demo;
using UnityEngine;

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
        Cv2.DrawContours(image, contours, -1, Scalar.Red, 2);
        //?fazer amanhã

        //recognize a hand using cascades and if its found, move the player to the left or right depending on the position of the blob
        if (contours.Length > 3)
        {
            //get the first blob
            var blob = contours[0];
            //get the center of the blob
            var center = Cv2.Moments(blob).M10 / Cv2.Moments(blob).M00;
            //get the center of the screen
            var screenCenter = Screen.width / 2;
            //get the difference between the center of the blob and the center of the screen
            var difference = center - screenCenter;

            //move the player to the left or right depending on the difference
            if (difference < 0)
            {
                player.transform.Translate(Vector2.right * (float)Math.Abs(difference) * Time.deltaTime);
            }
            else
            {
                player.transform.Translate(Vector2.left * (float)Math.Abs(difference) * Time.deltaTime);
            }
        }
        else
        {

        }

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
