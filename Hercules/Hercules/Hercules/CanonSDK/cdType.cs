using System;
using System.Runtime.InteropServices;

namespace RDC.CameraSDK 
{
	public class cdType 
	{
        /****************************************************************************
        ****************************************************************************
        *                 Canon Digital Camera SDK: Constants                      *
        ****************************************************************************
        ****************************************************************************/
		public const string CAMERAEVENTMESSAGESTRING_RELEASE_ON		= "Camera Event ReleaseOn";
		public const string CAMERAEVENTMESSAGESTRING_RELEASE_COMP	= "Camera Event ReleaseComplete";
		public const string CAMERAEVENTMESSAGESTRING_ABORT_PC_EVF	= "Camera Event Abort PC EVF";
		public const string CAMERAEVENTMESSAGESTRING_CHANGE_BY_UI	= "Camera Event Change By UI";

		public const UInt32 cdENUM_HAS_THUMBNAIL					= 0x00000001;

		public const UInt32 cdMEMTYPE_FILE							= 0;
		public const UInt32 cdMEMTYPE_STREAM						= 1;
        
        public const UInt32 cdBASE_IMG_PROP_ROTATION_ANGLE = 0x00001800;
        public const UInt32 cdIMG_PROP_ROTATION_ANGLE = 0x08001800;
        public const UInt32 cdIMG_PROP_LOSSLESS_ROTATION_ANGLE = 0x08008300;

        public const UInt32 FILEFORMAT_JPG = 0;
        public const UInt32 FILEFORMAT_BMP = 1;

        public const UInt32 cdREL_SET_ISO_SPEED_RATINGS = 0x0800000c;
        public const UInt32 cdREL_SET_PHOTO_EFFECT = 0x08000b00;

        /****************************************************************************
        ****************************************************************************
        *                 Canon Digital Camera SDK: Enumerations                   *
        ****************************************************************************
        ****************************************************************************/
		public enum cdItemType : uint
		{
          	cdITEM_TYPE_SUB_FOLDER	= 0x00000001,
			cdITEM_TYPE_IMAGE_ITEM	= 0x00000002,
			cdITEM_TYPE_FILE		= 0x00000003 
		}
		
		public enum cdDataType : uint
		{
          	cdDATA_TYPE_THUMBNAIL	 = 0x00000010,		/* Thumbnail image data */
			cdDATA_TYPE_PICTURE		 = 0x00000020,		/* Picture (Full resolution image) data */
			cdDATA_TYPE_SOUND		 = 0x00000030,		/* Sound data */
			cdDATA_TYPE_MOVIE		 = 0x00000040,		/* Movie data */
			cdDATA_TYPE_PLUS_JPEG	 = 0x00000050,		/* +Jpg image data */
			cdDATA_TYPE_PREVIEW_JPEG = 0x00000060 		/* Jpeg data included in RAW file */
		}
		
		[Flags]public enum cdRelDataKind : ushort
		{
			cdREL_KIND_THUMB_TO_PC		= 0x0001,
			cdREL_KIND_PICT_TO_PC		= 0x0002,
			cdREL_KIND_THUMB_TO_CAM		= 0x0004,
			cdREL_KIND_PICT_TO_CAM		= 0x0008  	
		}

        public enum cdDevicePropertyID : uint
        {
            cdDEVICE_PROP_INVALID = 0xFFFFFFFF,
            cdDEVICE_PROP_MODEL_ID = 0x00000001,
            cdDEVICE_PROP_MODEL_NAME = 0x00000002,
            cdDEVICE_PROP_SLIDE_SHOW_CAP = 0x00000003,
            cdDEVICE_PROP_UPLOAD_FILE_CAP = 0x00000004,
            cdDEVICE_PROP_ROTATION_CAP = 0x00000005,
            cdDEVICE_PROP_DPOF_CAP = 0x00000006,
            cdDEVICE_PROP_THUMB_VALID_AREA = 0x00000007,
            cdDEVICE_PROP_RELEASE_CONTROL_CAP = 0x00000008,
            cdDEVICE_PROP_RAW_DEVELOP_FACULTIES = 0x00000009,
            cdDEVICE_PROP_PARSE_FACULTIES = 0x0000000a,
            cdDEVICE_PROP_OWNER_NAME = 0x0800000c,
            cdDEVICE_PROP_TIME = 0x0800000d,
            cdDEVICE_PROP_FIRMWARE_VERSION = 0x0000000e,
            cdDEVICE_PROP_BATTERY_STATUS = 0x0000000f,
            cdDEVICE_PROP_DIRECT_TRANSFER_STATUS = 0x00000010
        }

        public enum cdWhence : ushort
        {
            cdSTART = 1,
            cdCURRENT = 2,
            cdEND = 3
        }

        public enum cdPermission : ushort
        {
            cdREAD = 0x0001,
            cdWRITE = 0x0002,
            cdUPDATE = 0x0004
        }

