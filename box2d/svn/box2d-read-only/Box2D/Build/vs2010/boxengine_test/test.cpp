

#include "glui/glui.h"
#include <Box2D/Box2D.h>
#include "boxengine.h"

#include <cstdio>
using namespace std;

#define BOXES 21

static float WorldX = 200.0f;

int32 mainWindow;
int32 framePeriod = 16;

int32 width = 1024;
int32 height = 768;


static int32 mousex;
static int32 mousey;
static float mjx;
static float mjy;


int Filter;
const int TF_NONE = 0;
const int TF_BILINEAR = 1;

static void DrawAxis(float x, float y, float angle)
{
	glBegin(GL_LINES);
	glColor3ub(255,0,0);
	glVertex2f( x-1.0f, y);
	glVertex2f( x, y );
	glColor3ub(0,255,0);
	glVertex2f( x, y + 1.0f);
	glVertex2f( x, y );
	glEnd();
}

static void DrawMouse()
{
	
	DrawAxis( mjx, mjy, 0.0f );
}

static void DrawCircle(b2Body *c)
{
	b2Vec2 position = c->GetPosition();
	
	//glPushMatrix();
	//glTranslatef( position.x, position.y,0.0f );
	DrawAxis( position.x, position.y, 0.0f);
	//glPopMatrix();
}


//affects all textures in memory.
void SetTextureFilter(int newfilter) 
{
        if (newfilter >= 0 && newfilter <= 1) 
        {
                Filter = newfilter;     
        }
}

static void DrawTexture()
{
	
        glEnable(GL_TEXTURE_2D);
        SetTextureFilter(TF_NONE);
        //LoadBitmapTexture(0);

        glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_REPLACE);
        //use GL_MODULATE instead of GL_REPLACE if lighting is being used

        //draw a square with specified texture coordinates

        float xvals[] = {0.0, 0.0, 40.0, 40.0};
        float yvals[] = {40.0, 0.0, 0.0, 40.0};

        float svals[] = {0, 0, 1, 1};
        float tvals[] = {1, 0, 0, 1};

        glPolygonMode(GL_FRONT_AND_BACK, GL_POLYGON);
        glBegin(GL_POLYGON);
                for (int i=0; i < 4; i++)
                {
                        glVertex2f(xvals[i], yvals[i]);
                        glTexCoord2f(svals[i], tvals[i]);
                }
        glEnd();
}

static void DrawBody(b2Body *g)
{


	b2Vec2 position = g->GetPosition();
	float32 angle = g->GetAngle();

	glPushMatrix();
	glTranslatef( position.x, position.y,0.0f );
	glRotatef( angle * ( 180.0f/3.141f ), 0.0f, 0.0f, 1.0f );

	b2Fixture *f = g->GetFixtureList();
	while (f!=NULL)
	{
		b2Shape *s = f->GetShape();
		b2PolygonShape *p = (b2PolygonShape *)s;

		glBegin(GL_LINE_LOOP);
		for (int32 v=0;v< p->GetVertexCount();v++)
		{
			b2Vec2 vec = p->GetVertex(v);
			glVertex2f( vec.x, vec.y );
		}
		glEnd();

		f = f->GetNext();
	}

	glPopMatrix();
}



static void DrawBox(float *data)
{

	glPushMatrix();
	glTranslatef( data[0], data[1], 0.0f );
	glRotatef( data[2] * ( 180.0f/3.141f ), 0.0f, 0.0f, 1.0f );
	
	glBegin(GL_LINE_LOOP);
	for (int i=0;i<4;i++)
	{
			glVertex2f( data[3+i*2], data[3+i*2+1] );
	}
	glEnd();

	glPopMatrix();
}


void SimulationLoop()
{
#if 1
	glClearColor(0.0f,0.0f,0.0f,1.0f);

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();


	//glOrtho( -width/2.0, width/2.0, -height/2.0, height/2.0, -100, 100 );

	glOrtho( -WorldX/2.0, WorldX/2.0, (-WorldX/2.0)*(height*1.0/width), (WorldX/2.0)*(height*1.0/width), -100, 100 );

	//DrawTexture();

	glColor3ub(255,255,255);

#endif


	BoxEngine_SimulationLoop();

	//	ground
	float data[11];
	BoxEngine_GetGroundData( data );
	DrawBox( data );

	
	//	catapult
	BoxEngine_GetCatapultData( data );
	DrawBox( data );

#if 1
	for (int i=0;i<BOXES;i++)
	{
		//b2Body *body = box[i];
		//DrawBody( body );

		BoxEngine_GetBoxData( i, data );

		DrawBox( data );
	}
#endif

	//	bullet
	int on = 0;
	BoxEngine_GetBulletData( &on, data );
	if (on)
	{
		DrawAxis( data[0], data[1], data[2] );
	}

	//	cursor...
	DrawMouse();

	glutSwapBuffers();

}


void Mouse(int32 button, int32 state, int32 x, int32 y)
{
	mousex = x;
	mousey = y;

	mjx = ((mousex*1.0f)/width)*WorldX - WorldX/2.0f;
	mjy = ((mousey*1.0f)/height)*(WorldX)*(height*1.0/width) - (WorldX/2.0f)*(height*1.0/width);
	mjy = -mjy;

	//return BoxEngine_Mouse(button,state,x,y);
	int ret = BoxEngine_Mouse(button,state,mjx,mjy);
	return;
}


void MouseMotion(int32 x, int32 y)
{
	mousex = x;
	mousey = y;

	
	mjx = ((mousex*1.0f)/width)*WorldX - WorldX/2.0f;
	mjy = ((mousey*1.0f)/height)*(WorldX)*(height*1.0/width) - (WorldX/2.0f)*(height*1.0/width);
	mjy = -mjy;

	//return BoxEngine_MouseMotion(x,y);
	return BoxEngine_MouseMotion(mjx,mjy);
}


void Timer(int w)
{
	glutSetWindow(mainWindow);
	glutPostRedisplay();
	glutTimerFunc(framePeriod, Timer, 0);
	//return BoxEngine_Timer(w);
}

// This is a simple example of building and running a simulation
// using Box2D. Here we create a large ground box and a small dynamic
// box.
// There are no graphics for this example. Box2D is meant to be used
// with your rendering engine in your game engine.
int main( int argc, char** argv)
{
	//B2_NOT_USED(argc);   
	//B2_NOT_USED(argv);

	//gw
	

	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE);
	glutInitWindowSize(width, height);
	char title[32];
	sprintf(title, "Box2D Version %d.%d.%d", b2_version.major, b2_version.minor, b2_version.revision);
	mainWindow = glutCreateWindow(title);
	glutDisplayFunc(SimulationLoop);
	GLUI_Master.set_glutMouseFunc(Mouse);
	glutMotionFunc(MouseMotion);



	glutTimerFunc(framePeriod, Timer, 0);
	
	BoxEngine_Init(mainWindow);

	glutMainLoop();

}