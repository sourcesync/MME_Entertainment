using System;
using System.Runtime.InteropServices;

namespace RDC.CameraSDK
{
    public class prType
    {
         /****************************************************************************
         ****************************************************************************
         *                 PowerShot RemoteCapture SDK : Enumerations               *
         ****************************************************************************
         ****************************************************************************/

        /* Definition of Opration Code */
        public enum prOperationCode : ushort
        {
	        prPTP_INITIATE_RELEASE_CONTROL				=0x9008,
	        prPTP_TERMINATE_RELEASE_CONTROL				=0x9009,
	        prPTP_RC_INITIATE_VIEW_FINDER				=0x900B,
	        prPTP_RC_TERMINATE_VIEW_FINDER				=0x900C,
	        prPTP_RC_RELEASE_DO_AE_AF_AWB				=0x900D,
	        prPTP_RC_FOCUS_LOCK							=0x9014,
	        prPTP_RC_FOCUS_UNLOCK						=0x9015,
	        prPTP_RC_CAPTURE							=0x901A,
	        prPTP_RC_GET_CHANGED_RELEASE_PARAMS_LIST	=0x9020
        }

        /* Progress Message */
        public enum prProgressMsg : int
        {
	        prMSG_DATA_HEADER						=0x0001,
	        prMSG_DATA								=0x0002,
	        prMSG_TERMINATION						=0x0004
        }
        
        /* Event Codes */
        public enum prptpEventCode : ushort
        {
	        prPTP_DEVICE_PROP_CHANGED				=0x4006,		/* Deveice property has been changed. */
	        prPTP_CAPTURE_COMPLETE					=0x400D,		/* Capture has finished. */
	        prPTP_SHUTDOWN_CF_GATE_WAS_OPENED		=0xC001,		/* The Device has shut down due to the opening of the SD card cover.*/
	        prPTP_RESET_HW_ERROR					=0xC005,		/* The device has generated a hardware error. */
	        prPTP_ABORT_PC_EVF						=0xC006,		/* The Viewfinder mode has been cancelled. */
	        prPTP_ENABLE_PC_EVF						=0xC007,		/* The Viewfinder mode has been enablede. */
	        prPTP_FULL_VIEW_RELEASED				=0xC008,		/* Transfer timing of main image data */
	        prPTP_THUMBNAIL_RELEASED				=0xC009,		/* Transfer timing of thumbnail image data */
	        prPTP_CHANGE_BATTERY_STATUS				=0xC00A,		/* The power condition of the camera has changed. */
	        prPTP_PUSHED_RELEASE_SW					=0xC00B,		/* User has pressed the release swtich on camera. */
	        prPTP_RC_PROP_CHANGED					=0xC00C,		/* A group of properties relating to release control have been changed. */
	        prPTP_RC_ROTATION_ANGLE_CHANGED			=0xC00D,		/* The angle of rotation of the camera has been changed. */
	        prPTP_RC_CHANGED_BY_CAM_UI				=0xC00E,		/* An operation control on the camera has been operated.*/
	        prCAL_SHUTDOWN							=0xD001		/* Shutdown */
        }