        public enum cdMemType : uint
        {
            cdMEMTYPE_FILE = 0,
            cdMEMTYPE_STREAM = 1
        }

        public enum cdProgressOption : uint
        {
            cdPROG_NO_REPORT = 0x00000001,	/* Not called callback function. */
            cdPROG_REPORT_DONE = 0x00000002,	/* Called only when operation is finished */
            cdPROG_REPORT_PERIODICALLY = 0x00000003	/* Called periodically during the operation */
        }

        public enum cdStreamError : uint
        {
            cdSTREAM_IO_ERROR = 0x000000A0,
            cdSTREAM_NOT_OPEN = 0x000000A1,
            cdSTREAM_ALREADY_OPEN = 0x000000A2,
            cdSTREAM_OPEN_ERROR = 0x000000A3,
            cdSTREAM_CLOSE_ERROR = 0x000000A4,
            cdSTREAM_SEEK_ERROR = 0x000000A5,
            cdSTREAM_TELL_ERROR = 0x000000A6,
            cdSTREAM_READ_ERROR = 0x000000A7,
            cdSTREAM_WRITE_ERROR = 0x000000A8,
            cdSTREAM_PERMISSION_ERROR = 0x000000A9,
            cdSTREAM_COULDNT_BEGIN_THREAD = 0x000000AA,
            cdSTREAM_BAD_OPTIONS = 0x000000AB,
            cdSTREAM_END_OF_STREAM = 0x000000AC
        }

        public enum cdProgressStatus : uint
        {
            cdSTATUS_TRANSFER = 0x00000001,	/* Status is data transfer between camera and host computer. */
            cdSTATUS_CONVERT = 0x00000002,	/* Status is image data converting.	 */
            cdSTATUS_DEVELOPMENT = 0x00000003	/* Status is image data development. */
        }

        public enum cdEVENT_SEVERITY : uint
        {
            cdEVENT_SEVERITY_NONE = 0x00000000,
            cdEVENT_SEVERITY_WARNING = 0x00020000,
            cdEVENT_SEVERITY_SHUTDOWN = 0x00040000
        }

        public enum cdReleaseEvent : uint
        {
            cdRELEASE_EVENT_RESET_HW_ERROR = 10,
            cdRELEASE_EVENT_CHANGED_BY_UI = 27,
            cdRELEASE_EVENT_RELEASE_START = 28,
            cdRESEASE_EVENT_RELEASE_COMPLETE = 29,
            cdRELEASE_EVENT_CAM_RELEASE_ON = 30,
            cdRELEASE_EVENT_ROTATION_ANGLE_CHANGED = 31,
            cdRELEASE_EVENT_ABORT_PC_EVF = 32,
            cdRELEASE_EVENT_ENABLE_PC_EVF = 33
        }

        public enum cdSourceType : uint
        {
            cdSRC_TYPE_INVALID = 0,
            cdSRC_TYPE_HOST = 1,
            cdSRC_TYPE_CAMERA = 2
        }

        [Flags]
        public enum cdAeAfAwbResetFlag : uint
        {
            cdAEAFAWB_RESET_AE = 0x00000001,		/* AE Reset */
            cdAEAFAWB_RESET_AF = 0x00000002,		/* AF Reset */
            cdAEAFAWB_RESET_AWB = 0x00000004		/* AWB Reset */
        }

        public enum cdImageSize : ushort
        {
            cdIMAGE_SIZE_UNKNOWN = 0xFFFF,
            cdIMAGE_SIZE_LARGE = 0x0000,
            cdIMAGE_SIZE_MEDIUM = 0x0001,
            cdIMAGE_SIZE_SMALL = 0x0002,
            cdIMAGE_SIZE_MEDIUM1 = 0x0005,
            cdIMAGE_SIZE_MEDIUM2 = 0x0006,
            cdIMAGE_SIZE_MEDIUM3 = 0x0007
        }

        public enum cdImageQuality : ushort
        {
            cdIMAGE_QUALITY_UNKNOWN = 0xFFFF,
            cdIMAGE_QUALITY_ECONOMY = 0x0001,
            cdIMAGE_QUALITY_NORMAL = 0x0002,
            cdIMAGE_QUALITY_FINE = 0x0003,
            cdCOMP_QUALITY_LOSSLESS = 0x0004,
            cdIMAGE_QUALITY_RAW = 0x0004,
            cdIMAGE_QUALITY_SUPERFINE = 0x0005
        }

        public enum cdReleaseControlCap : uint
        {
            cdRELEASE_CONTROL_CAP_SUPPORT = 0x40000000,
            cdRELEASE_CONTROL_CAP_ZOOM = 0x00000001,
            cdRELEASE_CONTROL_CAP_SETPRM = 0x00000002,
            cdRELEASE_CONTROL_CAP_VIEWFINDER = 0x00000004,
            cdRELEASE_CONTROL_CAP_ABORT_VIEWFINDER = 0x00000008,
            cdRELEASE_CONTROL_CAP_AF_LOCK = 0x00000010
        }

