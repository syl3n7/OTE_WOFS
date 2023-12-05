class Bullets {
  //propriedades
  PImage bullet;
  float posX, posY, tam, damage, speed;
  boolean stop;
  //construtor
  Bullets(String n, float x, float y, float t, float s) {
    bullet = requestImage("assets/images/bullet.png");
    posX = x;
    posY = y;
    tam = t;
    speed = s;
    damage = 10;
  }

  void shoot() //call all the methods
  {
    calcPosition();
    moveme();
    drawme();
  }

  //desenhar as balas no ecra
  void drawme() {
    image(bullet, posX, posY, tam, tam);
  }
  //mover a bullet a partir da posicao do player
  void moveme() {
    //Para a bala precorrer o Y desde o ponto de spawn ate ao final do Y do canvas
    if (posY < width-tam) 
    {
      posX += speed;
    }
  }

  void calcPosition() 
  {
    if (stop == false) 
    {
        posX = p1.posX + 90;
        posY = p1.posY + 30;
        stop = true;
    }
  }

  // void deleteBullet() 
  // {
  //   for (Bullet bullet : bullets) 
  //   {
  //       if (enemy.isDead()) bullets.remove(bullet); 
  //   }
  // }
}
