﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace FullScreenDemo
{

    /// <summary>
    /// A behavior that adds full-screen functionality to a window when the window is maximized,
    /// double clicked, or the escape key is pressed.
    /// </summary>
    public sealed class FullScreenBehavior : Behavior<Window>
    {

        #region Fields

        private HwndSource hwndSource;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FullScreenBehavior"/> class.
        /// </summary>
        public FullScreenBehavior( )
        {
        }

        #endregion

        #region Properties

        #region FullScreenOnMaximize

        /// <summary>
        /// Dependency property for the <see cref="P:FullScreenOnMaximize"/> property.
        /// </summary>
        public static readonly DependencyProperty FullScreenOnMaximizeProperty = DependencyProperty.Register( "FullScreenOnMaximize", typeof( bool ), typeof( FullScreenBehavior ), new PropertyMetadata( default( bool ) ) );

        /// <summary>
        /// Whether or not user initiated maximizing should put the window into full-screen mode.
        /// </summary>
        public bool FullScreenOnMaximize
        {
            get
            {
                return (bool)GetValue( FullScreenOnMaximizeProperty );
            }
            set
            {
                SetValue( FullScreenOnMaximizeProperty, value );
            }
        }

        #endregion

        #region FullScreenOnDoubleClick

        /// <summary>
        /// Dependency property for the <see cref="P:FullScreenOnDoubleClick"/> property.
        /// </summary>
        public static readonly DependencyProperty FullScreenOnDoubleClickProperty = DependencyProperty.Register( "FullScreenOnDoubleClick", typeof( bool ), typeof( FullScreenBehavior ), new PropertyMetadata( default( bool ) ) );

        /// <summary>
        /// Whether or not double clicking the window's contents should put the window into full-screen mode.
        /// </summary>
        public bool FullScreenOnDoubleClick
        {
            get
            {
                return (bool)GetValue( FullScreenOnDoubleClickProperty );
            }
            set
            {
                SetValue( FullScreenOnDoubleClickProperty, value );
            }
        }

        #endregion

        #region RestoreOnEscape

        /// <summary>
        /// Dependency property for the <see cref="P:RestoreOnEscape"/> property.
        /// </summary>
        public static readonly DependencyProperty RestoreOnEscapeProperty = DependencyProperty.Register( "RestoreOnEscape", typeof( bool ), typeof( FullScreenBehavior ), new PropertyMetadata( default( bool ) ) );

        /// <summary>
        /// Whether or not pressing escape while in full screen mode returns to windowed mode.
        /// </summary>
        public bool RestoreOnEscape
        {
            get
            {
                return (bool)GetValue( RestoreOnEscapeProperty );
            }
            set
            {
                SetValue( RestoreOnEscapeProperty, value );
            }
        }

        #endregion

        #region IsFullScreen

        /// <summary>
        /// Dependency property for the <see cref="P:IsFullScreen"/> property.
        /// </summary>
        private static readonly DependencyProperty IsFullScreenProperty = DependencyProperty.RegisterAttached( "IsFullScreen", typeof( bool ), typeof( FullScreenBehavior ), new PropertyMetadata( default( bool ), OnIsFullScreenChanged ) );

        /// <summary>
        /// Gets a value indicating whether or not the specified window is currently in full-screen mode.
        /// </summary>
        public static bool GetIsFullScreen( Window window )
        {
            return (bool)window.GetValue( IsFullScreenProperty );
        }

        /// <summary>
        /// Sets a value indicating whether or not the specified window is currently in full-screen mode.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="value">The value.</param>
        public static void SetIsFullScreen( Window window, bool value )
        {
            window.SetValue( IsFullScreenProperty, value );
        }

        /// <summary>
        /// Called when the value of the IsFullScreenProperty dependency property changes.
        /// </summary>
        /// <param name="sender">The control instance.</param>
        /// <param name="e">The event arguments.</param>
        private static void OnIsFullScreenChanged( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {

            var window = (Window)sender;
            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;

            if ( newValue != oldValue && window != null ) {

                if ( newValue ) {
                    window.WindowStyle = WindowStyle.None;
                    window.Topmost = true;
                    window.WindowState = WindowState.Maximized;
                }   // if
                else {
                    window.Topmost = false;
                    window.WindowStyle = WindowStyle.SingleBorderWindow;
                    window.WindowState = WindowState.Normal;
                }   // else

            }   // if

        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached( )
        {

            base.OnAttached( );

            AssociatedObject.SourceInitialized += Window_SourceInitialized;
            AssociatedObject.MouseDoubleClick += Window_MouseDoubleClick;
            AssociatedObject.KeyDown += Window_KeyDown;

            AttachHook( );

        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching( )
        {

            DetachHook( );

            AssociatedObject.SourceInitialized -= Window_SourceInitialized;
            AssociatedObject.MouseDoubleClick -= Window_MouseDoubleClick;
            AssociatedObject.KeyDown -= Window_KeyDown;

            base.OnDetaching( );

        }

        /// <summary>
        /// Adds the hook procedure to the Window's HwndSource.
        /// </summary>
        private void AttachHook( )
        {
            if ( hwndSource == null ) {
                hwndSource = (HwndSource)HwndSource.FromVisual( AssociatedObject );
                if ( hwndSource != null ) {
                    hwndSource.AddHook( WndProc );
                }   // if
            }   // if
        }

        /// <summary>
        /// Removes the hook procedure from the Window's HwndSource.
        /// </summary>
        private void DetachHook( )
        {

            if ( hwndSource != null ) {
                hwndSource.RemoveHook( WndProc );
                hwndSource = null;
            }   // if

        }

        /// <summary>
        /// A hook procedure that intercepts messages sent to the attached window.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <param name="msg">The message.</param>
        /// <param name="wParam">The wParam which varies by message.</param>
        /// <param name="lParam">The lParam which varies by message.</param>
        /// <param name="handled">Set to true to suppress default process of this message.</param>
        /// <returns>The return value which depends upon the message.</returns>
        private IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
        {

            if ( msg == WM_SYSCOMMAND ) {

                int wParam32 = wParam.ToInt32( ) & 0xFFF0;
                if ( wParam32 == SC_MAXIMIZE || wParam32 == SC_RESTORE ) {

                    if ( FullScreenOnMaximize ) {

                        // Cancel the default handling
                        handled = true;

                        // Go to full screen on maximize
                        // Return from full screen on restore
                        SetIsFullScreen( AssociatedObject, (wParam32 == SC_MAXIMIZE) );                        

                    }   // if

                }   // if

            }   // if

            return IntPtr.Zero;

        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the SourceInitialized event of the Window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:EventArgs"/> instance containing the event data.</param>
        private void Window_SourceInitialized( object sender, EventArgs e )
        {

            AttachHook( );

        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the Window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Window_MouseDoubleClick( object sender, MouseButtonEventArgs e )
        {

            if ( e.Handled == false ) {

                if ( FullScreenOnDoubleClick ) {

                    bool current = GetIsFullScreen( AssociatedObject );
                    SetIsFullScreen( AssociatedObject, !current );

                }   // if

            }   // if

        }

        /// <summary>
        /// Handles the KeyDown event of the Window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:KeyEventArgs"/> instance containing the event data.</param>
        private void Window_KeyDown( object sender, KeyEventArgs e )
        {

            if ( e.Key == Key.Escape && e.Handled == false ) {

                if ( RestoreOnEscape ) {

                    SetIsFullScreen( AssociatedObject, false );

                }   // if

            }   // if

        }

        #endregion

		#region Interop Stuff
		
		const int WM_SYSCOMMAND = 0x112;
        const int SC_RESTORE = 0xF120;
        const int SC_MAXIMIZE = 0xF030;

		#endregion
		
    }   // class

}   // namespace
