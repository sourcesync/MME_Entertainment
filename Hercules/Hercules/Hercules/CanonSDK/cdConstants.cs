using System;

namespace Rigaku.Components.CanonSDK 
{
	public class cdError 
	{
		/*-----------------------------------------------------------------------
		CD-SDK Functin Success Code
		------------------------------------------------------------------------*/
		public const UInt32 cdOK									= 0x00000000;

		/*-----------------------------------------------------------------------
		'   CD-SDK Function True/False Codes
		'------------------------------------------------------------------------*/
		public const UInt32 cdTRUE									= 0x00000001;
		public const UInt32 cdFALSE									= 0x00000000;

		/*-----------------------------------------------------------------------
		CD-SDK Generic Error IDs
		------------------------------------------------------------------------*/
		
		/* Miscellaneous errors */
		public const UInt32 cdUNIMPLEMENTED               			= 0x00000001; 
		public const UInt32 cdINTERNAL_ERROR						= 0x00000002;
		public const UInt32 cdMEM_ALLOC_FAILED					  	= 0x00000003;
		public const UInt32 cdMEM_FREE_FAILED						= 0x00000004;
		public const UInt32 cdOPERATION_CANCELLED					= 0x00000005;
		public const UInt32 cdINCOMPATIBLE_VERSION					= 0x00000006;
		public const UInt32 cdNOT_SUPPORTED							= 0x00000007;
		public const UInt32 cdUNEXPECTED_EXCEPTION					= 0x00000008;
		public const UInt32 cdPROTECTION_VIOLATION					= 0x00000009;  
		public const UInt32 cdMISSING_SUBCOMPONENT					= 0x0000000A;
		public const UInt32 cdSELECTION_UNAVAILABLE					= 0x0000000B;

		/* Function Parameter errors */
		public const UInt32 cdINVALID_PARAMETER 					= 0x00000060;
		public const UInt32 cdINVALID_HANDLE    					= 0x00000061;
		public const UInt32 cdINVALID_POINTER   					= 0x00000062;

		public const UInt32 cdENUM_NA								= 0x000000F0;
	}

	public class cdDevicePropertyID
	{
		public const UInt32 cdDEVICE_PROP_INVALID					= 0xFFFFFFFF;
		public const UInt32 cdDEVICE_PROP_MODEL_ID					= 0x00000001;
		public const UInt32 cdDEVICE_PROP_MODEL_NAME				= 0x00000002;
		public const UInt32 cdDEVICE_PROP_SLIDE_SHOW_CAP			= 0x00000003;
		public const UInt32 cdDEVICE_PROP_UPLOAD_FILE_CAP			= 0x00000004;
		public const UInt32 cdDEVICE_PROP_ROTATION_CAP				= 0x00000005;
		public const UInt32 cdDEVICE_PROP_DPOF_CAP					= 0x00000006;
		public const UInt32 cdDEVICE_PROP_THUMB_VALID_AREA			= 0x00000007;
		public const UInt32 cdDEVICE_PROP_RELEASE_CONTROL_CAP		= 0x00000008;
		public const UInt32 cdDEVICE_PROP_RAW_DEVELOP_FACULTIES		= 0x00000009;
		public const UInt32 cdDEVICE_PROP_PARSE_FACULTIES			= 0x0000000a;
		public const UInt32 cdDEVICE_PROP_OWNER_NAME				= 0x0800000c;
		public const UInt32 cdDEVICE_PROP_TIME						= 0x0800000d;
		public const UInt32 cdDEVICE_PROP_FIRMWARE_VERSION			= 0x0000000e;
		public const UInt32 cdDEVICE_PROP_BATTERY_STATUS			= 0x0000000f;
		public const UInt32 cdDEVICE_PROP_DIRECT_TRANSFER_STATUS	= 0x00000010;
	}

	/* cdWhence - From whence to seek in stream. */
	public class cdWhence
	{
		public const UInt16 cdSTART									= 1;
		public const UInt16 cdCURRENT								= 2;
		public const UInt16 cdEND									= 3;
	}

	/* cdPermission - Access permission with which to open stream. */
	public class cdPermission
	{
		public const UInt16 cdREAD									= 0x0001;
		public const UInt16 cdWRITE  								= 0x0002;
		public const UInt16 cdUPDATE 								= 0x0004;
	}

	/* cdMemType - Memory type of cdStgMedium */
	public class cdMemType
	{
		public const UInt32 cdMEMTYPE_FILE							= 0;
		public const UInt32 cdMEMTYPE_STREAM						= 1;
	}

	/* cdProgressOption - Options for progress callbadk functions. */
	public class cdProgressOption
	{
		public const UInt32 cdPROG_NO_REPORT						= 0x00000001;	/* Not called callback function. */
		public const UInt32 cdPROG_REPORT_DONE						= 0x00000002;	/* Called only when operation is finished */
		public const UInt32 cdPROG_REPORT_PERIODICALLY				= 0x00000003;	/* Called periodically during the operation */
	}

