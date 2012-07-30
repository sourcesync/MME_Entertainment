// boxengine.cpp : Defines the exported functions for the DLL application.
//

//#include "stdafx.h"
#include "boxengine.h"


// This is an example of an exported variable
BOXENGINE_API int nboxengine=0;

// This is an example of an exported function.
BOXENGINE_API int fnboxengine(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see boxengine.h for the class definition
Cboxengine::Cboxengine()
{
	return;
}



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



//#include "glui/glui.h"
#include <Box2D/Box2D.h>

#include <cstdio>
using namespace std;

#define GROUND_OFFSET_Y 25.0f
#define BULLET_OFFSET 4.0f

//gw
static b2Body* bodycatarm = NULL;
static b2World *world = NULL;
static b2Body* groundBody = NULL;
static b2RevoluteJoint *armJoint = NULL;
static b2MouseJoint *mouseJoint = NULL;
static b2Body *bullet = NULL;

//	Mouse stuff...
//static int32 mousex;
//static int32 mousey;
static float mjx;
static float mjy;

//	Castle parts...
#define BOXES 21
static float box_parms[BOXES][5] = { 
	{ 1.0, 5.0, 21.0, 5.0, 0.0},
	{ 1.0, 5.0, 29.0, 5.0, 0.0}, 
	{ 1.0, 5.0, 37.0, 5.0, 0.0},
	{ 5.0, 1.0, 25.0, 11.0, 0.0},
	{ 5.0, 1.0, 33.0, 11.0, 0.0},
		//
		{ 1.0, 2.5, 25.0, 13.5, 0.0 },
		{ 1.0, 2.5, 33.0, 13.5, 0.0 },
		//
	//
	{1.0, 10.0, 40.0, 10.0, 0.0},
	{1.0, 10.0, 56.0, 10.0, 0.0},
	{10.0,1.0, 48.0f, 21.0f, 0.0},
	//
	{1.0, 10.0, 40.0, 32.0, 0.0},
	{1.0, 10.0, 56.0, 32.0, 0.0},
	{10.0,1.0, 48.0f, 43.0f, 0.0},
	//
		{ 3.0, 3.0, 48.0, 45.5f, 0.0f},
	//
	{ 1.0, 5.0, 59.0, 5.0, 0.0},
	{ 1.0, 5.0, 67.0, 5.0, 0.0}, 
	{ 1.0, 5.0, 75.0, 5.0, 0.0},
	{ 5.0, 1.0, 63.0, 11.0, 0.0},
	{ 5.0, 1.0, 71.0, 11.0, 0.0},
		//
		{ 1.0, 2.5, 63.0, 13.5, 0.0 },
		{ 1.0, 2.5, 71.0, 13.5, 0.0 }
		//

							};

static b2Body *box[BOXES] = { NULL, NULL, NULL, NULL, NULL,
								NULL, NULL,
								NULL, NULL, NULL,	
								NULL, NULL, NULL,	
								NULL,
								NULL, NULL, NULL, NULL, NULL,
								NULL, NULL};



BOXENGINE_API void BoxEngine_GetBoxData( int idx, float *data )
{
	int counter = 0;

	

	b2Body *g = box[idx];
	
	b2Vec2 position = g->GetPosition();
	float32 angle = g->GetAngle();

	//glPushMatrix();
	//glTranslatef( position.x, position.y,0.0f );
	data[0] = position.x;
	data[1] = position.y;

	//glRotatef( angle * ( 180.0f/3.141f ), 0.0f, 0.0f, 1.0f );
	data[2] = angle ;

	b2Fixture *f = g->GetFixtureList();
	while (f!=NULL)
	{
		b2Shape *s = f->GetShape();
		b2PolygonShape *p = (b2PolygonShape *)s;

		//glBegin(GL_LINE_LOOP);
		for (int32 v=0;v< p->GetVertexCount();v++)
		{
			b2Vec2 vec = p->GetVertex(v);
			//glVertex2f( vec.x, vec.y );
			data[3 + 2*counter ] = vec.x;
			data[3 + (2*counter+1) ] = vec.y;
			
			counter++;
			if (counter==4) break;
		}
		//glEnd();

		//f = f->GetNext();
		break;
	}

	//glPopMatrix();

}


BOXENGINE_API void BoxEngine_GetGroundData( float *data )
{
	int counter = 0;

	b2Body *g = groundBody;
	
	b2Vec2 position = g->GetPosition();
	float32 angle = g->GetAngle();

	//glPushMatrix();
	//glTranslatef( position.x, position.y,0.0f );
	data[0] = position.x;
	data[1] = position.y;

	//glRotatef( angle * ( 180.0f/3.141f ), 0.0f, 0.0f, 1.0f );
	data[2] = angle ;

	b2Fixture *f = g->GetFixtureList();
	while (f!=NULL)
	{
		b2Shape *s = f->GetShape();
		b2PolygonShape *p = (b2PolygonShape *)s;

		//glBegin(GL_LINE_LOOP);
		for (int32 v=0;v< p->GetVertexCount();v++)
		{
			b2Vec2 vec = p->GetVertex(v);
			//glVertex2f( vec.x, vec.y );
			data[3 + 2*counter ] = vec.x;
			data[3 + (2*counter+1) ] = vec.y;
			
			counter++;
			if (counter==4) break;
		}
		//glEnd();

		//f = f->GetNext();
		f = NULL;
	}

	glPopMatrix();
}


BOXENGINE_API void BoxEngine_GetBulletData( int *on, float *data )
{
	if (!bullet)
	{
		*on = 0;
		return;
	}

	*on = 1;
	b2Vec2 position = bullet->GetPosition();
	
	data[0] = position.x;
	data[1] = position.y;
	data[2] = bullet->GetAngle();
}

BOXENGINE_API void BoxEngine_GetCatapultData( float *data )
{
	int counter = 0;

	b2Body *g = bodycatarm;
	
	b2Vec2 position = g->GetPosition();
	float32 angle = g->GetAngle();

	//glPushMatrix();
	//glTranslatef( position.x, position.y,0.0f );
	data[0] = position.x;
	data[1] = position.y;

	//glRotatef( angle * ( 180.0f/3.141f ), 0.0f, 0.0f, 1.0f );
	data[2] = angle;

	b2Fixture *f = g->GetFixtureList();
	while (f!=NULL)
	{
		b2Shape *s = f->GetShape();
		b2PolygonShape *p = (b2PolygonShape *)s;

		//glBegin(GL_LINE_LOOP);
		for (int32 v=0;v< p->GetVertexCount();v++)
		{
			b2Vec2 vec = p->GetVertex(v);
			//glVertex2f( vec.x, vec.y );
			data[3 + 2*counter ] = vec.x;
			data[3 + (2*counter+1) ] = vec.y;
			
			counter++;
			if (counter==4) break;
		}
		//glEnd();

		//f = f->GetNext();
		f = NULL;
	}

	glPopMatrix();
}

//gw
BOXENGINE_API void BoxEngine_SimulationLoop()
{
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

	}

}