        public enum cdShootingMode : ushort
        {
            cdSHOOTING_MODE_INVALID = 0xFFFF,
            cdSHOOTING_MODE_AUTO = 0x0000,
            cdSHOOTING_MODE_PROGRAM = 0x0001,
            cdSHOOTING_MODE_TV = 0x0002,
            cdSHOOTING_MODE_AV = 0x0003,
            cdSHOOTING_MODE_MANUAL = 0x0004,
            cdSHOOTING_MODE_A_DEP = 0x0005,
            cdSHOOTING_MODE_M_DEP = 0x0006,
            cdSHOOTING_MODE_BULB = 0x0007,
            cdSHOOTING_MODE_MANUAL_2 = 0x0065,
            cdSHOOTING_MODE_FAR_SCENE = 0x0066,
            cdSHOOTING_MODE_FAST_SHUTTER = 0x0067,
            cdSHOOTING_MODE_SLOW_SHUTTER = 0x0068,
            cdSHOOTING_MODE_NIGHT_SCENE = 0x0069,
            cdSHOOTING_MODE_GRAY_SCALE = 0x006a,
            cdSHOOTING_MODE_SEPIA = 0x006b,
            cdSHOOTING_MODE_PORTRAIT = 0x006c,
            cdSHOOTING_MODE_SPOT = 0x006d,
            cdSHOOTING_MODE_MACRO = 0x006e,
            cdSHOOTING_MODE_BW = 0x006f,
            cdSHOOTING_MODE_PANFOCUS = 0x0070,
            cdSHOOTING_MODE_VIVID = 0x0071,
            cdSHOOTING_MODE_NEUTRAL = 0x0072,
            cdSHOOTING_MODE_FLASH_OFF = 0x0073,
            cdSHOOTING_MODE_LONG_SHUTTER = 0x0074,
            cdSHOOTING_MODE_SUPER_MACRO = 0x0075,
            cdSHOOTING_MODE_FOLIAGE = 0x0076,
            cdSHOOTING_MODE_INDOOR = 0x0077,
            cdSHOOTING_MODE_FIREWORKS = 0x0078,
            cdSHOOTING_MODE_BEACH = 0x0079,
            cdSHOOTING_MODE_UNDERWATER = 0x007a,
            cdSHOOTING_MODE_SNOW = 0x007b,
            cdSHOOTING_MODE_KIDS_AND_PETS = 0x007c,
            cdSHOOTING_MODE_NIGHT_SNAPSHOT = 0x007d,
            cdSHOOTING_MODE_DIGITAL_MACRO = 0x007e
        }

        public enum cdFocusMode : ushort
        {
            cdFOCUS_MODE_SINGLE_AF = 0x0000, /* Single AF     */
            cdFOCUS_MODE_CONTINUOUS_AF = 0x0001, /* Continuous AF */
            cdFOCUS_MODE_PAN_FOCUS = 0x0010, /* Pan Focus     */
            cdFOCUS_MODE_ONE_SHOT_AF = 0x1000, /* One-Shot AF   */
            cdFOCUS_MODE_AI_SERVO_AF = 0x1001, /* AI Servo AF   */
            cdFOCUS_MODE_AI_FOCUS_AF = 0x1002, /* AI Focus AF   */
            cdFOCUS_MODE_MF = 0x0008, /* Manual Focus  */
            cdFOCUS_MODE_UNKNOWN = 0xffff  /* unknown       */
        }

        public enum cdAFDistance : ushort
        {
            cdAF_DISTANCE_MANUAL = 0x0000,
            cdAF_DISTANCE_AUTO = 0x0001,
            cdAF_DISTANCE_UNKNOWN = 0x0002,
            cdAF_DISTANCE_CLOSE_UP = 0x0003,
            cdAF_DISTANCE_VERY_CLOSE = 0x0004,
            cdAF_DISTANCE_CLOSE = 0x0005,
            cdAF_DISTANCE_MIDDLE = 0x0006,
            cdAF_DISTANCE_FAR = 0x0007,
            cdAF_DISTANCE_PAN_FOCUS = 0x0008,
            cdAF_DISTANCE_SUPER_MACRO = 0x0009,
            cdAF_DISTANCE_INFINITY = 0x000a,
            cdAF_DISTANCE_NA = 0x00ff
        }

