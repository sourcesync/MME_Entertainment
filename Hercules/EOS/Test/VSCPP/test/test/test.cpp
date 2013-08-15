// test.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"
#include "EDSDK.h"
#include "EDSDKErrors.h"
#include "EDSDKTypes.h"
EdsError getFirstCamera(EdsCameraRef *camera);
int _tmain(int argc, _TCHAR* argv[])
{  
	/*
	
	EdsError err=EDS_ERR_OK;
EdsCameraRef camera=NULL;
bool isSDKloaded=false;
// Initialize SDK
   err=EdsInitializeSDK();
if(err==EDS_ERR_OK)
{
isSDKloaded=true;
}

// Get first camera
if(err==EDS_ERR_OK)
{
err=getFirstCamera(&camera);
}
EdsOpenSession(camera);
EdsInt32 saveTarget = kEdsSaveTo_Host;
err = EdsSetPropertyData( camera, kEdsPropID_SaveTo, 0, 4, &saveTarget );
EdsSendCommand(camera, kEdsCameraCommand_TakePicture, 0);
EdsCloseSession(camera);
EdsTerminateSDK();


*/

return 0;
}



EdsError getFirstCamera(EdsCameraRef *camera)
{
EdsError err=EDS_ERR_OK;
EdsCameraListRef cameraList=NULL;
EdsUInt32 count=0;
// Get camera list
err = EdsGetCameraList(&cameraList);
// Get number of cameras
if(err == EDS_ERR_OK)
{
    err = EdsGetChildCount(cameraList, &count);
    if(count == 0)
    {
        err = EDS_ERR_DEVICE_NOT_FOUND;
    }
}
// Get first camera retrieved
if(err == EDS_ERR_OK)
{
    err = EdsGetChildAtIndex(cameraList , 0 , camera);
}
// Release camera list
if(cameraList != NULL)
{EdsRelease(cameraList);
cameraList = NULL;
}
return err;
}
