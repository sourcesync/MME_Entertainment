using System;
using System.Runtime.InteropServices; 
using System.Text;

namespace RDC.CameraSDK
{
	public class cdAPI 
	{
		/*-----------------------------------------------------------------------
		   Basic Functions 
		------------------------------------------------------------------------*/

        [DllImport("CDSDK.dll")]
		public static extern UInt32 CDStartSDK(ref cdType.cdVersionInfo pVersion, UInt32 Option);		
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDFinishSDK();
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetSDKVersion(ref cdType.cdVersionInfo pVersion);
		
		/*-----------------------------------------------------------------------
		   Source Control Functions
		------------------------------------------------------------------------*/

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetPreviousDeviceInfo(UInt32 Selected, ref cdType.cdSourceInfo pSourceInfo);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumDeviceReset(UInt32 Kind, ref UInt32 phEnum);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumDeviceNext(UInt32 hEnum, ref cdType.cdSourceInfo pSourceInfo);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetDeviceCount(UInt32 hEnum, ref UInt32 pCount);
		[DllImport("CDSDK.dll")]
		public static extern UInt32	CDEnumDeviceRelease(UInt32 hEnum);		

		[DllImport("CDSDK.dll")]
        //public static extern UInt32 CDOpenSource(ref cdType.cdSourceInfo pSourceInfo, ref UInt32 phSource);
        public static extern UInt32 CDOpenSource( IntPtr pSourceInfo, ref IntPtr phSource);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDCloseSource(IntPtr hSource);

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDRegisterEventCallbackFunction(IntPtr hSource,
			cdType.cdEventCallbackFunction pFunc, UInt32 Context, ref UInt32 phFunc);

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDUnregisterEventCallbackFunction(IntPtr hSource, UInt32 hFunc);			