        public enum cdPhotoEffect : ushort
        {
            cdPHOTO_EFFECT_OFF = 0x0000,	/* Off				*/
            cdPHOTO_EFFECT_VIVID = 0x0001,	/* Vivid			*/
            cdPHOTO_EFFECT_NEUTRAL = 0x0002,	/* Neutral			*/
            cdPHOTO_EFFECT_LOW_SHARPENING = 0x0003,	/* Low sharpening	*/
            cdPHOTO_EFFECT_SEPIA = 0x0004,	/* Sepia			*/
            cdPHOTO_EFFECT_BW = 0x0005,	/* Black & white	*/
            cdPHOTO_EFFECT_CUSTOM = 0x0006,	/* Custom			*/
            cdPHOTO_EFFECT_MY_COLOR = 0x0064,	/* My color data    */
            cdPHOTO_EFFECT_UNKNOWN = 0xffff 	/* Unknown			*/
        }

        public enum cdFlashMode : ushort
        {
            cdFLASH_MODE_OFF = 0x0000,
            cdFLASH_MODE_AUTO = 0x0001,
            cdFLASH_MODE_ON = 0x0002,
            cdFLASH_MODE_RED_EYE = 0x0003,
            cdFLASH_MODE_SLOW_SYNC = 0x0004,
            cdFLASH_MODE_AUTO_PLUS_RED_EYE = 0x0005,
            cdFLASH_MODE_ON_PLUS_RED_EYE = 0x0006,
            cdFLASH_MODE_NA = 0x00ff
        }

        public enum cdExposureCompensation : ushort
        {
            cdCOMP_300_PLUS = 0x0000,
            cdCOMP_266_PLUS = 0x0003,
            cdCOMP_250_PLUS = 0x0004,
            cdCOMP_233_PLUS = 0x0005,
            cdCOMP_200_PLUS = 0x0008,
            cdCOMP_166_PLUS = 0x000b,
            cdCOMP_150_PLUS = 0x000c,
            cdCOMP_133_PLUS = 0x000d,
            cdCOMP_100_PLUS = 0x0010,
            cdCOMP_066_PLUS = 0x0013,
            cdCOMP_050_PLUS = 0x0014,
            cdCOMP_033_PLUS = 0x0015,
            cdCOMP_000_PLUS = 0x0018,
            cdCOMP_033_MINUS = 0x001b,
            cdCOMP_050_MINUS = 0x001c,
            cdCOMP_066_MINUS = 0x001d,
            cdCOMP_100_MINUS = 0x0020,
            cdCOMP_133_MINUS = 0x0023,
            cdCOMP_150_MINUS = 0x0024,
            cdCOMP_166_MINUS = 0x0025,
            cdCOMP_200_MINUS = 0x0028,
            cdCOMP_233_MINUS = 0x002b,
            cdCOMP_250_MINUS = 0x002c,
            cdCOMP_266_MINUS = 0x002d,
            cdCOMP_300_MINUS = 0x0030,
            cdCOMP_NA = 0x00ff
        }

        public enum cdAperture : ushort
        {
            cdREMOTE_SET_AV_00 = 0x0000,	/* No Lens */
            cdREMOTE_SET_AV_NA = 0xffff,
            cdREMOTE_SET_AV_Open = 0x7fff,
            cdREMOTE_SET_AV_Max = 0x7ffe,

            /* 1/3 stop */
            cdREMOTE_SET_AV3_10 = 0x0008,
            cdREMOTE_SET_AV3_11 = 0x000b,
            cdREMOTE_SET_AV3_12 = 0x000d,
            cdREMOTE_SET_AV3_14 = 0x0010,
            cdREMOTE_SET_AV3_16 = 0x0013,
            cdREMOTE_SET_AV3_18 = 0x0015,
            cdREMOTE_SET_AV3_20 = 0x0018,
            cdREMOTE_SET_AV3_22 = 0x001b,
            cdREMOTE_SET_AV3_25 = 0x001d,
            cdREMOTE_SET_AV3_28 = 0x0020,
            cdREMOTE_SET_AV3_32 = 0x0023,
            cdREMOTE_SET_AV3_35 = 0x0025,
            cdREMOTE_SET_AV3_40 = 0x0028,
            cdREMOTE_SET_AV3_45 = 0x002b,
            cdREMOTE_SET_AV3_50 = 0x002d,
            cdREMOTE_SET_AV3_56 = 0x0030,
            cdREMOTE_SET_AV3_63 = 0x0033,
            cdREMOTE_SET_AV3_71 = 0x0035,
            cdREMOTE_SET_AV3_80 = 0x0038,
            cdREMOTE_SET_AV3_90 = 0x003b,
            cdREMOTE_SET_AV3_100 = 0x003d,
            cdREMOTE_SET_AV3_110 = 0x0040,
            cdREMOTE_SET_AV3_130 = 0x0043,
            cdREMOTE_SET_AV3_140 = 0x0045,
            cdREMOTE_SET_AV3_160 = 0x0048,
            cdREMOTE_SET_AV3_180 = 0x004b,
            cdREMOTE_SET_AV3_200 = 0x004d,
            cdREMOTE_SET_AV3_220 = 0x0050,
            cdREMOTE_SET_AV3_250 = 0x0053,
            cdREMOTE_SET_AV3_290 = 0x0055,
            cdREMOTE_SET_AV3_320 = 0x0058,
            cdREMOTE_SET_AV3_360 = 0x005b,
            cdREMOTE_SET_AV3_400 = 0x005d,
            cdREMOTE_SET_AV3_450 = 0x0060,
            cdREMOTE_SET_AV3_510 = 0x0063,
            cdREMOTE_SET_AV3_570 = 0x0065,
            cdREMOTE_SET_AV3_640 = 0x0068,
            cdREMOTE_SET_AV3_720 = 0x006b,
            cdREMOTE_SET_AV3_810 = 0x006d,
            cdREMOTE_SET_AV3_910 = 0x0070,

