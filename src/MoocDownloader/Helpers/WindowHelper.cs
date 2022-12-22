// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

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
/// 窗体辅助.
/// </summary>
public static class WindowHelper
{
    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
    internal static extern void DwmSetWindowAttribute
    (
        IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute
    );

    /// <summary>
    /// 设置窗体圆角样式.
    /// </summary>
    /// <param name="window">窗体.</param>
    public static void SetWindowCornerStyle(Window? window)
    {
        if (window is null)
        {
            return;
        }

        try
        {
            var hWnd = new WindowInteropHelper(window).EnsureHandle();
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;

            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
        }
        catch (Exception)
        {
            //
        }
    }
}