	/* Stream errors */
	public class cdStreamError
	{
		public const UInt32 cdSTREAM_IO_ERROR						= 0x000000A0;
		public const UInt32 cdSTREAM_NOT_OPEN						= 0x000000A1;
		public const UInt32 cdSTREAM_ALREADY_OPEN					= 0x000000A2;    
		public const UInt32 cdSTREAM_OPEN_ERROR						= 0x000000A3;
		public const UInt32 cdSTREAM_CLOSE_ERROR					= 0x000000A4;
		public const UInt32 cdSTREAM_SEEK_ERROR						= 0x000000A5;
		public const UInt32 cdSTREAM_TELL_ERROR						= 0x000000A6;
		public const UInt32 cdSTREAM_READ_ERROR						= 0x000000A7;
		public const UInt32 cdSTREAM_WRITE_ERROR					= 0x000000A8;
		public const UInt32 cdSTREAM_PERMISSION_ERROR				= 0x000000A9;
		public const UInt32 cdSTREAM_COULDNT_BEGIN_THREAD			= 0x000000AA;
		public const UInt32 cdSTREAM_BAD_OPTIONS					= 0x000000AB;
		public const UInt32 cdSTREAM_END_OF_STREAM					= 0x000000AC;
	}

	/* cdProgressStatus - Status of the current operation during progress callback function.*/
	public class cdProgressStatus
	{  
		public const UInt32 cdSTATUS_TRANSFER						= 0x00000001;	/* Status is data transfer between camera and host computer. */
		public const UInt32 cdSTATUS_CONVERT						= 0x00000002;	/* Status is image data converting.	 */
		public const UInt32 cdSTATUS_DEVELOPMENT					= 0x00000003;	/* Status is image data development. */
	}

	/*//////////  Event Severity levels ////////////////////////////////////////*/
	public class cdEVENT_SEVERITY
	{
		public const UInt32 cdEVENT_SEVERITY_NONE					= 0x00000000; 
		public const UInt32 cdEVENT_SEVERITY_WARNING				= 0x00020000; 
		public const UInt32 cdEVENT_SEVERITY_SHUTDOWN				= 0x00040000; 
	}

	public class cdReleaseEvent
	{
		public const UInt32 cdRELEASE_EVENT_RESET_HW_ERROR			= 10;
		public const UInt32 cdRELEASE_EVENT_CHANGED_BY_UI			= 27;
		public const UInt32 cdRELEASE_EVENT_RELEASE_START 			= 28;
		public const UInt32 cdRESEASE_EVENT_RELEASE_COMPLETE		= 29;
		public const UInt32 cdRELEASE_EVENT_CAM_RELEASE_ON			= 30;
		public const UInt32 cdRELEASE_EVENT_ROTATION_ANGLE_CHANGED	= 31;
		public const UInt32 cdRELEASE_EVENT_ABORT_PC_EVF			= 32;
		public const UInt32 cdRELEASE_EVENT_ENABLE_PC_EVF			= 33;
	}

	
	/* cdImageSize - Image size */
	public class cdImageSize
	{
		public const int cdIMAGE_SIZE_UNKNOWN						= 0xFFFF;
		public const int cdIMAGE_SIZE_LARGE							= 0x0000;
		public const int cdIMAGE_SIZE_MEDIUM						= 0x0001;
		public const int cdIMAGE_SIZE_SMALL							= 0x0002;
		public const int cdIMAGE_SIZE_MEDIUM1						= 0x0005;
		public const int cdIMAGE_SIZE_MEDIUM2						= 0x0006;
		public const int cdIMAGE_SIZE_MEDIUM3						= 0x0007;
	}

	/* cdCompQuality - Image data quality. */
	public class cdImageQuality
	{
		public const int cdIMAGE_QUALITY_UNKNOWN					= 0xFFFF;
		public const int cdIMAGE_QUALITY_ECONOMY					= 0x0001;
		public const int cdIMAGE_QUALITY_NORMAL						= 0x0002;
		public const int cdIMAGE_QUALITY_FINE						= 0x0003;
		//		public const UInt16 cdCOMP_QUALITY_LOSSLESS					= 0x0004;
		public const int cdIMAGE_QUALITY_RAW						= 0x0004;
		public const int cdIMAGE_QUALITY_SUPERFINE					= 0x0005;
	}

	/* cdReleaseControlCap
	   Capability of remote release control function of the camera.
	   OR value of the followings.                                */
	public class cdReleaseControlCap
	{
		public const UInt32 cdRELEASE_CONTROL_CAP_SUPPORT			= 0x40000000;
		public const UInt32 cdRELEASE_CONTROL_CAP_ZOOM				= 0x00000001;
		public const UInt32 cdRELEASE_CONTROL_CAP_SETPRM			= 0x00000002;
		public const UInt32 cdRELEASE_CONTROL_CAP_VIEWFINDER		= 0x00000004;
		public const UInt32 cdRELEASE_CONTROL_CAP_ABORT_VIEWFINDER	= 0x00000008;
		public const UInt32 cdRELEASE_CONTROL_CAP_AF_LOCK			= 0x00000010;
	}

