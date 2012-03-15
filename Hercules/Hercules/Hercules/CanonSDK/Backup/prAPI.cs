using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RDC.CameraSDK
{
    public class prAPI
    {
        /****************************************************************************
         ****************************************************************************
         *              PowerShot RemoteCapture SDK : Function Definitions          *
         ****************************************************************************
         ****************************************************************************/

        /*-----------------------------------------------------------------------
	        Basic Functions
        ------------------------------------------------------------------------*/
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_StartSDK();
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_FinishSDK();
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_GetDllsVersion(ref UInt32 pBufferSize, ref cdType.cdVersionInfo pVersion);

        //        prCAPI PR_GetFunctions(
        //    prFunctions* pFunctions
        //);


        /*-----------------------------------------------------------------------
	        Basic Camera Device Functios
        ------------------------------------------------------------------------*/

        /* Enumeration of Camera Devices */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_GetDeviceList(ref UInt32 pBufferSize, ref prType.prDeviceList pDeviceList);

        /* Creation/Deletion of Camera Handles */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_CreateCameraObject(ref prType.prDeviceInfoTable	pDeviceInfo, ref UInt32 pCameraHandle);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_DestroyCameraObject(UInt32 CameraHandle);

        /* Connecting/Disconnecting Camera Devices */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_ConnectCamera(UInt32	CameraHandle);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_DisconnectCamera(UInt32 CameraHandle);

        /* Retrieving Camera Events */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_SetEventCallBack(UInt32 CameraHandle, UInt32	Context, prType.prSetEventCB prSetEventCB);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_ClearEventCallBack(UInt32 CameraHandle);


        /* Retrieving Camera Device Performance Information */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_GetDeviceInfo(UInt32 CameraHandle, ref UInt32 pBufferSize, IntPtr pDeviceInfo);

        /*-----------------------------------------------------------------------
	        Remote Release Control Functions
        ------------------------------------------------------------------------*/

        /* Basic Functions */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_InitiateReleaseControl(UInt32 CameraHandle);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_TerminateReleaseControl(UInt32 CameraHandle);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_Release(UInt32 CameraHandle);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_GetReleasedData(UInt32 CameraHandle, UInt32 ObjectHandle,
                                                          prType.prptpEventCode EventCode, UInt32 TransSize,
                                                          UInt32 Context, prType.prGetFileDataCB pGetFileDataCB);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_GetNumAvailableShot(UInt32 CameraHandle, ref UInt32 pNum);

        /* Viewfinder Function */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_StartViewFinder(UInt32 CameraHandle, UInt32 Context, 
                                                          prType.prViewFinderCB pViewFinderCB);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_TermViewFinder(UInt32 CameraHandle);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_DoAeAfAwb(UInt32 CameraHandle, prType.prptpAeAfAwbResetFlag ResetFlag);

        /* AF Lock Settings */
        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_FocusLock(UInt32 CameraHandle);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_FocusUnlock(UInt32 CameraHandle);

        /*-----------------------------------------------------------------------
	        Device Property Functions
        ------------------------------------------------------------------------*/

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_GetDevicePropDesc(UInt32 CameraHandle, prType.prptpDevicePropCode DevicePropCode,
                                                         ref UInt32 pBufferSize, IntPtr pDevicePropDesc);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_GetDevicePropValue(UInt32 CameraHandle, prType.prptpDevicePropCode DevicePropCode,
                                                          ref UInt32 pBufferSize, IntPtr pDeviceProperty);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_SetDevicePropValue(UInt32 CameraHandle, prType.prptpDevicePropCode DevicePropCode,
                                                          UInt32 DataSize, IntPtr pDeviceProperty);

        [DllImport("PRSDK.dll")]
        public static extern UInt32 PR_RC_GetChangedReleaseParamesList(UInt32 CameraHandle, ref UInt32 pBufferSize,
                                                                       IntPtr pParamsList);
                
    }

}