        /* Device Property Codes */
        public enum prptpDevicePropCode : ushort
        {
	         prPTP_DEV_PROP_BUZZER							=0xD001,	/* Set on/off the device buzzer */
	         prPTP_DEV_PROP_BATTERY_KIND					=0xD002,	/* Type of the battery */
	         prPTP_DEV_PROP_BATTERY_STATUS					=0xD003,	/* Buttery Status */
	         prPTP_DEV_PROP_COMP_QUALITY					=0xD006,	/* Image quality */
	         prPTP_DEV_PROP_FULLVIEW_FILE_FORMAT			=0xD007,	/* Image type */
	         prPTP_DEV_PROP_IMAGE_SIZE						=0xD008,	/* Image size */
	         prPTP_DEV_PROP_SELFTIMER						=0xD009,	/* Self-timer*/
	         prPTP_DEV_PROP_STROBE_SETTING					=0xD00A,	/* Strobe setting */
	         prPTP_DEV_PROP_BEEP							=0xD00B,	/* Buzzer setting */
	         prPTP_DEV_PROP_EXPOSURE_MODE					=0xD00C,	/* Exposure mode setting */
	         prPTP_DEV_PROP_IMAGE_MODE						=0xD00D,	/* Image mode setting */
	         prPTP_DEV_PROP_DRIVE_MODE						=0xD00E,	/* Drive mode */
	         prPTP_DEV_PROP_EZOOM							=0xD00F,	/* Electonic zoom setting */
	         prPTP_DEV_PROP_ML_WEI_MODE						=0xD010,	/* Metering method */
	         prPTP_DEV_PROP_AF_DISTANCE						=0xD011,	/* Search range in the AF mode */
	         prPTP_DEV_PROP_FOCUS_POINT_SETTING				=0xD012,	/* Selection mode for focusing point */
	         prPTP_DEV_PROP_WB_SETTING						=0xD013,	/* White balance setting */
	         prPTP_DEV_PROP_SLOW_SHUTTER_SETTING			=0xD014,	/* Slow Shutter setting */
	         prPTP_DEV_PROP_AF_MODE							=0xD015,	/* Auto focus mode setting */
	         prPTP_DEV_PROP_IMAGE_STABILIZATION				=0xD016,	/* Image stabilization processing */
	         prPTP_DEV_PROP_CONTRAST						=0xD017,	/* Contrast */
	         prPTP_DEV_PROP_COLOR_GAIN						=0xD018,	/* Color Compensation */
	         prPTP_DEV_PROP_SHARPNESS						=0xD019,	/* Sharpness */
	         prPTP_DEV_PROP_SENSITIVITY						=0xD01A,	/* Sensitivity */
	         prPTP_DEV_PROP_PARAMETER_SET					=0xD01B,	/* Development parameter setting */
	         prPTP_DEV_PROP_ISO								=0xD01C,	/* ISO value */
	         prPTP_DEV_PROP_AV								=0xD01D,	/* Av value */
	         prPTP_DEV_PROP_TV								=0xD01E,	/* Tv value */
	         prPTP_DEV_PROP_EXPOSURE_COMP					=0xD01F,	/* Exposure compensation value */
	         prPTP_DEV_PROP_FLASH_COMP						=0xD020,	/* Flash exposure compensation value */
	         prPTP_DEV_PROP_AEB_EXPOSURE_COMP				=0xD021,	/* AEB exposure compensation value */
	         prPTP_DEV_PROP_AV_OPEN							=0xD023,	/* Open aperture value */
	         prPTP_DEV_PROP_AV_MAX							=0xD024,	/* maximum aperture value */
	         prPTP_DEV_PROP_FOCAL_LENGTH					=0xD025,	/* Value corresponding to the current focal distance multiplied by FocalLengthDenominator */
	         prPTP_DEV_PROP_FOCAL_LENGTH_TELE				=0xD026,	/* Value corresponding to the telescopic focal distance multiplied by FocalLengthDenominator */
	         prPTP_DEV_PROP_FOCAL_LENGTH_WIDE				=0xD027,	/* Value corresponding to the wide-angle focus distance multiplied by FocalLengthDenominator */
	         prPTP_DEV_PROP_FOCAL_LENGTH_DENOMI				=0xD028,	/* Focus information multiplier value */
	         prPTP_DEV_PROP_CAPTURE_TRANSFER_MODE			=0xD029,	/* Image transfer mode to be applied at caputre */
	         prPTP_DEV_PROP_ZOOM_POS						=0xD02A,	/* Current zoom position*/