            /* 1/2 stop */
            cdREMOTE_SET_AV2_10 = 0x0008,
            cdREMOTE_SET_AV2_12 = 0x000c,
            cdREMOTE_SET_AV2_14 = 0x0010,
            cdREMOTE_SET_AV2_18 = 0x0014,
            cdREMOTE_SET_AV2_20 = 0x0018,
            cdREMOTE_SET_AV2_25 = 0x001c,
            cdREMOTE_SET_AV2_28 = 0x0020,
            cdREMOTE_SET_AV2_35 = 0x0024,
            cdREMOTE_SET_AV2_40 = 0x0028,
            cdREMOTE_SET_AV2_45 = 0x002c,
            cdREMOTE_SET_AV2_56 = 0x0030,
            cdREMOTE_SET_AV2_67 = 0x0034,
            cdREMOTE_SET_AV2_80 = 0x0038,
            cdREMOTE_SET_AV2_95 = 0x003c,
            cdREMOTE_SET_AV2_110 = 0x0040,
            cdREMOTE_SET_AV2_130 = 0x0044,
            cdREMOTE_SET_AV2_160 = 0x0048,
            cdREMOTE_SET_AV2_190 = 0x004c,
            cdREMOTE_SET_AV2_220 = 0x0050,
            cdREMOTE_SET_AV2_270 = 0x0054,
            cdREMOTE_SET_AV2_320 = 0x0058,
            cdREMOTE_SET_AV2_380 = 0x005c,
            cdREMOTE_SET_AV2_450 = 0x0060,
            cdREMOTE_SET_AV2_540 = 0x0064,
            cdREMOTE_SET_AV2_640 = 0x0068,
            cdREMOTE_SET_AV2_760 = 0x006c,
            cdREMOTE_SET_AV2_910 = 0x0070,

            /* 1 stop */
            cdREMOTE_SET_AV1_10 = 0x0008,
            cdREMOTE_SET_AV1_14 = 0x0010,
            cdREMOTE_SET_AV1_20 = 0x0018,
            cdREMOTE_SET_AV1_28 = 0x0020,
            cdREMOTE_SET_AV1_40 = 0x0028,
            cdREMOTE_SET_AV1_56 = 0x0030,
            cdREMOTE_SET_AV1_80 = 0x0038,
            cdREMOTE_SET_AV1_110 = 0x0040,
            cdREMOTE_SET_AV1_160 = 0x0048,
            cdREMOTE_SET_AV1_220 = 0x0050,
            cdREMOTE_SET_AV1_320 = 0x0058,
            cdREMOTE_SET_AV1_450 = 0x0060,
            cdREMOTE_SET_AV1_640 = 0x0068,
            cdREMOTE_SET_AV1_910 = 0x0070
        }

        public enum cdShutterSpeed : ushort
        {
            cdREMOTE_SET_TV_NA = 0xffff,
            cdREMOTE_SET_TV_BULB = 0x0004,

