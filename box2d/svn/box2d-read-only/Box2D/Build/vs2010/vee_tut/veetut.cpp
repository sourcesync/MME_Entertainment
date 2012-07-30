//gw #include <SFML\Graphics.hpp>

#include "glui/glui.h"
#include <Box2D\Box2D.h>

/** We need this to easily convert between pixel and real-world coordinates*/
static const float SCALE = 30.f;
#if 0
int32 mainWindow;
int32 framePeriod = 16;


/** Create the base for the boxes to land */
void CreateGround(b2World& World, float X, float Y);

/** Create the boxes */
void CreateBox(b2World& World, int MouseX, int MouseY);


static void SimulationLoop()
{
	glClearColor(0.0f,0.0f,0.0f,1.0f);

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();

	
	glutSwapBuffers();
}


// This is used to control the frame rate (60Hz).
static void Timer(int)
{
	glutSetWindow(mainWindow);
	glutPostRedisplay();
	glutTimerFunc(framePeriod, Timer, 0);
}
#endif

#if 0
int main(int argc, char** argv)
{
    /** Prepare the window */
    //gw sf::RenderWindow Window(sf::VideoMode(800, 600, 32), "Test");
    //gw Window.SetFramerateLimit(60);

	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE);
	int32 width = 640;
	int32 height = 480;
	glutInitWindowSize(width, height);
	
	char title[32];
	sprintf(title, "Box2D Version %d.%d.%d", b2_version.major, b2_version.minor, b2_version.revision);

	mainWindow = glutCreateWindow(title);
	
	glutDisplayFunc(SimulationLoop);

    /** Prepare the world */
    b2Vec2 Gravity(0.f, 9.8f);
    b2World World(Gravity);
    CreateGround(World, 400.f, 500.f);

    /** Prepare textures */
    //gw sf::Texture GroundTexture;
    //gwsf::Texture BoxTexture;
    //gw GroundTexture.LoadFromFile("ground.png");
    //gw BoxTexture.LoadFromFile("box.png");

	/*gw
    while (Window.IsOpened())
    {
        if (sf::Mouse::IsButtonPressed(sf::Mouse::Left))
        {
            int MouseX = sf::Mouse::GetPosition(Window).x;
            int MouseY = sf::Mouse::GetPosition(Window).y;
            CreateBox(World, MouseX, MouseY);
        }
        World.Step(1/60.f, 8, 3);

        Window.Clear(sf::Color::White);
        int BodyCount = 0;
        for (b2Body* BodyIterator = World.GetBodyList(); BodyIterator != 0; BodyIterator = BodyIterator->GetNext())
        {
            if (BodyIterator->GetType() == b2_dynamicBody)
            {
                sf::Sprite Sprite;
                Sprite.SetTexture(BoxTexture);
                Sprite.SetOrigin(16.f, 16.f);
                Sprite.SetPosition(SCALE * BodyIterator->GetPosition().x, SCALE * BodyIterator->GetPosition().y);
                Sprite.SetRotation(BodyIterator->GetAngle() * 180/b2_pi);
                Window.Draw(Sprite);
                ++BodyCount;
            }
            else
            {
                sf::Sprite GroundSprite;
                GroundSprite.SetTexture(GroundTexture);
                GroundSprite.SetOrigin(400.f, 8.f);
                GroundSprite.SetPosition(BodyIterator->GetPosition().x * SCALE, BodyIterator->GetPosition().y * SCALE);
                GroundSprite.SetRotation(180/b2_pi * BodyIterator->GetAngle());
                Window.Draw(GroundSprite);
            }
        }
        Window.Display();
    }
	*/
	
	glutTimerFunc(framePeriod, Timer, 0);
	
	glutMainLoop();

	return 0;
}
#endif

void CreateBox(b2World& World, int MouseX, int MouseY)
{
    b2BodyDef BodyDef;
    BodyDef.position = b2Vec2(MouseX/SCALE, MouseY/SCALE);
    BodyDef.type = b2_dynamicBody;
    b2Body* Body = World.CreateBody(&BodyDef);

    b2PolygonShape Shape;
    Shape.SetAsBox((32.f/2)/SCALE, (32.f/2)/SCALE);
    b2FixtureDef FixtureDef;
    FixtureDef.density = 1.f;
    FixtureDef.friction = 0.7f;
    FixtureDef.shape = &Shape;
    Body->CreateFixture(&FixtureDef);
}

void CreateGround(b2World& World, float X, float Y)
{
    b2BodyDef BodyDef;
    BodyDef.position = b2Vec2(X/SCALE, Y/SCALE);
    BodyDef.type = b2_staticBody;
    b2Body* Body = World.CreateBody(&BodyDef);

    b2PolygonShape Shape;
    Shape.SetAsBox((800.f/2)/SCALE, (16.f/2)/SCALE);
    b2FixtureDef FixtureDef;
    FixtureDef.density = 0.f;
    FixtureDef.shape = &Shape;
    Body->CreateFixture(&FixtureDef);
}