	         prPTP_DEV_PROP_SUPPORTED_SIZE					=0xD02C,	/* Support size */
	         prPTP_DEV_PROP_SUPPORTED_THUMB_SIZE			=0xD02D,	/* Thumbnail size supported by the device */
	         prPTP_DEV_PROP_FIRMWARE_VERSION				=0xD031,	/* Version of the camera device firmware */
	         prPTP_DEV_PROP_CAMERA_MODEL_NAME				=0xD032,	/* Camera model */
	         prPTP_DEV_PROP_OWNER_NAME						=0xD033,	/* Owner name */
	         prPTP_DEV_PROP_CAMERA_TIME						=0xD034,	/* Current time information in the device */
	         prPTP_DEV_PROP_CAMERA_OUTPUT					=0xD036,	/* Destination of image signal output in the Viewfinder mode */
	         prPTP_DEV_PROP_DISP_AV							=0xD037,	/* How to display the Av value */
	         prPTP_DEV_PROP_AV_OPEN_APEX					=0xD038,	/* Open aperture value */
	         prPTP_DEV_PROP_EZOOM_SIZE						=0xD039,	/* Horizontal size of image to be cut out from CCD image using electronic zoom */
	         prPTP_DEV_PROP_ML_SPOT_POS						=0xD03A,	/* Spot metering positon */
	         prPTP_DEV_PROP_DISP_AV_MAX						=0xD03B,	/* How to display the maximin Av value */
	         prPTP_DEV_PROP_AV_MAX_APEX						=0xD03C,	/* minimum aperture value */
	         prPTP_DEV_PROP_EZOOM_START_POS					=0xD03D,	/* Zoom position at which the electornic zoom range starts */
	         prPTP_DEV_PROP_FOCAL_LENGTH_OF_TELE			=0xD03E,	/* Focal distance at the optical telescopic end */
	         prPTP_DEV_PROP_EZOOM_SIZE_OF_TELE				=0xD03F,	/* Horizontal size of image to be cut out from CCD image at the telescopic end of the electronic zoom range */
	         prPTP_DEV_PROP_PHOTO_EFFECT					=0xD040,	/* Photo effect */
	         prPTP_DEV_PROP_AF_LIGHT						=0xD041,	/* ON/OFF of AF assist light */
	         prPTP_DEV_PROP_FLASH_QUANTITY					=0xD042,	/* Number of flash levels that can be set in the manual mode */
	         prPTP_DEV_PROP_ROTATION_ANGLE					=0xD043,	/* Angle of rotation detected by the gravity sensor */
	         prPTP_DEV_PROP_ROTATION_SENCE					=0xD044,	/* Whether the gravity sensor is enable or disable */
	         prPTP_DEV_PROP_IMEGE_FILE_SIZE					=0xD048,	/* Image file size supported be the camera */
	         prPTP_DEV_PROP_CAMERA_MODEL_ID					=0xD049	/* Camera model ID */
        }

        public enum prptpDevicePropDataType : ushort
        {
            NotDefined = 0x0000,
            prInt8 = 0x0001,
            prUInt8 = 0x0002,
            prInt16 = 0x0003,
            prUInt16 = 0x0004,
            prInt32 = 0x0005,
            prUInt32 = 0x0006,
            prInt64 = 0x0007,
            prUInt64 = 0x0008
        }

        public enum prptpDevicePropFormFlag : byte
        {
            None = 0x00,
            Range = 0x01,
            Enumeration = 0x02
        }

        /* Image Transfer Mode */
        [Flags]
        public enum prptpTransferMode : ushort
        {
            NotDefined = 0x0000,
            ThumbnailToPC = 0x0001,
            FullImageToPC = 0x0002,
            ThumbnailToCamera = 0x0004,
            FullImageToCamera = 0x0008
        }

        /* File Format */
        public enum prptpFileFormat : byte
        {
            NotDefined = 0x00,
            JPEG = 0x01,
            CRW = 0x02
        }

        /* Image Size */
        public enum prptpImageSize : byte
        {
            Large = 0x00,
            Medium1 = 0x01,
            Small = 0x02,
            Medium2 = 0x03,
            Medium3 = 0x07
        }

        /* Image Quality */
        public enum prptpImageQuality : byte
        {
            NotDefined = 0x00,
            Economy = 0x01,
            Normal = 0x02,
            Fine = 0x03,
            Lossless = 0x04,
            SuperFine = 0x05,
            Unknown = 0xFF
        }
        
        /* AE,AF,AWB Reset flag */
        [Flags]public enum prptpAeAfAwbResetFlag : uint
        {
	        prptpAEAFAWB_RESET_AE		=0x00000001,		/* AE Reset */
	        prptpAEAFAWB_RESET_AF		=0x00000002,		/* AF Reset */
	        prptpAEAFAWB_RESET_AWB		=0x00000004		    /* AWB Reset */
        }

        /* AF Distance */

        public enum prptpAFDistance : byte
        {
            Manual =    0x0, // Manual
            Auto   =    0x01, // Auto
            Unknown =   0x02, // Unknown
            CloseUp =   0x03, // Zone Focus (Close-up)
            VeryClose = 0x04, // Zone Focus (Very Close )
            Close =     0x05, // Zone Focus (Close)
            Medium =    0x06, // Zone Focus (Medium)
            Far =       0x07, // Zone Focus (Far)
            Res1 =      0x08, // Zone Focus (Reserved 1)
            Res2 =      0x09, // Zone Focus (Reserved 2)
            Res3 =     0x0A, // Zone Focus (Reserved 3)
            Res4 =      0x0B, // Zone Focus (Reserved 4)
            Invalid =   0xFF // Invalid or the setting is not changed.
        }

