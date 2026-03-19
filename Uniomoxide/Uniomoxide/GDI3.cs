using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Uniomoxide
{
	// Token: 0x0200000A RID: 10
	internal class GDI3
	{
		// Token: 0x0600003B RID: 59
		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x0600003C RID: 60
		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x0600003D RID: 61
		[DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int nIndex);

		// Token: 0x0600003E RID: 62
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x0600003F RID: 63
		[DllImport("gdi32.dll")]
		private static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x06000040 RID: 64
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDIBSection(IntPtr hdc, ref GDI3.BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x06000041 RID: 65
		[DllImport("gdi32.dll")]
		private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		// Token: 0x06000042 RID: 66
		[DllImport("gdi32.dll")]
		private static extern bool DeleteObject(IntPtr hObject);

		// Token: 0x06000043 RID: 67
		[DllImport("gdi32.dll")]
		private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		// Token: 0x06000044 RID: 68
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vKey);

		// Token: 0x06000045 RID: 69 RVA: 0x00003F38 File Offset: 0x00002138
		private static void PixelThread()
		{
			while (GDI3.Running)
			{
				int num = GDI3.PixelStart;
				num += 2;
				if (num > 256)
				{
					num = 2;
				}
				GDI3.PixelStart = num;
				Thread.Sleep(800);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003F7C File Offset: 0x0000217C
		private static void PixelateBuffer(uint[] px, int w, int h, int block)
		{
			if (px != null && block > 1)
			{
				for (int i = 0; i < h; i += block)
				{
					for (int j = 0; j < w; j += block)
					{
						uint num = 0U;
						uint num2 = 0U;
						uint num3 = 0U;
						int num4 = 0;
						int num5 = Math.Min(block, h - i);
						int num6 = Math.Min(block, w - j);
						for (int k = 0; k < num5; k++)
						{
							int num7 = (i + k) * w + j;
							for (int l = 0; l < num6; l++)
							{
								uint num8 = px[num7 + l];
								num3 += num8 & 255U;
								num2 += (num8 >> 8) & 255U;
								num += (num8 >> 16) & 255U;
								num4++;
							}
						}
						if (num4 != 0)
						{
							byte b = (byte)((ulong)num / (ulong)((long)num4));
							byte b2 = (byte)((ulong)num2 / (ulong)((long)num4));
							byte b3 = (byte)((ulong)num3 / (ulong)((long)num4));
							uint num9 = (uint)(((int)b << 16) | ((int)b2 << 8) | (int)b3);
							for (int m = 0; m < num5; m++)
							{
								int num10 = (i + m) * w + j;
								for (int n = 0; n < num6; n++)
								{
									px[num10 + n] = num9;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000040BC File Offset: 0x000022BC
		public static void Init()
		{
			IntPtr dc = GDI3.GetDC(IntPtr.Zero);
			if (!(dc == IntPtr.Zero))
			{
				GDI3.GetSystemMetrics(0);
			}
		}

		// Token: 0x0400002F RID: 47
		private const int SM_CXSCREEN = 0;

		// Token: 0x04000030 RID: 48
		private const int SM_CYSCREEN = 1;

		// Token: 0x04000031 RID: 49
		private const uint BI_RGB = 0U;

		// Token: 0x04000032 RID: 50
		private const int SRCCOPY = 13369376;

		// Token: 0x04000033 RID: 51
		private static volatile int PixelStart = 2;

		// Token: 0x04000034 RID: 52
		private static volatile bool Running = true;

		// Token: 0x02000016 RID: 22
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct BITMAPINFOHEADER
		{
			// Token: 0x04000079 RID: 121
			public uint biSize;

			// Token: 0x0400007A RID: 122
			public int biWidth;

			// Token: 0x0400007B RID: 123
			public int biHeight;

			// Token: 0x0400007C RID: 124
			public ushort biPlanes;

			// Token: 0x0400007D RID: 125
			public ushort biBitCount;

			// Token: 0x0400007E RID: 126
			public uint biCompression;

			// Token: 0x0400007F RID: 127
			public uint biSizeImage;

			// Token: 0x04000080 RID: 128
			public int biXPelsPerMeter;

			// Token: 0x04000081 RID: 129
			public int biYPelsPerMeter;

			// Token: 0x04000082 RID: 130
			public uint biClrUsed;

			// Token: 0x04000083 RID: 131
			public uint biClrImportant;
		}

		// Token: 0x02000017 RID: 23
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct RGBQUAD
		{
			// Token: 0x04000084 RID: 132
			public byte rgbBlue;

			// Token: 0x04000085 RID: 133
			public byte rgbGreen;

			// Token: 0x04000086 RID: 134
			public byte rgbRed;

			// Token: 0x04000087 RID: 135
			public byte rgbReserved;
		}

		// Token: 0x02000018 RID: 24
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct BITMAPINFO
		{
			// Token: 0x04000088 RID: 136
			public GDI3.BITMAPINFOHEADER bmiHeader;

			// Token: 0x04000089 RID: 137
			public GDI3.RGBQUAD bmiColors;
		}
	}
}
