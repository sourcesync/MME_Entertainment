using System;

namespace RDC.CameraSDK
{
    public enum prError:uint
    {
       
        /****************************************************************************
         ****************************************************************************
         *            PowerShot RemoteCapture SDK : ResponseCode Summary            *
         ****************************************************************************
         ****************************************************************************/

        /*-----------------------------------------------------------------------
           PRSDK Error Code Masks
        ------------------------------------------------------------------------*/
         prERROR_COMPONENTID_MASK					        =0x00F00000,
         prERROR_ERRORID_MASK						        =0x0000FFFF,


        /*-----------------------------------------------------------------------
           PRSDK Base Component IDs
        ------------------------------------------------------------------------*/
         prERROR_PTP_COMPONENTID				  		    =0x00100000,		/* PTP operation errors */
         prERROR_PRSDK_COMPONENTID				  	        =0x00200000,		/* PRSDK Internal Error */
         prERROR_WIA_STI_COMPONENTID				 	    =0x00300000,		/* Errors generated by the Windows WIA/STI */
         prERROR_WINDOWS_COMPONENTID	  				    =0x00400000,		/* Errors generated by the GetLastError() function in WIN32 API*/
         prERROR_COMIF_COMPONENTID	  				        =0x00500000,		/* Windows COM I/F errors */

        /*-----------------------------------------------------------------------
           PRSDK Functin Success Code
        ------------------------------------------------------------------------*/
         prOK										        =0x00000000,


        /*-----------------------------------------------------------------------
           PTP Operation Error IDs
        ------------------------------------------------------------------------*/
         prRESPONSE_Undefined				  				=0x00002000,
         prRESPONSE_GeneralError				  			=0x00002002,
         prRESPONSE_SessionNotOpen							=0x00002003,
         prRESPONSE_InvalidTransactionID					=0x00002004,
         prRESPONSE_OperationNotSupported					=0x00002005,
         prRESPONSE_ParameterNotSupported					=0x00002006,
         prRESPONSE_IncompleteTransfer						=0x00002007,
         prRESPONSE_InvalidStorageID						=0x00002008,
         prRESPONSE_InvalidObjectHandle						=0x00002009,
         prRESPONSE_DevicePropNotSupported					=0x0000200A,
         prRESPONSE_InvalidObjectFormatCode					=0x0000200B,
         prRESPONSE_StoreFull				  				=0x0000200C,
         prRESPONSE_ObjectWriteProtected				    =0x0000200D,
         prRESPONSE_StoreRead_Only							=0x0000200E,
         prRESPONSE_AccessDenied							=0x0000200F,
         prRESPONSE_NoThumbnailPresent						=0x00002010,
         prRESPONSE_SelfTestFailed							=0x00002011,
         prRESPONSE_PartialDeletion							=0x00002012,
         prRESPONSE_StoreNotAvailable						=0x00002013,
         prRESPONSE_SpecificationByFormatUnsupported		=0x00002014,
         prRESPONSE_NoValidObjectInfo						=0x00002015,
         prRESPONSE_InvalidCodeFormat						=0x00002016,
         prRESPONSE_UnknownVendorCode						=0x00002017,
         prRESPONSE_CaptureAlreadyTerminated				=0x00002018,
         prRESPONSE_DeviceBusy				  				=0x00002019,
         prRESPONSE_InvalidParentObject						=0x0000201A,
         prRESPONSE_InvalidDevicePropFormat					=0x0000201B,
         prRESPONSE_InvalidDevicePropValue					=0x0000201C,
         prRESPONSE_InvalidParameter						=0x0000201D,
         prRESPONSE_SessionAlreadyOpen						=0x0000201E,
         prRESPONSE_TransactionCancelled					=0x0000201F,
         prRESPONSE_SpecificationOfDestinationUnsupported	=0x00002020,

        /* Vendor Extension Error IDs */
         prRESPONSE_Ex_Undefined							=0x0000A000,
         prRESPONSE_Ex_UnknownCommandReceived				=0x0000A001,
         prRESPONSE_Ex_MemAllocFailed						=0x0000A002,
         prRESPONSE_Ex_InternalError						=0x0000A003,
         prRESPONSE_Ex_DirIOError							=0x0000A004,
         prRESPONSE_Ex_RefusedByOtherProcess				=0x0000A005,
         prRESPONSE_Ex_CoverClosed							=0x0000A006,
         prRESPONSE_Ex_NoRelease							=0x0000A007,
         prRESPONSE_Ex_DeviceIsHot							=0x0000A008,
         prRESPONSE_Ex_LowBattery							=0x0000A009,
         prRESPONSE_Ex_AlreadyExit							=0x0000A00A,

        /*-----------------------------------------------------------------------
           PRSDK Internal Error IDs
        ------------------------------------------------------------------------*/
        /* Miscellaneous errors */
         prUNIMPLEMENTED               						=0x00000001,
         prINTERNAL_ERROR									=0x00000002,
         prMEM_ALLOC_FAILED					  				=0x00000003,
         prMEM_FREE_FAILED									=0x00000004,
         prOPERATION_CANCELLED								=0x00000005,
         prINCOMPATIBLE_VERSION								=0x00000006,
         prNOT_SUPPORTED							  		=0x00000007,
         prUNEXPECTED_EXCEPTION								=0x00000008,
         prPROTECTION_VIOLATION								=0x00000009,
         prMISSING_SUBCOMPONENT								=0x0000000A,
         prSELECTION_UNAVAILABLE							=0x0000000B,

        /* Function Parameter errors */
         prINVALID_PARAMETER								=0x00000021,
         prINVALID_HANDLE						  			=0x00000022,

        /* ... */
         prINVALID_FN_CALL									=0x00000061,
         prWAIT_TIMEOUT_ERROR					    		=0x00000062,
         prINSUFFICIENT_BUFFER					    		=0x00000063,
         prEVENT_CALLBACK_EXIST                             =0x00000064

    }



}