            /* 1/3 stop */
            cdREMOTE_SET_TV3_30sec = 0x0010,
            cdREMOTE_SET_TV3_25sec = 0x0013,
            cdREMOTE_SET_TV3_20sec = 0x0015,
            cdREMOTE_SET_TV3_15sec = 0x0018,
            cdREMOTE_SET_TV3_13sec = 0x001b,
            cdREMOTE_SET_TV3_10sec = 0x001d,
            cdREMOTE_SET_TV3_8sec = 0x0020,
            cdREMOTE_SET_TV3_6sec = 0x0023,
            cdREMOTE_SET_TV3_5sec = 0x0025,
            cdREMOTE_SET_TV3_4sec = 0x0028,
            cdREMOTE_SET_TV3_3sec2 = 0x002b,
            cdREMOTE_SET_TV3_2sec5 = 0x002d,
            cdREMOTE_SET_TV3_2sec = 0x0030,
            cdREMOTE_SET_TV3_1sec6 = 0x0033,
            cdREMOTE_SET_TV3_1sec3 = 0x0035,
            cdREMOTE_SET_TV3_1sec = 0x0038,
            cdREMOTE_SET_TV3_0sec8 = 0x003b,
            cdREMOTE_SET_TV3_0sec6 = 0x003d,
            cdREMOTE_SET_TV3_0sec5 = 0x0040,
            cdREMOTE_SET_TV3_0sec4 = 0x0043,
            cdREMOTE_SET_TV3_0sec3 = 0x0045,
            cdREMOTE_SET_TV3_4 = 0x0048,
            cdREMOTE_SET_TV3_5 = 0x004b,
            cdREMOTE_SET_TV3_6 = 0x004d,
            cdREMOTE_SET_TV3_8 = 0x0050,
            cdREMOTE_SET_TV3_10 = 0x0053,
            cdREMOTE_SET_TV3_13 = 0x0055,
            cdREMOTE_SET_TV3_15 = 0x0058,
            cdREMOTE_SET_TV3_20 = 0x005b,
            cdREMOTE_SET_TV3_25 = 0x005d,
            cdREMOTE_SET_TV3_30 = 0x0060,
            cdREMOTE_SET_TV3_40 = 0x0063,
            cdREMOTE_SET_TV3_50 = 0x0065,
            cdREMOTE_SET_TV3_60 = 0x0068,
            cdREMOTE_SET_TV3_80 = 0x006b,
            cdREMOTE_SET_TV3_100 = 0x006d,
            cdREMOTE_SET_TV3_125 = 0x0070,
            cdREMOTE_SET_TV3_160 = 0x0073,
            cdREMOTE_SET_TV3_200 = 0x0075,
            cdREMOTE_SET_TV3_250 = 0x0078,
            cdREMOTE_SET_TV3_320 = 0x007b,
            cdREMOTE_SET_TV3_400 = 0x007d,
            cdREMOTE_SET_TV3_500 = 0x0080,
            cdREMOTE_SET_TV3_640 = 0x0083,
            cdREMOTE_SET_TV3_800 = 0x0085,
            cdREMOTE_SET_TV3_1000 = 0x0088,
            cdREMOTE_SET_TV3_1250 = 0x008b,
            cdREMOTE_SET_TV3_1600 = 0x008d,
            cdREMOTE_SET_TV3_2000 = 0x0090,
            cdREMOTE_SET_TV3_2500 = 0x0093,
            cdREMOTE_SET_TV3_3200 = 0x0095,
            cdREMOTE_SET_TV3_4000 = 0x0098,
            cdREMOTE_SET_TV3_5000 = 0x009b,
            cdREMOTE_SET_TV3_6400 = 0x009d,
            cdREMOTE_SET_TV3_8000 = 0x00a0,
            cdREMOTE_SET_TV3_10000 = 0x00a3,
            cdREMOTE_SET_TV3_12800 = 0x00a5,
            cdREMOTE_SET_TV3_16000 = 0x00a8,

            /* 1/2 stop */
            cdREMOTE_SET_TV2_30sec = 0x0010,
            cdREMOTE_SET_TV2_20sec = 0x0014,
            cdREMOTE_SET_TV2_15sec = 0x0018,
            cdREMOTE_SET_TV2_10sec = 0x001c,
            cdREMOTE_SET_TV2_8sec = 0x0020,
            cdREMOTE_SET_TV2_6sec = 0x0024,
            cdREMOTE_SET_TV2_4sec = 0x0028,
            cdREMOTE_SET_TV2_3sec = 0x002c,
            cdREMOTE_SET_TV2_2sec = 0x0030,
            cdREMOTE_SET_TV2_1sec5 = 0x0034,
            cdREMOTE_SET_TV2_1sec = 0x0038,
            cdREMOTE_SET_TV2_0sec7 = 0x003C,
            cdREMOTE_SET_TV2_0sec5 = 0x0040,
            cdREMOTE_SET_TV2_0sec3 = 0x0044,
            cdREMOTE_SET_TV2_4 = 0x0048,
            cdREMOTE_SET_TV2_6 = 0x004c,
            cdREMOTE_SET_TV2_8 = 0x0050,
            cdREMOTE_SET_TV2_10 = 0x0054,
            cdREMOTE_SET_TV2_15 = 0x0058,
            cdREMOTE_SET_TV2_20 = 0x005c,
            cdREMOTE_SET_TV2_30 = 0x0060,
            cdREMOTE_SET_TV2_45 = 0x0064,
            cdREMOTE_SET_TV2_60 = 0x0068,
            cdREMOTE_SET_TV2_90 = 0x006c,
            cdREMOTE_SET_TV2_125 = 0x0070,
            cdREMOTE_SET_TV2_180 = 0x0074,
            cdREMOTE_SET_TV2_250 = 0x0078,
            cdREMOTE_SET_TV2_350 = 0x007c,
            cdREMOTE_SET_TV2_500 = 0x0080,
            cdREMOTE_SET_TV2_750 = 0x0084,
            cdREMOTE_SET_TV2_1000 = 0x0088,
            cdREMOTE_SET_TV2_1500 = 0x008c,
            cdREMOTE_SET_TV2_2000 = 0x0090,
            cdREMOTE_SET_TV2_3000 = 0x0094,
            cdREMOTE_SET_TV2_4000 = 0x0098,
            cdREMOTE_SET_TV2_6000 = 0x009c,
            cdREMOTE_SET_TV2_8000 = 0x00a0,
            cdREMOTE_SET_TV2_12000 = 0x00a4,
            cdREMOTE_SET_TV2_16000 = 0x00a8,