	/* cdShootingMode */
	public class cdShootingMode
	{
		public const UInt16 cdSHOOTING_MODE_INVALID					= 0xFFFF;
		public const UInt16 cdSHOOTING_MODE_AUTO					= 0x0000;
		public const UInt16 cdSHOOTING_MODE_PROGRAM					= 0x0001;
		public const UInt16 cdSHOOTING_MODE_TV						= 0x0002;
		public const UInt16 cdSHOOTING_MODE_AV						= 0x0003;
		public const UInt16 cdSHOOTING_MODE_MANUAL					= 0x0004;
		public const UInt16 cdSHOOTING_MODE_A_DEP					= 0x0005;
		public const UInt16 cdSHOOTING_MODE_M_DEP					= 0x0006;
		public const UInt16 cdSHOOTING_MODE_BULB					= 0x0007;
		public const UInt16 cdSHOOTING_MODE_MANUAL_2				= 0x0065;
		public const UInt16 cdSHOOTING_MODE_FAR_SCENE				= 0x0066;
		public const UInt16 cdSHOOTING_MODE_FAST_SHUTTER			= 0x0067;
		public const UInt16 cdSHOOTING_MODE_SLOW_SHUTTER			= 0x0068;
		public const UInt16 cdSHOOTING_MODE_NIGHT_SCENE				= 0x0069;
		public const UInt16 cdSHOOTING_MODE_GRAY_SCALE				= 0x006a;
		public const UInt16 cdSHOOTING_MODE_SEPIA					= 0x006b;
		public const UInt16 cdSHOOTING_MODE_PORTRAIT				= 0x006c;
		public const UInt16 cdSHOOTING_MODE_SPOT					= 0x006d;
		public const UInt16 cdSHOOTING_MODE_MACRO					= 0x006e;
		public const UInt16 cdSHOOTING_MODE_BW						= 0x006f;
		public const UInt16 cdSHOOTING_MODE_PANFOCUS				= 0x0070;
		public const UInt16 cdSHOOTING_MODE_VIVID					= 0x0071;
		public const UInt16 cdSHOOTING_MODE_NEUTRAL					= 0x0072;
		public const UInt16 cdSHOOTING_MODE_FLASH_OFF				= 0x0073;
		public const UInt16 cdSHOOTING_MODE_LONG_SHUTTER			= 0x0074;
		public const UInt16 cdSHOOTING_MODE_SUPER_MACRO				= 0x0075;
		public const UInt16 cdSHOOTING_MODE_FOLIAGE					= 0x0076;
		public const UInt16 cdSHOOTING_MODE_INDOOR					= 0x0077;
		public const UInt16 cdSHOOTING_MODE_FIREWORKS				= 0x0078;
		public const UInt16 cdSHOOTING_MODE_BEACH					= 0x0079;
		public const UInt16 cdSHOOTING_MODE_UNDERWATER				= 0x007a;
		public const UInt16 cdSHOOTING_MODE_SNOW					= 0x007b;
		public const UInt16 cdSHOOTING_MODE_KIDS_AND_PETS			= 0x007c;
		public const UInt16 cdSHOOTING_MODE_NIGHT_SNAPSHOT			= 0x007d;
		public const UInt16 cdSHOOTING_MODE_DIGITAL_MACRO			= 0x007e;
	}

	/* cdFocusMode - AF mode setting */
	public class cdFocusMode
	{
		public const UInt16 cdFOCUS_MODE_SINGLE_AF  				= 0x0000; /* Single AF     */
		public const UInt16 cdFOCUS_MODE_CONTINUOUS_AF				= 0x0001; /* Continuous AF */
		public const UInt16 cdFOCUS_MODE_PAN_FOCUS  				= 0x0010; /* Pan Focus     */
		public const UInt16 cdFOCUS_MODE_ONE_SHOT_AF 				= 0x1000; /* One-Shot AF   */
		public const UInt16 cdFOCUS_MODE_AI_SERVO_AF 				= 0x1001; /* AI Servo AF   */
		public const UInt16 cdFOCUS_MODE_AI_FOCUS_AF 				= 0x1002; /* AI Focus AF   */
		public const UInt16 cdFOCUS_MODE_MF   						= 0x0008; /* Manual Focus  */
		public const UInt16 cdFOCUS_MODE_UNKNOWN  					= 0xffff; /* unknown       */
	}

