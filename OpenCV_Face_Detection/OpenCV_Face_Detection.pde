//importar bibliotecas
import processing.video.*;
import gab.opencv.*;
import java.awt.Rectangle;
//declarar objetos
OpenCV opencv;
Rectangle[] faces;
Capture video;
//inicializar canvas e objetos
void setup() {
  size(800, 480);
  video = new Capture(this, width / 2, height / 2, 30);
  opencv = new OpenCV(this, width / 2, height / 2);
  //https://atduskgreg.github.io/opencv-processing/reference/
  opencv.loadCascade(OpenCV.CASCADE_FRONTALFACE);
  video.start();
}

void captureEvent(Capture c)
{
  c.read();
}

void draw() 
{
  scale(2);
  opencv.loadImage(video);
  image(video, 0, 0);
  noFill();
  stroke(0, 255, 0);
  strokeWeight(3);
  Rectangle[] faces = opencv.detect();
  println(faces.length);
  
  for (int i = 0; i < faces.length; i++) 
  {
    println(faces[i].x + "," + faces[i].y);
    rect(faces[i].x, faces[i].y, faces[i].width, faces[i].height);
  }
}   
