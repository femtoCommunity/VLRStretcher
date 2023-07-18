using Produire;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VLRStretcher.Helper
{
	public class VLRStretcherHelper : IProduireStaticClass // これを実装しないとプロデルから利用できない
	{
		// 手順
		[自分("を")]
		public bool 実行する([省略][で] string プロセス名 = "VALORANT-Win64-Shipping")
		{
			return WindowHelper.Execute(プロセス名);
		}
	}

	class WindowHelper
	{
		const int GWL_STYLE = -16;
		const int WS_BORDER = 0x00800000;
		const int SW_MAXIMIZE = 3;

		[DllImport("user32.dll")]
		static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		public static bool Execute(string processName)
		{
			Process[] processes = Process.GetProcessesByName(processName);
			if (processes.Length > 0)
			{
				IntPtr mainWindowHandle = processes[0].MainWindowHandle;
				IntPtr windowStyle = GetWindowLongPtr(mainWindowHandle, GWL_STYLE);

				// ウィンドウスタイルを変更
				windowStyle = new IntPtr((long)windowStyle & ~WS_BORDER);
				SetWindowLongPtr(mainWindowHandle, GWL_STYLE, windowStyle);

				// ウィンドウを最大化
				ShowWindow(mainWindowHandle, SW_MAXIMIZE);

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
