import java.awt.*; //importar libraria grafica
import processing.video.*;

//declarar objetos
Capture video;
public Menu m;
boolean displayGame = false;
CloudsGen c1, c2, c3, c4, c5;
public Player p1;
public Enemy[] enemies;
public ArrayList<Bullets> bullets;
public int score, lives, center_x, center_y;
public float hits;

int blobCounter = 0;
int maxLife = 50;
color trackColor; 
float threshold = 40;
float distThreshold = 50;
ArrayList<Blob> blobs = new ArrayList<Blob>();

void setup() { //codigo apenas executado no inicio do programa
    video = new Capture(this, 1281, 961);
    video.start();
    surface.setTitle("Watch out for the seagull!"); //titulo da janela
    size(1281, 961); //fullscreen
    frameRate(60); //especificar framerate a usar
    Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize(); //https://forum.processing.org/one/topic/dynamic-screen-background-resize-need-guidance.html //dinamic window size begin (without borders) //ir buscar a dimensao da tela
    int screenWidth = screenSize.width; //ir buscar a largura da tela
    int screenHeight = screenSize.height; //ir buscar a largura da tela
    surface.setSize(width, height /*screenWidth, screenHfeight*/);
    smooth(4); //funcao de antialiasing
    center_x = screenWidth / 2 - width / 2; //centrar a janela no eixo X
    center_y = screenHeight / 2 - height / 2; //centrar a janela no eixo Y
    surface.setLocation(center_x, center_y); //set location of canvas to center of screen resolution
    imageMode(CENTER); //funcao para centrar o spawn de imagens
    rectMode(CENTER); //função para centrar o spawn de rectângulos
    textAlign(CENTER); //funcao para alinhar o texto ao centro
    noStroke(); //funcao para retirar o Stroke das figuras geometricas
    //Inicializar Objetos
    score = 0;
    lives = 3;
    m = new Menu(width / 2, height / 2); //menu
    c1 = new CloudsGen("assets/images/cloud1.png", 100, random(height)); //nuvem 1
    c2 = new CloudsGen("assets/images/cloud2.png", 200, random(height)); //nuvem 2
    c3 = new CloudsGen("assets/images/cloud3.png", 300, random(height)); //nuvem 3
    c4 = new CloudsGen("assets/images/cloud4.png", 400, random(height)); //nuvem 4
    c5 = new CloudsGen("assets/images/cloud5.png", 500, random(height)); //nuvem 5
    p1 = new Player("assets/images/Player.png", width / 2, -200); //player 1 //spawn fora do canvas para animar a entrada do player no jogo

    blobs = new ArrayList<Blob>();

    trackColor = color(183, 12, 83);

    enemies = new Enemy[10];
    for (int i = 0; i < enemies.length; i++) 
    {
        if(i%2 == 0) enemies[i] = new Enemy("assets/images/Boat_1.png"); //enemy class with img1
        else enemies[i] = new Enemy("assets/images/Boat_2.png"); //enemy class with img2   
    }

    bullets = new ArrayList<Bullets>();
    //TODO hits = e1.get(p1.level).health / p1.dmg;
}

void draw() {
    // desenhar os elementos do programa no ecra mediante condicoes especificadas
    m.start(); // verifica presses

    if (m.i.active) 
    {
        // instrucoes ativos
        m.i.drawme();
        m.i.back.drawme();
    }

    if (m.highscore.active) 
    {
        // highscore ativos
        m.highscore.drawme();
        m.highscore.back.drawme();
    }

    if (m.state) 
    {
        // menu ativo
        m.start.drawme();
        m.exit.drawme();
        m.highscorebttn.drawme();
        m.instructionsbttn.drawme();
    }
    //println(displayGame);
    if (displayGame) 
    {
        // jogo ativo
        checkBlobs();
        if (p1.moveUnLock) 
        {
            //! check if this is working
            for (Blob b : blobs) 
            {
                //b.show();
                float bx = b.minx + b.maxx / 2;
                float by = b.miny + b.maxy / 2;
                //* 1281 x 961 webcam resolution
                bx = map(bx, 0, 1281, 0, width);
                by = map(by, 0, 961, 0, height);
            }
            //! mudar isto para a posX e posY dos blobs
            //? p1.posX = map(faces[0].x, 0, 160, 0, height);
            //? p1.posY = map(faces[0].y, 0, 90, 0, height);
        }
            m.background.drawme();
            m.back.drawme(); // desenhar o botão de pausa
            c1.drawme(); // desenhar e mover nuvem1
            c2.drawme(); // desenhar e mover nuvem2
            c3.drawme(); // desenhar e mover nuvem3
            c4.drawme(); // desenhar e mover nuvem4
            c5.drawme(); // desenhar e mover nuvem5
            p1.drawme(); // desenhar e mover o player1
    
        for (Bullets bullets : bullets) 
        {
            bullets.shoot();
        }
        for (Enemy enemy : enemies) 
        {
            enemy.spawnEnemy();
        }

    //TODO score(); // incrementar score
    }
}

void keyPressed() {
    if (key == ' ') p1.shoot(); // disparar a bala ate width, se pressionado novamente, da reset a posicao da bala e volta a desenhar ate width
    //codigopara movimento importado do exemplo fornecido pelo professor
    if (key == 's' || key == 'S') p1.moveDown = true;
    if (key == 'w' || key == 'W') p1.moveUp = true;
    if (key == 'a' || key == 'A') p1.moveLeft = true;
    if (key == 'd' || key == 'D') p1.moveRight = true;
}

void keyReleased() {
    if (key == 's' || key == 'S') p1.moveDown = false;
    if (key == 'w' || key == 'W') p1.moveUp = false;
    if (key == 'a' || key == 'A') p1.moveLeft = false;
    if (key == 'd' || key == 'D') p1.moveRight = false;
}

//acrescentar pontuacao na tabela
//TODO void score() {
//     textSize(32); // era fixe colocar isto numa funcao propria para mostrar no ecra, em vez de estar aqui perdido
//     text("Score: " + score, m.i.posX, height / 8);
//     //if (p1.b1.get(p1.level).enemycheck()) score++;
//     if (score == 10 && e1.get(p1.level).health < 10) {
//         p1.level = 1;
//         hits = e1.get(p1.level).health / p1.dmg;
//     }
//     if (score == 20 && e1.get(p1.level).health < 10) {
//         p1.level = 2;
//         hits = e1.get(p1.level).health / p1.dmg;
//     }
// }

void mousePressed() 
{ //quando clicar no botao do rato dentro das condicoes especificadas(dentro dos limites da imagem do botao)
    if (m.start.press()) m.start.pressed = true;
    if (m.exit.press()) m.exit.pressed = true;
    if (m.back.press()) m.back.pressed = true;
    if (m.instructionsbttn.press()) m.instructionsbttn.pressed = true;
    if (m.i.back.press()) m.i.back.pressed = true;
    if (m.highscorebttn.press()) m.highscorebttn.pressed = true;
    if (m.highscore.back.press()) m.highscore.back.pressed = true;
    // Save color where the mouse is clicked in trackColor variable
    int loc = mouseX + mouseY*video.width;
    trackColor = video.pixels[loc];
    println(red(trackColor), green(trackColor), blue(trackColor));
}

//?distance squared function 2d space
float distSq(float x1, float y1, float x2, float y2) {
  float d = (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1);
  return d;
}

//?distance squared function 3d space
float distSq(float x1, float y1, float z1, float x2, float y2, float z2) {
  float d = (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1) +(z2-z1)*(z2-z1);
  return d;
}

void captureEvent(Capture c) {
    c.read();
}