	/* cdAFDistance - Auto focus mode by which distance is measured */
	public class cdAFDistance
	{
		public const UInt16 cdAF_DISTANCE_MANUAL					= 0x0000;
		public const UInt16 cdAF_DISTANCE_AUTO						= 0x0001;
		public const UInt16 cdAF_DISTANCE_UNKNOWN					= 0x0002;
		public const UInt16 cdAF_DISTANCE_CLOSE_UP					= 0x0003;	
		public const UInt16 cdAF_DISTANCE_VERY_CLOSE				= 0x0004;
		public const UInt16 cdAF_DISTANCE_CLOSE						= 0x0005;
		public const UInt16 cdAF_DISTANCE_MIDDLE					= 0x0006;	
		public const UInt16 cdAF_DISTANCE_FAR						= 0x0007;
		public const UInt16 cdAF_DISTANCE_PAN_FOCUS					= 0x0008;
		public const UInt16 cdAF_DISTANCE_SUPER_MACRO				= 0x0009;
		public const UInt16 cdAF_DISTANCE_INFINITY					= 0x000a;
		public const UInt16 cdAF_DISTANCE_NA						= 0x00ff;
	}
	/* cdPhotoEffect */
	public class cdPhotoEffect
	{
		public const UInt16 cdPHOTO_EFFECT_OFF						= 0x0000;	/* Off				*/
		public const UInt16 cdPHOTO_EFFECT_VIVID					= 0x0001;	/* Vivid			*/
		public const UInt16 cdPHOTO_EFFECT_NEUTRAL					= 0x0002;	/* Neutral			*/
		public const UInt16 cdPHOTO_EFFECT_LOW_SHARPENING			= 0x0003;	/* Low sharpening	*/
		public const UInt16 cdPHOTO_EFFECT_SEPIA					= 0x0004;	/* Sepia			*/
		public const UInt16 cdPHOTO_EFFECT_BW						= 0x0005;	/* Black & white	*/
		public const UInt16 cdPHOTO_EFFECT_CUSTOM					= 0x0006;	/* Custom			*/
		public const UInt16 cdPHOTO_EFFECT_MY_COLOR					= 0x0064;	/* My color data    */
		public const UInt16 cdPHOTO_EFFECT_UNKNOWN					= 0xffff;	/* Unknown			*/
	}

	/* cdFlashMode */
	public class cdFlashMode
	{
		public const UInt16 cdFLASH_MODE_OFF						= 0x0000;
		public const UInt16 cdFLASH_MODE_AUTO						= 0x0001;
		public const UInt16 cdFLASH_MODE_ON							= 0x0002;
		public const UInt16 cdFLASH_MODE_RED_EYE					= 0x0003;
		public const UInt16 cdFLASH_MODE_SLOW_SYNC					= 0x0004;
		public const UInt16 cdFLASH_MODE_AUTO_PLUS_RED_EYE			= 0x0005;
		public const UInt16 cdFLASH_MODE_ON_PLUS_RED_EYE			= 0x0006;
		public const UInt16 cdFLASH_MODE_NA							= 0x00ff;
	}

	/* cdCompensation */
	public class cdExposureCompensation
	{
		public const UInt16 cdCOMP_300_PLUS							= 0x0000;
		public const UInt16 cdCOMP_266_PLUS 						= 0x0003;
		public const UInt16 cdCOMP_250_PLUS							= 0x0004;
		public const UInt16 cdCOMP_233_PLUS							= 0x0005;
		public const UInt16 cdCOMP_200_PLUS							= 0x0008;
		public const UInt16 cdCOMP_166_PLUS							= 0x000b;
		public const UInt16 cdCOMP_150_PLUS							= 0x000c;
		public const UInt16 cdCOMP_133_PLUS							= 0x000d;
		public const UInt16 cdCOMP_100_PLUS							= 0x0010;
		public const UInt16 cdCOMP_066_PLUS							= 0x0013;
		public const UInt16 cdCOMP_050_PLUS							= 0x0014;
		public const UInt16 cdCOMP_033_PLUS							= 0x0015;
		public const UInt16 cdCOMP_000_PLUS							= 0x0018;
		public const UInt16 cdCOMP_033_MINUS						= 0x001b;
		public const UInt16 cdCOMP_050_MINUS						= 0x001c;
		public const UInt16 cdCOMP_066_MINUS						= 0x001d;
		public const UInt16 cdCOMP_100_MINUS						= 0x0020;
		public const UInt16 cdCOMP_133_MINUS						= 0x0023;
		public const UInt16 cdCOMP_150_MINUS						= 0x0024;
		public const UInt16 cdCOMP_166_MINUS						= 0x0025;
		public const UInt16 cdCOMP_200_MINUS						= 0x0028;
		public const UInt16 cdCOMP_233_MINUS						= 0x002b;
		public const UInt16 cdCOMP_250_MINUS						= 0x002c;
		public const UInt16 cdCOMP_266_MINUS						= 0x002d;
		public const UInt16 cdCOMP_300_MINUS						= 0x0030;
		public const UInt16 cdCOMP_NA								= 0x00ff;
	}

	/* cdRemoteSetAv - Av in remote release control mode. */
	public class cdRemoteSetAv
	{
		public const UInt16 cdREMOTE_SET_AV_00						= 0x0000;	/* No Lens */
		public const UInt16 cdREMOTE_SET_AV_NA 						= 0xffff;
		public const UInt16 cdREMOTE_SET_AV_Open 					= 0x7fff;
		public const UInt16 cdREMOTE_SET_AV_Max						= 0x7ffe;

