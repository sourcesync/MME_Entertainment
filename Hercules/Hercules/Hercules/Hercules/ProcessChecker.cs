﻿using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Windows.Forms;

/// <summary>
/// Check running processes for an already-running instance. Implements a simple and
/// always effective algorithm to find currently running processes with a main window
/// matching a given substring and focus it.
/// Combines code written by Lion Shi (MS) and Sam Allen.
/// </summary>
static class ProcessChecker
{
    /// <summary>
    /// Stores a required string that must be present in the window title for it
    /// to be detected.
    /// </summary>
    static string _requiredString;

    /// <summary>
    /// Contains signatures for C++ DLLs using interop.
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProcDel lpEnumFunc,
            Int32 lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd,
            ref Int32 lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString,
            Int32 nMaxCount);

        public const int SW_SHOWNORMAL = 1;
    }

    public delegate bool EnumWindowsProcDel(IntPtr hWnd, Int32 lParam);

    /// <summary>
    /// Perform finding and showing of running window.
    /// </summary>
    /// <returns>Bool, which is important and must be kept to match up
    /// with system call.</returns>
    static private bool EnumWindowsProc(IntPtr hWnd, Int32 lParam)
    {
        int processId = 0;
        NativeMethods.GetWindowThreadProcessId(hWnd, ref processId);

        StringBuilder caption = new StringBuilder(1024);
        NativeMethods.GetWindowText(hWnd, caption, 1024);

        // Use IndexOf to make sure our required string is in the title.
        if (processId == lParam && (caption.ToString().IndexOf(_requiredString,
            StringComparison.OrdinalIgnoreCase) != -1))
        {
            // Restore the window.
            NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_SHOWNORMAL);
            NativeMethods.SetForegroundWindow(hWnd);
        }
        return true; // Keep this.
    }

    /// <summary>
    /// Find out if we need to continue to load the current process. If we
    /// don't focus the old process that is equivalent to this one.
    /// </summary>
    /// <param name="forceTitle">This string must be contained in the window
    /// to restore. Use a string that contains the most
    /// unique sequence possible. If the program has windows with the string
    /// "Journal", pass that word.</param>
    /// <returns>False if no previous process was activated. True if we did
    /// focus a previous process and should simply exit the current one.</returns>
    static public bool IsOnlyProcess(string forceTitle)
    {
        //MyProcess myProcess = new MyProcess();

        Process my = System.Diagnostics.Process.GetCurrentProcess();
        String name = my.ProcessName;

        _requiredString = forceTitle;
        foreach (Process proc in Process.GetProcessesByName(name))  //Application.ProductName))
        {
            if (proc.Id != Process.GetCurrentProcess().Id)
            {
                NativeMethods.EnumWindows(new EnumWindowsProcDel(EnumWindowsProc),
                    proc.Id);
                return false;
            }
        }
        return true;
    }

    static public bool IsOnlyProcess2(string forceTitle)
    {
        //MyProcess myProcess = new MyProcess();

        Process my = System.Diagnostics.Process.GetCurrentProcess();
        String myname = my.ProcessName;

        _requiredString = forceTitle;
        foreach (Process proc in Process.GetProcesses()) //Application.ProductName))
        {
            String name = proc.ProcessName;

            if ( name.ToLower().Contains("hercules") )
            {
                //if (name.Contains("vshost")) continue;

                
                if (proc.Id == Process.GetCurrentProcess().Id)  // check if self...
                {
                    //NativeMethods.EnumWindows(new EnumWindowsProcDel(EnumWindowsProc),proc.Id);
                    //return false;
                    //System.Windows.Forms.MessageBox.Show("OK pids=" +
                    //    proc.Id + " " + proc.Id);
                    continue;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Another Hercules Processing is Already Running.");
                    //pids=" +
                    //    proc.Id + " " + proc.Id);
                    return false;
                }
            }
        }
        return true;
    }

}