            /* 1 stop */
            cdREMOTE_SET_TV1_30sec = 0x0010,
            cdREMOTE_SET_TV1_15sec = 0x0018,
            cdREMOTE_SET_TV1_8sec = 0x0020,
            cdREMOTE_SET_TV1_4sec = 0x0028,
            cdREMOTE_SET_TV1_2sec = 0x0030,
            cdREMOTE_SET_TV1_1sec = 0x0038,
            cdREMOTE_SET_TV1_0sec5 = 0x0040,
            cdREMOTE_SET_TV1_4 = 0x0048,
            cdREMOTE_SET_TV1_8 = 0x0050,
            cdREMOTE_SET_TV1_15 = 0x0058,
            cdREMOTE_SET_TV1_30 = 0x0060,
            cdREMOTE_SET_TV1_60 = 0x0068,
            cdREMOTE_SET_TV1_125 = 0x0070,
            cdREMOTE_SET_TV1_250 = 0x0078,
            cdREMOTE_SET_TV1_500 = 0x0080,
            cdREMOTE_SET_TV1_1000 = 0x0088,
            cdREMOTE_SET_TV1_2000 = 0x0090,
            cdREMOTE_SET_TV1_4000 = 0x0098,
            cdREMOTE_SET_TV1_8000 = 0x00a0,
            cdREMOTE_SET_TV1_16000 = 0x00a8
        }

        public enum cdISOSpeed : ushort
        {
            cdREL_VAL_ISO_AUTO = 0x0000,	/* Auto		*/
            cdREL_VAL_ISO_6 = 0x0028,	/* ISO 6	*/
            cdREL_VAL_ISO_8 = 0x002b,	/* ISO 8	*/
            cdREL_VAL_ISO_10 = 0x002d,	/* ISO 10	*/
            cdREL_VAL_ISO_12 = 0x0030,	/* ISO 12	*/
            cdREL_VAL_ISO_16 = 0x0033,	/* ISO 16	*/
            cdREL_VAL_ISO_20 = 0x0035,	/* ISO 20	*/
            cdREL_VAL_ISO_25 = 0x0038,	/* ISO 25	*/
            cdREL_VAL_ISO_32 = 0x003b,	/* ISO 32	*/
            cdREL_VAL_ISO_40 = 0x003d,	/* ISO 40	*/
            cdREL_VAL_ISO_50 = 0x0040,	/* ISO 50	*/
            cdREL_VAL_ISO_64 = 0x0043,	/* ISO 64	*/
            cdREL_VAL_ISO_80 = 0x0045,	/* ISO 80	*/
            cdREL_VAL_ISO_100 = 0x0048,	/* ISO 100	*/
            cdREL_VAL_ISO_125 = 0x004b,	/* ISO 125	*/
            cdREL_VAL_ISO_160 = 0x004d,	/* ISO 160	*/
            cdREL_VAL_ISO_200 = 0x0050,	/* ISO 200	*/
            cdREL_VAL_ISO_250 = 0x0053,	/* ISO 250	*/
            cdREL_VAL_ISO_320 = 0x0055,	/* ISO 320	*/
            cdREL_VAL_ISO_400 = 0x0058,	/* ISO 400	*/
            cdREL_VAL_ISO_500 = 0x005b,	/* ISO 500	*/
            cdREL_VAL_ISO_640 = 0x005d,	/* ISO 640	*/
            cdREL_VAL_ISO_800 = 0x0060,	/* ISO 800	*/
            cdREL_VAL_ISO_1000 = 0x0063,	/* ISO 1000	*/
            cdREL_VAL_ISO_1250 = 0x0065,	/* ISO 1250	*/
            cdREL_VAL_ISO_1600 = 0x0068,	/* ISO 1600	*/
            cdREL_VAL_ISO_2000 = 0x006b,	/* ISO 2000	*/
            cdREL_VAL_ISO_2500 = 0x006d,	/* ISO 2500	*/
            cdREL_VAL_ISO_3200 = 0x0070,	/* ISO 3200	*/
            cdREL_VAL_ISO_4000 = 0x0073,	/* ISO 4000	*/
            cdREL_VAL_ISO_5000 = 0x0075,	/* ISO 5000	*/
            cdREL_VAL_ISO_6400 = 0x0078,	/* ISO 6400	*/
        }

