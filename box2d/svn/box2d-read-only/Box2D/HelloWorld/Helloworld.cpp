/*
* Copyright (c) 2006-2007 Erin Catto http://www.box2d.org
*
* This software is provided 'as-is', without any express or implied
* warranty.  In no event will the authors be held liable for any damages
* arising from the use of this software.
* Permission is granted to anyone to use this software for any purpose,
* including commercial applications, and to alter it and redistribute it
* freely, subject to the following restrictions:
* 1. The origin of this software must not be misrepresented; you must not
* claim that you wrote the original software. If you use this software
* in a product, an acknowledgment in the product documentation would be
* appreciated but is not required.
* 2. Altered source versions must be plainly marked as such, and must not be
* misrepresented as being the original software.
* 3. This notice may not be removed or altered from any source distribution.
*/



#include "glui/glui.h"
#include <Box2D/Box2D.h>

#include <cstdio>
using namespace std;

//gw
int32 mainWindow;
int32 framePeriod = 16;
//b2Body* body;
//b2Body* body2;
//b2Body* body3;
b2Body* bodycatarm;
b2World *world;
b2Body* groundBody;
b2RevoluteJoint *armJoint;
int32 width = 640;
int32 height = 480;
b2MouseJoint *mouseJoint = NULL;
b2Body *bullet = NULL;

//	Mouse stuff...
int32 mousex;
int32 mousey;
float mjx;
float mjy;

//	Castle parts...
#define BOXES 5
float box_parms[BOXES][5] = { { 1.0, 5.0, 21.0, 5.0, 0.0},
							  { 1.0, 5.0, 29.0, 5.0, 0.0}, 
							  { 1.0, 5.0, 37.0, 5.0, 0.0},
							  { 5.0, 1.0, 25.0, 10.0, 0.0},
							  { 5.0, 1.0, 33.0, 10.0, 0.0},
							};

b2Body *box[BOXES] = { NULL, NULL, NULL, NULL, NULL};		


//gw

BOOL LoadBitmapFromBMPFile( LPTSTR szFileName, HBITMAP *phBitmap,
   HPALETTE *phPalette )
   {

   BITMAP  bm;

   *phBitmap = NULL;
   *phPalette = NULL;

   // Use LoadImage() to get the image loaded into a DIBSection
   *phBitmap = (HBITMAP)LoadImage( NULL, szFileName, IMAGE_BITMAP, 0, 0,
               LR_CREATEDIBSECTION | LR_DEFAULTSIZE | LR_LOADFROMFILE );
   if( *phBitmap == NULL )
     return FALSE;

   // Get the color depth of the DIBSection
   GetObject(*phBitmap, sizeof(BITMAP), &bm );
   // If the DIBSection is 256 color or less, it has a color table
   if( ( bm.bmBitsPixel * bm.bmPlanes ) <= 8 )
   {
   HDC           hMemDC;
   HBITMAP       hOldBitmap;
   RGBQUAD       rgb[256];
   LPLOGPALETTE  pLogPal;
   WORD          i;

   // Create a memory DC and select the DIBSection into it
   hMemDC = CreateCompatibleDC( NULL );
   hOldBitmap = (HBITMAP)SelectObject( hMemDC, *phBitmap );
   // Get the DIBSection's color table
   GetDIBColorTable( hMemDC, 0, 256, rgb );
   // Create a palette from the color tabl
   pLogPal = (LOGPALETTE *)malloc( sizeof(LOGPALETTE) + (256*sizeof(PALETTEENTRY)) );
   pLogPal->palVersion = 0x300;
   pLogPal->palNumEntries = 256;
   for(i=0;i<256;i++)
   {
     pLogPal->palPalEntry[i].peRed = rgb[i].rgbRed;
     pLogPal->palPalEntry[i].peGreen = rgb[i].rgbGreen;
     pLogPal->palPalEntry[i].peBlue = rgb[i].rgbBlue;
     pLogPal->palPalEntry[i].peFlags = 0;
   }
   *phPalette = CreatePalette( pLogPal );
   // Clean up
   free( pLogPal );
   SelectObject( hMemDC, hOldBitmap );
   DeleteDC( hMemDC );
   }
   else   // It has no color table, so use a halftone palette
   {
   HDC    hRefDC;

   hRefDC = GetDC( NULL );
   *phPalette = CreateHalftonePalette( hRefDC );
   ReleaseDC( NULL, hRefDC );
   }
   return TRUE;

}


int Filter;
const int TF_NONE = 0;
const int TF_BILINEAR = 1;

//affects all textures in memory.
void SetTextureFilter(int newfilter) 
{
        if (newfilter >= 0 && newfilter <= 1) 
        {
                Filter = newfilter;     
        }
}