        /* Metering */

        public enum prptpMetering : byte
        {
            Center =            0x00, // Center-weighted Metering
            Spot =              0x01, // Spot Metering
            Average =           0x02, // Average Metering
            Evaluative =        0x03, // Evaluative Metering
            Partial =           0x04, // Partial Metering
            CenterWeighted =    0x05, // Center-weighted Average Metering
            AFInterlockedSpot = 0x06, // Spot Metering Interlocked with AF Frame
            MultiSpot =         0x07, // Multi-Spot Metering
            Invalid =           0xFF // Invalid or the setting is not changed.
        }

        /* Exposure Mode */
        public enum prptpShootingMode : byte
        {
            Auto = 0x00,
			Program = 0x01,
			Tv = 0x02,
			Av = 0x03,
			Manual = 0x04,
			A_DEP = 0x05,
			M_DEP = 0x06,
			Bulb = 0x07,
			Manual2 = 0x80,
			MyColor = 0x81,
			Portrait = 0x82,
			Landscape = 0x83,
			Nightscene = 0x84,
			Forest = 0x85,
			Snow = 0x86,
			Beach = 0x87,
			Fireworks = 0x88,
			Party = 0x89,
			Nightsnap = 0x8a,
			Stitch = 0x8b,
			Movie = 0x8c,
			Custom = 0x8d,
			Interval = 0x8e,
			DigitalMacro = 0x8f,
			LongShutter = 0x90,
			Underwater = 0x91,
			KidsAndPets = 0x92,
			FastShutter = 0x93,
			SlowShutter = 0x94,
			Custom1 = 0x95,
			Custom2 = 0x96,
			Neutral = 0x97,
			Gray = 0x98,
			Sepia = 0x99,
			Vivid = 0x9A,
			Sports = 0x9B,
			Macro = 0x9C,
			SuperMacro = 0x9D,
			PanFocus = 0x9E,
			BW = 0x9F,
			FlashInhibit = 0xA0
        }

        /* Exposure Compensation */
        public enum prptpExposureCompensation : byte
        {
            prCOMP_300_PLUS = 0x00,
            prCOMP_266_PLUS = 0x03,
            prCOMP_250_PLUS = 0x04,
            prCOMP_233_PLUS = 0x05,
            prCOMP_200_PLUS = 0x08,
            prCOMP_166_PLUS = 0x0b,
            prCOMP_150_PLUS = 0x0c,
            prCOMP_133_PLUS = 0x0d,
            prCOMP_100_PLUS = 0x10,
            prCOMP_066_PLUS = 0x13,
            prCOMP_050_PLUS = 0x14,
            prCOMP_033_PLUS = 0x15,
            prCOMP_000_PLUS = 0x18,
            prCOMP_033_MINUS = 0x1b,
            prCOMP_050_MINUS = 0x1c,
            prCOMP_066_MINUS = 0x1d,
            prCOMP_100_MINUS = 0x20,
            prCOMP_133_MINUS = 0x23,
            prCOMP_150_MINUS = 0x24,
            prCOMP_166_MINUS = 0x25,
            prCOMP_200_MINUS = 0x28,
            prCOMP_233_MINUS = 0x2b,
            prCOMP_250_MINUS = 0x2c,
            prCOMP_266_MINUS = 0x2d,
            prCOMP_300_MINUS = 0x30,
        }

        /* Strobe setting */
        public enum prptpStrobeSetting : byte
        {
            Off = 0x00,
			Auto = 0x01,
			On = 0x02,
			RedEye = 0x03,
			SlowSync = 0x04,
			AutoRedEye = 0x05,
			OnRedEye = 0x06
        }