		/* 1/3 stop */
		public const UInt16 cdREMOTE_SET_AV3_10						= 0x0008;
		public const UInt16 cdREMOTE_SET_AV3_11						= 0x000b;
		public const UInt16 cdREMOTE_SET_AV3_12						= 0x000d;
		public const UInt16 cdREMOTE_SET_AV3_14						= 0x0010;
		public const UInt16 cdREMOTE_SET_AV3_16						= 0x0013;
		public const UInt16 cdREMOTE_SET_AV3_18						= 0x0015;
		public const UInt16 cdREMOTE_SET_AV3_20						= 0x0018;
		public const UInt16 cdREMOTE_SET_AV3_22						= 0x001b;
		public const UInt16 cdREMOTE_SET_AV3_25						= 0x001d;
		public const UInt16 cdREMOTE_SET_AV3_28						= 0x0020;
		public const UInt16 cdREMOTE_SET_AV3_32						= 0x0023;
		public const UInt16 cdREMOTE_SET_AV3_35						= 0x0025;
		public const UInt16 cdREMOTE_SET_AV3_40						= 0x0028;
		public const UInt16 cdREMOTE_SET_AV3_45						= 0x002b;
		public const UInt16 cdREMOTE_SET_AV3_50						= 0x002d;
		public const UInt16 cdREMOTE_SET_AV3_56						= 0x0030;
		public const UInt16 cdREMOTE_SET_AV3_63						= 0x0033;
		public const UInt16 cdREMOTE_SET_AV3_71						= 0x0035;
		public const UInt16 cdREMOTE_SET_AV3_80						= 0x0038;
		public const UInt16 cdREMOTE_SET_AV3_90						= 0x003b;
		public const UInt16 cdREMOTE_SET_AV3_100					= 0x003d;
		public const UInt16 cdREMOTE_SET_AV3_110					= 0x0040;
		public const UInt16 cdREMOTE_SET_AV3_130					= 0x0043;
		public const UInt16 cdREMOTE_SET_AV3_140					= 0x0045;
		public const UInt16 cdREMOTE_SET_AV3_160					= 0x0048;
		public const UInt16 cdREMOTE_SET_AV3_180					= 0x004b;
		public const UInt16 cdREMOTE_SET_AV3_200					= 0x004d;
		public const UInt16 cdREMOTE_SET_AV3_220					= 0x0050;
		public const UInt16 cdREMOTE_SET_AV3_250					= 0x0053;
		public const UInt16 cdREMOTE_SET_AV3_290					= 0x0055;
		public const UInt16 cdREMOTE_SET_AV3_320					= 0x0058;
		public const UInt16 cdREMOTE_SET_AV3_360					= 0x005b;
		public const UInt16 cdREMOTE_SET_AV3_400					= 0x005d;
		public const UInt16 cdREMOTE_SET_AV3_450					= 0x0060;
		public const UInt16 cdREMOTE_SET_AV3_510					= 0x0063;
		public const UInt16 cdREMOTE_SET_AV3_570					= 0x0065;
		public const UInt16 cdREMOTE_SET_AV3_640					= 0x0068;
		public const UInt16 cdREMOTE_SET_AV3_720					= 0x006b;
		public const UInt16 cdREMOTE_SET_AV3_810					= 0x006d;
		public const UInt16 cdREMOTE_SET_AV3_910					= 0x0070;

		/* 1/2 stop */
		public const UInt16 cdREMOTE_SET_AV2_10						= 0x0008;
		public const UInt16 cdREMOTE_SET_AV2_12						= 0x000c;
		public const UInt16 cdREMOTE_SET_AV2_14						= 0x0010;
		public const UInt16 cdREMOTE_SET_AV2_18						= 0x0014;
		public const UInt16 cdREMOTE_SET_AV2_20						= 0x0018;
		public const UInt16 cdREMOTE_SET_AV2_25						= 0x001c;
		public const UInt16 cdREMOTE_SET_AV2_28						= 0x0020;
		public const UInt16 cdREMOTE_SET_AV2_35						= 0x0024;
		public const UInt16 cdREMOTE_SET_AV2_40						= 0x0028;
		public const UInt16 cdREMOTE_SET_AV2_45						= 0x002c;
		public const UInt16 cdREMOTE_SET_AV2_56						= 0x0030;
		public const UInt16 cdREMOTE_SET_AV2_67						= 0x0034;
		public const UInt16 cdREMOTE_SET_AV2_80						= 0x0038;
		public const UInt16 cdREMOTE_SET_AV2_95						= 0x003c;
		public const UInt16 cdREMOTE_SET_AV2_110					= 0x0040;
		public const UInt16 cdREMOTE_SET_AV2_130					= 0x0044;
		public const UInt16 cdREMOTE_SET_AV2_160					= 0x0048;
		public const UInt16 cdREMOTE_SET_AV2_190					= 0x004c;
		public const UInt16 cdREMOTE_SET_AV2_220					= 0x0050;
		public const UInt16 cdREMOTE_SET_AV2_270					= 0x0054;
		public const UInt16 cdREMOTE_SET_AV2_320					= 0x0058;
		public const UInt16 cdREMOTE_SET_AV2_380					= 0x005c;
		public const UInt16 cdREMOTE_SET_AV2_450					= 0x0060;
		public const UInt16 cdREMOTE_SET_AV2_540					= 0x0064;
		public const UInt16 cdREMOTE_SET_AV2_640					= 0x0068;
		public const UInt16 cdREMOTE_SET_AV2_760					= 0x006c;
		public const UInt16 cdREMOTE_SET_AV2_910					= 0x0070;

