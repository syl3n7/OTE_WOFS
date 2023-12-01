class Background 
{
//propriedades
    float posX, posY;
    PImage img;
//construtor
    Background(String n, float x, float y) 
    {
        img = requestImage(n);
        posX = x;
        posY = y;
    }
//metodo desenhar para o canvas
    void drawme() 
    {
        image(img, posX, posY);
        boolean bg = true;
    }
    void changeImage(String name){
        img = requestImage(name);
    }
}