BOXENGINE_API void BoxEngine_MouseMotion(int32 x, int32 y)
{
	mjx = x;
	mjy = y;

	if (mouseJoint!=NULL)
	{
		b2Vec2 locationWorld = b2Vec2(mjx,mjy);
		mouseJoint->SetTarget( locationWorld);
	}
}

BOXENGINE_API int BoxEngine_Mouse(int32 button, int32 state, int32 x, int32 y)
{
	if (button==2)
	{
		if (bullet!=NULL)
		{
			world->DestroyBody(bullet);
			bullet = NULL;
		}
		return 1;
	}


	mjx = x;
	mjy = y;

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

		return 2;
	}
	else
	{
		int ret = 3;
		if ( bullet == NULL )
		{
			b2BodyDef bulletBodyDef;
			bulletBodyDef.type = b2_dynamicBody;
			bulletBodyDef.bullet = true;
			bulletBodyDef.position.Set( mjx, mjy + BULLET_OFFSET );
		
			bullet = world->CreateBody( &bulletBodyDef );
			bullet->SetActive(false);

			b2CircleShape circle;
			circle.m_radius = 3.0f;

			b2FixtureDef ballShapeDef;
			ballShapeDef.shape = &circle;
			ballShapeDef.density = 0.2f;
			ballShapeDef.restitution = 0.2f;
			ballShapeDef.friction = 0.99f;
			bullet->CreateFixture( &ballShapeDef );


			bullet->SetActive(true);

			ret = 4;
		}

		world->DestroyJoint(mouseJoint);
		mouseJoint = NULL;

		return ret;
	}

	return 0;
}