        /* ISO Speed */
        public enum prptpISOSpeed : uint
        {
            prREL_VAL_ISO_AUTO = 0x0000,	/* Auto		*/
            prREL_VAL_ISO_6 = 0x0028,	    /* ISO 6	*/
            prREL_VAL_ISO_8 = 0x002b,	    /* ISO 8	*/
            prREL_VAL_ISO_10 = 0x002d,	/* ISO 10	*/
            prREL_VAL_ISO_12 = 0x0030,	/* ISO 12	*/
            prREL_VAL_ISO_16 = 0x0033,	/* ISO 16	*/
            prREL_VAL_ISO_20 = 0x0035,	/* ISO 20	*/
            prREL_VAL_ISO_25 = 0x0038,	/* ISO 25	*/
            prREL_VAL_ISO_32 = 0x003b,	/* ISO 32	*/
            prREL_VAL_ISO_40 = 0x003d,	/* ISO 40	*/
            prREL_VAL_ISO_50 = 0x0040,	/* ISO 50	*/
            prREL_VAL_ISO_64 = 0x0043,	/* ISO 64	*/
            prREL_VAL_ISO_80 = 0x0045,	/* ISO 80	*/
            prREL_VAL_ISO_100 = 0x0048,	/* ISO 100	*/
            prREL_VAL_ISO_125 = 0x004b,	/* ISO 125	*/
            prREL_VAL_ISO_160 = 0x004d,	/* ISO 160	*/
            prREL_VAL_ISO_200 = 0x0050,	/* ISO 200	*/
            prREL_VAL_ISO_250 = 0x0053,	/* ISO 250	*/
            prREL_VAL_ISO_320 = 0x0055,	/* ISO 320	*/
            prREL_VAL_ISO_400 = 0x0058,	/* ISO 400	*/
            prREL_VAL_ISO_500 = 0x005b,	/* ISO 500	*/
            prREL_VAL_ISO_640 = 0x005d,	/* ISO 640	*/
            prREL_VAL_ISO_800 = 0x0060,	/* ISO 800	*/
            prREL_VAL_ISO_1000 = 0x0063,	/* ISO 1000	*/
            prREL_VAL_ISO_1250 = 0x0065,	/* ISO 1250	*/
            prREL_VAL_ISO_1600 = 0x0068,	/* ISO 1600	*/
            prREL_VAL_ISO_2000 = 0x006b,	/* ISO 2000	*/
            prREL_VAL_ISO_2500 = 0x006d,	/* ISO 2500	*/
            prREL_VAL_ISO_3200 = 0x0070,	/* ISO 3200	*/
            prREL_VAL_ISO_4000 = 0x0073,	/* ISO 4000	*/
            prREL_VAL_ISO_5000 = 0x0075,	/* ISO 5000	*/
            prREL_VAL_ISO_6400 = 0x0078,	/* ISO 6400	*/
        }

        /* Photo effect */
        public enum prptpPhotoEffect : uint
        {
            Off = 0x0000,
            Vivid = 0x0001,
            Neutral = 0x0002,
            Soft = 0x0003,
            Sepia = 0x0004,
            Monochrome = 0x0005
        }

        /* Port type */
        public enum prPorttype : ushort	
        {
	        prPORTTYPE_WIA			=0x0001,		/*	WIA	*/
	        prPORTTYPE_STI			=0x0002		    /*	STI	*/
        }

        /* AV */
        public enum prptpAperture : ushort
        {
            prREMOTE_SET_AV_NA = 0xFFFF,

            /* 1/3 stop */
            prREMOTE_SET_AV3_10 = 0x08,
            prREMOTE_SET_AV3_11 = 0x0b,
            prREMOTE_SET_AV3_12 = 0x0d,
            prREMOTE_SET_AV3_14 = 0x10,
            prREMOTE_SET_AV3_16 = 0x13,
            prREMOTE_SET_AV3_18 = 0x15,
            prREMOTE_SET_AV3_20 = 0x18,
            prREMOTE_SET_AV3_22 = 0x1b,
            prREMOTE_SET_AV3_25 = 0x1d,
            prREMOTE_SET_AV3_28 = 0x20,
            prREMOTE_SET_AV3_32 = 0x23,
            prREMOTE_SET_AV3_35 = 0x25,
            prREMOTE_SET_AV3_40 = 0x28,
            prREMOTE_SET_AV3_45 = 0x2b,
            prREMOTE_SET_AV3_50 = 0x2d,
            prREMOTE_SET_AV3_56 = 0x30,
            prREMOTE_SET_AV3_63 = 0x33,
            prREMOTE_SET_AV3_71 = 0x35,
            prREMOTE_SET_AV3_80 = 0x38,
            prREMOTE_SET_AV3_90 = 0x3b,
            prREMOTE_SET_AV3_100 = 0x3d,
            prREMOTE_SET_AV3_110 = 0x40,
            prREMOTE_SET_AV3_130 = 0x43,
            prREMOTE_SET_AV3_140 = 0x45,
            prREMOTE_SET_AV3_160 = 0x48,
            prREMOTE_SET_AV3_180 = 0x4b,
            prREMOTE_SET_AV3_200 = 0x4d,
            prREMOTE_SET_AV3_220 = 0x50,
            prREMOTE_SET_AV3_250 = 0x53,
            prREMOTE_SET_AV3_290 = 0x55,
            prREMOTE_SET_AV3_320 = 0x58,
            prREMOTE_SET_AV3_360 = 0x5b,
            prREMOTE_SET_AV3_400 = 0x5d,
            prREMOTE_SET_AV3_450 = 0x60,
            prREMOTE_SET_AV3_510 = 0x63,
            prREMOTE_SET_AV3_570 = 0x65,
            prREMOTE_SET_AV3_640 = 0x68,
            prREMOTE_SET_AV3_720 = 0x6b,
            prREMOTE_SET_AV3_810 = 0x6d,
            prREMOTE_SET_AV3_910 = 0x70,

