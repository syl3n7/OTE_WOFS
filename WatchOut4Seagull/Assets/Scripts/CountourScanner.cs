using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Demo;
using UnityEngine;

public class CountourScanner : WebCamera
{
    private Mat image;
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);

        //Cv2.CvtColor(img, img, ColorConversionCodes.BGR2GRAY);
        //Cv2.Threshold(img, img, 127, 255, ThresholdTypes.Binary);
        //Cv2.FindContours(img, out var contours, out var hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
        //Cv2.DrawContours(img, contours, -1, Scalar.Red, 2);
        //

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