static b2Body* CreateBody( int which )
{
	float scalex = box_parms[which][0];
	float scaley = box_parms[which][1];
	float posx = box_parms[which][2];
	float posy = box_parms[which][3] - GROUND_OFFSET_Y;
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

BOXENGINE_API int BoxEngine_Stop()
{
	if (world==NULL) return 0;

	if (bullet!=NULL)
	{
		world->DestroyBody( bullet );
		bullet= NULL;
	}

	if ( mouseJoint!=NULL )
	{
		world->DestroyJoint( mouseJoint );
		mouseJoint = NULL;
	}

	if ( armJoint!=NULL )
	{
		world->DestroyJoint( armJoint );
		armJoint = NULL;
	}

	if (bodycatarm!=NULL)
	{
		world->DestroyBody( bodycatarm );
		bodycatarm= NULL;
	}

	if (groundBody!=NULL)
	{
		world->DestroyBody( groundBody );
		groundBody= NULL;
	}

	for (int i=0;i< BOXES;i++)
	{
		if (box[i]!=NULL) world->DestroyBody(box[i]);
		box[i] = NULL;
	}

	world = NULL;

	return 1;
}

// This is a simple example of building and running a simulation
// using Box2D. Here we create a large ground box and a small dynamic
// box.
// There are no graphics for this example. Box2D is meant to be used
// with your rendering engine in your game engine.
BOXENGINE_API int BoxEngine_Init(int32 win) //int main( int argc, char** argv)
{

	// Define the gravity vector.
	b2Vec2 gravity(0.0f, -10.0f);

	// Construct a world object, which will hold and simulate the rigid bodies.
	//gw b2World world(gravity);
	//gw b2World *world = new b2World(gravity);
	world = new b2World(gravity);


	// Define the ground body.
	b2BodyDef groundBodyDef;
	groundBodyDef.position.Set(0.0f, -10.0f - GROUND_OFFSET_Y );
	// Call the body factory which allocates memory for the ground body
	// from a pool and creates the ground box shape (also from a pool).
	// The body is also added to the world.
	//gw b2Body* groundBody = world.CreateBody(&groundBodyDef);
	//b2Body* groundBody = world->CreateBody(&groundBodyDef);
	groundBody = world->CreateBody(&groundBodyDef);
	// Define the ground box shape.
	b2PolygonShape groundBox;
	// The extents are the half-widths of the box.
	groundBox.SetAsBox(100.0f, 10.0f);
	// Add the ground fixture to the ground body.
	groundBody->CreateFixture(&groundBox, 0.0f);


	for ( int i=0; i<BOXES; i++)
	{
		box[i] = CreateBody( i );
	}


	/* catapult arm */
	b2BodyDef catarmdef;
	catarmdef.type = b2_dynamicBody;
	catarmdef.linearDamping = 1;
	catarmdef.angularDamping = 1;
	catarmdef.position.Set( -40, 10 - GROUND_OFFSET_Y );
	bodycatarm = world->CreateBody( &catarmdef );

	b2PolygonShape armBox;
	b2FixtureDef armBoxDef;
	armBoxDef.shape = &armBox;
	armBoxDef.density = 0.3f;
	armBox.SetAsBox( 1, 10 );
	b2Fixture *armFixture = bodycatarm->CreateFixture( &armBoxDef );

	/*catapult joint*/
	b2RevoluteJointDef armJointDef;
	armJointDef.Initialize( groundBody, bodycatarm, b2Vec2(-40, 1 - GROUND_OFFSET_Y) ); // -10,1
	armJointDef.enableMotor = true;
	armJointDef.enableLimit = true;
	armJointDef.motorSpeed = -1000;
	armJointDef.lowerAngle = 9.0 * (3.14/180.0);
	armJointDef.upperAngle = 75.0 * (3.14/180.0);
	armJointDef.maxMotorTorque = 10000;

	armJoint = (b2RevoluteJoint*)world->CreateJoint( &armJointDef );
	
	

	return 0;
}
