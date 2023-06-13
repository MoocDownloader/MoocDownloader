using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace MoocDownloader.Helpers;

public enum DWMWINDOWATTRIBUTE
{
    DWMWA_WINDOW_CORNER_PREFERENCE = 33
}

public enum DWM_WINDOW_CORNER_PREFERENCE
{
    DWMWCP_DEFAULT = 0,
    DWMWCP_DONOTROUND = 1,
    DWMWCP_ROUND = 2,
    DWMWCP_ROUNDSMALL = 3
}

/// <summary>
/// Window helper.
/// </summary>
public static class WindowHelper
{
    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
    internal static extern void DwmSetWindowAttribute(
        IntPtr hwnd,
        DWMWINDOWATTRIBUTE attribute,
        ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
        uint cbAttribute);

    /// <summary>
    /// Apply rounded corners in desktop apps for Windows 11.
    /// </summary>
    /// <param name="window">The window.</param>
    public static void SetWindowCornerStyle(Window window)
    {
        try
        {
            const DWMWINDOWATTRIBUTE attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;
            var hWnd = new WindowInteropHelper(window).EnsureHandle();

            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
        }
        catch (ArgumentException)
        {
            //
        }
    }
}