            /* 1/2 stop */
            prREMOTE_SET_AV2_10 = 0x08,
            prREMOTE_SET_AV2_12 = 0x0c,
            prREMOTE_SET_AV2_14 = 0x10,
            prREMOTE_SET_AV2_18 = 0x14,
            prREMOTE_SET_AV2_20 = 0x18,
            prREMOTE_SET_AV2_25 = 0x1c,
            prREMOTE_SET_AV2_28 = 0x20,
            prREMOTE_SET_AV2_35 = 0x24,
            prREMOTE_SET_AV2_40 = 0x28,
            prREMOTE_SET_AV2_45 = 0x2c,
            prREMOTE_SET_AV2_56 = 0x30,
            prREMOTE_SET_AV2_67 = 0x34,
            prREMOTE_SET_AV2_80 = 0x38,
            prREMOTE_SET_AV2_95 = 0x3c,
            prREMOTE_SET_AV2_110 = 0x40,
            prREMOTE_SET_AV2_130 = 0x44,
            prREMOTE_SET_AV2_160 = 0x48,
            prREMOTE_SET_AV2_190 = 0x4c,
            prREMOTE_SET_AV2_220 = 0x50,
            prREMOTE_SET_AV2_270 = 0x54,
            prREMOTE_SET_AV2_320 = 0x58,
            prREMOTE_SET_AV2_380 = 0x5c,
            prREMOTE_SET_AV2_450 = 0x60,
            prREMOTE_SET_AV2_540 = 0x64,
            prREMOTE_SET_AV2_640 = 0x68,
            prREMOTE_SET_AV2_760 = 0x6c,
            prREMOTE_SET_AV2_910 = 0x70,

            /* 1 stop */
            prREMOTE_SET_AV1_10 = 0x08,
            prREMOTE_SET_AV1_14 = 0x10,
            prREMOTE_SET_AV1_20 = 0x18,
            prREMOTE_SET_AV1_28 = 0x20,
            prREMOTE_SET_AV1_40 = 0x28,
            prREMOTE_SET_AV1_56 = 0x30,
            prREMOTE_SET_AV1_80 = 0x38,
            prREMOTE_SET_AV1_110 = 0x40,
            prREMOTE_SET_AV1_160 = 0x48,
            prREMOTE_SET_AV1_220 = 0x50,
            prREMOTE_SET_AV1_320 = 0x58,
            prREMOTE_SET_AV1_450 = 0x60,
            prREMOTE_SET_AV1_640 = 0x68,
            prREMOTE_SET_AV1_910 = 0x70

        }

        /* TV */
        public enum prptpShutterSpeed : ushort
        {
            prREMOTE_SET_TV_NA = 0xFFFF,
            prREMOTE_SET_TV_BULB = 0x04,