		/*-----------------------------------------------------------------------
		   Device Management Functions 
		------------------------------------------------------------------------*/

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumDevicePropertyReset(IntPtr hSource, UInt32 Option, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetDevicePropertyCount(UInt32 hEnum, ref UInt32 pCount);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumDevicePropertyRelease(UInt32 hEnum);
//		[DllImport ("CDSDK.dll")]
//		public static extern UInt32 CDGetDevicePropertyData(IntPtr hSource, UInt32 DevPropID, ref UInt32 pBufSize, ref UInt32 pBuf, UInt32 Option);
//		[DllImport ("CDSDK.dll")]
//		public static extern UInt32 CDGetDevicePropertyData(IntPtr hSource, UInt32 DevPropID, ref UInt32 pBufSize, [MarshalAs(UnmanagedType.LPTStr, SizeConst = 32)] string pBuf, UInt32 Option);
		[DllImport ("CDSDK.dll", CharSet=CharSet.Auto)]
		public static extern unsafe UInt32 CDGetDevicePropertyData(IntPtr hSource, UInt32 DevPropID, Int32* pBufSize, void* pBuf, UInt32 Option);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDLockUI(IntPtr hSource);			
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDUnlockUI(IntPtr hSource);					

		/*-----------------------------------------------------------------------
		   Collect Functions  
		------------------------------------------------------------------------*/

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumVolumeReset(IntPtr hSource,	ref UInt32 phEnum);			
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumVolumeNext(UInt32	hEnum, ref UInt32 phVolume);			
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetVolumeCount(UInt32	hEnum,ref UInt32 pCount);			
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumVolumeRelease(UInt32 hEnum);			
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetVolumeName(UInt32 hVolume,	ref string pVolName);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetVolumeInfo(UInt32 hVolume,	ref cdType.cdVolumeInfo pVolInfo);

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumItemReset(UInt32 hParent, UInt32 Option,	ref UInt32 phEnum);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumItemNext(UInt32 hEnum, ref UInt32 phItem);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetItemCount(UInt32 hEnum, ref UInt32	pCount);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumItemRelease(UInt32 hEnum);

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetItemInfo(UInt32 hItem,	ref cdType.cdItemInfo pItemInfo);

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumImageItemReset(UInt32 hItem, UInt32 Depth, UInt32 Option, ref UInt32 phEnum);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumImageItemNext(UInt32 hEnum, ref UInt32	phImage);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetImageItemCount(UInt32 hEnum,	ref UInt32 pCount);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumImageItemRelease(UInt32 hEnum);


		/*-----------------------------------------------------------------------
			Image Management Functions  
		------------------------------------------------------------------------*/

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDOpenImage(UInt32 hImgItem);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDCloseImage(UInt32 hImgItem);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetThumbnail(UInt32 hImgItem,	ref UInt32	phImgData);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetPicture(UInt32 hImgItem,	ref UInt32 phImgData);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetMovie (UInt32 hImgItem, ref UInt32 phImgData);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetSound (UInt32 hImgItem, ref UInt32 phImgData);

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDDeleteImage(UInt32 hParent, UInt32 hImgItem);			

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetImageData(UInt32 hImgData, ref	cdType.cdStgMedium pStgMediumDest,
			cdType.cdProgressCallbackFunction pCallbackFunc, UInt32 Context, UInt32 ProgressOption);			
		
		/*-----------------------------------------------------------------------
		   Image Property Functions
		------------------------------------------------------------------------*/

		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumImageItemPropertyReset(UInt32 hImgItem,	ref UInt32 phEnum);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDEnumImageItemPropertyRelease(UInt32	hEnum);
		[DllImport("CDSDK.dll")]
		public static extern UInt32 CDGetImageItemProperty(UInt32 hImgItem, UInt32 ImgPropID, 
			ref UInt32 pBufSize, ref UInt32 pBuf);

		/*-----------------------------------------------------------------------
		   Remote Release Control Functions  
		------------------------------------------------------------------------*/

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnterReleaseControl(IntPtr hSource, cdType.cdReleaseEventCallbackFunction pFunc, UInt32 Context);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDExitReleaseControl(IntPtr hSource);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSelectReleaseDataKind(IntPtr hSource, cdType.cdRelDataKind Kind);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDStartViewfinder(IntPtr hSource, UInt32 Format, cdType.cdViewFinderCallbackFunction pFunc, UInt32 Context);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDTermViewfinder(IntPtr hSource);
		
		[DllImport ("CDSDK.dll")]
        public static extern UInt32 CDActViewfinderAutoFunctions(IntPtr hSource, cdType.cdAeAfAwbResetFlag ActivateFlag);
		
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDRelease(IntPtr hSource, UInt16 bSync, cdType.cdProgressCallbackFunction pCallbackFunc,
                                              UInt32 Context, cdType.cdProgressOption ProgressOption, ref UInt32 pNumData);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetReleasedData(IntPtr hSource, cdType.cdProgressCallbackFunction pCallbackFunc, UInt32 Context,
                                                      cdType.cdProgressOption ProgressOption, ref cdType.cdReleaseImageInfo pInfo, ref cdType.cdStgMedium pStgMedium);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetMaximumZoomPos(IntPtr hSource, ref UInt32 pMaxZoomPos, ref UInt32 pMaxOpticalZoomPos);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetZoomPos(IntPtr hSource, ref UInt32 pZoomPos);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetDZoomMagnification (IntPtr hSource, ref cdType.cdURational pDZoomMag);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetZoomPos(IntPtr hSource, UInt32 ZoomPos);
		
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDAFLock(IntPtr hSource, UInt32 bLock);

		[DllImport ("CDSDK.dll")]
        public static extern UInt32 CDSetImageFormatAttribute(IntPtr hSource, cdType.cdImageQuality pQuality, cdType.cdImageSize pSize);
		[DllImport ("CDSDK.dll")]
        public static extern UInt32 CDGetImageFormatAttribute(IntPtr hSource, ref cdType.cdImageQuality pQuality, 
                                                                              ref cdType.cdImageSize pSize);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumImageFormatAttributeReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
        public static extern UInt32 CDEnumImageFormatAttributeNext(UInt32 hEnum, ref cdType.cdImageQuality pQuality, 
                                                                                 ref cdType.cdImageSize pSize);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumImageFormatAttributeRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetImageFormatAttributeCount(UInt32 hEnum, ref UInt32 pCount);


		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetShootingMode(IntPtr hSource, UInt16 ShootingMode);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetShootingMode(IntPtr hSource, ref UInt16 pShootingMode);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumShootingModeReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumShootingModeNext(UInt32 hEnum, ref UInt16 pShootingMode);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumShootingModeRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetShootingModeCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetExposureComp(IntPtr hSource, UInt16 ExpoComp);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetExposureComp(IntPtr hSource, ref UInt16 pExpoComp);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumExposureCompReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumExposureCompNext(UInt32 hEnum, ref UInt16 pComp);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumExposureCompRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetExposureCompCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetAvValue (IntPtr hSource, UInt16 Av);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetAvValue (IntPtr hSource, ref UInt16 pAv);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumAvValueReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumAvValueNext(UInt32 hEnum, ref UInt16 pAv);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumAvValueRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetAvValueCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetTvValue (IntPtr hSource, UInt16 Tv);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetTvValue (IntPtr hSource, ref UInt16 pTv);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumTvValueReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumTvValueNext(UInt32 hEnum, ref UInt16 pTv);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumTvValueRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetTvValueCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetWBSetting(IntPtr hSource, UInt16 WhiteBalance);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetWBSetting(IntPtr hSource, ref UInt16 pWhiteBalance);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumWBSettingReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumWBSettingNext(UInt32 hEnum, ref UInt16 pWBLightSrc);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumWBSettingRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetWBSettingCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetAFDistanceSetting (IntPtr hSource, UInt16 AFDistance);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetAFDistanceSetting (IntPtr hSource, ref UInt16 pAFDistance);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumAFDistanceSettingReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumAFDistanceSettingNext(UInt32 hEnum, ref UInt16 pAFDistance);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumAFDistanceSettingRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetAFDistanceSettingCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
        public static extern UInt32 CDSetFlashSetting(IntPtr hSource, cdType.cdFlashMode Mode, UInt16 Compensation);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetFlashSetting(IntPtr hSource, ref UInt16 pMode, ref UInt16 pCompensation);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumFlashSettingReset(IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumFlashSettingNext(UInt32 hEnum, ref UInt16 pFlashMode);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumFlashSettingRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetFlashSettingCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumRelCamSettingReset (IntPtr hSource, ref UInt32 phEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumRelCamSettingNext (UInt32 hEnum, ref cdType.cdRelCamSettingStruct pRelCamSettingStruct);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetRelCamSettingCount (UInt32 hEnum, ref UInt32 pCount);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumRelCamSettingRelease (UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetRelCamSettingData(IntPtr hSource, UInt32 SettingID, ref UInt32 pBufSize, ref UInt16 pBuf);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDSetRelCamSettingData(IntPtr hSource, UInt32 SettingID, UInt32 BufSize, ref UInt16 pBuf);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumRelCamSettingDataReset(IntPtr hSource, UInt32 SettingID, ref UInt32 phEnum, ref UInt32 pBufSize);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumRelCamSettingDataNext(UInt32 hEnum, UInt32 BufSize, ref UInt16 pBuf);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDEnumRelCamSettingDataRelease(UInt32 hEnum);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetRelCamSettingDataCount(UInt32 hEnum, ref UInt32 pCount);

		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetNumAvailableShot(IntPtr hSource, ref UInt32 pNum);


		/*-----------------------------------------------------------------------
		   Utility Functions 
		------------------------------------------------------------------------*/

//		[DllImport ("CDSDK.dll")]
//		public static extern UInt32 CDCreateMemStream(UInt32 StartSize, UInt32 MinAllocSize, ref cdType.cdStream pStream);
//		[DllImport ("CDSDK.dll")]
//		public static extern UInt32 CDDestroyMemStream(ref cdType.cdStream pStream);
//		[DllImport ("CDSDK.dll")]
//		public static extern UInt32 CDGetStreamInfo(ref cdType.cdStream pStream, ref UInt32 pSize, IntPtr ppMem);
		[DllImport ("CDSDK.dll")]
		public static extern UInt32 CDGetImagePropertyPart(UInt32 hImgItem, ref cdType.cdStgMedium pStgMedium);

	}

}
