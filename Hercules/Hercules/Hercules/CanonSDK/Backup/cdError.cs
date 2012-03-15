using System;

namespace RDC.CameraSDK 
{
	public enum cdError:uint
	{
		/*-----------------------------------------------------------------------
		CD-SDK Functin Success Code
		------------------------------------------------------------------------*/
		 cdOK									= 0x00000000,

		/*-----------------------------------------------------------------------
		'   CD-SDK Function True/False Codes
		'------------------------------------------------------------------------*/
		 cdTRUE									= 0x00000001,
		 cdFALSE								= 0x00000000,

		/*-----------------------------------------------------------------------
		CD-SDK Generic Error IDs
		------------------------------------------------------------------------*/
		
		/* Miscellaneous errors */
		 cdUNIMPLEMENTED               			= 0x00000001, 
		 cdINTERNAL_ERROR						= 0x00000002,
		 cdMEM_ALLOC_FAILED					  	= 0x00000003,
		 cdMEM_FREE_FAILED						= 0x00000004,
		 cdOPERATION_CANCELLED					= 0x00000005,
		 cdINCOMPATIBLE_VERSION					= 0x00000006,
		 cdNOT_SUPPORTED						= 0x00000007,
		 cdUNEXPECTED_EXCEPTION					= 0x00000008,
		 cdPROTECTION_VIOLATION					= 0x00000009,  
		 cdMISSING_SUBCOMPONENT					= 0x0000000A,
		 cdSELECTION_UNAVAILABLE				= 0x0000000B,

		/* Function Parameter errors */
		 cdINVALID_PARAMETER 					= 0x00000060,
		 cdINVALID_HANDLE    					= 0x00000061,
		 cdINVALID_POINTER   					= 0x00000062,

		 cdENUM_NA								= 0x000000F0
	}

}