            /* 1/3 stop */
            prREMOTE_SET_TV3_30sec = 0x10,
            prREMOTE_SET_TV3_25sec = 0x13,
            prREMOTE_SET_TV3_20sec = 0x15,
            prREMOTE_SET_TV3_15sec = 0x18,
            prREMOTE_SET_TV3_13sec = 0x1b,
            prREMOTE_SET_TV3_10sec = 0x1d,
            prREMOTE_SET_TV3_8sec = 0x20,
            prREMOTE_SET_TV3_6sec = 0x23,
            prREMOTE_SET_TV3_5sec = 0x25,
            prREMOTE_SET_TV3_4sec = 0x28,
            prREMOTE_SET_TV3_3sec2 = 0x2b,
            prREMOTE_SET_TV3_2sec5 = 0x2d,
            prREMOTE_SET_TV3_2sec = 0x30,
            prREMOTE_SET_TV3_1sec6 = 0x33,
            prREMOTE_SET_TV3_1sec3 = 0x35,
            prREMOTE_SET_TV3_1sec = 0x38,
            prREMOTE_SET_TV3_0sec8 = 0x3b,
            prREMOTE_SET_TV3_0sec6 = 0x3d,
            prREMOTE_SET_TV3_0sec5 = 0x40,
            prREMOTE_SET_TV3_0sec4 = 0x43,
            prREMOTE_SET_TV3_0sec3 = 0x45,
            prREMOTE_SET_TV3_4 = 0x48,
            prREMOTE_SET_TV3_5 = 0x4b,
            prREMOTE_SET_TV3_6 = 0x4d,
            prREMOTE_SET_TV3_8 = 0x50,
            prREMOTE_SET_TV3_10 = 0x53,
            prREMOTE_SET_TV3_13 = 0x55,
            prREMOTE_SET_TV3_15 = 0x58,
            prREMOTE_SET_TV3_20 = 0x5b,
            prREMOTE_SET_TV3_25 = 0x5d,
            prREMOTE_SET_TV3_30 = 0x60,
            prREMOTE_SET_TV3_40 = 0x63,
            prREMOTE_SET_TV3_50 = 0x65,
            prREMOTE_SET_TV3_60 = 0x68,
            prREMOTE_SET_TV3_80 = 0x6b,
            prREMOTE_SET_TV3_100 = 0x6d,
            prREMOTE_SET_TV3_125 = 0x70,
            prREMOTE_SET_TV3_160 = 0x73,
            prREMOTE_SET_TV3_200 = 0x75,
            prREMOTE_SET_TV3_250 = 0x78,
            prREMOTE_SET_TV3_320 = 0x7b,
            prREMOTE_SET_TV3_400 = 0x7d,
            prREMOTE_SET_TV3_500 = 0x80,
            prREMOTE_SET_TV3_640 = 0x83,
            prREMOTE_SET_TV3_800 = 0x85,
            prREMOTE_SET_TV3_1000 = 0x88,
            prREMOTE_SET_TV3_1250 = 0x8b,
            prREMOTE_SET_TV3_1600 = 0x8d,
            prREMOTE_SET_TV3_2000 = 0x90,
            prREMOTE_SET_TV3_2500 = 0x93,
            prREMOTE_SET_TV3_3200 = 0x95,
            prREMOTE_SET_TV3_4000 = 0x98,

            /* 1/2 stop */
            prREMOTE_SET_TV2_30sec = 0x10,
            prREMOTE_SET_TV2_20sec = 0x14,
            prREMOTE_SET_TV2_15sec = 0x18,
            prREMOTE_SET_TV2_10sec = 0x1c,
            prREMOTE_SET_TV2_8sec = 0x20,
            prREMOTE_SET_TV2_6sec = 0x24,
            prREMOTE_SET_TV2_4sec = 0x28,
            prREMOTE_SET_TV2_3sec = 0x2c,
            prREMOTE_SET_TV2_2sec = 0x30,
            prREMOTE_SET_TV2_1sec5 = 0x34,
            prREMOTE_SET_TV2_1sec = 0x38,
            prREMOTE_SET_TV2_0sec7 = 0x3C,
            prREMOTE_SET_TV2_0sec5 = 0x40,
            prREMOTE_SET_TV2_0sec3 = 0x44,
            prREMOTE_SET_TV2_4 = 0x48,
            prREMOTE_SET_TV2_6 = 0x4c,
            prREMOTE_SET_TV2_8 = 0x50,
            prREMOTE_SET_TV2_10 = 0x54,
            prREMOTE_SET_TV2_15 = 0x58,
            prREMOTE_SET_TV2_20 = 0x5c,
            prREMOTE_SET_TV2_30 = 0x60,
            prREMOTE_SET_TV2_45 = 0x64,
            prREMOTE_SET_TV2_60 = 0x68,
            prREMOTE_SET_TV2_90 = 0x6c,
            prREMOTE_SET_TV2_125 = 0x70,
            prREMOTE_SET_TV2_180 = 0x74,
            prREMOTE_SET_TV2_250 = 0x78,
            prREMOTE_SET_TV2_350 = 0x7c,
            prREMOTE_SET_TV2_500 = 0x80,
            prREMOTE_SET_TV2_750 = 0x84,
            prREMOTE_SET_TV2_1000 = 0x88,
            prREMOTE_SET_TV2_1500 = 0x8c,
            prREMOTE_SET_TV2_2000 = 0x90,
            prREMOTE_SET_TV2_3000 = 0x94,
            prREMOTE_SET_TV2_4000 = 0x98,

