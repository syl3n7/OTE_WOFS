class Enemy {
  //propriedades
  float trand = 5;
  float tsmoothed;
  PImage img;
  float posX, posY, speed, damage, tam;
  int health;
  //constructor
  Enemy(String nome) {
    img = requestImage(nome);
    posX = width-tam;
    posY = height/2;
    speed = 5;
    damage = 5;
    health = 100;
  }

  void spawnEnemy() {
      spawn();
      //difficulty();
      move();
  }

  void spawn() 
  {
    //img.resize(int(tam), int(tam)); //redimensiona a imagem
    if(health > 0) image(img, posX, posY);
    //fill(255, 0, 0, 200); //hitbox debug only
    //rect(posX, posY+10, 150, 70); //hitbox debug only
    //textSize(24);
    //text("Health: " + health, posX, posY-40);
  }

  void move() 
  { 
    //this validates the enemy's health and its X position 
    if (posY <= (width) && health > 0) posY += speed;
    else {
    //this code block is responsible for the repetition of the object
      posY = random(1700, 2300);
      posX = random(0, height - height/14 - 4);
      health = 100; 
    }
  }
    //checks if enemy is alive or dead
  boolean isDead() {
      if (health <= 0) return true;
      else return false;
  }

  //Todo Add enemies in stages
  /*void stageOne(){
    img = requestImage(assets/images/Boat_1.png);
  }*/ 

  /*void stageTwo(){
    img = requestImage(assets/images/Boat_2.png);
  }*/ 

  /*void stageTwo(){
    img = requestImage(assets/images/Boat_3.png);
  }*/ 

  /*void stageTwo(){
    img = requestImage(assets/images/Boat_4.png);
  }*/ 
}