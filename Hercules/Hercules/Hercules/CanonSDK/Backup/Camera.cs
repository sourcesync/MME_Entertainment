using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

using System.ComponentModel;

namespace RDC.CameraSDK
{
	public class ImageTransferEventArgs : EventArgs
	{
		#region fields
		private string _fileName;
		#endregion

		#region properties
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				this._fileName = value;
			}
		}
		#endregion

		#region methods
		//ctor
		public ImageTransferEventArgs()
		{
			this._fileName = string.Empty;
		}
		public ImageTransferEventArgs(string fileName)
		{
			this._fileName = fileName;
		}
		#endregion
	}

	public class StreamEventArgs : EventArgs
	{
		#region fields
		private Bitmap _frame;
		#endregion

		#region properties
		public Bitmap frame
		{
			get
			{
				return this._frame;
			}
			set
			{
				this._frame = value;
			}
		}
		#endregion

		#region methods
		//ctor
		public StreamEventArgs()
		{
			this._frame = null;
		}
		public StreamEventArgs(Bitmap frame)
		{
			this._frame = frame;
		}
		#endregion
	}

	public class ProgressEventArgs : EventArgs
	{
		#region fields
		private int _progress;
		#endregion

		#region properties
		public int progress
		{
			get
			{
				return this._progress;
			}
			set
			{
				this._progress = value;
			}
		}
		#endregion

		#region methods
		//ctor
		public ProgressEventArgs()
		{
			this._progress = 0;
		}
		public ProgressEventArgs(int progress)
		{
			this._progress = progress;
		}
		#endregion
	}

	public class ConnectionEventArgs : EventArgs
	{
		#region fields
		private string _cameraName;
		#endregion

		#region properties
		public string CameraName
		{
			get
			{
				return this._cameraName;
			}
			set
			{
				this._cameraName = value;
			}
		}
		#endregion

		#region methods
		//ctor
		public ConnectionEventArgs()
		{
			this._cameraName = string.Empty;
		}
		public ConnectionEventArgs(string cameraName)
		
        {
			this._cameraName = cameraName;
		}
		#endregion
	}

	public class StatusEventArgs : EventArgs
	{
		#region fields
		// Preview 
		private bool _previewEnabled = false;
		
		// ImageFormatAttibutes
		private UInt16 _imageSize = 0;
		private UInt16 _imageQuality = 0;
		
		// NumAvailableShot
		private UInt32 _numAvailableShot = 0;

		// Zoom positions
		private UInt32 _zoomPos = 0;

		// ShootingMode
		private UInt16 _shootingMode = 0;		

		// ExposureCompensation
		private UInt16 _exposureCompensation = 0;
		
		// FlashMode
		private UInt16 _flashMode = 0;

		// ISOSpeed
		private UInt16 _ISOSpeed = 0;

		// PhotoEffect
		private UInt16 _photoEffect = 0;

        // AF Distance
        private byte _AFDistance = 0;
		#endregion

		#region properties
		public bool	PreviewEnabled
		{
			get
			{
				return this._previewEnabled;
			}
			set
			{
				this._previewEnabled = value;
			}
		}

		public int ImageSize
		{
			get
			{
				return (int)this._imageSize;
			}
			set
			{
				this._imageSize = (UInt16)value;
			}
		}
		public int ImageQuality
		{
			get
			{
				return (int)this._imageQuality;
			}
			set
			{
				this._imageQuality = (UInt16)value;
			}
		}
		public int AvailableShots
		{
			get
			{
              	return (int)this._numAvailableShot;
			}
			set
			{
              	this._numAvailableShot = (UInt16)value;
			}
		}
		public int ZoomPosition
		{
          	get
			{
              	return (int)this._zoomPos;
			}
			set
			{
              	this._zoomPos = (UInt16)value;
			}
		}
		public int ShootingMode
		{
			get
			{
              	return (int)this._shootingMode;
			}
			set
			{
              	this._shootingMode = (UInt16)value;
			}
		}
		public int ExposureComp
		{
			get
			{
              	return (int)this._exposureCompensation;
			}
			set
			{
              	this._exposureCompensation = (UInt16)value;
			}
		}
		public int FlashMode
		{
			get
			{
              	return (int)this._flashMode;
			}
			set
			{
              	this._flashMode = (UInt16) value;
			}
		}
		public int ISOSpeed
		{
			get
			{
              	return (int)this._ISOSpeed;
			}
			set
			{
              	this._ISOSpeed = (UInt16)value;
			}
		}
		public int PhotoEffect
		{
			get
			{
				return (int)this._photoEffect;
			}
			set
			{
              	this._photoEffect = (UInt16)value;
			}
		}
        public byte AFDistance { get { return _AFDistance; } }

		#endregion

		#region methods
		//ctor
		public StatusEventArgs()
		{

		}
		public StatusEventArgs(bool PreviewEnabled, int ImageSize, int ImageQuality, 
								int AvailableShots, int ZoomPosition, int ShootingMode,
								int ExposureComp, int FlashMode, int ISOSpeed, int PhotoEffect, byte AFDistance)
		{
			this._previewEnabled = PreviewEnabled;
			this._imageSize = (UInt16)ImageSize;
			this._imageQuality = (UInt16)ImageQuality;
			this._numAvailableShot = (UInt32)AvailableShots;
			this._zoomPos = (UInt32)ZoomPosition;
			this._shootingMode = (UInt16)ShootingMode;
			this._exposureCompensation = (UInt16)ExposureComp;
			this._flashMode = (UInt16)FlashMode;
			this._ISOSpeed = (UInt16)ISOSpeed;
			this._photoEffect = (UInt16)PhotoEffect;
            this._AFDistance = AFDistance;
		}
		#endregion
	}

    public class DeviceListEventArgs : EventArgs
    {
        #region fields
        List<string> _deviceNames;
        #endregion

        #region Properties
        public List<string> DeviceNames { get { return _deviceNames; } }
        #endregion

        #region constructor
        public DeviceListEventArgs(List<string> deviceNames)
        {
            _deviceNames = deviceNames;
        }
        #endregion
    }

	public class Camera
    {
        #region Threads
        BackgroundWorker connectionWorker;
        BackgroundWorker enumerateDevicesWorker;
        #endregion // Threads

        #region Constants
        private const int MY_BUFFER_SIZE = 20 * 1024;	/* 20KB */
        private const int VF_BUFFER_SIZE = 100 * 1024;	/* 100KB */
        private const int MY_TRANSFER_SIZE = 1600 * 1024;	/* 1600KB */    
        #endregion

		#region Members
        /* SDK common Member Area */
        bool m_fVFEnd = false;          // The flag of a view finder of operation 
        bool m_fProgramRelease = false;
        bool m_fMCard = false;

        /*	PRSDK Member Area	*/
        UInt32 pr_hCamera;				//	PRSDK Camera handle
        prType.prptpFileFormat pr_FileType;
        prType.prptpTransferMode pr_TransferMode;
        UInt16 pr_ReleaseComp;
        UInt32 pr_PicHandle;
        UInt32 pr_ThmbHandle;
        FileStream pr_FileStream;

        /*	PRSDK CallBack Functions & Thread Function */
        static prType.prSetEventCB pr_SetEventCB;
        static prType.prGetFileDataCB pr_GetFileDataCB;
        static prType.prViewFinderCB pr_ViewFinderCB;
        
        /*	CDSDK Member Area	*/
        IntPtr cd_hSource = new IntPtr();   //	CDSDK Camera handle
        IntPtr cd_pSourceInfo = new IntPtr(); 
        UInt32 cd_hCallbackFunction;
      
        /* CDSDK call back function & Thread Function */
		static cdType.cdEventCallbackFunction cd_EventCallbackFunction;
		static cdType.cdProgressCallbackFunction cd_ProgressCallbackFunction;
		static cdType.cdViewFinderCallbackFunction cd_ViewFinderCallBackDelegate;
		static cdType.cdReleaseEventCallbackFunction cd_ReleaseEventCallbackFunction;

        /* Common members */
		string m_asyncFileName = String.Empty;

		Bitmap m_image = null;

        List<SDKInfo> devices = new List<SDKInfo>();

        List<string> deviceNames;

        // Connected camera name
        string m_connectedCamera = string.Empty;
		
		// NumAvailableShot
		UInt32 m_NumAvailableShot = 0;

		// Zoom positions
		UInt32 m_MaxZoomPos = 0;
		UInt32 m_MaxOpticalZoomPos = 0;
		UInt32 m_ZoomPos = 0;
		cdType.cdURational m_DZoomPos = new CameraSDK.cdType.cdURational();

		// ImageFormatAttibutes
        UInt16 m_ImageSize = 0;
        prType.prptpImageQuality m_ImageQuality = 0;

		// ShootingMode
		UInt16 m_ShootingMode = 0;		

		// ExposureCompensation
		UInt16 m_ExposureCompensation = 0;
		
		// FlashMode
		UInt16 m_FlashMode = 0;

		// ISOSpeed
		UInt16 m_ISOSpeed = 0;

		// PhotoEffect
		UInt16 m_PhotoEffect = 0;

        // AFDistance
        prType.prptpAFDistance m_AFDistance = 0;
        prType.prptpMetering m_Metering;

		#endregion

        #region Enums and Structures
        protected enum SDKType:uint
        {
            PRSDK	= 0x00000001,	/* Camara devices enumerated by PS-ReC SDK */
            CDSDK   = 0x00000002	/* Camera devices enumerated by CD-SDK */
        }
       
        /* Camera device information (PS-ReC SDK and CD-SDK) */
        protected struct SDKInfo
        {
            public SDKType  SelectedSDK;		        /* PRSDK or CDSDK */
            public prType.prDeviceInfoTable PRSDK_Src;	/* Camera device information (PS-ReC SDK) */
            public cdType.cdSourceInfo CDSDK_Src;	    /* Camera device information (CD-SDK) */
	    } ;

        protected struct EVENT_GENERIC_CONTAINER
        {
	        public UInt32 ContainerLength;
	        public UInt16 ContainerType;
	        public UInt16 Code;
	        public UInt32 TransactionID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	        public UInt32[] Parameter;
        } ;

        #endregion

        #region Delegates
        public delegate void DeviceNamesEnumeratedEvent(object sender, DeviceListEventArgs e);
        public event DeviceNamesEnumeratedEvent DevicesEnumerated;

        public delegate void ReceivedFrameEventHandler(object sender, StreamEventArgs e);
		public event ReceivedFrameEventHandler ReceivedFrame;

		public delegate void ImageTransferStartedEventHandler(object sender, ImageTransferEventArgs e);
		public event ImageTransferStartedEventHandler ImageTransferStarted;

		public delegate void ImageAcquiredEventHandler(object sender, StreamEventArgs e);
		public event ImageAcquiredEventHandler ImageAcquired;

		public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
		public event ProgressEventHandler Progress;
		
		public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);
		public event ConnectionEventHandler Connected;

        public delegate void ConnectionFailedEventHandler(object sender, ConnectionEventArgs e);
        public event ConnectionFailedEventHandler ConnectionFailed;
   
		public delegate void DisconnectionEventHandler(object sender);
		public event DisconnectionEventHandler Disconnected;

		public delegate void StatusEventHandler(object sender, StatusEventArgs e);
		public event StatusEventHandler StatusChanged;

		#endregion
        
		#region Properties
        public bool IsPRSDK
        {
            get
            {
                return (cd_hSource.ToInt32() <= 0);
            }
        }
        
        public List<string> DeviceNames
        {
            get
            {
                    DoGetDevices(this, new DoWorkEventArgs(null));

                return deviceNames;
            }
        }

		public bool	PreviewEnabled
		{
			get
			{
              	return this.m_fVFEnd;
			}
		}
		public bool HasMemoryCard
		{
			get
			{
				return m_fMCard;
			}
		}
		public int MaxZoomPos
		{
			get
			{
				return (int)m_MaxZoomPos;
			}
		}
		public int MaxOpticalZoomPos
		{
			get
			{
				return (int)m_MaxOpticalZoomPos;
			}
		}

        public prType.prptpAFDistance AFDistance 
        {
            get { return m_AFDistance; }
            set { SetAFDistance((byte)value); }
        }

		public int ZoomPos
		{
			get
			{
				return (int)m_ZoomPos;
			}
			set
			{
                /* Is CD-SDK connection? */
                if (cd_hSource.ToInt32() > 0)
                {
                    CDSDK_SetZoom((UInt32)value);
                }
                else
                {
                    PRSDK_SetZoom((UInt32)value);
                }
			}
		}
		public int ImageSize
		{
			get
			{
				return (int)m_ImageSize;
			}
			set
			{
				SetImageFormat((UInt16)value, m_ImageQuality);
			}
		}
        public prType.prptpImageQuality ImageQuality
		{
			get
			{
				return m_ImageQuality;
			}
			set
			{
				SetImageFormat(m_ImageSize, value);
			}
		}
		public int ShootingMode
		{
			get
			{
				return (int)m_ShootingMode;
			}
			set
			{
				SetShootingMode((UInt16)value);
			}
		}
		public int ExposureCompensation
		{
			get
			{
				return (int)m_ExposureCompensation;
			}
			set
			{
				SetExposureCompensation((UInt16)value);
			}
		}
		public int FlashMode
		{
			get
			{
				return (int)m_FlashMode;
			}
			set
			{
				SetFlashMode((UInt16)value);
			}
		}
		public int ISOSpeed
		{
			get
			{
				return (int)m_ISOSpeed;
			}
			set
			{
				SetISOSpeed((UInt16)value);
			}
		}
		public int PhotoEffect
		{
			get
			{
				return (int)m_PhotoEffect;
			}
			set
			{
				SetPhotoEffect((UInt16)value);
			}
		}
		public int NumAvailableShots
		{
			get
			{
				/* The number of sheets which can be remaining photoed is acquired. */
				UInt32 Num = 0;
				UInt32 err = cdAPI.CDGetNumAvailableShot(cd_hSource, ref Num );
				if( err == (UInt32)cdError.cdOK )
				{
					m_NumAvailableShot = Num;
				}
				return (int)m_NumAvailableShot;
			}
		}
        public string ConnectedCameraName
        {
            get
            {
                return m_connectedCamera;
            }
        }
		public prType.prptpMetering MeteringMode
        {
            get { return m_Metering; }
            set { SetMeteringMode(value); }
        }


		#endregion

		#region Constructor
		public Camera()
		{
            connectionWorker = new BackgroundWorker();
            connectionWorker.DoWork += new DoWorkEventHandler(DoConnection);
            connectionWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(connectionWorker_WorkerComplete);

            enumerateDevicesWorker = new BackgroundWorker();
            enumerateDevicesWorker.DoWork += new DoWorkEventHandler(DoGetDevices);
            enumerateDevicesWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(enumerateDevicesWorker_RunWorkerCompleted);
		}
		#endregion

		#region Callback Function
        /* CDSDK Callback functions */
		private UInt32 CD_CameraCallBackFunc(UInt32 EventID, IntPtr pData, UInt32 DataSize, UInt32 Context)
		{
			/* A security level is checked. */
			switch( EventID )
			{
                case (UInt32)cdType.cdEVENT_SEVERITY.cdEVENT_SEVERITY_SHUTDOWN:
					Console.WriteLine("CamCallBackFunc - SEVERITY_SHUTDOWN (" + pData.ToString() + ")");
					break;
                case (UInt32)cdType.cdEVENT_SEVERITY.cdEVENT_SEVERITY_NONE:
					Console.WriteLine("CamCallBackFunc - SEVERITY_NONE (" + pData.ToString() + ")");
					break;
                case (UInt32)cdType.cdEVENT_SEVERITY.cdEVENT_SEVERITY_WARNING:
					Console.WriteLine("CamCallBackFunc - SEVERITY_WARNING (" + pData.ToString() + ")");
					break;
			}
		
			return (UInt32)cdError.cdOK;
		}
        private UInt32 CD_ReleaseCallBackFunc(UInt32 EventID, IntPtr pData, UInt32 DataSize, UInt32 Context)
		{
			UInt32 err = (UInt32)cdError.cdOK;
				
			switch( EventID  )
			{
                case (UInt32)cdType.cdReleaseEvent.cdRELEASE_EVENT_RESET_HW_ERROR:
					Console.WriteLine("RelCallBackFunc - RESET_HW_ERROR (" + pData.ToString() + ")");
					break;
                case (UInt32)cdType.cdReleaseEvent.cdRELEASE_EVENT_CHANGED_BY_UI:
					Console.WriteLine("RelCallBackFunc - CHANGED_BY_UI (" + pData.ToString() + ")");
					break;
                case (UInt32)cdType.cdReleaseEvent.cdRELEASE_EVENT_RELEASE_START:
					Console.WriteLine("RelCallBackFunc - RELEASE_START (" + pData.ToString() + ")");
					break;
                case (UInt32)cdType.cdReleaseEvent.cdRESEASE_EVENT_RELEASE_COMPLETE:
					Console.WriteLine("RelCallBackFunc - RELEASE_COMPLETE (" + pData.ToString() + ")");
					break;
                case (UInt32)cdType.cdReleaseEvent.cdRELEASE_EVENT_CAM_RELEASE_ON:
					Console.WriteLine("RelCallBackFunc - CAM_RELEASE_ON (" + pData.ToString() + ")");
					break;
                case (UInt32)cdType.cdReleaseEvent.cdRELEASE_EVENT_ABORT_PC_EVF:
					Console.WriteLine("RelCallBackFunc - ABORT_PC_EVF (" + pData.ToString() + ")");
					break;
			}
				
			return	err;
		}
        private UInt32 CD_ProgressCallBackFunc(UInt32 Progress, UInt32 Status, UInt32 Context)
		{
            UInt32 err = (UInt32)cdError.cdOK;
			
			if (this.Progress != null)
			{
				this.Progress(this, new ProgressEventArgs((int)Progress));
			}
				
			return	err;
		}
        private UInt32 CD_ViewFinderCallBackFunc(IntPtr pBuf, UInt32 Size, UInt32 Format, UInt32 Context)
		{
			if ( Format == cdType.FILEFORMAT_BMP )
			{
				// Get the BITMAPHEADER from pBuf
				BITMAPHEADER frameHeader = new BITMAPHEADER();
				frameHeader = (BITMAPHEADER)Marshal.PtrToStructure(pBuf, frameHeader.GetType());

				// Create byte array (BITMAPHEADER + image data) and copy from pBuf
				byte[] VideoData = new byte[frameHeader.bmfHeader.bfSize];
				Marshal.Copy(pBuf, VideoData, 0, (int)frameHeader.bmfHeader.bfSize);

				// Get image size
				int width = frameHeader.bmiHeader.biWidth;
				int height = frameHeader.bmiHeader.biHeight;
				
				// Create new Bitmap
				Bitmap frame = new Bitmap(width, height);
                // Lock the bitmap data
				BitmapData bmpData = frame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

				// Copy the image data from memory to the Scan0 property of our Bitmap
				// Start at index 54 because of BITMAPHEADER
                Marshal.Copy(VideoData, 54, bmpData.Scan0, VideoData.Length - 54);

                //Copy(VideoData, 54, bmpData.Scan0, 0, VideoData.Length - 54);
               
               	frame.UnlockBits(bmpData);
				frame.RotateFlip(RotateFlipType.Rotate180FlipX);

				// Create delegate
				if (this.ReceivedFrame != null)
				{
                    Delegate[] clients = this.ReceivedFrame.GetInvocationList();
                    foreach (Delegate del in clients)
                    {
                        try
                        {
                            del.DynamicInvoke(new object[] { this, new StreamEventArgs(frame) });
                        }
                        catch { }
                    }
                }
			}
            return (UInt32)cdError.cdOK;
		}

        /* PRSDK Callback functions */
        private UInt32 PR_CameraCallBackFunc(UInt32 CameraHandle, UInt32 Context, IntPtr pEventData)
        {
            EVENT_GENERIC_CONTAINER pEventDataTemp = new EVENT_GENERIC_CONTAINER();

            pEventDataTemp = (EVENT_GENERIC_CONTAINER)Marshal.PtrToStructure(pEventData, pEventDataTemp.GetType());

            switch (pEventDataTemp.Code)
            {
                case (ushort)prType.prptpEventCode.prCAL_SHUTDOWN:
                    //CpThis->PostMessage(CpThis->m_CameraShutDown);
                    break;
                case (ushort)prType.prptpEventCode.prPTP_ABORT_PC_EVF:
                    //CpThis->PostMessage(CpThis->m_AbortPCEVF);
                    break;
                case (ushort)prType.prptpEventCode.prPTP_FULL_VIEW_RELEASED:
                    pr_PicHandle = pEventDataTemp.Parameter[0];
                    pr_ReleaseComp |= (UInt16)prType.prptpTransferMode.FullImageToPC;
                    break;
                case (ushort)prType.prptpEventCode.prPTP_THUMBNAIL_RELEASED:
                    pr_ThmbHandle = pEventDataTemp.Parameter[0];
                    pr_ReleaseComp |= (UInt16)prType.prptpTransferMode.ThumbnailToPC;
                    break;
                case (ushort)prType.prptpEventCode.prPTP_CAPTURE_COMPLETE:
                    break;
                case (ushort)prType.prptpEventCode.prPTP_PUSHED_RELEASE_SW:
                    //CpThis->PostMessage( CpThis->m_ReleaseOnMessage );
                    break;
                case (ushort)prType.prptpEventCode.prPTP_RC_PROP_CHANGED:

                case (ushort)prType.prptpEventCode.prPTP_DEVICE_PROP_CHANGED:
                    //CpThis->PostMessage( CpThis->m_ChangeByUI );
                    break;
            }
        	
	        return	(UInt32)prError.prOK;
        }
        private UInt32 PR_ProgressCallBackFunc(UInt32 CameraHandle,
										   UInt32 ObjectHandle,
										   UInt32 Context,
										   ref prType.prProgress pProgress)
        {
	        /* Save image data into a file */
	        try
	        {
		        switch( pProgress.lMessage )
		        {
			        case prType.prProgressMsg.prMSG_DATA_HEADER:
                        /* Open a file */
                        pr_FileStream = System.IO.File.Create(m_asyncFileName);
                        
				        break;
			        case prType.prProgressMsg.prMSG_DATA:
                        /* Move offset and save the size of data into a file */
                        pr_FileStream.Seek((long)pProgress.lOffset, SeekOrigin.Begin);

                        /* Copy the data from the IntPtr to an array of bytes */
                        byte[] bData = new byte[(int)pProgress.lLength] ;

                        /* Write the byte array to file */
                        Marshal.Copy(pProgress.pbData, bData, 0, (int)pProgress.lLength);
                        pr_FileStream.Write(bData, 0, (int)pProgress.lLength);
                                                 
                        /* Notify clients of progress */
                        if (this.Progress != null)
                        {
                            this.Progress(this, new ProgressEventArgs((int)pProgress.lPercentComplete));
                        }
                                              
				        break;
			        case prType.prProgressMsg.prMSG_TERMINATION:
				        
                        /* Close the file */
                        pr_FileStream.Close();
                        pr_FileStream.Dispose();
                       
				        break;
		        }
	        }
	        catch	/* Error in file save */
	        {
                /* Close the file */
                pr_FileStream.Close();
                pr_FileStream.Dispose();

		        return (UInt32)prError.prOPERATION_CANCELLED;	/* Return prOPERATION_CANCELLED */
	        }

            return (UInt32)prError.prOK;
        }
        private UInt32 PR_ViewFinderCallBackFunc(UInt32 CameraHandle, UInt32 Context, UInt32 Size, IntPtr pVFData)
        {
            // Create a buffer for the viewfinder data
            byte[] viewFinderData = new byte[VF_BUFFER_SIZE];

            // Copy the data from the IntPtr to the byte array
            Marshal.Copy(pVFData, viewFinderData, 0, (int)Size);

            System.IO.MemoryStream ms = new MemoryStream(viewFinderData);
            Bitmap b = new Bitmap(ms);
            
            // Create delegate
            if (this.ReceivedFrame != null)
            {
                Delegate[] clients = this.ReceivedFrame.GetInvocationList();
                foreach (Delegate del in clients)
                {
                    try
                    {
                        del.DynamicInvoke(new object[] { this, new StreamEventArgs(b) });
                    }
                    catch { }
                }
            }
            // Free resources
            ms.Close();

	        return (UInt32)prError.prOK;
        }
        #endregion

        #region StartSDK
        public bool StartSDK()
		{
			UInt32 err;

		    /* PS-Rec SDK START */
            err = prAPI.PR_StartSDK();
            if (err != (UInt32)prError.prOK)
            {
                HandleError("PRStartSDK", err);
                return false;
            }

			/* CDSDK is changed the first stage. */
			cdType.cdVersionInfo Version = new cdType.cdVersionInfo();
			Version.Size = 40;
			Version.MajorVersion = 7;
			Version.MinorVersion = 3;
			Version.ReleaseVersion = 0;

            err = (UInt32)cdAPI.CDStartSDK(ref Version, 0);
            if (err != (UInt32)cdError.cdOK) 
			{
				HandleError("CDStartSDK", err);
			}

            return (err == (UInt32)cdError.cdOK);
		}
		public void EndSDK() 
		{
            UInt32 err;

            // Make sure the camera is disconnected
            Disconnect();

            /* End processing of CDSDK is performed. */
            err = cdAPI.CDFinishSDK();
            if (err != (UInt32)cdError.cdOK)
            {
                HandleError("CDFinishSDK", err);
            }

            /* End processing of PS-ReC SDK is performed. */
            err = prAPI.PR_FinishSDK();
            if (err != (UInt32)prError.prOK)
            {
                HandleError("PR_FinishSDK", err);
            }
		}
		#endregion

		#region Devices
		public void GetDevices()
		{
            if(!enumerateDevicesWorker.IsBusy)
            {
                enumerateDevicesWorker.RunWorkerAsync();
            }
		}

        public void DoGetDevices(object sender, DoWorkEventArgs ea)
        {
            // Clear the device array list
            devices.Clear();
            // Create a new array of device names
            deviceNames = new List<string>();
            // Enumerate devices using the PRSDK
            PRSDK_EnumerateDevices(ref deviceNames);
            //Enumerate devices using the CDSDK
            CDSDK_EnumerateDevices(ref deviceNames);
            // Return all named devices
            ea.Result = deviceNames;
        }

        void enumerateDevicesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DevicesEnumerated != null)
            {
                DevicesEnumerated(this, new DeviceListEventArgs(deviceNames));
            }
        }

        private void CDSDK_EnumerateDevices(ref List<string> names)
        {
            UInt32 err = (UInt32)cdError.cdOK;
            
            try
            {
                UInt32 EnumDevice = 0;
                err = cdAPI.CDEnumDeviceReset(1, ref EnumDevice);
                if (err != (UInt32)cdError.cdOK)
                {
                    HandleError("CDEnumDeviceReset", err);
                    return;
                }

                UInt32 count = 0;
                err = cdAPI.CDGetDeviceCount(EnumDevice, ref count);
                if (err != (UInt32)cdError.cdOK)
                {
                    HandleError("CDGetDeviceCount", err);
                    return;
                }

                SDKInfo pSourceInfo;
                for (int i = 0; i < count; i++)
                {
                    // Create a new source info
                    pSourceInfo = new SDKInfo();
                    // This source will be enumerated by the CDSDK
                    pSourceInfo.SelectedSDK = SDKType.CDSDK;
                    err = cdAPI.CDEnumDeviceNext(EnumDevice, ref pSourceInfo.CDSDK_Src);
                    if (err == (UInt32)cdError.cdOK)
                    {
                        // Don't add this camera if it has already been enumerated by the PRSDK
                        if (!names.Contains(pSourceInfo.CDSDK_Src.Name))
                        {
                            devices.Add(pSourceInfo);
                            names.Add(pSourceInfo.CDSDK_Src.Name);
                        }
                    }
                    else
                    {
                        HandleError("CDEnumDeviceNext", err);
                        return;
                    }
                }

                err = cdAPI.CDEnumDeviceRelease(EnumDevice);
                if (err != (UInt32)cdError.cdOK)
                {
                    HandleError("CDEnumDeviceRelease", err);
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.ToString());
            }

            return;
        }

        private void PRSDK_EnumerateDevices(ref List<string> names)
        {
            UInt32 err = (UInt32)prError.prOK;
	        UInt32 bufSize = 0;
            prType.prDeviceList pGetDevList = new prType.prDeviceList();
                                  
            for(;;)
            {
                /* Enumerate camera devices by PS-ReC SDK */
                err = prAPI.PR_GetDeviceList(ref bufSize, ref pGetDevList);
		        if(err == (UInt32)prError.prOK)
		        {
                    break;
		        }
		        if((err & (UInt32)prError.prERROR_ERRORID_MASK) != (UInt32)prError.prINSUFFICIENT_BUFFER)
		        {
                    HandleError("PR_GetDeviceList", err);
                    return;
		        }
            }

            if (pGetDevList.NumList > 0)
            {	
                for(int i = 0; i<(pGetDevList.NumList); i++)
                {
                    // Create a SDKInfo structure for each DeviceInfo object in the device list
                    SDKInfo pDeviceInfo = new SDKInfo();
                    pDeviceInfo.SelectedSDK = SDKType.PRSDK;
                    pDeviceInfo.PRSDK_Src = pGetDevList.DeviceInfo[i];

                    // Add the SDKInfo object to the devices arraylist
                    names.Add(pDeviceInfo.PRSDK_Src.ModelName);
                    devices.Add(pDeviceInfo);
                }
            }

        }

		#endregion

		#region Connection
        public void Connect(string deviceName)
        {
            m_connectedCamera = string.Empty;

            DoConnection(null, new DoWorkEventArgs(deviceName));

            if (!string.IsNullOrEmpty(m_connectedCamera))
            {
                if (Connected != null)
                {
                    Connected(this, new ConnectionEventArgs(m_connectedCamera));
                }
            }
            else
            {
                if (ConnectionFailed != null)
                {
                    ConnectionFailed(this, new ConnectionEventArgs());
                }
            }
        }

		public void ConnectAsync(string DeviceName)
		{
            if (!connectionWorker.IsBusy)
            {
                connectionWorker.RunWorkerAsync(DeviceName);
            }
		}

        void connectionWorker_WorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                string cameraName = e.Result.ToString();

                if (Connected != null)
                {
                    Connected(this, new ConnectionEventArgs(cameraName));
                }
            }
            else
            {
                if (ConnectionFailed != null)
                {
                    ConnectionFailed(this, new ConnectionEventArgs());
                }
            }
        }

        // TODO: Report progress of < 100 if it fails.
        private void DoConnection(object sender, DoWorkEventArgs ea)
        {
            string DeviceName = ea.Argument.ToString();

            SDKInfo pSourceInfo = new SDKInfo();

            for (int i = 0; i < this.devices.Count; i++)
            {
                string name = String.Empty;
                //Determine which SDK this device was enumerated by
                if (((SDKInfo)devices[i]).SelectedSDK == SDKType.CDSDK)
                {
                    name = ((SDKInfo)devices[i]).CDSDK_Src.Name;
                }
                else
                {
                    name = ((SDKInfo)devices[i]).PRSDK_Src.ModelName;
                }

                if (name == DeviceName)
                {
                    pSourceInfo = (SDKInfo)devices[i];
                    break;
                }
            }

            if (pSourceInfo.SelectedSDK == SDKType.CDSDK)
            {
                if (!CDSDK_Connect(pSourceInfo.CDSDK_Src))
                {
                    return;
                }
            }
            else
            {
                if (!PRSDK_Connect(pSourceInfo.PRSDK_Src))
                {
                    return;
                }
            }

            // Save the name of the connected camera
            m_connectedCamera = DeviceName;

            // Send the "Connected" event
/*            if (this.Connected != null)
            {
                this.Connected(this, new ConnectionEventArgs(DeviceName));
            }
  */          // Success!

            ea.Result = DeviceName;
        }
        private bool PRSDK_Connect(prType.prDeviceInfoTable src)
        {
            UInt32 err = (UInt32)prError.prOK; 
            UInt32 BufferSize = 0;
            IntPtr pDataBuffer = IntPtr.Zero;
            bool IsRC, IsEVF, IsAeaf, IsExpoMode, IsExpoComp, 
                 IsStrobeSet, IsISO, IsPhotoEffect;

            /* Init variables */
            IsRC = false;
            IsEVF = false;
            IsAeaf = false;
            IsExpoMode = false;
            IsExpoComp = false;
            IsStrobeSet = false;
            IsISO = false;
            IsPhotoEffect = false;
           
	        /* Making of camera steering wheel */
	        err = prAPI.PR_CreateCameraObject(ref src, ref pr_hCamera);
	        if ( err != (UInt32)prError.prOK )
	        {
		        goto ErrorFinish;
	        }

            /* Registration of event callback function */
            pr_SetEventCB = new prType.prSetEventCB(this.PR_CameraCallBackFunc);
            err = prAPI.PR_SetEventCallBack(pr_hCamera, 0, pr_SetEventCB);
            if (err != (UInt32)prError.prOK)
            {
                goto ErrorFinish;
            }

	        /* Connection of camera device */
	        err = prAPI.PR_ConnectCamera(pr_hCamera);
	        if ( err != (UInt32)prError.prOK )
	        {
		        goto ErrorFinish;
	        }

            /* Device information is acuired. */
            pDataBuffer = Marshal.AllocHGlobal(MY_BUFFER_SIZE);
            BufferSize = MY_BUFFER_SIZE;
            err = prAPI.PR_GetDeviceInfo(pr_hCamera, ref BufferSize, pDataBuffer);
            if (err != (UInt32)prError.prOK)
            {
                goto ErrorFinish;
            }

            /* Check if Release Control is supported */
            IsSupportedRCandEVF(pDataBuffer, ref IsRC, ref IsEVF, ref IsAeaf);
            if (IsRC == false)	/* not support Release Control */
            {
                goto ErrorFinish;
            }

            /* Release Control mode start */
            err = prAPI.PR_InitiateReleaseControl(pr_hCamera);
            if (err != (UInt32)prError.prOK)
            {
                goto ErrorFinish;
            }

            /* Device information is acquired. */
            BufferSize = MY_BUFFER_SIZE;
            err = prAPI.PR_GetDeviceInfo(pr_hCamera, ref BufferSize, pDataBuffer);
            if (err != (UInt32)prError.prOK)
            {
                goto ErrorFinish;
            }
            IsSupportedCapRelPrm(pDataBuffer, ref IsExpoMode, ref IsExpoComp,
                                 ref IsStrobeSet, ref IsISO, ref IsPhotoEffect);

            /* For the time being, only support full size images - set the capture mode to full size only */
            err = PRSDK_SetTransferMode(prType.prptpTransferMode.FullImageToPC);

            if (err != (UInt32)prError.prOK)
            {
                goto ErrorFinish;
            }

            /* Figure out which file type the camera is set to send */
            err = PRSDK_GetTransferMode(ref pr_TransferMode);
            if (err != (UInt32)prError.prOK)
            {
                goto ErrorFinish;
            }

            /* Figure out which file type the camera is set to send */
            err = PRSDK_GetFileType(ref pr_FileType);
            if (err != (UInt32)prError.prOK)
            {
                goto ErrorFinish;
            }

            /* Free the buffer */
            Marshal.FreeHGlobal(pDataBuffer);

            // Initialize the zoom
            return PRSDK_InitZoom();

        ErrorFinish:
            /* Free the buffer */
            Marshal.FreeHGlobal(pDataBuffer);

	        /* Disconnect the camera */
            PRSDK_Disconnect();
           
	        if ( err != (UInt32)prError.prOK )
	        {
                HandleError("PRSDK_Connect", err);
	        }
	        else
	        {
                HandleError("Not Supported or Data Error",0);
	        }
            return false;
        }
        private bool CDSDK_Connect(cdType.cdSourceInfo src)
        {
            UInt32 err = (UInt32)cdError.cdOK;

            if (src.SourceType == (UInt32)cdType.cdSourceType.cdSRC_TYPE_INVALID) // invalid type
                return false;

            // Copy the cdType.cdSourceInfo structure to the buffer.
            cd_pSourceInfo = Marshal.AllocHGlobal(Marshal.SizeOf(src));
            Marshal.StructureToPtr(src, cd_pSourceInfo, true);

            /* A device is opened. */
            err = cdAPI.CDOpenSource(cd_pSourceInfo, ref cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* UI is locked so that information may not be changed. */
            err = cdAPI.CDLockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* The function which receives the event from a camera is set up. */
            cd_EventCallbackFunction = new cdType.cdEventCallbackFunction(this.CD_CameraCallBackFunc);
            err = cdAPI.CDRegisterEventCallbackFunction(cd_hSource, cd_EventCallbackFunction, 0, ref cd_hCallbackFunction);

            /* The existence of memory card is checked. */
            UInt32 hEnumVol = 0;
            UInt32 hVolume = 0;
            cdType.cdVolumeInfo SVolInfo = new cdType.cdVolumeInfo();

            m_fMCard = false;
            /*Volumes are enumerated.*/
            err = cdAPI.CDEnumVolumeReset(cd_hSource, ref hEnumVol);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* It repeats until it enumerates all volumes. */
            while ((err = cdAPI.CDEnumVolumeNext(hEnumVol, ref hVolume)) == (UInt32)cdError.cdOK)
            {
                /* The information on volume is acquired. */

                err = cdAPI.CDGetVolumeInfo(hVolume, ref SVolInfo);
                if (err != (UInt32)cdError.cdOK)
                {
                    cdAPI.CDEnumVolumeRelease(hEnumVol);
                    goto camerr;
                }

                if (SVolInfo.TotalSpace != 0)
                {
                    m_fMCard = true;
                    break;
                }
            }
            if (err != (UInt32)cdError.cdOK && ((err & (UInt32)cdError.cdENUM_NA) != (UInt32)cdError.cdENUM_NA))
            {
                cdAPI.CDEnumVolumeRelease(hEnumVol);
                goto camerr;
            }

            /* Listing of volume is ended. */
            err = cdAPI.CDEnumVolumeRelease(hEnumVol);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            UInt32 hEnum = 0;
            err = cdAPI.CDEnumDevicePropertyReset(cd_hSource, 0, ref hEnum);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            err = cdAPI.CDEnumDevicePropertyRelease(hEnum);
            hEnum = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* A camera is set as remote release control mode. */
            cd_ReleaseEventCallbackFunction = new cdType.cdReleaseEventCallbackFunction(CD_ReleaseCallBackFunc);
            err = cdAPI.CDEnterReleaseControl(cd_hSource, cd_ReleaseEventCallbackFunction, 0);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* For the time being, only support full size images - set the capture mode to full size only */
            err = cdAPI.CDSelectReleaseDataKind(cd_hSource, cdType.cdRelDataKind.cdREL_KIND_PICT_TO_PC);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // Initialize the zoom
            CDSDK_InitZoom();

            /* The lock of UI is canceled. */
            err = cdAPI.CDUnlockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                cdAPI.CDExitReleaseControl(cd_hSource);
                goto camerr;
            }

            return true;

        camerr:
            if (err != (UInt32)cdError.cdNOT_SUPPORTED)
            {
                Console.WriteLine("Error Nr.:" + err.ToString());
            }

            if (cd_hSource.ToInt32() != 0)
            {
                if (cd_hCallbackFunction != 0)
                {
                    cdAPI.CDUnregisterEventCallbackFunction(cd_hSource, cd_hCallbackFunction);
                    cd_hCallbackFunction = 0;
                }

                cdAPI.CDUnlockUI(cd_hSource);
                cdAPI.CDCloseSource(cd_hSource);
                cd_hSource = IntPtr.Zero;
            }
            return false;
        }
	        
        public void Disconnect()
		{
            /* End the view finder. */
            StopLiveViewer();

            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32()>0)
            {
                CDSDK_Disconnect();
            }
            else
            {
                PRSDK_Disconnect();
            }

            // Clear the name of the connected camera
            m_connectedCamera = string.Empty;

			// Create delegate
			if (this.Disconnected != null)
			{
				this.Disconnected(this);
			}						
			return;
		}
        private void PRSDK_Disconnect()
        {
            if (pr_hCamera != 0)
            {
                /* Terminate Release Control mode */
                prAPI.PR_TerminateReleaseControl(pr_hCamera);
                /* Disconnect the camera */
                prAPI.PR_DisconnectCamera(pr_hCamera);
                /* Release the camera events */
                prAPI.PR_ClearEventCallBack(pr_hCamera);
                /* Destory camera object */
                prAPI.PR_DestroyCameraObject(pr_hCamera);

                pr_hCamera = 0;
            }
        }
        private void CDSDK_Disconnect()
        {
            UInt32 err = 0;

            if (cd_hSource.ToInt32() > 0)
            {
                /* Remote release control mode is ended. */
                err = cdAPI.CDExitReleaseControl(cd_hSource);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                /* The function which receives the event from a camera is canceled. */
                if (cd_hCallbackFunction > 0)
                {
                    err = cdAPI.CDUnregisterEventCallbackFunction(cd_hSource, cd_hCallbackFunction);
                    if (err != (UInt32)cdError.cdOK)
                    {
                        goto camerr;
                    }
                    cd_hCallbackFunction = 0;
                }

                /* A device is closed. */
                err = cdAPI.CDCloseSource(cd_hSource);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                cd_hSource = IntPtr.Zero;
            }

            // Free the memory used by the Transport structure
            Marshal.FreeHGlobal(cd_pSourceInfo);
            cd_pSourceInfo = IntPtr.Zero;

            return;

            camerr:

            // Free the memory used by the Transport structure
            Marshal.FreeHGlobal(cd_pSourceInfo);
            cd_pSourceInfo = IntPtr.Zero;

            Console.WriteLine("Disconnect error: " + err.ToString());
        }

		#endregion

		#region LiveViewer
        public void StartLiveViewer()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_StartLiveViewer();
            }
            else
            {
                PRSDK_StartLiveViewer();
            }
        }
        private void PRSDK_StartLiveViewer()
        {
 	        if ( !m_fVFEnd )
	        {
                UInt32 err = (UInt32)prError.prOK;

                /* Start Viewfinder */
                pr_ViewFinderCB = new prType.prViewFinderCB(PR_ViewFinderCallBackFunc);
		        err = prAPI.PR_RC_StartViewFinder(pr_hCamera,
										     0,
										     pr_ViewFinderCB);
		        if (err != (UInt32)prError.prOK)
		        {
			        HandleError("StartViewFinder",err);
		        }

                /* The viewfinder is started */
                m_fVFEnd = true;
                
                // Create delegate
                if (this.StatusChanged != null)
                {
                    this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                             (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                             (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
                }

	        }
        }
        private void CDSDK_StartLiveViewer()
        {
            UInt32 err;

            /* UI is locked so that information may not be changed. */
            err = cdAPI.CDLockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            if (!m_fVFEnd)
            {
                /* The thread which displays view finder is created. */
                m_fVFEnd = true;

                /* A view finder is started. */
                cd_ViewFinderCallBackDelegate = new cdType.cdViewFinderCallbackFunction(CD_ViewFinderCallBackFunc);
                err = cdAPI.CDStartViewfinder(cd_hSource, cdType.FILEFORMAT_BMP, cd_ViewFinderCallBackDelegate, 0);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }
            }

            /* The lock of UI is canceled. */
            err = cdAPI.CDUnlockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }

            return;

        camerr:
            Console.WriteLine("ViewFinder error: " + err.ToString());
            cdAPI.CDUnlockUI(cd_hSource);
        }

        public void StopLiveViewer()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_StopLiveViewer();
            }
            else
            {
                PRSDK_StopLiveViewer();
            }
        }
        private void PRSDK_StopLiveViewer()
        {
	        if (m_fVFEnd)
            {
		        /* Terminate Viewfinder */
		        prAPI.PR_RC_TermViewFinder(pr_hCamera);

                /* The view finder is finished */
                m_fVFEnd = false;

		        // Create delegate
                if (this.StatusChanged != null)
                {
                    this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                             (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                             (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
                }
            }
        }
        private void CDSDK_StopLiveViewer()
        {
            // TODO: Add your control notification handler code here
            UInt32 err;

            if (m_fVFEnd)
            {
                /* UI is locked so that information may not be changed. */
                err = cdAPI.CDLockUI(cd_hSource);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                /* A view finder is ended. */
                err = cdAPI.CDTermViewfinder(cd_hSource);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                /* A thread is ended. */
                m_fVFEnd = false;

                /* The lock of UI is canceled. */
                err = cdAPI.CDUnlockUI(cd_hSource);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                // Create delegate
                if (this.StatusChanged != null)
                {
                    this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                             (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                             (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
                }

            }


            return;

        camerr:
            Console.WriteLine("ViewFinder error: " + err.ToString());
            cdAPI.CDUnlockUI(cd_hSource);
        }

	    #endregion

		#region AE/AF
        public void UpdateAE()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AE);
            }
            else
            {
                PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AE);
            }
        }
        public void UpdateAF()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AF);
            }
            else
            {
                PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AF);
            }
        }
        public void UpdateAWB()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AWB);
            }
            else
            {
                PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AWB);
            }
        }
		public void UpdateAEAF()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AE | 
                                 cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AF);
            }
            else
            {
                PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AE | 
                                 prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AF);
            }
		}
        public void UpdateAEAWB()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AE | 
                                 cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AWB);
            }
            else
            {
                PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AE | 
                                 prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AWB);
            }
        }
        public void UpdateAFAWB()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AF | 
                                 cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AWB);
            }
            else
            {
                PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AF | 
                                 prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AWB);
            }
        }
        public void UpdateAEAFAWB()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AE | 
                                 cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AF | 
                                 cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AWB);
            }
            else
            {
                PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AE | 
                                 prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AF | 
                                 prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AWB);
            }
        }
       
        private void PRSDK_UpdateAEAF(prType.prptpAeAfAwbResetFlag setting)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Update AE, AF, AWB. */
            //prType.prptpAeAfAwbResetFlag setting = prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AE |
            //                                       prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AF |
            //                                       prType.prptpAeAfAwbResetFlag.prptpAEAFAWB_RESET_AWB;
            err = prAPI.PR_RC_DoAeAfAwb(pr_hCamera, setting);
            if (err != (UInt32)prError.prOK)
            {
                HandleError("LockAF error", err);
            }
        }
        private void CDSDK_UpdateAEAF(cdType.cdAeAfAwbResetFlag setting)
        {
            UInt32 err = (UInt32)cdError.cdOK;
            //cdType.cdAeAfAwbResetFlag setting = cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AE |
            //                                    cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AF |
            //                                    cdType.cdAeAfAwbResetFlag.cdAEAFAWB_RESET_AWB;

            /* UI is locked so that information may not be changed. */
            err = cdAPI.CDLockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* AE and AF are readjusted. */
            err = cdAPI.CDActViewfinderAutoFunctions(cd_hSource, setting);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* The lock of UI is canceled. */
            err = cdAPI.CDUnlockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return;

        camerr:
            Console.WriteLine("UpdateAEAF error: " + err.ToString());
            cdAPI.CDUnlockUI(cd_hSource);
        }

		public void LockAF()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_LockAF();
            }
            else
            {
                PRSDK_LockAF();
            }

		}
        private void PRSDK_LockAF()
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Lock the focus. */
            err = prAPI.PR_RC_FocusLock(pr_hCamera);
            if (err != (UInt32)prError.prOK)
            {
                HandleError("LockAF error", err);
            }
        }
        private void CDSDK_LockAF()
        {
            UInt32 err;

            /* Lock the focus. */
            err = (UInt32)cdAPI.CDAFLock(cd_hSource, (UInt32)cdError.cdTRUE);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return;

        camerr:
            Console.WriteLine("LockAF error: " + err.ToString());
        }

		public void UnlockAF()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_UnlockAF();
            }
            else
            {
                PRSDK_UnlockAF();
            }

		}
        private void PRSDK_UnlockAF()
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Unlock the focus. */
            err = prAPI.PR_RC_FocusUnlock(pr_hCamera);
            if (err != (UInt32)prError.prOK)
            {
                HandleError("LockAF error", err);
            }
        }
        private void CDSDK_UnlockAF()
        {
            UInt32 err;

            /* Lock the focus. */
            err = (UInt32)cdAPI.CDAFLock(cd_hSource, (UInt32)cdError.cdFALSE);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return;

        camerr:
            Console.WriteLine("UnlockAF error: " + err.ToString());
        }
		#endregion

		#region Zoom
        private bool PRSDK_InitZoom()
        {
            UInt32 err = (UInt32)prError.prOK;
            UInt16 DZoomStart = 0;
            UInt16 minZoom = 0;
            UInt16 maxZoom = 0;
            UInt16 zoomStep = 0;
            UInt16 zoomPos = 0;

            // Get the zoom range
            err = PRSDK_GetZoomRange(ref minZoom,ref maxZoom,ref zoomStep);
            if (err != (UInt32)prError.prOK)
            {
                return false;
            }

            // Get the max optical zoom
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_EZOOM_START_POS,
                                                 prType.prptpDevicePropDataType.prUInt16, ref DZoomStart);
            if (err != (UInt32)prError.prOK)
            {
                return false;
            }

            // Get the current zoom position
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_ZOOM_POS,
                                                 prType.prptpDevicePropDataType.prUInt16, ref zoomPos);
            if (err != (UInt32)prError.prOK)
            {
                return false;
            }

            // Store the output
            m_MaxZoomPos = maxZoom;
            m_MaxOpticalZoomPos = (UInt32)(DZoomStart - zoomStep);
            m_ZoomPos = zoomPos;
                                      
            return true;
        }
        private bool CDSDK_InitZoom()
        {
            UInt32 err;

            /* Get maximum zoom positions */
            err = cdAPI.CDGetMaximumZoomPos(cd_hSource, ref m_MaxZoomPos, ref m_MaxOpticalZoomPos);
            if (err != (UInt32)cdError.cdOK)
            {
                return false;
            }

            /* Get current zoom positions */
            err = cdAPI.CDGetZoomPos(cd_hSource, ref m_ZoomPos);
            if (err != (UInt32)cdError.cdOK)
            {
                return false;
            }

            /* Get current digital zoom positions */
            err = cdAPI.CDGetDZoomMagnification(cd_hSource, ref m_DZoomPos);
            if (err != (UInt32)cdError.cdOK)
            {
                return false;
            }

            return true;
        }
      
        private void PRSDK_SetZoom(UInt32 zoomPos)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pZoomPos = Marshal.AllocHGlobal(sizeof(UInt16));
            Marshal.StructureToPtr((UInt16)zoomPos, pZoomPos, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_ZOOM_POS,
                                              sizeof(UInt16),
                                              pZoomPos);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pZoomPos);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored zoom position
            m_ZoomPos = zoomPos;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;

        }
        private void CDSDK_SetZoom(UInt32 zoomPos)
        {
            UInt32 err;

            /* UI is locked so that information may not be changed. */
            err = cdAPI.CDLockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* Set zoom position. */
            err = cdAPI.CDSetZoomPos(cd_hSource, zoomPos);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            /* The lock of UI is canceled. */
            err = cdAPI.CDUnlockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // We got here without any errors, set the stored zoom position
            m_ZoomPos = zoomPos;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;

        camerr:
            Console.WriteLine("SetZoom error: " + err.ToString());
            cdAPI.CDUnlockUI(cd_hSource);
        }
		#endregion

		#region Image Formats
        public Dictionary<string, UInt32> GetImageSizeFormats()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetImageSizeFormats();
            }
            else
            {
                return PRSDK_GetImageSizeFormats();
            }
		}
        private Dictionary<string, UInt32> PRSDK_GetImageSizeFormats()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, UInt32> sizes = new Dictionary<string, UInt32>();
            List<byte> al_Sizes = new List<byte>();

            // Get the current value
            byte currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_IMAGE_SIZE,
                                                 prType.prptpDevicePropDataType.prUInt8, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_ImageSize = (UInt16)currentVal;

            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_IMAGE_SIZE, 
                                             prType.prptpDevicePropDataType.prUInt8, ref al_Sizes);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_Sizes.Count>0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach(byte val in al_Sizes)
                {
                    // If the value isn't in the hashtable, add it
                    string key = ((prType.prptpImageSize)val).ToString();
                    if (!sizes.ContainsKey(key))
                    {
                        sizes.Add(key, val);
                    }
                }
                return sizes;
            }
            else
            {
                return null;
            }
        }
        private Dictionary<string, UInt32> CDSDK_GetImageSizeFormats()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            cdType.cdImageSize CurrSize = cdType.cdImageSize.cdIMAGE_SIZE_UNKNOWN;
            cdType.cdImageQuality CurrQuality = cdType.cdImageQuality.cdIMAGE_QUALITY_UNKNOWN;
            cdType.cdImageSize SizePossibleValue = cdType.cdImageSize.cdIMAGE_SIZE_UNKNOWN;
            cdType.cdImageQuality QualityPossibleValue = cdType.cdImageQuality.cdIMAGE_QUALITY_UNKNOWN;
            UInt32 hEnumPossibleValue = 0;

            Dictionary<string, UInt32> sizes = new Dictionary<string, UInt32>();

            // Get current setting for default value.
            err = cdAPI.CDGetImageFormatAttribute(cd_hSource, ref CurrQuality, ref CurrSize);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }
            m_ImageSize = (UInt16)CurrSize;
            m_ImageQuality = (prType.prptpImageQuality) CurrQuality;

            // Get all shooting modes which can be set to camera,
            // and set the list box.
            err = cdAPI.CDEnumImageFormatAttributeReset(cd_hSource, ref hEnumPossibleValue);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = cdAPI.CDEnumImageFormatAttributeNext(hEnumPossibleValue, ref QualityPossibleValue,
                                                                                   ref SizePossibleValue)) == (UInt32)cdError.cdOK)
            {
                string size;
                switch (SizePossibleValue)
                {
                    case cdType.cdImageSize.cdIMAGE_SIZE_UNKNOWN: size = "(Unknown)"; break;
                    case cdType.cdImageSize.cdIMAGE_SIZE_LARGE: size = "Large"; break;
                    case cdType.cdImageSize.cdIMAGE_SIZE_MEDIUM: size = "Medium"; break;
                    case cdType.cdImageSize.cdIMAGE_SIZE_MEDIUM1: size = "Medium1"; break;
                    case cdType.cdImageSize.cdIMAGE_SIZE_MEDIUM2: size = "Medium2"; break;
                    case cdType.cdImageSize.cdIMAGE_SIZE_MEDIUM3: size = "Medium3"; break;
                    case cdType.cdImageSize.cdIMAGE_SIZE_SMALL: size = "Small"; break;
                    default: size = "(Error)"; break;
                };
                // If the value isn't in the hashtable, add it
                if (!sizes.ContainsKey(size))
                {
                    sizes.Add(size, (UInt16)SizePossibleValue);
                }
            }

            err = cdAPI.CDEnumImageFormatAttributeRelease(hEnumPossibleValue);
            hEnumPossibleValue = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return sizes;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumImageFormatAttributeRelease(hEnumPossibleValue);
            }

            Console.WriteLine("GetImageSizeFormats error: " + err.ToString());
            return null;
        }

        public Dictionary<string, prType.prptpImageQuality> GetImageQualityFormats()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetImageQualityFormats();
            }
            else
            {
                return PRSDK_GetImageQualityFormats();
            }
		}
        private Dictionary<string, prType.prptpImageQuality> PRSDK_GetImageQualityFormats()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, prType.prptpImageQuality> qualities = new Dictionary<string, prType.prptpImageQuality>();
            List<byte> al_Quality = new List<byte>();

            // Get the current value
            byte currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_COMP_QUALITY, 
                                                 prType.prptpDevicePropDataType.prUInt8, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_ImageQuality = (prType.prptpImageQuality) currentVal;


            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_COMP_QUALITY,
                                             prType.prptpDevicePropDataType.prUInt8, ref al_Quality);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_Quality.Count > 0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (prType.prptpImageQuality val in al_Quality)
                {
                    // If the value isn't in the hashtable, add it
                    string key = ((prType.prptpImageQuality)val).ToString();
                    if (!qualities.ContainsKey(key))
                    {
                        qualities.Add(key, val);
                    }
                }
                return qualities;
            }
            else
            {
                return null;
            }
        }
        private Dictionary<string, prType.prptpImageQuality> CDSDK_GetImageQualityFormats()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            cdType.cdImageSize CurrSize = cdType.cdImageSize.cdIMAGE_SIZE_UNKNOWN;
            cdType.cdImageQuality CurrQuality = cdType.cdImageQuality.cdIMAGE_QUALITY_UNKNOWN;
            cdType.cdImageSize SizePossibleValue = cdType.cdImageSize.cdIMAGE_SIZE_UNKNOWN;
            cdType.cdImageQuality QualityPossibleValue = cdType.cdImageQuality.cdIMAGE_QUALITY_UNKNOWN;
            UInt32 hEnumPossibleValue = 0;

            Dictionary<string, prType.prptpImageQuality> qualities = new Dictionary<string, prType.prptpImageQuality>();

            // Get current setting for default value.
            err = cdAPI.CDGetImageFormatAttribute(cd_hSource, ref CurrQuality, ref CurrSize);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }
            m_ImageSize = (UInt16)CurrSize;
            m_ImageQuality = (prType.prptpImageQuality)CurrQuality;

            // Get all shooting modes which can be set to camera,
            // and set the list box.
            err = cdAPI.CDEnumImageFormatAttributeReset(cd_hSource, ref hEnumPossibleValue);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = (UInt32)cdAPI.CDEnumImageFormatAttributeNext(hEnumPossibleValue, ref QualityPossibleValue, ref SizePossibleValue)) == (UInt32)cdError.cdOK)
            {
                string quality;
                switch (QualityPossibleValue)
                {
                    case cdType.cdImageQuality.cdIMAGE_QUALITY_UNKNOWN: quality = "(Unknown)"; break;
                    case cdType.cdImageQuality.cdIMAGE_QUALITY_ECONOMY: quality = "Economy"; break;
                    case cdType.cdImageQuality.cdIMAGE_QUALITY_FINE: quality = "Fine"; break;
                    case cdType.cdImageQuality.cdIMAGE_QUALITY_NORMAL: quality = "Normal"; break;
                    case cdType.cdImageQuality.cdIMAGE_QUALITY_RAW: quality = "Raw"; break;
                    case cdType.cdImageQuality.cdIMAGE_QUALITY_SUPERFINE: quality = "Superfine"; break;
                    default: quality = "Unknown"; break;
                };
                // If the value isn't in the hashtable, add it
                if (!qualities.ContainsKey(quality))
                {
                    qualities.Add(quality, (prType.prptpImageQuality)QualityPossibleValue);
                }
            }

            err = cdAPI.CDEnumImageFormatAttributeRelease(hEnumPossibleValue);
            hEnumPossibleValue = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return qualities;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumImageFormatAttributeRelease(hEnumPossibleValue);
            }

            Console.WriteLine("GetImageQualityFormats error: " + err.ToString());
            return null;
        }

        private void SetImageFormat(UInt16 size, prType.prptpImageQuality quality)
		{
            bool vf = m_fVFEnd;

            if (m_fVFEnd)
            {
                StopLiveViewer();
            }

            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetImageFormat((cdType.cdImageSize)size,(cdType.cdImageQuality)quality);
            }
            else
            {
                PRSDK_SetImageFormat((prType.prptpImageSize) size, (prType.prptpImageQuality) quality);
            }

            if (vf)
            {
                StartLiveViewer();
            }
		}
        private void PRSDK_SetImageFormat(prType.prptpImageSize size, prType.prptpImageQuality quality)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the image size */
            IntPtr pSize = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.StructureToPtr((byte)size, pSize, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_IMAGE_SIZE,
                                              sizeof(byte),
                                              pSize);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pSize);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored image size 
            m_ImageSize = (UInt16)size;

            /* Allocate memory to marshal the image quality */
            IntPtr pQuality = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.StructureToPtr((byte)quality, pQuality, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_COMP_QUALITY,
                                              sizeof(byte),
                                              pQuality);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pQuality);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored image quality
            m_ImageQuality = quality;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }
        private void CDSDK_SetImageFormat(cdType.cdImageSize size, cdType.cdImageQuality quality)
        {
            UInt32 err = cdAPI.CDSetImageFormatAttribute(cd_hSource, quality,
                                                         size);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // Set the data members
            m_ImageSize = (UInt16)size;
            m_ImageQuality = (CameraSDK.prType.prptpImageQuality)quality;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }

            return;

        camerr:
            Console.WriteLine("SetImageFormat error: " + err.ToString());
        }
		#endregion

		#region ShootingMode
        public Dictionary<string, int> GetShootingModes()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetShootingModes();
            }
            else
            {
                return PRSDK_GetShootingModes();
            }
		}
        private Dictionary<string, int> PRSDK_GetShootingModes()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, int> shootingModes = new Dictionary<string, int>();
            List<byte> al_ShootingModes = new List<byte>();

            // Get the current value
            byte currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_MODE,
                                                 prType.prptpDevicePropDataType.prUInt8, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_ShootingMode = (UInt16)currentVal;


            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_MODE,
                                             prType.prptpDevicePropDataType.prUInt8, ref al_ShootingModes);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_ShootingModes.Count > 0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (byte val in al_ShootingModes)
                {
                    // If the value isn't in the hashtable, add it
                    string key = ((prType.prptpShootingMode)val).ToString();
                    if (!shootingModes.ContainsKey(key))
                    {
                        shootingModes.Add(key, val);
                    }
                }
                return shootingModes;
            }
            else
            {
                return null;
            }
        }
        private Dictionary<string, int> CDSDK_GetShootingModes()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt16 CurrValue = (UInt16)cdType.cdShootingMode.cdSHOOTING_MODE_INVALID;
            UInt16 PossibleValue = (UInt16)cdType.cdShootingMode.cdSHOOTING_MODE_INVALID;
            UInt32 hEnumPossibleValue = 0;

            Dictionary<string, int> modes = new Dictionary<string, int>();

            // Get current setting for default value.
            err = cdAPI.CDGetShootingMode(cd_hSource, ref CurrValue);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }
            m_ShootingMode = CurrValue;

            // Get all shooting modes which can be set to camera,
            // and set the list box.
            err = cdAPI.CDEnumShootingModeReset(cd_hSource, ref hEnumPossibleValue);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = (UInt32)cdAPI.CDEnumShootingModeNext(hEnumPossibleValue, ref PossibleValue)) == (UInt32)cdError.cdOK)
            {
                string mode;
                switch ((cdType.cdShootingMode)PossibleValue)
                {
                    case cdType.cdShootingMode.cdSHOOTING_MODE_INVALID: mode = "(Invalid)"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_AUTO: mode = "Auto"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_PROGRAM: mode = "Program"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_TV: mode = "Tv"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_AV: mode = "Av"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_MANUAL: mode = "Manual"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_A_DEP: mode = "A_DEP"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_M_DEP: mode = "M_DEP"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_BULB: mode = "Bulb"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_MANUAL_2: mode = "Manual 2"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_FAR_SCENE: mode = "Far Scene"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_FAST_SHUTTER: mode = "Fast Shutter"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_SLOW_SHUTTER: mode = "Slow Shutter"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_NIGHT_SCENE: mode = "Night Scene"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_GRAY_SCALE: mode = "Gray Scale"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_SEPIA: mode = "Sepia"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_PORTRAIT: mode = "Portrait"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_SPOT: mode = "Spot"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_MACRO: mode = "Macro"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_BW: mode = "BW"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_PANFOCUS: mode = "Panfocus"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_VIVID: mode = "Vivid"; break;
                    case cdType.cdShootingMode.cdSHOOTING_MODE_NEUTRAL: mode = "Neutral"; break;
                    default: mode = "(Error)"; break;
                };
                // If the value isn't in the hashtable, add it
                if (!modes.ContainsKey(mode))
                {
                    modes.Add(mode, PossibleValue);
                }  
            }

            err = cdAPI.CDEnumShootingModeRelease(hEnumPossibleValue);
            hEnumPossibleValue = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return modes;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumShootingModeRelease(hEnumPossibleValue);
            }

            Console.WriteLine("GetShootingModes error: " + err.ToString());
            return null;
        }

		private void SetShootingMode(UInt16 shootingMode)
		{
            bool vf = m_fVFEnd;

            if (m_fVFEnd)
            {
                StopLiveViewer();
            }

            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetShootingMode((cdType.cdShootingMode)shootingMode);
            }
            else
            {
                PRSDK_SetShootingMode((prType.prptpShootingMode)shootingMode);
            }

            if (vf)
            {
                StartLiveViewer();
            }
		}
        private void PRSDK_SetShootingMode(prType.prptpShootingMode shootingMode)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pShootingMode = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.StructureToPtr((byte)shootingMode, pShootingMode, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_MODE,
                                              sizeof(byte),
                                              pShootingMode);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pShootingMode);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored shooting mode
            m_ShootingMode = (UInt16)shootingMode;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }
        private void CDSDK_SetShootingMode(cdType.cdShootingMode shootingMode)
        {
            UInt32 err = (UInt32)cdError.cdOK;

            // Get current setting for default value.
            err = cdAPI.CDSetShootingMode(cd_hSource, (UInt16)shootingMode);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // No error, set the current value
            m_ShootingMode = (UInt16)shootingMode;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }

            return;

        camerr:

            Console.WriteLine("SetShootingMode error: " + err.ToString());
            return;
        }
 
        private UInt16 GetAvValue()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetAvValue();
            }
            else
            {
                return PRSDK_GetAvValue();
            }
        }
        private UInt16 PRSDK_GetAvValue()
        {
            UInt32 err = (UInt32)prError.prOK;
            ushort currentVal = (UInt16)prType.prptpAperture.prREMOTE_SET_AV_NA;

            // Get the current value
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_AV,
                                                 prType.prptpDevicePropDataType.prUInt16, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return (UInt16)prType.prptpAperture.prREMOTE_SET_AV_NA;
            }
            
            // No error, set the current value
            return (UInt16)currentVal;

        }
        private UInt16 CDSDK_GetAvValue()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt16 CurrValue = (UInt16)cdType.cdAperture.cdREMOTE_SET_AV_NA;

            // Get current setting for default value.
            err = cdAPI.CDGetAvValue(cd_hSource, ref CurrValue);
            if (err != (UInt32)cdError.cdOK)
            {
                return (UInt16)cdType.cdAperture.cdREMOTE_SET_AV_NA;
            }
            return CurrValue;
        }

        private void SetAvValue(UInt16 Av)
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetAvValue((cdType.cdAperture)Av);
            }
            else
            {
                PRSDK_SetAvValue((prType.prptpAperture)Av);
            }
        }
        private void PRSDK_SetAvValue(prType.prptpAperture Av)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the AV value */
            IntPtr pAv = Marshal.AllocHGlobal(sizeof(UInt16));
            Marshal.StructureToPtr((UInt16)Av, pAv, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_AV,
                                              sizeof(UInt16),
                                              pAv);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pAv);

            if (err != (UInt32)prError.prOK)
            {
                Console.WriteLine("SetAvValue error: " + err.ToString());
            }

        }
        private void CDSDK_SetAvValue(cdType.cdAperture Av)
        {
            UInt32 err = (UInt32)cdError.cdOK;

            // Get current setting for default value.
            err = cdAPI.CDSetAvValue(cd_hSource, (UInt16)Av);
            if (err != (UInt32)cdError.cdOK)
            {
                Console.WriteLine("SetAvValue error: " + err.ToString());
            }
        }

        private UInt16 GetTvValue()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetTvValue();
            }
            else
            {
                return PRSDK_GetTvValue();
            }
        }
        private UInt16 PRSDK_GetTvValue()
        {
            UInt32 err = (UInt32)prError.prOK;
            ushort currentVal = (UInt16)prType.prptpShutterSpeed.prREMOTE_SET_TV_NA;

            // Get the current value
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_TV,
                                                 prType.prptpDevicePropDataType.prUInt16, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return (UInt16)prType.prptpShutterSpeed.prREMOTE_SET_TV_NA;
            }

            // No error, set the current value
            return (UInt16)currentVal;
        }
        private UInt16 CDSDK_GetTvValue()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt16 CurrValue = (UInt16)cdType.cdShutterSpeed.cdREMOTE_SET_TV_NA;

            // Get current setting for default value.
            err = cdAPI.CDGetTvValue(cd_hSource, ref CurrValue);
            if (err != (UInt32)cdError.cdOK)
            {
                return (UInt16)cdType.cdShutterSpeed.cdREMOTE_SET_TV_NA;
            }
            return CurrValue;
        }

        private void SetTvValue(UInt16 Tv)
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetTvValue((cdType.cdShutterSpeed)Tv);
            }
            else
            {
                PRSDK_SetTvValue((prType.prptpShutterSpeed)Tv);
            }
        }
        private void PRSDK_SetTvValue(prType.prptpShutterSpeed Tv)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the TV value */
            IntPtr pTv = Marshal.AllocHGlobal(sizeof(UInt16));
            Marshal.StructureToPtr((UInt16)Tv, pTv, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_TV,
                                              sizeof(UInt16),
                                              pTv);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pTv);

            if (err != (UInt32)prError.prOK)
            {
                Console.WriteLine("SetTvValue error: " + err.ToString());
            }
        }
        private void CDSDK_SetTvValue(cdType.cdShutterSpeed Tv)
        {
            UInt32 err = (UInt32)cdError.cdOK;

            // Get current setting for default value.
            err = cdAPI.CDSetTvValue(cd_hSource, (UInt16)Tv);
            if (err != (UInt32)cdError.cdOK)
            {
                Console.WriteLine("SetTvValue error: " + err.ToString());
            }
        }


		#endregion

		#region Exposure
        public Dictionary<string, ushort> GetExposureCompensation()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetExposureCompensation();
            }
            else
            {
                return PRSDK_GetExposureCompensation();
            }
		}
        private Dictionary<string, ushort> PRSDK_GetExposureCompensation()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, ushort> exposures = new Dictionary<string, ushort>();
            List<byte> al_Exposures = new List<byte>();

            // Get the current value
            byte currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_COMP,
                                                 prType.prptpDevicePropDataType.prUInt8, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_ExposureCompensation = (UInt16)currentVal;


            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_COMP,
                                             prType.prptpDevicePropDataType.prUInt8, ref al_Exposures);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_Exposures.Count > 0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (byte val in al_Exposures)
                {
                    string expo;
                    switch ((prType.prptpExposureCompensation)val)
                    {
                        case prType.prptpExposureCompensation.prCOMP_300_PLUS: expo = "+ 3"; break;
                        case prType.prptpExposureCompensation.prCOMP_266_PLUS: expo = "+ 2 2/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_250_PLUS: expo = "+ 2 1/2"; break;
                        case prType.prptpExposureCompensation.prCOMP_233_PLUS: expo = "+ 2 1/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_200_PLUS: expo = "+ 2"; break;
                        case prType.prptpExposureCompensation.prCOMP_166_PLUS: expo = "+ 1 2/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_150_PLUS: expo = "+ 1 1/2"; break;
                        case prType.prptpExposureCompensation.prCOMP_133_PLUS: expo = "+ 1 1/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_100_PLUS: expo = "+ 1"; break;
                        case prType.prptpExposureCompensation.prCOMP_066_PLUS: expo = "+ 2/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_050_PLUS: expo = "+ 1/2"; break;
                        case prType.prptpExposureCompensation.prCOMP_033_PLUS: expo = "+ 1/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_000_PLUS: expo = "0"; break;
                        case prType.prptpExposureCompensation.prCOMP_033_MINUS: expo = "- 1/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_050_MINUS: expo = "- 1/2"; break;
                        case prType.prptpExposureCompensation.prCOMP_066_MINUS: expo = "- 2/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_100_MINUS: expo = "- 1"; break;
                        case prType.prptpExposureCompensation.prCOMP_133_MINUS: expo = "- 1 1/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_150_MINUS: expo = "- 1 1/2"; break;
                        case prType.prptpExposureCompensation.prCOMP_166_MINUS: expo = "- 1 2/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_200_MINUS: expo = "- 2"; break;
                        case prType.prptpExposureCompensation.prCOMP_233_MINUS: expo = "- 2 1/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_250_MINUS: expo = "- 2 1/2"; break;
                        case prType.prptpExposureCompensation.prCOMP_266_MINUS: expo = "- 2 2/3"; break;
                        case prType.prptpExposureCompensation.prCOMP_300_MINUS: expo = "- 3"; break;
                        default: expo = "(Error)"; break;
                    }
                    // If the value isn't in the hashtable, add it
                    if (!exposures.ContainsKey(expo))
                    {
                        exposures.Add(expo, val);
                    }  
                }
                return exposures;
            }
            else
            {
                return null;
            }
        }
        private Dictionary<string, ushort> CDSDK_GetExposureCompensation()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt16 CurrValue = (UInt16)cdType.cdExposureCompensation.cdCOMP_NA;
            UInt16 PossibleValue = (UInt16)cdType.cdExposureCompensation.cdCOMP_NA;
            UInt32 hEnumPossibleValue = 0;

            Dictionary<string, ushort> exposures = new Dictionary<string, ushort>();

            // Get current setting for default value.
            err = cdAPI.CDGetExposureComp(cd_hSource, ref CurrValue);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // No error, set the current value
            m_ExposureCompensation = CurrValue;

            // Get all exposure compensation values which can be set to camera,
            // and set the list box.
            err = cdAPI.CDEnumExposureCompReset(cd_hSource, ref hEnumPossibleValue);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = (UInt32)cdAPI.CDEnumExposureCompNext(hEnumPossibleValue, ref PossibleValue)) == (UInt32)cdError.cdOK)
            {
                string expo;
                switch ((cdType.cdExposureCompensation)PossibleValue)
                {
                    case cdType.cdExposureCompensation.cdCOMP_300_PLUS: expo = "+ 3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_266_PLUS: expo = "+ 2 2/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_250_PLUS: expo = "+ 2 1/2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_233_PLUS: expo = "+ 2 1/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_200_PLUS: expo = "+ 2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_166_PLUS: expo = "+ 1 2/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_150_PLUS: expo = "+ 1 1/2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_133_PLUS: expo = "+ 1 1/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_100_PLUS: expo = "+ 1"; break;
                    case cdType.cdExposureCompensation.cdCOMP_066_PLUS: expo = "+ 2/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_050_PLUS: expo = "+ 1/2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_033_PLUS: expo = "+ 1/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_000_PLUS: expo = "0"; break;
                    case cdType.cdExposureCompensation.cdCOMP_033_MINUS: expo = "- 1/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_050_MINUS: expo = "- 1/2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_066_MINUS: expo = "- 2/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_100_MINUS: expo = "- 1"; break;
                    case cdType.cdExposureCompensation.cdCOMP_133_MINUS: expo = "- 1 1/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_150_MINUS: expo = "- 1 1/2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_166_MINUS: expo = "- 1 2/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_200_MINUS: expo = "- 2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_233_MINUS: expo = "- 2 1/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_250_MINUS: expo = "- 2 1/2"; break;
                    case cdType.cdExposureCompensation.cdCOMP_266_MINUS: expo = "- 2 2/3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_300_MINUS: expo = "- 3"; break;
                    case cdType.cdExposureCompensation.cdCOMP_NA: expo = "(NA)"; break;
                    default: expo = "(Error)"; break;
                };
                // If the value isn't in the hashtable, add it
                if (!exposures.ContainsKey(expo))
                {
                    exposures.Add(expo, PossibleValue);
                }  
            }

            err = cdAPI.CDEnumExposureCompRelease(hEnumPossibleValue);
            hEnumPossibleValue = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return exposures;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumExposureCompRelease(hEnumPossibleValue);
            }

            Console.WriteLine("GetExposureCompensation error: " + err.ToString());

            return null;
        }

		private void SetExposureCompensation(UInt16 expComp)
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetExposureCompensation((cdType.cdExposureCompensation)expComp);
            }
            else
            {
                PRSDK_SetExposureCompensation((prType.prptpExposureCompensation)expComp);
            }
		}
        private void PRSDK_SetExposureCompensation(prType.prptpExposureCompensation expComp)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pExpComp = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.StructureToPtr((byte)expComp, pExpComp, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_COMP,
                                              sizeof(byte),
                                              pExpComp);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pExpComp);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the exposure compensation
            m_ExposureCompensation = (UInt16)expComp;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }
        private void CDSDK_SetExposureCompensation(cdType.cdExposureCompensation expComp)
        {
            UInt32 err = cdAPI.CDSetExposureComp(cd_hSource, (UInt16)expComp);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }
           // No error, set the current value
            m_ExposureCompensation = (UInt16)expComp;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;

        camerr:
            Console.WriteLine("SetExposureCompensation error: " + err.ToString());

            return;
        }

        #endregion

		#region Flash
        public Dictionary<string, ushort> GetFlashModes()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetFlashModes();
            }
            else
            {
                return PRSDK_GetFlashModes();
            }
		}
        private Dictionary<string, ushort> PRSDK_GetFlashModes()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, ushort> flashModes = new Dictionary<string, ushort>();
            List<byte> al_FlashModes = new List<byte>();

            // Get the current value
            byte currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_STROBE_SETTING,
                                                 prType.prptpDevicePropDataType.prUInt8, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_FlashMode = (UInt16)currentVal;


            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_STROBE_SETTING,
                                             prType.prptpDevicePropDataType.prUInt8, ref al_FlashModes);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_FlashModes.Count > 0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (byte val in al_FlashModes)
                {
                    string strobe = String.Empty;
                    switch ((prType.prptpStrobeSetting)val)
                    {
                        case prType.prptpStrobeSetting.Auto:
                            strobe = "Auto";
                            break;
                        case prType.prptpStrobeSetting.AutoRedEye:
                            strobe = "Auto (Red Eye)";
                            break;
                        case prType.prptpStrobeSetting.Off:
                            strobe = "Off";
                            break;
                        case prType.prptpStrobeSetting.On:
                            strobe = "On";
                            break;
                        case prType.prptpStrobeSetting.OnRedEye:
                            strobe = "On (Red Eye)";
                            break;
                        case prType.prptpStrobeSetting.RedEye:
                            strobe = "Red Eye";
                            break;
                        case prType.prptpStrobeSetting.SlowSync:
                            strobe = "Slow Sync.";
                            break;
                        default:
                            strobe = "(Error)";
                            break;
                    }

                    // If the value isn't in the hashtable, add it
                    if (!flashModes.ContainsKey(strobe))
                    {
                        flashModes.Add(strobe, val);
                    }
                }
                return flashModes;
            }
            else
            {
                return null;
            }
        }
        private Dictionary<string, ushort> CDSDK_GetFlashModes()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt16 CurrValue = (UInt16)cdType.cdFlashMode.cdFLASH_MODE_NA;
            UInt16 PossibleValue = (UInt16)cdType.cdFlashMode.cdFLASH_MODE_NA;
            UInt32 hEnumPossibleValue = 0;
            UInt16 Comp = (UInt16)cdType.cdExposureCompensation.cdCOMP_NA;

            Dictionary<string, ushort> flashModes = new Dictionary<string, ushort>();

            // Get current setting for default value.
            err = cdAPI.CDGetFlashSetting(cd_hSource, ref CurrValue, ref Comp);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            m_FlashMode = CurrValue;

            // Get all flash settings which can be set to camera,
            // and set the list box.
            err = cdAPI.CDEnumFlashSettingReset(cd_hSource, ref hEnumPossibleValue);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = cdAPI.CDEnumFlashSettingNext(hEnumPossibleValue, ref PossibleValue)) == (UInt32)cdError.cdOK)
            {
                string flash;
                switch ((cdType.cdFlashMode)PossibleValue)
                {
                    case cdType.cdFlashMode.cdFLASH_MODE_OFF: flash = "Off"; break;
                    case cdType.cdFlashMode.cdFLASH_MODE_AUTO: flash = "Auto"; break;
                    case cdType.cdFlashMode.cdFLASH_MODE_ON: flash = "On"; break;
                    case cdType.cdFlashMode.cdFLASH_MODE_RED_EYE: flash = "Red Eye"; break;
                    case cdType.cdFlashMode.cdFLASH_MODE_SLOW_SYNC: flash = "Slow Sync."; break;
                    case cdType.cdFlashMode.cdFLASH_MODE_AUTO_PLUS_RED_EYE: flash = "Auto (Red Eye)"; break;
                    case cdType.cdFlashMode.cdFLASH_MODE_ON_PLUS_RED_EYE: flash = "On (Red Eye)"; break;
                    case cdType.cdFlashMode.cdFLASH_MODE_NA: flash = "(NA)"; break;
                    default: flash = "(Error)"; break;
                };

                flashModes.Add(flash, PossibleValue);
            }

            err = cdAPI.CDEnumFlashSettingRelease(hEnumPossibleValue);
            hEnumPossibleValue = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return flashModes;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumFlashSettingRelease(hEnumPossibleValue);
            }


            return null;
        }

		private void SetFlashMode(UInt16 flashMode)
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetFlashMode((cdType.cdFlashMode)flashMode);
            }
            else
            {
                PRSDK_SetFlashMode((prType.prptpStrobeSetting)flashMode);
            }
		}
        private void PRSDK_SetFlashMode(prType.prptpStrobeSetting flashMode)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pFlashMode = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.StructureToPtr((byte)flashMode, pFlashMode, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_STROBE_SETTING,
                                              sizeof(byte),
                                              pFlashMode);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pFlashMode);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the flash mode
            m_FlashMode = (UInt16)flashMode;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }
        private void CDSDK_SetFlashMode(cdType.cdFlashMode flashMode)
        {
            UInt32 err = cdAPI.CDSetFlashSetting(cd_hSource, flashMode, (UInt16)cdType.cdExposureCompensation.cdCOMP_NA);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // We got here without any errors, set the stored flash mode
            m_FlashMode = (UInt16)flashMode;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;

        camerr:
            Console.WriteLine("SetFlashMode error: " + err.ToString());
        }
		#endregion

		#region ISO
        public Dictionary<string, UInt16> GetISOSpeeds()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetISOSpeeds();
            }
            else
            {
                return PRSDK_GetISOSpeeds();
            }
		}
        private Dictionary<string, UInt16> PRSDK_GetISOSpeeds()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, UInt16> ISOSpeeds = new Dictionary<string, UInt16>();
            List<byte> al_ISOSpeeds = new List<byte>();

            // Get the current value
            UInt16 currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_ISO,
                                                 prType.prptpDevicePropDataType.prUInt16, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_ISOSpeed = (UInt16)currentVal;


            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_ISO,
                                             prType.prptpDevicePropDataType.prUInt16, ref al_ISOSpeeds);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_ISOSpeeds.Count > 0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (byte val in al_ISOSpeeds)
                {
                    string ISO = String.Empty;
                    switch ((prType.prptpISOSpeed)val)
                    {
                        case prType.prptpISOSpeed.prREL_VAL_ISO_6:
                            ISO = "6";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_8:
                            ISO = "8";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_10:
                            ISO = "10";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_12:
                            ISO = "12";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_16:
                            ISO = "16";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_20:
                            ISO = "20";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_25:
                            ISO = "25";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_32:
                            ISO = "32";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_40:
                            ISO = "40";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_50:
                            ISO = "50";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_64:
                            ISO = "64";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_80:
                            ISO = "80";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_100:
                            ISO = "100";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_125:
                            ISO = "125";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_160:
                            ISO = "160";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_200:
                            ISO = "200";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_250:
                            ISO = "250";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_320:
                            ISO = "320";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_400:
                            ISO = "400";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_500:
                            ISO = "500";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_640:
                            ISO = "640";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_800:
                            ISO = "800";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_1000:
                            ISO = "1000";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_1250:
                            ISO = "1250";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_1600:
                            ISO = "1600";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_2000:
                            ISO = "2000";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_2500:
                            ISO = "2500";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_3200:
                            ISO = "3200";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_4000:
                            ISO = "4000";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_5000:
                            ISO = "5000";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_6400:
                            ISO = "6400";
                            break;
                        case prType.prptpISOSpeed.prREL_VAL_ISO_AUTO:
                            ISO = "Auto";
                            break;
                        default:
                            ISO = "(Error)";
                            break;
                    }

                    // If the value isn't in the hashtable, add it
                    if (!ISOSpeeds.ContainsKey(ISO))
                    {
                        ISOSpeeds.Add(ISO, val);
                    }
                }
                return ISOSpeeds;
            }
            else
            {
                return null;
            }
        }
        private Dictionary<string, UInt16> CDSDK_GetISOSpeeds()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt32 TargetSettingID = cdType.cdREL_SET_ISO_SPEED_RATINGS;
            UInt16 CurrValue = (UInt16)cdType.cdISOSpeed.cdREL_VAL_ISO_AUTO;
            UInt16 PossibleValue = (UInt16)cdType.cdISOSpeed.cdREL_VAL_ISO_AUTO;
            UInt32 hEnumCamSetting = 0;
            UInt32 hEnumPossibleValue = 0;
            UInt32 BufSize = 0;
            cdType.cdRelCamSettingStruct relStruct = new CameraSDK.cdType.cdRelCamSettingStruct();

            Dictionary<string, UInt16> isoSpeeds = new Dictionary<string, UInt16>();

            // Check whether the camera supports the ISO setting. 
            err = cdAPI.CDEnumRelCamSettingReset(cd_hSource, ref hEnumCamSetting);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = cdAPI.CDEnumRelCamSettingNext(hEnumCamSetting, ref relStruct)) == (UInt32)cdError.cdOK)
            {
                if (relStruct.SettingID != TargetSettingID)
                {
                    continue;
                }

                // Camera supports ISO setting. //
                BufSize = 2;

                // Get current setting for default value.
                err = cdAPI.CDGetRelCamSettingData(cd_hSource, TargetSettingID, ref BufSize, ref CurrValue);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                // Get all ISO values which can be set to camera,
                // and set the list box.
                err = cdAPI.CDEnumRelCamSettingDataReset(cd_hSource, TargetSettingID, ref hEnumPossibleValue, ref BufSize);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                if (BufSize == 2)
                {
                    while ((err = cdAPI.CDEnumRelCamSettingDataNext(hEnumPossibleValue, BufSize, ref PossibleValue)) == (UInt32)cdError.cdOK)
                    {
                        string speed;
                        switch ((cdType.cdISOSpeed)PossibleValue)
                        {
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_AUTO: speed = "AUTO"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_6: speed = "6"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_8: speed = "8"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_10: speed = "10"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_12: speed = "12"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_16: speed = "16"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_20: speed = "20"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_25: speed = "25"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_32: speed = "32"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_40: speed = "40"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_50: speed = "50"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_64: speed = "64"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_80: speed = "80"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_100: speed = "100"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_125: speed = "125"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_160: speed = "160"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_200: speed = "200"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_250: speed = "250"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_320: speed = "320"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_400: speed = "400"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_500: speed = "500"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_640: speed = "640"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_800: speed = "800"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_1000: speed = "1000"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_1250: speed = "1250"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_1600: speed = "1600"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_2000: speed = "2000"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_2500: speed = "2500"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_3200: speed = "3200"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_4000: speed = "4000"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_5000: speed = "5000"; break;
                            case cdType.cdISOSpeed.cdREL_VAL_ISO_6400: speed = "6400"; break;
                            default: speed = "(Error)"; break;
                        };

                        isoSpeeds.Add(speed, (UInt16)PossibleValue);
                    }
                }

                err = cdAPI.CDEnumRelCamSettingDataRelease(hEnumPossibleValue);
                hEnumPossibleValue = 0;
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                break;
            }

            m_ISOSpeed = CurrValue;

            err = cdAPI.CDEnumRelCamSettingRelease(hEnumCamSetting);
            hEnumCamSetting = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return isoSpeeds;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumRelCamSettingDataRelease(hEnumPossibleValue);
            }
            if (hEnumCamSetting > 0)
            {
                cdAPI.CDEnumRelCamSettingRelease(hEnumCamSetting);
            }

            return isoSpeeds;
        }

		private void SetISOSpeed(UInt16 ISOSpeed)
		{
            bool vf = m_fVFEnd;

            if (m_fVFEnd)
            {
                StopLiveViewer();
            }
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetISOSpeed((cdType.cdISOSpeed)ISOSpeed);
            }
            else
            {
                PRSDK_SetISOSpeed((prType.prptpISOSpeed)ISOSpeed);
            }

            if (m_fVFEnd)
            {
                StartLiveViewer();
            }
		}

        private void PRSDK_SetISOSpeed(prType.prptpISOSpeed ISOSpeed)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pISOSpeed = Marshal.AllocHGlobal(sizeof(UInt16));
            Marshal.StructureToPtr((UInt16)ISOSpeed, pISOSpeed, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_ISO,
                                              sizeof(UInt16),
                                              pISOSpeed);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pISOSpeed);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored ISOSpeed
            m_ISOSpeed = (UInt16)ISOSpeed;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }
        private void CDSDK_SetISOSpeed(cdType.cdISOSpeed ISOSpeed)
        {
            UInt32 size = 2;
            UInt16 speed = (UInt16)ISOSpeed;

            UInt32 err = cdAPI.CDSetRelCamSettingData(cd_hSource, cdType.cdREL_SET_ISO_SPEED_RATINGS, size, ref speed);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // We got here without any errors, set the stored ISO speed
            m_ISOSpeed = (UInt16)ISOSpeed;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;

        camerr:
            Console.WriteLine("SetISOSpeed error: " + err.ToString());
        }

		#endregion

        #region AF Distance
        public Dictionary<string, byte> GetAFDistances()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetAFDistances();
            }
            else
            {
                return PRSDK_GetAFDistances();
            }
        }

        private Dictionary<string, byte> PRSDK_GetAFDistances()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, byte> afDistances = new Dictionary<string, byte>();
            List<byte> al_AFdistances = new List<byte>();

            // Get the current value
            byte currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_AF_DISTANCE,
                                                 prType.prptpDevicePropDataType.prUInt8, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_AFDistance = (prType.prptpAFDistance)currentVal;

            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_AF_DISTANCE,
                                             prType.prptpDevicePropDataType.prUInt8, ref al_AFdistances);

            if (err == (UInt32)prError.prOK)
            {
                if (al_AFdistances.Count == 0)
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (byte val in al_AFdistances)
                {
                    // If the value isn't in the hashtable, add it
                    string key = ((prType.prptpAFDistance)val).ToString();
                    if (!afDistances.ContainsKey(key))
                    {
                        afDistances.Add(key, val);
                    }
                }
                return afDistances;
            }
            else
            {
                return null;
            }
        }

        private void SetAFDistance(byte value)
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetAFDistance((cdType.cdAFDistance)value);
            }
            else
            {
                PRSDK_SetAFDistance((prType.prptpAFDistance)value);
            }

        }

        private void CDSDK_SetAFDistance(cdType.cdAFDistance cdAFDistance)
        {
            uint err = cdAPI.CDSetAFDistanceSetting(cd_hSource, (ushort)cdAFDistance);

            if ((uint)cdError.cdOK == err)
            {
                m_AFDistance = (prType.prptpAFDistance)cdAFDistance;
            }
        }

        private Dictionary<string, byte> CDSDK_GetAFDistances()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt16 CurrValue = (UInt16) cdType.cdAFDistance.cdAF_DISTANCE_UNKNOWN;
            UInt16 PossibleValue = (UInt16)cdType.cdAFDistance.cdAF_DISTANCE_NA;
            UInt32 hEnumPossibleValue = 0;

            Dictionary<string, byte> modes = new Dictionary<string, byte>();
            // Get current setting for default value.
            err = cdAPI.CDGetAFDistanceSetting(cd_hSource, ref CurrValue);
            if (err != (uint)cdError.cdOK)
            {
                goto camerr;
            }

            // Get all shooting modes which can be set to camera,
            // and set the list box.
            err = cdAPI.CDEnumAFDistanceSettingReset(cd_hSource, ref hEnumPossibleValue);
            if (err != (uint)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = cdAPI.CDEnumAFDistanceSettingNext(hEnumPossibleValue, ref PossibleValue)) == (uint)cdError.cdOK)
            {
                string mode = "";
                switch (PossibleValue)
                {
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_AUTO: mode = "Auto"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_CLOSE: mode = "Close"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_CLOSE_UP: mode = "Close Up"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_FAR: mode = "Far"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_INFINITY: mode = "Infinity"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_MANUAL: mode = "Manual"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_MIDDLE: mode = "Middle"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_NA: mode = "NA"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_PAN_FOCUS: mode = "Pan Focus"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_SUPER_MACRO: mode = "Super Macro"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_UNKNOWN: mode = "Unknown"; break;
                    case (ushort)cdType.cdAFDistance.cdAF_DISTANCE_VERY_CLOSE: mode = "Very close"; break;

                };

                modes[mode] = (byte)PossibleValue;
            }

            err = cdAPI.CDEnumAFDistanceSettingRelease(hEnumPossibleValue);
            hEnumPossibleValue = 0;
            if (err != (uint)cdError.cdOK)
            {
                goto camerr;
            }

            m_AFDistance = (prType.prptpAFDistance)CurrValue;

            return modes;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumAFDistanceSettingRelease(hEnumPossibleValue);
            }

            return null;
        }


        private void PRSDK_SetAFDistance(prType.prptpAFDistance prptpAFDistance)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pDist = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.StructureToPtr((byte)prptpAFDistance, pDist, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_AF_DISTANCE,
                                              sizeof(byte),
                                              pDist);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pDist);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored photo effect
            m_AFDistance = (prType.prptpAFDistance)prptpAFDistance;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }

        #endregion // AF Distance

        #region PhotoEffect
        public Dictionary<string, int> GetPhotoEffects()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                return CDSDK_GetPhotoEffects();
            }
            else
            {
                return PRSDK_GetPhotoEffects();
            }
		}

        private Dictionary<string, int> PRSDK_GetPhotoEffects()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, int> photoEffects = new Dictionary<string, int>();
            List<byte> al_photoEffects = new List<byte>();

            // Get the current value
            UInt16 currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_PHOTO_EFFECT,
                                                 prType.prptpDevicePropDataType.prUInt16, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_PhotoEffect = currentVal;

            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_PHOTO_EFFECT,
                                             prType.prptpDevicePropDataType.prUInt16, ref al_photoEffects);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_photoEffects.Count > 0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (byte val in al_photoEffects)
                {
                    // If the value isn't in the hashtable, add it
                    string key = ((prType.prptpPhotoEffect)val).ToString();
                    if (!photoEffects.ContainsKey(key))
                    {
                        photoEffects.Add(key, val);
                    }
                }
                return photoEffects;
            }
            else
            {
                return null;
            }
        }
        private Dictionary<string, int> CDSDK_GetPhotoEffects()
        {
            UInt32 err = (UInt32)cdError.cdOK;
            UInt32 TargetSettingID = cdType.cdREL_SET_PHOTO_EFFECT;
            UInt16 CurrValue = (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_UNKNOWN;
            UInt16 PossibleValue = (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_UNKNOWN;
            UInt32 hEnumCamSetting = 0;
            UInt32 hEnumPossibleValue = 0;
            UInt32 BufSize = 0;
            cdType.cdRelCamSettingStruct relStruct = new CameraSDK.cdType.cdRelCamSettingStruct();

            Dictionary<string, int> photoEffects = new Dictionary<string, int>();

            // Check whether the camera supports the Photo Effect setting. 
            err = cdAPI.CDEnumRelCamSettingReset(cd_hSource, ref hEnumCamSetting);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            while ((err = cdAPI.CDEnumRelCamSettingNext(hEnumCamSetting, ref relStruct)) == (UInt32)cdError.cdOK)
            {
                if (relStruct.SettingID != TargetSettingID)
                {
                    continue;
                }

                // Camera supports Photo Effect setting. //
                BufSize = 2;

                // Get current setting for default value.
                err = cdAPI.CDGetRelCamSettingData(cd_hSource, TargetSettingID, ref BufSize, ref CurrValue);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                // Get all Photo Effect settings which can be set to camera,
                // and set the list box.
                err = cdAPI.CDEnumRelCamSettingDataReset(cd_hSource, TargetSettingID, ref hEnumPossibleValue, ref BufSize);
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                if (BufSize == 2)
                {
                    while ((err = cdAPI.CDEnumRelCamSettingDataNext(hEnumPossibleValue, BufSize, ref PossibleValue)) == (UInt32)cdError.cdOK)
                    {
                        string effect;
                        switch (PossibleValue)
                        {
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_OFF: effect = "Off"; break;
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_VIVID: effect = "Vivid"; break;
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_NEUTRAL: effect = "Neutral"; break;
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_LOW_SHARPENING: effect = "Low Sharpening"; break;
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_SEPIA: effect = "Sepia"; break;
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_BW: effect = "BW"; break;
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_CUSTOM: effect = "Custom"; break;
                            case (UInt16)cdType.cdPhotoEffect.cdPHOTO_EFFECT_UNKNOWN: effect = "(Unknown)"; break;
                            default: effect = "(Error)"; break;
                        };
                        photoEffects.Add(effect, PossibleValue);
                    }
                }

                err = cdAPI.CDEnumRelCamSettingDataRelease(hEnumPossibleValue);
                hEnumPossibleValue = 0;
                if (err != (UInt32)cdError.cdOK)
                {
                    goto camerr;
                }

                break;
            }

            m_PhotoEffect = CurrValue;

            err = cdAPI.CDEnumRelCamSettingRelease(hEnumCamSetting);
            hEnumCamSetting = 0;
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            return photoEffects;

        camerr:
            if (hEnumPossibleValue > 0)
            {
                cdAPI.CDEnumRelCamSettingDataRelease(hEnumPossibleValue);
            }
            if (hEnumCamSetting > 0)
            {
                cdAPI.CDEnumRelCamSettingRelease(hEnumCamSetting);
            }

            return photoEffects;
        }

		private void SetPhotoEffect(UInt16 photoEffect)
		{
            bool vf = m_fVFEnd;

            if (m_fVFEnd)
            {
                StopLiveViewer();
            }

            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                CDSDK_SetPhotoEffect((cdType.cdPhotoEffect)photoEffect);
            }
            else
            {
                PRSDK_SetPhotoEffect((prType.prptpPhotoEffect)photoEffect);
            }

            if (vf)
            {
                StartLiveViewer();
            }
		}
        private void PRSDK_SetPhotoEffect(prType.prptpPhotoEffect photoEffect)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pPhotoEffect = Marshal.AllocHGlobal(sizeof(UInt16));
            Marshal.StructureToPtr((UInt16)photoEffect, pPhotoEffect, true);

            uint sz = sizeof(UInt16);

            err = prAPI.PR_GetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_PHOTO_EFFECT,
                                              ref sz,
                                              pPhotoEffect);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_PHOTO_EFFECT,
                                              sz,
                                              pPhotoEffect);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pPhotoEffect);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored photo effect
            m_PhotoEffect = (UInt16)photoEffect;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }
        private void CDSDK_SetPhotoEffect(cdType.cdPhotoEffect photoEffect)
        {
            UInt32 size = 2;
            UInt16 effect = (UInt16)photoEffect;
            UInt32 err = cdAPI.CDSetRelCamSettingData(cd_hSource, cdType.cdREL_SET_PHOTO_EFFECT, size, ref  effect);
            if (err != (UInt32)cdError.cdOK)
            {
                goto camerr;
            }

            // Store the photo effect setting
            m_PhotoEffect = (UInt16)photoEffect;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;

        camerr:
            Console.WriteLine("SetPhotoEffect error: " + err.ToString());
        }
		#endregion

        #region Metering
        public Dictionary<string, prType.prptpMetering> GetMeteringModes()
        {
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                // This mode is not supported in the CD SDK
                return null;
            }
            else
            {
                return PRSDK_GetMeteringModes();
            }
        }

        private Dictionary<string, prType.prptpMetering> PRSDK_GetMeteringModes()
        {
            UInt32 err = (UInt32)prError.prOK;
            Dictionary<string, prType.prptpMetering> meteringModes = new Dictionary<string, prType.prptpMetering>();
            List<byte> al_meteringModes = new List<byte>();

            // Get the current value
            byte currentVal = 0;
            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_ML_WEI_MODE,
                                                 prType.prptpDevicePropDataType.prUInt8, ref currentVal);
            if (err != (UInt32)prError.prOK)
            {
                return null;
            }
            // No error, set the current value
            m_Metering = (prType.prptpMetering)currentVal;

            // Get the enumeration values supported by the camera
            err = PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode.prPTP_DEV_PROP_ML_WEI_MODE,
                                             prType.prptpDevicePropDataType.prUInt8, ref al_meteringModes);

            if (err == (UInt32)prError.prOK)
            {
                if (!(al_meteringModes.Count > 0))
                {
                    return null;
                }
                // Create a hashtable that has the values paired with the string that fits
                foreach (byte val in al_meteringModes)
                {
                    // If the value isn't in the hashtable, add it
                    string key = ((prType.prptpMetering)val).ToString();
                    if (!meteringModes.ContainsKey(key))
                    {
                        meteringModes.Add(key, (prType.prptpMetering)val);
                    }
                }
                return meteringModes;
            }
            else
            {
                return null;
            }
        }

        private void SetMeteringMode(prType.prptpMetering mode)
        {
            bool vf = m_fVFEnd;

            if (m_fVFEnd)
            {
                StopLiveViewer();
            }

            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() > 0)
            {
                // Not supported
            }
            else
            {
                PRSDK_SetMeteringMode(mode);
            }

            if (vf)
            {
                StartLiveViewer();
            }
        }
        // TODO: Make status changed event send the metering mode, and add an enum to describe which item has changed.
        private void PRSDK_SetMeteringMode(prType.prptpMetering mode)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the zoom position */
            IntPtr pMode = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.StructureToPtr((byte)mode, pMode, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_ML_WEI_MODE,
                                              sizeof(byte),
                                              pMode);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pMode);

            if (err != (UInt32)prError.prOK)
            {
                return;
            }

            // We got here without any errors, set the stored photo effect
            m_Metering = mode;

            // Create delegate
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new StatusEventArgs(m_fVFEnd, (int)m_ImageSize, (int)m_ImageQuality,
                         (int)m_NumAvailableShot, (int)m_ZoomPos, (int)m_ShootingMode,
                         (int)m_ExposureCompensation, (int)m_FlashMode, (int)m_ISOSpeed, (int)m_PhotoEffect, (byte)m_AFDistance));
            }
            return;
        }

        #endregion
        
        #region Release
        public Bitmap Release(string filename)
        {
            m_asyncFileName = filename;

            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() != 0)
            {
                return CDSDK_Snap();
            }
            else
            {
                // Make sure the filename has the right extension for the file type that we are taking.
                if (pr_FileType == prType.prptpFileFormat.JPEG)
                {
                    Path.ChangeExtension(m_asyncFileName, ".jpg");
                }
                else if (pr_FileType == prType.prptpFileFormat.CRW)
                {
                    Path.ChangeExtension(m_asyncFileName, ".crw");
                }

                return PRSDK_Snap();
            }

        }

		public bool AsyncRelease(string fileName)
		{
			if (!m_fProgramRelease)
			{
				m_asyncFileName = fileName;
				Thread AsyncThread = new System.Threading.Thread(new ThreadStart(Snap));
				AsyncThread.Name = "AsyncRelease";
				AsyncThread.Start();
				return true;
			}
			else
			{
              	return false;
			}
		}

		private void Snap()
		{
            /* Is CD-SDK connection? */
            if (cd_hSource.ToInt32() != 0)
            {
                CDSDK_Snap();
            }
            else
            {
                // Make sure the filename has the right extension for the file type that we are taking.
                if (pr_FileType == prType.prptpFileFormat.JPEG)
                {
                    Path.ChangeExtension(m_asyncFileName, ".jpg");
                }
                else if (pr_FileType == prType.prptpFileFormat.CRW)
                {
                    Path.ChangeExtension(m_asyncFileName, ".crw");
                }
                PRSDK_Snap();
            }
		}

        private Bitmap PRSDK_Snap()
        {
            UInt32 err = (UInt32)prError.prOK;
          
             /* Make sure this function cannot be called iteratively. */
            m_fProgramRelease = true;

            /* Stop the live preview. */
            StopLiveViewer();

            /* A photograph is taken. */
            err = prAPI.PR_RC_Release(pr_hCamera);
            if ( err != (UInt32)prError.prOK )
            {
	            goto PRSDK_SnapErr;
            }

            /* Send an event with the image */
            if (this.ImageTransferStarted != null)
            {
                this.ImageTransferStarted(this, new ImageTransferEventArgs(m_asyncFileName));
            }
            
            if ((pr_ReleaseComp & (UInt16)prType.prptpTransferMode.FullImageToPC) > 0)
            {
                /* The data of the thumbnail is acuired. */
                pr_GetFileDataCB = new prType.prGetFileDataCB(this.PR_ProgressCallBackFunc);
                err = prAPI.PR_RC_GetReleasedData(pr_hCamera,
                                            pr_PicHandle,
                                            prType.prptpEventCode.prPTP_FULL_VIEW_RELEASED,
                                            (UInt32)MY_TRANSFER_SIZE,   /* Set 1KB anyway. You can change this parameter. */
                                            0,
                                            pr_GetFileDataCB);

	            // Clear the picture handle
                pr_PicHandle = 0;

                if (err != (UInt32)prError.prOK)
                {
                    pr_ReleaseComp = 0;
                    goto PRSDK_SnapErr;
                }
            }

            if ((pr_ReleaseComp & (UInt16)prType.prptpTransferMode.ThumbnailToPC) > 0)
            {
                /* The data of the thumbnail is acquired. */
                pr_GetFileDataCB = new prType.prGetFileDataCB(this.PR_ProgressCallBackFunc);
                prAPI.PR_RC_GetReleasedData(pr_hCamera,
                                            pr_ThmbHandle,
                                            prType.prptpEventCode.prPTP_THUMBNAIL_RELEASED,
                                            (UInt32)MY_TRANSFER_SIZE,   /* Set 1KB anyway. You can change this parameter. */
                                            0,
                                            pr_GetFileDataCB);

	            // Clear the thumbnail handle
                pr_ThmbHandle = 0;

                if (err != (UInt32)prError.prOK)
                {
                    pr_ReleaseComp = 0;
                    goto PRSDK_SnapErr;
                }
            }
            /* Clear the release composition flag */
            pr_ReleaseComp = 0;
                   
            /* Since we used a file instead of a memory stream, we must load the file from disk to get an image */
            /* Check to see if the file exists */
            if (File.Exists(m_asyncFileName))
            {
                // Load the image first into a temp image, then into our in-memory image
                // this allows us to delete the file while we still are displaying the image.
                try
                {
                    Bitmap tempImage = new Bitmap(m_asyncFileName);
                    m_image = new Bitmap(tempImage);
                    tempImage.Dispose();
                }
                catch
                {
                    m_image = null;
                }
            }
            else
            {
                m_image = null;
            }

         
            /* It sets up so that a complete message may be received. */
            m_fProgramRelease = false;

            /* Send an event with the image */
            if (this.ImageAcquired != null)
            {
                this.ImageAcquired(this, new StreamEventArgs(m_image));
            }

            /* Exit the subroutine. */
            return m_image;

        PRSDK_SnapErr:

            /* Clear the image */
            m_image = null;

            /* Allow images to be snapped again */
            m_fProgramRelease = false;

            /* Send an event with the image */
            if (this.ImageTransferStarted != null)
            {
                this.ImageTransferStarted(this, new ImageTransferEventArgs(m_asyncFileName));
            }

            /* Send an event with the image */
            if (this.ImageAcquired != null)
            {
                this.ImageAcquired(this, new StreamEventArgs(m_image));
            }

            return m_image;           
        }

        private Bitmap CDSDK_Snap()
        {
            UInt32 err = 0;
            UInt32 NumData = 0;

            /* Make sure this function cannot be called iteratively. */
            m_fProgramRelease = true;

            // cdReleaseImageInfo for image storage
            cdType.cdStgMedium myMedium = new cdType.cdStgMedium();
            myMedium.Type = cdType.cdMEMTYPE_FILE;
            myMedium.u.lpszFileName = m_asyncFileName;

            // cdStgMedium for image storage
            cdType.cdReleaseImageInfo relImgInfo = new cdType.cdReleaseImageInfo();

            /* UI is locked so that information may not be changed. */
            err = cdAPI.CDLockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto CDSDK_SnapErr;
            }

            /* Stop the live preview. */
            StopLiveViewer();

            /* Create a progress delegate */
            cd_ProgressCallbackFunction = new cdType.cdProgressCallbackFunction(CD_ProgressCallBackFunc);

            /* A photograph is taken. */
            NumData = 0;
            UInt16 bSync = 0;
            err = cdAPI.CDRelease(cd_hSource, bSync, null, 0, cdType.cdProgressOption.cdPROG_NO_REPORT, ref NumData);
            if (err != (UInt32)cdError.cdOK)
            {
                goto CDSDK_SnapErr;
            }

            /* Send an event with the image */
            if (this.ImageTransferStarted != null)
            {
                this.ImageTransferStarted(this, new ImageTransferEventArgs(m_asyncFileName));
            }

            /* Get the image from the camera */
            err = cdAPI.CDGetReleasedData(cd_hSource, cd_ProgressCallbackFunction, 0, 
                                          cdType.cdProgressOption.cdPROG_REPORT_DONE, ref relImgInfo, ref myMedium);

            /* Since we used a file instead of a memory stream, we must load the file from disk to get an image */
            /* Check to see if the file exists */
            if (File.Exists(m_asyncFileName))
            {
                // Load the image first into a temp image, then into our in-memory image
                // this allows us to delete the file while we still are displaying the image.
                try
                {
                    Bitmap tempImage = new Bitmap(m_asyncFileName);
                    m_image = new Bitmap(tempImage);
                    tempImage.Dispose();
                }
                catch
                {
                    m_image = null;
                }
            }
            else
            {
                m_image = null;
            }

            /* The lock of UI is canceled. */
            err = cdAPI.CDUnlockUI(cd_hSource);
            if (err != (UInt32)cdError.cdOK)
            {
                goto CDSDK_SnapErr;
            }

            /* It sets up so that a complete message may be received. */
            m_fProgramRelease = false;

            /* Send an event with the image */
            if (this.ImageAcquired != null)
            {
                this.ImageAcquired(this, new StreamEventArgs(m_image));
            }

            /* Exit the subroutine. */
            return m_image;

        CDSDK_SnapErr:
            /* Restore the camera control */
            cdAPI.CDUnlockUI(cd_hSource);

            /* Clear the image */
            m_image = null;

            /* Allow images to be snapped again */
            m_fProgramRelease = false;

            /* Send an event with the image */
            if (this.ImageTransferStarted != null)
            {
                this.ImageTransferStarted(this, new ImageTransferEventArgs(m_asyncFileName));
            }

            /* Send an event with the image */
            if (this.ImageAcquired != null)
            {
                this.ImageAcquired(this, new StreamEventArgs(m_image));
            }

            return m_image;
        }

		#endregion

		#region Helpers
		private void HandleError(string _s, UInt32 _err) 
		{
	        Console.WriteLine("Error! " + _s + " " +_err.ToString("X"));
		}

		#endregion

        #region PRSDK Helpers
        private void IsSupportedRCandEVF(IntPtr pDeviceInfo,	/* (IN) DeviceInfo data set*/
                                 ref bool pbRC,
                                 ref bool pbEVF,
                                 ref bool pbAwb)
        {
            IntPtr pDeviceInfoTmp;
            byte bNum;
            UInt32 dwNum, i;
            UInt16 wOperation;

            /* Flag if the operation is support or not */
            bool fInitiate, fTerminate, fCapture;
            bool fFvInitiate, fFvTerminate, fAeAfAwb;

            fInitiate = false;
            fTerminate = false;
            fCapture = false;
            fFvInitiate = false;
            fFvTerminate = false;
            fAeAfAwb = false;

            /* Move the potiner to support operation */
            pDeviceInfoTmp = pDeviceInfo;

            /* Standard version */
            /* Vendor extendedID */
            /* Vendor extended version */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt16)
                                                                  + sizeof(UInt32)
                                                                  + sizeof(UInt16));
            /* Vendor extended information */
            bNum = Marshal.ReadByte(pDeviceInfoTmp);
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(byte)
                                                                 + sizeof(ushort) * bNum);
            /* Function moded */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt16));

            /* Suppored operations */
            dwNum = (UInt32)Marshal.ReadInt32(pDeviceInfoTmp);	/* number of elements */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt32));

            /* Loop for the number of elements */
            for (i = 0; i < dwNum; i++)
            {
                wOperation = (UInt16)Marshal.ReadInt16(pDeviceInfoTmp);
                switch (wOperation)
                {
                    case (ushort)prType.prOperationCode.prPTP_INITIATE_RELEASE_CONTROL:
                        fInitiate = true;
                        break;
                    case (ushort)prType.prOperationCode.prPTP_TERMINATE_RELEASE_CONTROL:
                        fTerminate = true;
                        break;
                    case (ushort)prType.prOperationCode.prPTP_RC_CAPTURE:
                        fCapture = true;
                        break;
                    case (ushort)prType.prOperationCode.prPTP_RC_INITIATE_VIEW_FINDER:
                        fFvInitiate = true;
                        break;
                    case (ushort)prType.prOperationCode.prPTP_RC_TERMINATE_VIEW_FINDER:
                        fFvTerminate = true;
                        break;
                    case (ushort)prType.prOperationCode.prPTP_RC_RELEASE_DO_AE_AF_AWB:
                        fAeAfAwb = true;
                        break;
                    default:
                        break;
                }
                pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt16));
            }
            /* The following information is not checked */
            /* Supported events */
            /* Supported device properties */
            /* Supported captured image types */
            /* Supported image types */
            /* Company information */
            /* Model name */
            /* Device version */
            /* Serial number */

            /* Is Remote Capture supported? */
            if ((fInitiate == true) && (fTerminate == true) && (fCapture == true))
            {
                pbRC = true;
            }
            /* Is Viewfinder supported? */
            if ((fFvInitiate == true) && (fFvTerminate == true))
            {
                pbEVF = true;
            }
            /* Is AeAfAwb supported? */
            if (fAeAfAwb == true)
            {
                pbAwb = true;
            }
        }

        private void IsSupportedCapRelPrm(IntPtr pDeviceInfo,	/* (IN) DeviceInfo data set */
                                          ref bool pExposureMode,
                                          ref bool pExposureComp,
                                          ref bool pStrobeSet,
                                          ref bool pISO,
                                          ref bool pPhotoEffect)
        {
            IntPtr pDeviceInfoTmp;
            byte bNum;
            UInt32 dwNum, i;
            UInt16 wDeviceProp;

            /* Move the potiner to support operation */
            pDeviceInfoTmp = pDeviceInfo;

            /* Standard version */
            /* Vendor extended ID */
            /* Vendor extended version */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt16)
                                                       + sizeof(UInt32)
                                                       + sizeof(UInt16));
            /* Vendor extended information */
            bNum = Marshal.ReadByte(pDeviceInfoTmp);
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(byte)
                                                                 + sizeof(ushort) * bNum);

            /* Function modes */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt16));

            /* Supported operations */
            dwNum = (UInt32)Marshal.ReadInt32(pDeviceInfoTmp);	/* number of elements */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt32)
                                                                 + sizeof(UInt16) * dwNum);

            /* Supported events */
            dwNum = (UInt32)Marshal.ReadInt32(pDeviceInfoTmp);	/* number of elements */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt32)
                                                                 + sizeof(UInt16) * dwNum);

            /* check supported device properties */
            dwNum = (UInt32)Marshal.ReadInt32(pDeviceInfoTmp);	/* number of elements */
            pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt32));
            for (i = 0; i < dwNum; i++)
            {
                wDeviceProp = (UInt16)Marshal.ReadInt16(pDeviceInfoTmp);
                /* turn on the flags of the supported device properties */
                switch (wDeviceProp)
                {
                    case (ushort)prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_MODE:
                        pExposureMode = true;
                        break;
                    case (ushort)prType.prptpDevicePropCode.prPTP_DEV_PROP_EXPOSURE_COMP:
                        pExposureComp = true;
                        break;
                    case (ushort)prType.prptpDevicePropCode.prPTP_DEV_PROP_STROBE_SETTING:
                        pStrobeSet = true;
                        break;
                    case (ushort)prType.prptpDevicePropCode.prPTP_DEV_PROP_ISO:
                        pISO = true;
                        break;
                    case (ushort)prType.prptpDevicePropCode.prPTP_DEV_PROP_PHOTO_EFFECT:
                        pPhotoEffect = true;
                        break;
                    default:
                        break;
                }
                pDeviceInfoTmp = new IntPtr(pDeviceInfoTmp.ToInt32() + sizeof(UInt16));
            }
        }

        private UInt32 PRSDK_GetTransferMode(ref prType.prptpTransferMode transferMode)
        {
            UInt32 err = (UInt32)prError.prOK;
            UInt16 sTrandMode = 0;

            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_CAPTURE_TRANSFER_MODE,
                                                 prType.prptpDevicePropDataType.prUInt16,
                                                 ref sTrandMode);
            transferMode = (prType.prptpTransferMode)sTrandMode;
            return err;
        }

        private UInt32 PRSDK_SetTransferMode(prType.prptpTransferMode transferMode)
        {
            UInt32 err = (UInt32)prError.prOK;
            /* Allocate memory to marshal the transfer mode */
            IntPtr pTransMode = Marshal.AllocHGlobal(sizeof(UInt16));
            Marshal.StructureToPtr((UInt16)transferMode, pTransMode, true);

            err = prAPI.PR_SetDevicePropValue(pr_hCamera,
                                              prType.prptpDevicePropCode.prPTP_DEV_PROP_CAPTURE_TRANSFER_MODE,
                                              sizeof(UInt16),
                                              pTransMode);
            /* Free the pointer memory */
            Marshal.FreeHGlobal(pTransMode);

            /* Return the the error code */
            return err;
        }

        private UInt32 PRSDK_GetFileType(ref prType.prptpFileFormat fileType)
        {
            UInt32 err = (UInt32)prError.prOK;
            byte bFileType = 0;

            err = PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode.prPTP_DEV_PROP_FULLVIEW_FILE_FORMAT,
                                                 prType.prptpDevicePropDataType.prUInt8,
                                                 ref bFileType);
            fileType = (prType.prptpFileFormat)bFileType;
            return err;
        }

        private UInt32 PRSDK_GetZoomRange(ref UInt16 min, ref UInt16 max, ref UInt16 step)
        {
            UInt32 err = (UInt32)prError.prOK;
            UInt32 BufferSize = 0;
            IntPtr pDataBuffer = IntPtr.Zero;
            IntPtr pDataBufferTmp;
            prType.prptpDevicePropFormFlag formFlag;

            pDataBuffer = Marshal.AllocHGlobal(MY_BUFFER_SIZE);
            BufferSize = MY_BUFFER_SIZE;
            err = prAPI.PR_GetDevicePropDesc(pr_hCamera,
                                             prType.prptpDevicePropCode.prPTP_DEV_PROP_ZOOM_POS,
                                             ref BufferSize,
                                             pDataBuffer);
            if (err != (UInt32)prError.prOK)
            {
                goto GetZoomRangeError;
            }

            /* Move the potiner to support operation */
            pDataBufferTmp = pDataBuffer;
           
            /* DevicePropCode */
            if (prType.prptpDevicePropCode.prPTP_DEV_PROP_ZOOM_POS != 
               (prType.prptpDevicePropCode)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GetZoomRangeError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Data type */
            if (prType.prptpDevicePropDataType.prUInt16 != 
               (prType.prptpDevicePropDataType)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GetZoomRangeError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Get/Set */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));

            /* FactoryDefaultValue */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* CurrentValue */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* FormFlag */
            formFlag = (prType.prptpDevicePropFormFlag)Marshal.ReadByte(pDataBufferTmp);
            if (formFlag != prType.prptpDevicePropFormFlag.Range)
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GetZoomRangeError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));

            /* Range-Form */
            min = (UInt16)Marshal.ReadInt16(pDataBufferTmp);
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));
            max = (UInt16)Marshal.ReadInt16(pDataBufferTmp);
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));
            step = (UInt16)Marshal.ReadInt16(pDataBufferTmp);
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            return (UInt32)prError.prOK;

        GetZoomRangeError:
            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            // Default to NotDefined
            return err;

        }

        private UInt32 PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode PropertyCode, 
                                                      prType.prptpDevicePropDataType DataType,
                                                      ref byte PropertyValue)
        {
            UInt32 err = (UInt32)prError.prOK;
            UInt32 BufferSize = 0;
            IntPtr pDataBuffer = IntPtr.Zero;
            IntPtr pDataBufferTmp;

            pDataBuffer = Marshal.AllocHGlobal(MY_BUFFER_SIZE);
            BufferSize = MY_BUFFER_SIZE;
            err = prAPI.PR_GetDevicePropDesc(pr_hCamera,
                                             PropertyCode,
                                             ref BufferSize,
                                             pDataBuffer);
            if (err != (UInt32)prError.prOK)
            {
                goto GetDevicePropertyError;
            }

            /* Move the potiner to support operation */
            pDataBufferTmp = pDataBuffer;
           
            /* DevicePropCode */
            if (PropertyCode != (prType.prptpDevicePropCode)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GetDevicePropertyError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Data type */
            if (DataType != (prType.prptpDevicePropDataType)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GetDevicePropertyError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Get/Set */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));

            /* FactoryDefaultValue */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));

            /* CurrentValue */
            PropertyValue = Marshal.ReadByte(pDataBufferTmp);

            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            return (UInt32)prError.prOK;

        GetDevicePropertyError:
            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            // Default to NotDefined
            return err;

        }
        private UInt32 PRSDK_GenericGetDeviceProperty(prType.prptpDevicePropCode PropertyCode,
                                                      prType.prptpDevicePropDataType DataType,
                                                      ref ushort PropertyValue)
        {
            UInt32 err = (UInt32)prError.prOK;
            UInt32 BufferSize = 0;
            IntPtr pDataBuffer = IntPtr.Zero;
            IntPtr pDataBufferTmp;

            pDataBuffer = Marshal.AllocHGlobal(MY_BUFFER_SIZE);
            BufferSize = MY_BUFFER_SIZE;
            err = prAPI.PR_GetDevicePropDesc(pr_hCamera,
                                             PropertyCode,
                                             ref BufferSize,
                                             pDataBuffer);
            if (err != (UInt32)prError.prOK)
            {
                goto GetDevicePropertyError;
            }

            /* Move the pointer to support operation */
            pDataBufferTmp = pDataBuffer;
            
            /* DevicePropCode */
            if (PropertyCode != (prType.prptpDevicePropCode)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GetDevicePropertyError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Data type */
            if (DataType != (prType.prptpDevicePropDataType)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GetDevicePropertyError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Get/Set */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));

            /* FactoryDefaultValue */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* CurrentValue */
            PropertyValue = (UInt16)Marshal.ReadInt16(pDataBufferTmp);

            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            return (UInt32)prError.prOK;

        GetDevicePropertyError:
            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            // Default to NotDefined
            return err;

        }

        private UInt32 PRSDK_GenericGetEnumValues(prType.prptpDevicePropCode PropertyCode,
                                                 prType.prptpDevicePropDataType DataType,
                                                 ref List<byte> enumValues)
        {
            UInt32 err = (UInt32)prError.prOK;
            UInt32 BufferSize = 0;
            IntPtr pDataBuffer = IntPtr.Zero;
            IntPtr pDataBufferTmp;
            prType.prptpDevicePropFormFlag formFlag;

            pDataBuffer = Marshal.AllocHGlobal(MY_BUFFER_SIZE);
            BufferSize = MY_BUFFER_SIZE;
            err = prAPI.PR_GetDevicePropDesc(pr_hCamera,
                                             PropertyCode,
                                             ref BufferSize,
                                             pDataBuffer);
            if (err != (UInt32)prError.prOK)
            {
                goto GenericGetEnumValuesError;
            }

            /* Move the potiner to support operation */
            pDataBufferTmp = pDataBuffer;

            /* DevicePropCode */
            if (PropertyCode != (prType.prptpDevicePropCode)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GenericGetEnumValuesError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Data type */
            if (DataType != (prType.prptpDevicePropDataType)Marshal.ReadInt16(pDataBufferTmp))
            {
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GenericGetEnumValuesError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));

            /* Get/Set */
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));

            if (DataType == prType.prptpDevicePropDataType.prUInt8)
            {
                /* FactoryDefaultValue */
                pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));
                /* CurrentValue */
                pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));
            }
            else if (DataType == prType.prptpDevicePropDataType.prUInt16)
            {
                /* FactoryDefaultValue */
                pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));
                /* CurrentValue */
                pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));
            }

            /* FormFlag */
            formFlag = (prType.prptpDevicePropFormFlag)Marshal.ReadByte(pDataBufferTmp);
            if (formFlag != prType.prptpDevicePropFormFlag.Enumeration)
            {
                /* Error, if not Enumulation */
                err = (UInt32)prError.prRESPONSE_GeneralError;
                goto GenericGetEnumValuesError;
            }
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));

            /* Enumeration-Form */
            int wNum = Marshal.ReadInt16(pDataBufferTmp);
            pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));
            for (int i = 0; i < wNum; i++)
            {
                // Add the value to the enumerated values
                enumValues.Add(Marshal.ReadByte(pDataBufferTmp));
                
                // Advance the pointer
                if (DataType == prType.prptpDevicePropDataType.prUInt8)
                {
                    pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(byte));
                }
                else if (DataType == prType.prptpDevicePropDataType.prUInt16)
                {
                    pDataBufferTmp = new IntPtr(pDataBufferTmp.ToInt32() + sizeof(UInt16));
                }
            }

            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            return (UInt32)prError.prOK;

        GenericGetEnumValuesError:
            /* Free the memory allocated to the pointer */
            Marshal.FreeHGlobal(pDataBuffer);

            // Default to NotDefined
            return err;

        }
        #endregion

        #region Bitmap structures
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BITMAPINFOHEADER
        {
            public UInt32 biSize;
            public Int32 biWidth;
            public Int32 biHeight;
            public Int16 biPlanes;
            public Int16 biBitCount;
            public UInt32 biCompression;
            public UInt32 biSizeImage;
            public Int32 biXPelsPerMeter;
            public Int32 biYPelsPerMeter;
            public UInt32 biClrUsed;
            public UInt32 biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BITMAPFILEHEADER // 14 bytes
        {
            public Int16 bfType; //"magic cookie" - must be "BM"
            public Int32 bfSize;
            public Int16 bfReserved1;
            public Int16 bfReserved2;
            public Int32 bfOffBits;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPHEADER
        {
            [MarshalAs(UnmanagedType.Struct, SizeConst = 14)]
            public BITMAPFILEHEADER bmfHeader;
            [MarshalAs(UnmanagedType.Struct, SizeConst = 40)]
            public BITMAPINFOHEADER bmiHeader;
        }

        #endregion
    }
}
