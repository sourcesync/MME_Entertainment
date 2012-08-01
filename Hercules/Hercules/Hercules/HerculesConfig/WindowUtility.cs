using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace MME.HerculesConfig
{
    public class WindowUtility
    {
        public static string FriendlyOrder(int i)
        {
            switch (i)
            {
                case 6:
                    return "sixth";
                case 5:
                    return "fifth";
                case 4:
                    return "fourth";
                case 3:
                    return "third";
                case 2:
                    return "second";
            }

            return "first";
        }

        public static void GoFullScreen(System.Windows.Forms.Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
        }

        public static System.Drawing.Image GetScreenImage(string screenname)
        {
            FileStream fs;
            fs = new FileStream(string.Format("Skins\\{0}\\Screens\\{1}",
                ConfigUtility.Skin,
                screenname), FileMode.Open, FileAccess.Read);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return img;
        }


        public static void SetScreen(PictureBox pb, string screenname)
        {
            if (pb != null && pb.Image != null)
            {
                pb.Image.Dispose();
            }

            FileStream fs;
            fs = new FileStream(string.Format("Skins\\{0}\\Screens\\{1}",
                ConfigUtility.Skin,
                screenname), FileMode.Open, FileAccess.Read);
            pb.Image = System.Drawing.Image.FromStream(fs);
            fs.Close();
        }

        public static System.Windows.Controls.Image GetScreenImageWPF(string screenname)
        {
            try
            {
                FileStream fs;
                fs = new FileStream(string.Format("Skins\\{0}\\Screens\\{1}",
                    ConfigUtility.Skin,
                    screenname), FileMode.Open, FileAccess.Read);
                FileStream stream = fs;

                //Image i = new Image();
                System.Windows.Controls.Image i = new System.Windows.Controls.Image();
                System.Windows.Media.Imaging.BitmapImage src = new System.Windows.Media.Imaging.BitmapImage();
                src.BeginInit();
                src.StreamSource = stream;
                src.EndInit();
                i.Source = src;
                i.Stretch = System.Windows.Media.Stretch.Uniform;

                //Uri uri = new Uri(path, UriKind.Relative);
                //BitmapImage bm = new BitmapImage(uri);
                //System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                //fs.Close();
                return i;
            }
            catch (System.Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
            return null;
        }

        public static System.Collections.ArrayList GetMenu(String skip)
        {
            System.Collections.ArrayList result = new System.Collections.ArrayList();
            foreach (String dir in Directory.EnumerateDirectories(string.Format("Skins\\{0}\\Menu", ConfigUtility.Skin)))
            {
                String[] parts = dir.Split(new char[] { '\\' });
                String fname = parts[parts.Length - 1];
                if (fname == skip) continue;
                result.Add(fname);
            }

            return result;
        }

        public static System.Collections.ArrayList GetMain(String skip)
        {
            try
            {
                String main_dir = "Main";
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "MainMenuDir")))
                {
                    main_dir = ConfigUtility.GetConfig(ConfigUtility.Config, "MainMenuDir");
                }

                System.Collections.ArrayList result = new System.Collections.ArrayList();

                foreach (String fl in Directory.EnumerateFiles(string.Format("Skins\\{0}\\{1}", ConfigUtility.Skin, main_dir)))
                {
                    String[] parts = fl.Split(new char[] { '\\' });
                    String fname = parts[parts.Length - 1];
                    parts = fname.Split(new char[] { '.' });
                    if (parts[0] == skip) continue;

                    String use = parts[0].Substring(10);
                    result.Add(use);
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public static System.Collections.ArrayList GetMainPaths(String skip)
        {
            try
            {
                String main_dir = "Main";
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "MainMenuDir")))
                {
                    main_dir = ConfigUtility.GetConfig(ConfigUtility.Config, "MainMenuDir");
                }

                System.Collections.ArrayList result = new System.Collections.ArrayList();

                foreach (String fl in Directory.EnumerateFiles(string.Format("Skins\\{0}\\{1}", ConfigUtility.Skin, main_dir)))
                {
                    if (fl == skip) continue;
                    result.Add(fl);
                }

                return result;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public static System.Collections.Hashtable[] GetMenuDescription()
        {
            System.Collections.Hashtable item_name_hash = new System.Collections.Hashtable();
            System.Collections.Hashtable item_desc_hash = new System.Collections.Hashtable();

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                String Path = string.Format("Skins\\{0}\\Menu\\descriptions.txt", ConfigUtility.Skin);
                using (StreamReader sr = new StreamReader(Path))
                {
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] parts = line.Trim().Split( new char[] {':'} );
                        if ( parts.Count() == 3 )
                        {
                            String fname = parts[0];
                            String name = parts[1];
                            String des = parts[2];
                            item_name_hash[ fname ] = name;
                            item_desc_hash[ fname ] = des;
                        }
                    }
                }

                System.Collections.Hashtable[] arr = new System.Collections.Hashtable[2];
                arr[0] = item_name_hash;
                arr[1] = item_desc_hash;
                return arr;
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static System.Collections.ArrayList GetMenuIconFilenames()
        {
            System.Collections.ArrayList result = new System.Collections.ArrayList();
            foreach (String dir in Directory.EnumerateFiles(string.Format("Skins\\{0}\\Menu", ConfigUtility.Skin), "*.png"))
            {
                result.Add(dir);
            }
            return result;
        }

        public static String[] GetMenuPaths(String menu)
        {
            System.Collections.ArrayList files = new System.Collections.ArrayList();
            foreach (String file in Directory.EnumerateFiles(
                string.Format("Skins\\{0}\\Menu\\{1}", ConfigUtility.Skin, menu)))
            {
                files.Add(file);
            }

            String[] arr = new String[files.Count];
            for (int j = 0; j < files.Count; j++)
            {
                arr[j] = (String)files[j];
            }

            return arr;
        }

        public static double[] GetMenuCosts(String menu)
        {
            System.Collections.ArrayList files = new System.Collections.ArrayList();
            foreach (String file in Directory.EnumerateFiles(
                string.Format("Skins\\{0}\\Menu\\{1}", ConfigUtility.Skin, menu)))
            {
                files.Add(file);
            }

            double[] arr = new double[files.Count];
            for (int j = 0; j < files.Count; j++)
            {
                arr[j] = 1.0;
            }

            return arr;
        }

        public static String[][] GetAllMenuPaths()
        {
            
            System.Collections.ArrayList paths = new System.Collections.ArrayList();
            System.Collections.ArrayList names = GetMenu(null);

            String[][] pths = new String[names.Count][];

            for ( int i=0;i< names.Count;i++)
            {
                String menu = (String)names[i];
                pths[i] = GetMenuPaths(menu);
            }

            
            return pths;
        }

        public static System.Windows.Media.Imaging.BitmapImage GetMenuBitmapWPF(string menu, string item)
        {
            FileStream fs;
            fs = new FileStream(
                string.Format("Skins\\{0}\\Menu\\{1}\\{2}",
                ConfigUtility.Skin, menu, item), 
                FileMode.Open, FileAccess.Read);
            FileStream stream = fs;

            System.Windows.Controls.Image i = new System.Windows.Controls.Image();
            i.SnapsToDevicePixels = true;
            System.Windows.Media.Imaging.BitmapImage src = new System.Windows.Media.Imaging.BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();

            return src;
        }

        public static System.Windows.Media.Imaging.BitmapImage GetScreenBitmapWPF(string screenname)
        {
            FileStream fs;
            String str = string.Format("Skins\\{0}\\Screens\\{1}",
                ConfigUtility.Skin,
                screenname);
            fs = new FileStream(str, FileMode.Open, FileAccess.Read);
            FileStream stream = fs;

            System.Windows.Controls.Image i = new System.Windows.Controls.Image();
            i.SnapsToDevicePixels = true;
            System.Windows.Media.Imaging.BitmapImage src = new System.Windows.Media.Imaging.BitmapImage();
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();

            return src;
        }

        public static System.Windows.Media.Imaging.BitmapImage GetBitmapWPF(string path)
        {
            FileStream fs;
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            FileStream stream = fs;

            System.Windows.Controls.Image i = new System.Windows.Controls.Image();
            i.SnapsToDevicePixels = true;
            System.Windows.Media.Imaging.BitmapImage src = new System.Windows.Media.Imaging.BitmapImage();
            
            src.BeginInit();
            src.StreamSource = stream;
            src.EndInit();

            return src;
        }


        #region Definitions/DLL Imports
        // For PInvoke: Contains information about an entry in the Internet cache
        [StructLayout(LayoutKind.Explicit, Size = 80)]
        public struct INTERNET_CACHE_ENTRY_INFOA
        {
            [FieldOffset(0)]
            public uint dwStructSize;
            [FieldOffset(4)]
            public IntPtr lpszSourceUrlName;
            [FieldOffset(8)]
            public IntPtr lpszLocalFileName;
            [FieldOffset(12)]
            public uint CacheEntryType;
            [FieldOffset(16)]
            public uint dwUseCount;
            [FieldOffset(20)]
            public uint dwHitRate;
            [FieldOffset(24)]
            public uint dwSizeLow;
            [FieldOffset(28)]
            public uint dwSizeHigh;
            [FieldOffset(32)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastModifiedTime;
            [FieldOffset(40)]
            public System.Runtime.InteropServices.ComTypes.FILETIME ExpireTime;
            [FieldOffset(48)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
            [FieldOffset(56)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastSyncTime;
            [FieldOffset(64)]
            public IntPtr lpHeaderInfo;
            [FieldOffset(68)]
            public uint dwHeaderInfoSize;
            [FieldOffset(72)]
            public IntPtr lpszFileExtension;
            [FieldOffset(76)]
            public uint dwReserved;
            [FieldOffset(76)]
            public uint dwExemptDelta;
        }
        // For PInvoke: Initiates the enumeration of the cache groups in the Internet cache
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindFirstUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstUrlCacheGroup(
            int dwFlags,
            int dwFilter,
            IntPtr lpSearchCondition,
            int dwSearchCondition,
            ref long lpGroupId,
            IntPtr lpReserved);
        // For PInvoke: Retrieves the next cache group in a cache group enumeration
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindNextUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextUrlCacheGroup(
            IntPtr hFind,
            ref long lpGroupId,
            IntPtr lpReserved);
        // For PInvoke: Releases the specified GROUPID and any associated state in the cache index file
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "DeleteUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool DeleteUrlCacheGroup(
            long GroupId,
            int dwFlags,
            IntPtr lpReserved);
        // For PInvoke: Begins the enumeration of the Internet cache
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindFirstUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstUrlCacheEntry(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern,
            IntPtr lpFirstCacheEntryInfo,
            ref int lpdwFirstCacheEntryInfoBufferSize);
        // For PInvoke: Retrieves the next entry in the Internet cache
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindNextUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextUrlCacheEntry(
            IntPtr hFind,
            IntPtr lpNextCacheEntryInfo,
            ref int lpdwNextCacheEntryInfoBufferSize);
        // For PInvoke: Removes the file that is associated with the source name from the cache, if the file exists
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "DeleteUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool DeleteUrlCacheEntry(
            IntPtr lpszUrlName);
        #endregion
        #region Public Static Functions
        /// 
        /// Clears the cache of the web browser
        /// 
        public static void ClearCache()
        {
            // Indicates that all of the cache groups in the user's system should be enumerated
            const int CACHEGROUP_SEARCH_ALL = 0x0;
            // Indicates that all the cache entries that are associated with the cache group
            // should be deleted, unless the entry belongs to another cache group.
            const int CACHEGROUP_FLAG_FLUSHURL_ONDELETE = 0x2;
            // File not found.
            const int ERROR_FILE_NOT_FOUND = 0x2;
            // No more items have been found.
            const int ERROR_NO_MORE_ITEMS = 259;
            // Pointer to a GROUPID variable
            long groupId = 0;
            // Local variables
            int cacheEntryInfoBufferSizeInitial = 0;
            int cacheEntryInfoBufferSize = 0;
            IntPtr cacheEntryInfoBuffer = IntPtr.Zero;
            INTERNET_CACHE_ENTRY_INFOA internetCacheEntry;
            IntPtr enumHandle = IntPtr.Zero;
            bool returnValue = false;
            // Delete the groups first.
            // Groups may not always exist on the system.
            // For more information, visit the following Microsoft Web site:
            // http://msdn.microsoft.com/library/?url=/workshop/networking/wininet/overview/cache.asp   
            // By default, a URL does not belong to any group. Therefore, that cache may become
            // empty even when the CacheGroup APIs are not used because the existing URL does not belong to any group.   
            enumHandle = FindFirstUrlCacheGroup(0, CACHEGROUP_SEARCH_ALL, IntPtr.Zero, 0, ref groupId, IntPtr.Zero);
            // If there are no items in the Cache, you are finished.
            if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
            {
                return;
            }
            // Loop through Cache Group, and then delete entries.
            while (true)
            {
                if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error())
                {
                    break;
                }
                // Delete a particular Cache Group.
                returnValue = DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero);
                if (!returnValue && ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error())
                {
                    returnValue = FindNextUrlCacheGroup(enumHandle, ref groupId, IntPtr.Zero);
                }
                if (!returnValue && (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()))
                    break;
            }
            // Start to delete URLs that do not belong to any group.
            enumHandle = FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
            if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
            {
                return;
            }
            cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
            cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
            enumHandle = FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
            while (true)
            {
                internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));
                if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || cacheEntryInfoBufferSize == 0)
                {
                    break;
                }
                cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;
                returnValue = DeleteUrlCacheEntry(internetCacheEntry.lpszSourceUrlName);
                if (!returnValue)
                {
                    returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                }
                if (!returnValue && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                {
                    break;
                }
                if (!returnValue && cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize)
                {
                    cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                    cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
                    returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                }
            }
            Marshal.FreeHGlobal(cacheEntryInfoBuffer);
        }

        #endregion
    }
}