void LoadBitmapTexture(int id) 
{       
        HBITMAP hBmp = NULL;
		HPALETTE phPalette = NULL;

        //hBmp = (HBITMAP) ::LoadImage(hMainInstance, 
        //        MAKEINTRESOURCE(id), IMAGE_BITMAP, 0, 0, LR_CREATEDIBSECTION);
		BOOL success = LoadBitmapFromBMPFile( L"back-muscles-square.bmp", &hBmp, &phPalette );

        //get info about the bitmap
        BITMAP BM;
        ::GetObject(hBmp, sizeof(BM), &BM);

        //tell OpenGL to ignore padding at ends of rows
        glPixelStorei(GL_UNPACK_ALIGNMENT, 4);

        glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
        glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

        if (0) //(Filter == TF_NONE) 
        {
                glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
                glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
        }
        else
        {
                glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
                glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
        }

        glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);

        glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, BM.bmWidth, BM.bmHeight,
                0, GL_BGR_EXT, GL_UNSIGNED_BYTE, BM.bmBits);

        DeleteObject((HGDIOBJ) hBmp);  //avoid memory leak (Windows)
}

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

static void DrawTexture()
{
	
        glEnable(GL_TEXTURE_2D);
        SetTextureFilter(TF_NONE);
        LoadBitmapTexture(0);

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


//gw
void SimulationLoop()
{
	glClearColor(0.0f,0.0f,0.0f,1.0f);

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();


	//glOrtho( -width/2.0, width/2.0, -height/2.0, height/2.0, -100, 100 );

	glOrtho( -100/2.0, 100/2.0, (-100/2.0)*(480.0f/640.0), (100/2.0)*(480.0f/640.0), -100, 100 );

	//DrawTexture();

	glColor3ub(255,255,255);
	DrawBody( groundBody );

	// Prepare for simulation. Typically we use a time step of 1/60 of a
	// second (60Hz) and 10 iterations. This provides a high quality simulation
	// in most game scenarios.
	float32 timeStep = 1.0f / 60.0f;
	int32 velocityIterations = 6;
	int32 positionIterations = 2;

	// This is our little game loop.
	//for (int32 i = 0; i < 60; ++i)
	{
		// Instruct the world to perform a single step of simulation.
		// It is generally best to keep the time step and iterations fixed.
		//gw world.Step(timeStep, velocityIterations, positionIterations);
		world->Step(timeStep, velocityIterations, positionIterations);

		// Now print the position and angle of the body.
		///b2Vec2 position = body->GetPosition();
		//float32 angle = body->GetAngle();

		//printf("%4.2f %4.2f %4.2f\n", position.x, position.y, angle);

		for (int i=0;i<BOXES;i++)
		{
			b2Body *body = box[i];
			DrawBody( body );
		}

		DrawBody( bodycatarm );
		if (bullet!=NULL) DrawCircle( bullet );
		//DrawAxis( position.x, position.y, angle );
	}

	DrawMouse();

	glutSwapBuffers();
}


// This is used to control the frame rate (60Hz).
static void Timer(int)
{
	glutSetWindow(mainWindow);
	glutPostRedisplay();
	glutTimerFunc(framePeriod, Timer, 0);
}
//gw



static void MouseMotion(int32 x, int32 y)
{
	mousex = x;
	mousey = y;

	mjx = ((mousex*1.0f)/width)*100.0f - 100.0f/2.0f;
	mjy = ((mousey*1.0f)/height)*(100.0f)*(480.0f/640.0f) - (100.0f/2.0f)*(480.0f/640.0f);
	mjy = -mjy;

	if (mouseJoint!=NULL)
	{
		b2Vec2 locationWorld = b2Vec2(mjx,mjy);
		mouseJoint->SetTarget( locationWorld);
	}
}

static void Mouse(int32 button, int32 state, int32 x, int32 y)
{
	if (button==2)
	{
		if (bullet!=NULL)
		{
			world->DestroyBody(bullet);
			bullet = NULL;
		}
		return;
	}


	mousex = x;
	mousey = y;

	mjx = ((mousex*1.0f)/width)*100.0f - 100.0f/2.0f;
	mjy = ((mousey*1.0f)/height)*(100.0f)*(480.0f/640.0f) - (100.0f/2.0f)*(480.0f/640.0f);
	mjy = -mjy;

	// Use the mouse to move things around.
	if (mouseJoint == NULL )
	{
		b2MouseJointDef md;
		md.bodyA = groundBody;
		md.bodyB = bodycatarm;
		b2Vec2 locationWorld = b2Vec2(mjx,mjy);
		md.target = locationWorld;
		md.maxForce = 100000;

		mouseJoint = (b2MouseJoint *)world->CreateJoint(&md);
	}
	else
	{
		if ( bullet == NULL )
		{
			b2BodyDef bulletBodyDef;
			bulletBodyDef.type = b2_dynamicBody;
			bulletBodyDef.bullet = true;
			bulletBodyDef.position.Set( mjx, mjy + 4 );
		
			bullet = world->CreateBody( &bulletBodyDef );
			bullet->SetActive(false);

			b2CircleShape circle;
			circle.m_radius = 1.0f;

			b2FixtureDef ballShapeDef;
			ballShapeDef.shape = &circle;
			ballShapeDef.density = 0.8f;
			ballShapeDef.restitution = 0.2f;
			ballShapeDef.friction = 0.99f;
			bullet->CreateFixture( &ballShapeDef );


			bullet->SetActive(true);
		}

		world->DestroyJoint(mouseJoint);
		mouseJoint = NULL;
	}
}

b2Body* CreateBody( int which )
{
	float scalex = box_parms[which][0];
	float scaley = box_parms[which][1];
	float posx = box_parms[which][2];
	float posy = box_parms[which][3];
	float angle = box_parms[which][4];

	// Define the dynamic body. We set its position and call the body factory.
	b2BodyDef bodyDef;
	bodyDef.type = b2_dynamicBody;
	bodyDef.position.Set(  posx, posy ); //20.0f, 1.0f); //4
	//gw b2Body* body = world.CreateBody(&bodyDef);
	//body = world.CreateBody(&bodyDef);
	b2Body *body = world->CreateBody(&bodyDef);
	// Define another box shape for our dynamic body.
	b2PolygonShape dynamicBox;
	dynamicBox.SetAsBox(  scalex, scaley ); //1.0f, 1.0f);
	// Define the dynamic body fixture.
	b2FixtureDef fixtureDef;
	fixtureDef.shape = &dynamicBox;
	// Set the box density to be non-zero, so it will be dynamic.
	fixtureDef.density = 1.0f;
	// Override the default friction.
	fixtureDef.friction = 0.3f;
	// Add the shape to the body.
	body->CreateFixture(&fixtureDef);
	return body;
}


// This is a simple example of building and running a simulation
// using Box2D. Here we create a large ground box and a small dynamic
// box.
// There are no graphics for this example. Box2D is meant to be used
// with your rendering engine in your game engine.
int main( int argc, char** argv)
{
	B2_NOT_USED(argc);   
	B2_NOT_USED(argv);

	//gw
	
#if 1
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE);
	glutInitWindowSize(width, height);
	char title[32];
	sprintf(title, "Box2D Version %d.%d.%d", b2_version.major, b2_version.minor, b2_version.revision);
	mainWindow = glutCreateWindow(title);
	glutDisplayFunc(SimulationLoop);
	GLUI_Master.set_glutMouseFunc(Mouse);
	glutMotionFunc(MouseMotion);
#endif

	//gw

	// Define the gravity vector.
	b2Vec2 gravity(0.0f, -10.0f);

	// Construct a world object, which will hold and simulate the rigid bodies.
	//gw b2World world(gravity);
	//gw b2World *world = new b2World(gravity);
	world = new b2World(gravity);


	// Define the ground body.
	b2BodyDef groundBodyDef;
	groundBodyDef.position.Set(0.0f, -10.0f);
	// Call the body factory which allocates memory for the ground body
	// from a pool and creates the ground box shape (also from a pool).
	// The body is also added to the world.
	//gw b2Body* groundBody = world.CreateBody(&groundBodyDef);
	//b2Body* groundBody = world->CreateBody(&groundBodyDef);
	groundBody = world->CreateBody(&groundBodyDef);
	// Define the ground box shape.
	b2PolygonShape groundBox;
	// The extents are the half-widths of the box.
	groundBox.SetAsBox(50.0f, 10.0f);
	// Add the ground fixture to the ground body.
	groundBody->CreateFixture(&groundBox, 0.0f);

	/*
	float scalex = box_parms[0][0];
	float scaley = box_parms[0][1];
	float posx = box_parms[0][2];
	float posy = box_parms[0][3];
	float angle = box_parms[0][4];

	// Define the dynamic body. We set its position and call the body factory.
	b2BodyDef bodyDef;
	bodyDef.type = b2_dynamicBody;
	bodyDef.position.Set(  posx, posy ); //20.0f, 1.0f); //4
	//gw b2Body* body = world.CreateBody(&bodyDef);
	//body = world.CreateBody(&bodyDef);
	body = world->CreateBody(&bodyDef);
	// Define another box shape for our dynamic body.
	b2PolygonShape dynamicBox;
	dynamicBox.SetAsBox(  scalex, scaley ); //1.0f, 1.0f);
	// Define the dynamic body fixture.
	b2FixtureDef fixtureDef;
	fixtureDef.shape = &dynamicBox;
	// Set the box density to be non-zero, so it will be dynamic.
	fixtureDef.density = 1.0f;
	// Override the default friction.
	fixtureDef.friction = 0.3f;
	// Add the shape to the body.
	body->CreateFixture(&fixtureDef);
	*/
	//box[0] = CreateBody(0);

	/*
	// Define the dynamic body. We set its position and call the body factory.
	b2BodyDef bodyDef2;
	bodyDef2.type = b2_dynamicBody;
	bodyDef2.position.Set(20.0f, 2.0f); //1,8
	//gw b2Body* body = world.CreateBody(&bodyDef);
	//body = world.CreateBody(&bodyDef);
	body2 = world->CreateBody(&bodyDef2);
	// Define another box shape for our dynamic body.
	b2PolygonShape dynamicBox2;
	dynamicBox2.SetAsBox(1.0f, 1.0f);
	// Define the dynamic body fixture.
	b2FixtureDef fixtureDef2;
	fixtureDef2.shape = &dynamicBox2;
	// Set the box density to be non-zero, so it will be dynamic.
	fixtureDef2.density = 1.0f;
	// Override the default friction.
	fixtureDef2.friction = 0.3f;
	// Add the shape to the body.
	body2->CreateFixture(&fixtureDef2);
	*/
	//box[1] = CreateBody(1);

	/*
	// Define the dynamic body. We set its position and call the body factory.
	b2BodyDef bodyDef3;
	bodyDef3.type = b2_dynamicBody;
	bodyDef3.position.Set(20.0f, 3.0f); //2,10
	//gw b2Body* body = world.CreateBody(&bodyDef);
	//body = world.CreateBody(&bodyDef);
	body3 = world->CreateBody(&bodyDef3);
	// Define another box shape for our dynamic body.
	b2PolygonShape dynamicBox3;
	dynamicBox3.SetAsBox(1.0f, 1.0f);
	// Define the dynamic body fixture.
	b2FixtureDef fixtureDef3;
	fixtureDef3.shape = &dynamicBox3;
	// Set the box density to be non-zero, so it will be dynamic.
	fixtureDef3.density = 1.0f;
	// Override the default friction.
	fixtureDef3.friction = 0.3f;
	// Add the shape to the body.
	body3->CreateFixture(&fixtureDef3);
	*/
	//box[2] = CreateBody( 2 );

	for ( int i=0; i<BOXES; i++)
	{
		box[i] = CreateBody( i );
	}


	/* catapult arm */
	b2BodyDef catarmdef;
	catarmdef.type = b2_dynamicBody;
	catarmdef.linearDamping = 1;
	catarmdef.angularDamping = 1;
	catarmdef.position.Set( -20, 10 );
	bodycatarm = world->CreateBody( &catarmdef );

	b2PolygonShape armBox;
	b2FixtureDef armBoxDef;
	armBoxDef.shape = &armBox;
	armBoxDef.density = 0.3f;
	armBox.SetAsBox( 1, 10 );
	b2Fixture *armFixture = bodycatarm->CreateFixture( &armBoxDef );

	/*catapult joint*/
	b2RevoluteJointDef armJointDef;
	armJointDef.Initialize( groundBody, bodycatarm, b2Vec2(-20,1) ); // -10,1
	armJointDef.enableMotor = true;
	armJointDef.enableLimit = true;
	armJointDef.motorSpeed = -1000;
	armJointDef.lowerAngle = 9.0 * (3.14/180.0);
	armJointDef.upperAngle = 75.0 * (3.14/180.0);
	armJointDef.maxMotorTorque = 10000;

	armJoint = (b2RevoluteJoint*)world->CreateJoint( &armJointDef );
	
	/*GW
	// Prepare for simulation. Typically we use a time step of 1/60 of a
	// second (60Hz) and 10 iterations. This provides a high quality simulation
	// in most game scenarios.
	float32 timeStep = 1.0f / 60.0f;
	int32 velocityIterations = 6;
	int32 positionIterations = 2;

	// This is our little game loop.
	for (int32 i = 0; i < 60; ++i)
	{
		// Instruct the world to perform a single step of simulation.
		// It is generally best to keep the time step and iterations fixed.
		//gw world.Step(timeStep, velocityIterations, positionIterations);
		world->Step(timeStep, velocityIterations, positionIterations);

		// Now print the position and angle of the body.
		b2Vec2 position = body->GetPosition();
		float32 angle = body->GetAngle();

		printf("%4.2f %4.2f %4.2f\n", position.x, position.y, angle);
	}
	GW*/
	

	// When the world destructor is called, all bodies and joints are freed. This can
	// create orphaned pointers, so be careful about your world management.

	//gw
	
#if 1
	glutTimerFunc(framePeriod, Timer, 0);
	
	glutMainLoop();
#endif

	//gw

	return 0;
}
