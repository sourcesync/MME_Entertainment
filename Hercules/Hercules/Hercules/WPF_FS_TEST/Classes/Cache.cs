using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace WP7Square.Classes
{
    public static class Cache
    {
        private static Dictionary<string, object> pool = new Dictionary<string, object>();
        
        public static object Get(string key)
        {
            if (!pool.ContainsKey(key))
            {
                return null;
            }
            return pool[key];
        }

        public static void Remove(string key)
        {
            Set(key, null);
        }

        public static void Set(string key, object value)
        {
            if (pool.ContainsKey(key))
            {
                pool.Remove(key);
            }
            if (key != null)
            {
                pool.Add(key, value);
            }            
        }

    }
}