		/* 1 stop */
		public const UInt16 cdREMOTE_SET_AV1_10						= 0x0008;
		public const UInt16 cdREMOTE_SET_AV1_14						= 0x0010;
		public const UInt16 cdREMOTE_SET_AV1_20						= 0x0018;
		public const UInt16 cdREMOTE_SET_AV1_28						= 0x0020;
		public const UInt16 cdREMOTE_SET_AV1_40						= 0x0028;
		public const UInt16 cdREMOTE_SET_AV1_56						= 0x0030;
		public const UInt16 cdREMOTE_SET_AV1_80						= 0x0038;
		public const UInt16 cdREMOTE_SET_AV1_110					= 0x0040;
		public const UInt16 cdREMOTE_SET_AV1_160					= 0x0048;
		public const UInt16 cdREMOTE_SET_AV1_220					= 0x0050;
		public const UInt16 cdREMOTE_SET_AV1_320					= 0x0058;
		public const UInt16 cdREMOTE_SET_AV1_450					= 0x0060;
		public const UInt16 cdREMOTE_SET_AV1_640					= 0x0068;
		public const UInt16 cdREMOTE_SET_AV1_910					= 0x0070;
	}

	/* cdRemoteSetTv - Tv in remote release control mode. */
	public class cdRemoteSetTv
	{
		public const UInt16 cdREMOTE_SET_TV_NA    					= 0xffff;
		public const UInt16 cdREMOTE_SET_TV_BULB					= 0x0004;

		/* 1/3 stop */
		public const UInt16 cdREMOTE_SET_TV3_30sec					= 0x0010;
		public const UInt16 cdREMOTE_SET_TV3_25sec					= 0x0013;
		public const UInt16 cdREMOTE_SET_TV3_20sec					= 0x0015;
		public const UInt16 cdREMOTE_SET_TV3_15sec					= 0x0018;
		public const UInt16 cdREMOTE_SET_TV3_13sec					= 0x001b;
		public const UInt16 cdREMOTE_SET_TV3_10sec					= 0x001d;
		public const UInt16 cdREMOTE_SET_TV3_8sec					= 0x0020;
		public const UInt16 cdREMOTE_SET_TV3_6sec					= 0x0023;
		public const UInt16 cdREMOTE_SET_TV3_5sec					= 0x0025;
		public const UInt16 cdREMOTE_SET_TV3_4sec					= 0x0028;
		public const UInt16 cdREMOTE_SET_TV3_3sec2					= 0x002b;
		public const UInt16 cdREMOTE_SET_TV3_2sec5					= 0x002d;
		public const UInt16 cdREMOTE_SET_TV3_2sec					= 0x0030;
		public const UInt16 cdREMOTE_SET_TV3_1sec6					= 0x0033;
		public const UInt16 cdREMOTE_SET_TV3_1sec3					= 0x0035;
		public const UInt16 cdREMOTE_SET_TV3_1sec					= 0x0038;
		public const UInt16 cdREMOTE_SET_TV3_0sec8					= 0x003b;
		public const UInt16 cdREMOTE_SET_TV3_0sec6					= 0x003d;
		public const UInt16 cdREMOTE_SET_TV3_0sec5					= 0x0040;
		public const UInt16 cdREMOTE_SET_TV3_0sec4					= 0x0043;
		public const UInt16 cdREMOTE_SET_TV3_0sec3					= 0x0045;
		public const UInt16 cdREMOTE_SET_TV3_4						= 0x0048;
		public const UInt16 cdREMOTE_SET_TV3_5						= 0x004b;
		public const UInt16 cdREMOTE_SET_TV3_6						= 0x004d;
		public const UInt16 cdREMOTE_SET_TV3_8						= 0x0050;
		public const UInt16 cdREMOTE_SET_TV3_10						= 0x0053;
		public const UInt16 cdREMOTE_SET_TV3_13						= 0x0055;
		public const UInt16 cdREMOTE_SET_TV3_15						= 0x0058;
		public const UInt16 cdREMOTE_SET_TV3_20						= 0x005b;
		public const UInt16 cdREMOTE_SET_TV3_25						= 0x005d;
		public const UInt16 cdREMOTE_SET_TV3_30						= 0x0060;
		public const UInt16 cdREMOTE_SET_TV3_40						= 0x0063;
		public const UInt16 cdREMOTE_SET_TV3_50						= 0x0065;
		public const UInt16 cdREMOTE_SET_TV3_60						= 0x0068;
		public const UInt16 cdREMOTE_SET_TV3_80						= 0x006b;
		public const UInt16 cdREMOTE_SET_TV3_100					= 0x006d;
		public const UInt16 cdREMOTE_SET_TV3_125					= 0x0070;
		public const UInt16 cdREMOTE_SET_TV3_160					= 0x0073;
		public const UInt16 cdREMOTE_SET_TV3_200					= 0x0075;
		public const UInt16 cdREMOTE_SET_TV3_250					= 0x0078;
		public const UInt16 cdREMOTE_SET_TV3_320					= 0x007b;
		public const UInt16 cdREMOTE_SET_TV3_400					= 0x007d;
		public const UInt16 cdREMOTE_SET_TV3_500					= 0x0080;
		public const UInt16 cdREMOTE_SET_TV3_640					= 0x0083;
		public const UInt16 cdREMOTE_SET_TV3_800					= 0x0085;
		public const UInt16 cdREMOTE_SET_TV3_1000					= 0x0088;
		public const UInt16 cdREMOTE_SET_TV3_1250					= 0x008b;
		public const UInt16 cdREMOTE_SET_TV3_1600					= 0x008d;
		public const UInt16 cdREMOTE_SET_TV3_2000					= 0x0090;
		public const UInt16 cdREMOTE_SET_TV3_2500					= 0x0093;
		public const UInt16 cdREMOTE_SET_TV3_3200					= 0x0095;
		public const UInt16 cdREMOTE_SET_TV3_4000					= 0x0098;
		public const UInt16 cdREMOTE_SET_TV3_5000					= 0x009b;
		public const UInt16 cdREMOTE_SET_TV3_6400					= 0x009d;
		public const UInt16 cdREMOTE_SET_TV3_8000					= 0x00a0;
		public const UInt16 cdREMOTE_SET_TV3_10000					= 0x00a3;
		public const UInt16 cdREMOTE_SET_TV3_12800					= 0x00a5;
		public const UInt16 cdREMOTE_SET_TV3_16000					= 0x00a8;

