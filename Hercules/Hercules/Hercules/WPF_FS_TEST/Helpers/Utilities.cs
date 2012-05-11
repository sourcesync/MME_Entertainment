using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Diagnostics;
using System.Windows.Controls;
using WP7Square.Extensions;

namespace WP7Square.Helpers
{
    public class Utilities
    {
        public static string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return string.Empty;
            }

            if (!phone.StartsWith("+") && phone.Length <= 10)
            {
                if (phone.Length == 10)
                {
                    return "({0}) {1}-{2}".FormatWith(
                        phone.Substring(0, 3),
                        phone.Substring(3, 3),
                        phone.Substring(6, 4));
                }

                if (phone.Length == 7)
                {
                    return "{0}-{1}".FormatWith(
                        phone.Substring(0, 3),
                        phone.Substring(3, 4));
                }

                return string.Empty;
            }

            return phone;
        }

        public static void ScrollToCenter(ScrollViewer scrollViewer)
        {
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.ExtentWidth / 2 - scrollViewer.Width / 2);
            scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight / 2 - scrollViewer.Height / 2);
        }

        public static string GetElapsedText(string dateTime)
        {
            return GetElapsedText(DateTime.Parse(dateTime));
        }

        public static string GetElapsedText(DateTime dateTime)
        {
            return GetElapsedText(DateTime.Now - dateTime);
        }

        public static string GetElapsedText(DateTime firstDateTime, DateTime lastDateTime)
        {
            return GetElapsedText(lastDateTime - firstDateTime);
        }

        public static string GetElapsedText(TimeSpan timeSpan)
        {
            if (Math.Abs(timeSpan.Days) == 1)
                return "Yesterday";
            if (Math.Abs(timeSpan.Days) > 1)
                return "{0} days ago".FormatWith(Math.Abs(timeSpan.Days));
            if (Math.Abs(timeSpan.Hours) == 1)
                return "1 hour ago";
            if (Math.Abs(timeSpan.Hours) > 1)
                return "{0} hrs ago".FormatWith(Math.Abs(timeSpan.Hours));
            if (Math.Abs(timeSpan.Minutes) == 1)
                return "1 min ago";
            if (Math.Abs(timeSpan.Minutes) > 1)
                return "{0} mins ago".FormatWith(Math.Abs(timeSpan.Minutes));
            if (Math.Abs(timeSpan.Seconds) == 1)
                return "1 sec ago";
            if (Math.Abs(timeSpan.Seconds) > 1)
                return "{0} secs ago".FormatWith(Math.Abs(timeSpan.Seconds));
            return string.Empty;
        }

        private static double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}