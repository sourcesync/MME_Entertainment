// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the BOXENGINE_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// BOXENGINE_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef BOXENGINE_EXPORTS
#define BOXENGINE_API __declspec(dllexport)
#else
#define BOXENGINE_API __declspec(dllimport)
#endif


#include "glui/glui.h"
#include <Box2D/Box2D.h>

// This class is exported from the boxengine.dll
class BOXENGINE_API Cboxengine {
public:
	Cboxengine(void);
	// TODO: add your methods here.
};

extern "C"
{

extern BOXENGINE_API int nboxengine;

BOXENGINE_API int fnboxengine(void);

BOXENGINE_API void BoxEngine_SimulationLoop();

BOXENGINE_API int BoxEngine_Init(int32 w);

BOXENGINE_API int BoxEngine_Stop();

BOXENGINE_API void BoxEngine_Timer(int);

BOXENGINE_API int BoxEngine_Mouse(int32 button, int32 state, int32 x, int32 y);


BOXENGINE_API void BoxEngine_MouseMotion(int32 x, int32 y);


BOXENGINE_API void BoxEngine_GetBoxData( int idx, float *data );

BOXENGINE_API void BoxEngine_GetGroundData( float *data );

BOXENGINE_API void BoxEngine_GetCatapultData( float *data );


BOXENGINE_API void BoxEngine_GetBulletData( int *on, float *data );
}