		/* 1/2 stop */
		public const UInt16 cdREMOTE_SET_TV2_30sec					= 0x0010;
		public const UInt16 cdREMOTE_SET_TV2_20sec					= 0x0014;
		public const UInt16 cdREMOTE_SET_TV2_15sec					= 0x0018;
		public const UInt16 cdREMOTE_SET_TV2_10sec					= 0x001c;
		public const UInt16 cdREMOTE_SET_TV2_8sec					= 0x0020;
		public const UInt16 cdREMOTE_SET_TV2_6sec					= 0x0024;
		public const UInt16 cdREMOTE_SET_TV2_4sec					= 0x0028;
		public const UInt16 cdREMOTE_SET_TV2_3sec					= 0x002c;
		public const UInt16 cdREMOTE_SET_TV2_2sec					= 0x0030;
		public const UInt16 cdREMOTE_SET_TV2_1sec5					= 0x0034;
		public const UInt16 cdREMOTE_SET_TV2_1sec					= 0x0038;
		public const UInt16 cdREMOTE_SET_TV2_0sec7					= 0x003C;
		public const UInt16 cdREMOTE_SET_TV2_0sec5					= 0x0040;
		public const UInt16 cdREMOTE_SET_TV2_0sec3					= 0x0044;
		public const UInt16 cdREMOTE_SET_TV2_4						= 0x0048;
		public const UInt16 cdREMOTE_SET_TV2_6						= 0x004c;
		public const UInt16 cdREMOTE_SET_TV2_8						= 0x0050;
		public const UInt16 cdREMOTE_SET_TV2_10						= 0x0054;
		public const UInt16 cdREMOTE_SET_TV2_15						= 0x0058;
		public const UInt16 cdREMOTE_SET_TV2_20						= 0x005c;
		public const UInt16 cdREMOTE_SET_TV2_30						= 0x0060;
		public const UInt16 cdREMOTE_SET_TV2_45						= 0x0064;
		public const UInt16 cdREMOTE_SET_TV2_60						= 0x0068;
		public const UInt16 cdREMOTE_SET_TV2_90						= 0x006c;
		public const UInt16 cdREMOTE_SET_TV2_125					= 0x0070;
		public const UInt16 cdREMOTE_SET_TV2_180					= 0x0074;
		public const UInt16 cdREMOTE_SET_TV2_250					= 0x0078;
		public const UInt16 cdREMOTE_SET_TV2_350					= 0x007c;
		public const UInt16 cdREMOTE_SET_TV2_500					= 0x0080;
		public const UInt16 cdREMOTE_SET_TV2_750					= 0x0084;
		public const UInt16 cdREMOTE_SET_TV2_1000					= 0x0088;
		public const UInt16 cdREMOTE_SET_TV2_1500					= 0x008c;
		public const UInt16 cdREMOTE_SET_TV2_2000					= 0x0090;
		public const UInt16 cdREMOTE_SET_TV2_3000					= 0x0094;
		public const UInt16 cdREMOTE_SET_TV2_4000					= 0x0098;
		public const UInt16 cdREMOTE_SET_TV2_6000					= 0x009c;
		public const UInt16 cdREMOTE_SET_TV2_8000					= 0x00a0;
		public const UInt16 cdREMOTE_SET_TV2_12000					= 0x00a4;
		public const UInt16 cdREMOTE_SET_TV2_16000					= 0x00a8;