            /* 1 stop */
            prREMOTE_SET_TV1_30sec = 0x10,
            prREMOTE_SET_TV1_15sec = 0x18,
            prREMOTE_SET_TV1_8sec = 0x20,
            prREMOTE_SET_TV1_4sec = 0x28,
            prREMOTE_SET_TV1_2sec = 0x30,
            prREMOTE_SET_TV1_1sec = 0x38,
            prREMOTE_SET_TV1_0sec5 = 0x40,
            prREMOTE_SET_TV1_4 = 0x48,
            prREMOTE_SET_TV1_8 = 0x50,
            prREMOTE_SET_TV1_15 = 0x58,
            prREMOTE_SET_TV1_30 = 0x60,
            prREMOTE_SET_TV1_60 = 0x68,
            prREMOTE_SET_TV1_125 = 0x70,
            prREMOTE_SET_TV1_250 = 0x78,
            prREMOTE_SET_TV1_500 = 0x80,
            prREMOTE_SET_TV1_1000 = 0x88,
            prREMOTE_SET_TV1_2000 = 0x90,
            prREMOTE_SET_TV1_4000 = 0x98,

        }


        ///* Object type */
        //public enum prptpObjectFormatCode : ushort
        //{
        //    prPTP_EXIF_JPEG		= 0x3801,	/* EXIF JPEG */
        //    prPTP_CRW			= 0xB101	/* RAW */
        //}

        /****************************************************************************
        ****************************************************************************
        *                 PowerShot RemoteCapture SDK : Structures                    *
        ****************************************************************************
        ****************************************************************************/

        public struct prVerInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string ModuleName;				/* Module name (512 characters)	*/
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Version;					/* Version (32 characters) */
	       
        };

        public struct prDllsVerInfo{
	        public UInt32          Entry;						/* Number of modules included in this structure */
	        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public prVerInfo[]    VerInfo; 			            /* Array of file version number information of PS-ReC SDK modules */
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
        public unsafe struct prDeviceInfoTable{
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
	        public string           DeviceInternalName;         /* Internal device name (512 characters) */
	        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string           ModelName;                  /* Camera model name (32 characters) */
            public UInt16           Generation;			    	/* Camera generation number */
            public UInt32           Reserved1;  				/* Reserved */
            public UInt32           ModelID;					/* Camera model ID */
            public UInt16           Reserved2;				    /* Reserved */
            public UInt16           PortType;					/* Port type 0x01ÅFWIA / 0x02ÅFSTI */
            public UInt32           Reserved3;				    /* Reserved */
        };

        public struct prDeviceList{
            public UInt32 NumList;					            /* Number of camera device information included in this structure */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public prDeviceInfoTable[] DeviceInfo;	            /* Camera device information */
        };

        public struct prProgress{
            public prProgressMsg lMessage;	    	/* Message */
            public Int32 lStatus;					/* Status */
            public UInt32 lPercentComplete;			/* The uint of this parameter is percent */
            public UInt32 lOffset;					/* Offset */
            public UInt32 lLength;					/* Size */
            public UInt32 lReserved;				/* Reserved */
            public UInt32 lResLength;				/* Reserved */
            public IntPtr pbData;					/* Pointer to the buffer in which the transferred data is stored. */
        };

        /****************************************************************************
        ****************************************************************************
        *            PowerShot RemoteCapture SDK : Callback Functions                 *
        ****************************************************************************
        ****************************************************************************/

        /* Called when a camera event occurs. */
        public delegate UInt32 prSetEventCB(UInt32 CameraHandle, UInt32 Context, IntPtr pEventData);

        /* Called at regular intervals during processing to notify
           the client application of the progress . */
        public delegate UInt32 prGetFileDataCB(UInt32 CameraHandle, UInt32 ObjectHandle, 
                                               UInt32 Context, ref prProgress pProgress);
        
        /* Callback function for retrieving Viewfinder data. */
        public delegate UInt32 prViewFinderCB(UInt32 CameraHandle, UInt32 Context, UInt32 Size, IntPtr pVFData);

    }
}