		/****************************************************************************
		****************************************************************************
		*                 Canon Digital Camera SDK : Structures                    *
		****************************************************************************
		****************************************************************************/

		public struct cdVersionInfo 
		{
			public UInt16 Size;                 /* Size of this structure */
			public UInt16 MajorVersion;         /* Major version number. */
			public UInt16 MinorVersion;         /* Minor version number. */
			public UInt16 ReleaseVersion;       /* Release versionn number. 0 means unknown.*/
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
			public string chVersion;            /* String of version number.  e.x "1.0.0.1" */
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		public struct cdPortDescripSTI 
		{
			public UInt32 DataType;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)]
			public string DeviceInternalName;
		}

		public struct cdPortDescripWIA 
		{
			public UInt32 DataType;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=512)]
			public string szLaunchedDeviceName;		
		}
		
		public struct cdPortDescripRsrvd 
		{
			public UInt32 ModelID;						
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=76)]
			public string szLaunchedDeviceName;		
		}
	
		public struct cdPortDescriptionUnion 
		{
			public cdPortDescripSTI STI;
			public cdPortDescripWIA WIA;
			public cdPortDescripRsrvd rsrvd;
		}

		public struct cdSourceInfo 
		{
			public UInt32 SourceType;           /* Type of the device */ 
			public UInt32 rsrvd;                /* Reserved */
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)]
			public string Name;					/* Device model name (invalid if cdSRC_TYPE_HOST) */
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)]
			public string NameInOS;				/* Device model name in OS */
			public UInt32 PortType;				/* Port type */			
			public cdPortDescriptionUnion u;	/* Union of I/O port descriptor */
		}

		public struct cdVolumeInfo
		{
			public UInt16 HWtype;		    	/* Type of device that the volume is    */
			public bool isRemoveable;			/* Whether the volume is removeable     */
			public UInt64 TotalSpace;			/* Total volume space in Kilobytes      */
			public UInt64 FreeSpace;			/* Total free volume space in Kilobytes */
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)]
			public string Name;	          		/* Volume name                          */
		}

		public struct cdItemInfo 
		{
			public UInt32 Type;					/* Type of item                         */
			public UInt32 Attributes;			/* Attributes of item                   */
			public UInt32 TimeStamp;			/* Time                                 */
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=512)]
			public string Name;					/* Name of item                         */
			public UInt32 Size;					/* Data size of main image file in item */
			public UInt32 numThumbnail;			/* Number of thumbnail data	            */
			public UInt32 numPicture;			/* Number of picture data	            */
			public UInt32 numMovie;				/* Number of movie data	                */
			public UInt32 numSound;				/* Number of sound data	                */
		}

		public struct cdStgMedium 
		{
			public UInt32 Type;
			public cdStgMediumUnion u;			
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
			public struct cdStgMediumUnion 
		{
			[MarshalAs(UnmanagedType.LPStr, SizeConst=128)]
			public string lpszFileName;
		}

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
			public struct cdURational
		{
			public UInt32 Numerator;
			public UInt32 Denominator;
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct cdRelCamSettingStruct
		{
			public UInt32 SettingID;		/* Camera setting ID		*/
			public UInt32 Access;			/* Attribute				*/
			public UInt32 rsrvd;			/* Reserved					*/
		}
 
		/* cdReleaseImageInfo */
		[StructLayout(LayoutKind.Sequential)]
			public struct cdReleaseImageInfo			
		{
			public UInt32	SequenceID;			/* Capture ID					        */
			public UInt32 DataType;				/* Shows the type of image data         */
			public byte Format;				/* The format of the image data         */
			public UInt32 DataSize;				/* The size of the image data.          */
			public char[] FileName;				/* Not used, Length = 2                 */
		}
 
		/****************************************************************************
		****************************************************************************
		*            Canon Digital Camera SDK : Callback Functions                 *
		****************************************************************************
		****************************************************************************/

        /* Called when a camera event occurs. */
		public delegate UInt32 cdEventCallbackFunction(UInt32 EventID, IntPtr pData, UInt32 DataSize, UInt32 Context);

        /* Called when a camera event occurs relating to the image. */
        public delegate UInt32 cdReleaseEventCallbackFunction(UInt32 EventID, IntPtr pData, UInt32 DataSize, UInt32 Context);

        /* Called at regular intervals during processing to notify
          the client application of the progress. */
		public delegate UInt32 cdProgressCallbackFunction(UInt32 Progress, UInt32 Status, UInt32 Context);

        /* Callback function for retrieving Viewfinder data. */
        public delegate UInt32 cdViewFinderCallbackFunction(IntPtr pBuf, UInt32 Size, UInt32 Format, UInt32 Context);

	}
}