		/* 1 stop */
		public const UInt16 cdREMOTE_SET_TV1_30sec					= 0x0010;
		public const UInt16 cdREMOTE_SET_TV1_15sec					= 0x0018;
		public const UInt16 cdREMOTE_SET_TV1_8sec					= 0x0020;
		public const UInt16 cdREMOTE_SET_TV1_4sec					= 0x0028;
		public const UInt16 cdREMOTE_SET_TV1_2sec					= 0x0030;
		public const UInt16 cdREMOTE_SET_TV1_1sec					= 0x0038;
		public const UInt16 cdREMOTE_SET_TV1_0sec5					= 0x0040;
		public const UInt16 cdREMOTE_SET_TV1_4						= 0x0048;
		public const UInt16 cdREMOTE_SET_TV1_8						= 0x0050;
		public const UInt16 cdREMOTE_SET_TV1_15						= 0x0058;
		public const UInt16 cdREMOTE_SET_TV1_30						= 0x0060;
		public const UInt16 cdREMOTE_SET_TV1_60						= 0x0068;
		public const UInt16 cdREMOTE_SET_TV1_125					= 0x0070;
		public const UInt16 cdREMOTE_SET_TV1_250					= 0x0078;
		public const UInt16 cdREMOTE_SET_TV1_500					= 0x0080;
		public const UInt16 cdREMOTE_SET_TV1_1000					= 0x0088;
		public const UInt16 cdREMOTE_SET_TV1_2000					= 0x0090;
		public const UInt16 cdREMOTE_SET_TV1_4000					= 0x0098;
		public const UInt16 cdREMOTE_SET_TV1_8000					= 0x00a0;
		public const UInt16 cdREMOTE_SET_TV1_16000					= 0x00a8;
	}
	/* ISO speed setting.
		   Attribute : Depends on the camera model.
		   Size/Type : 2 / cdUInt16 */
	public class cdISOSpeeds
	{
		public const UInt16 cdREL_VAL_ISO_AUTO						= 0x0000;	/* Auto		*/
		public const UInt16 cdREL_VAL_ISO_6							= 0x0028;	/* ISO 6	*/
		public const UInt16 cdREL_VAL_ISO_8							= 0x002b;	/* ISO 8	*/
		public const UInt16 cdREL_VAL_ISO_10						= 0x002d;	/* ISO 10	*/
		public const UInt16 cdREL_VAL_ISO_12						= 0x0030;	/* ISO 12	*/
		public const UInt16 cdREL_VAL_ISO_16						= 0x0033;	/* ISO 16	*/
		public const UInt16 cdREL_VAL_ISO_20						= 0x0035;	/* ISO 20	*/
		public const UInt16 cdREL_VAL_ISO_25						= 0x0038;	/* ISO 25	*/
		public const UInt16 cdREL_VAL_ISO_32						= 0x003b;	/* ISO 32	*/
		public const UInt16 cdREL_VAL_ISO_40						= 0x003d;	/* ISO 40	*/
		public const UInt16 cdREL_VAL_ISO_50						= 0x0040;	/* ISO 50	*/
		public const UInt16 cdREL_VAL_ISO_64						= 0x0043;	/* ISO 64	*/
		public const UInt16 cdREL_VAL_ISO_80						= 0x0045;	/* ISO 80	*/
		public const UInt16 cdREL_VAL_ISO_100						= 0x0048;	/* ISO 100	*/
		public const UInt16 cdREL_VAL_ISO_125						= 0x004b;	/* ISO 125	*/
		public const UInt16 cdREL_VAL_ISO_160						= 0x004d;	/* ISO 160	*/
		public const UInt16 cdREL_VAL_ISO_200						= 0x0050;	/* ISO 200	*/
		public const UInt16 cdREL_VAL_ISO_250						= 0x0053;	/* ISO 250	*/
		public const UInt16 cdREL_VAL_ISO_320						= 0x0055;	/* ISO 320	*/
		public const UInt16 cdREL_VAL_ISO_400						= 0x0058;	/* ISO 400	*/
		public const UInt16 cdREL_VAL_ISO_500						= 0x005b;	/* ISO 500	*/
		public const UInt16 cdREL_VAL_ISO_640						= 0x005d;	/* ISO 640	*/
		public const UInt16 cdREL_VAL_ISO_800						= 0x0060;	/* ISO 800	*/
		public const UInt16 cdREL_VAL_ISO_1000						= 0x0063;	/* ISO 1000	*/
		public const UInt16 cdREL_VAL_ISO_1250						= 0x0065;	/* ISO 1250	*/
		public const UInt16 cdREL_VAL_ISO_1600						= 0x0068;	/* ISO 1600	*/
		public const UInt16 cdREL_VAL_ISO_2000						= 0x006b;	/* ISO 2000	*/
		public const UInt16 cdREL_VAL_ISO_2500						= 0x006d;	/* ISO 2500	*/
		public const UInt16 cdREL_VAL_ISO_3200						= 0x0070;	/* ISO 3200	*/
		public const UInt16 cdREL_VAL_ISO_4000						= 0x0073;	/* ISO 4000	*/
		public const UInt16 cdREL_VAL_ISO_5000						= 0x0075;	/* ISO 5000	*/
		public const UInt16 cdREL_VAL_ISO_6400						= 0x0078;	/* ISO 6400	*/
		public const UInt16 cdREL_VAL_ISO_NA						= 0xffff;	/* Invalid	*/
	}
}

