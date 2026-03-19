using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Uniomoxide
{
	// Token: 0x0200000B RID: 11
	internal class GDI4
	{
		// Token: 0x0600004A RID: 74
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x0600004B RID: 75
		[DllImport("gdi32.dll")]
		private static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x0600004C RID: 76
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDIBSection(IntPtr hdc, ref GDI4.BITMAPINFO bmi, uint usage, out IntPtr bits, IntPtr h, uint offset);

		// Token: 0x0600004D RID: 77
		[DllImport("gdi32.dll")]
		private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

		// Token: 0x0600004E RID: 78
		[DllImport("gdi32.dll")]
		private static extern bool BitBlt(IntPtr d, int dx, int dy, int w, int h, IntPtr s, int sx, int sy, uint rop);

		// Token: 0x0600004F RID: 79
		[DllImport("gdi32.dll")]
		private static extern bool PlgBlt(IntPtr hdc, GDI4.POINT[] p, IntPtr src, int x, int y, int w, int h, IntPtr mask, int mx, int my);

		// Token: 0x06000050 RID: 80
		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hwnd);

		// Token: 0x06000051 RID: 81
		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		// Token: 0x06000052 RID: 82
		[DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int n);

		// Token: 0x06000053 RID: 83 RVA: 0x000040E8 File Offset: 0x000022E8
		private static GDI4.HSL RGBtoHSL(byte r, byte g, byte b)
		{
			float num = (float)r / 255f;
			float num2 = (float)g / 255f;
			float num3 = (float)b / 255f;
			float num4 = Math.Max(num, Math.Max(num2, num3));
			float num5 = Math.Min(num, Math.Min(num2, num3));
			float num6 = (num4 + num5) * 0.5f;
			float num7 = 0f;
			float num8 = 0f;
			if (num4 != num5)
			{
				float num9 = num4 - num5;
				num8 = ((num6 > 0.5f) ? (num9 / (2f - num4 - num5)) : (num9 / (num4 + num5)));
				if (num4 == num)
				{
					num7 = (num2 - num3) / num9 + (float)((num2 < num3) ? 6 : 0);
				}
				else if (num4 == num2)
				{
					num7 = (num3 - num) / num9 + 2f;
				}
				else
				{
					num7 = (num - num2) / num9 + 4f;
				}
				num7 /= 6f;
			}
			return new GDI4.HSL
			{
				h = num7,
				s = num8,
				l = num6
			};
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C8C File Offset: 0x00000E8C
		private static float hue2rgb(float p, float q, float t)
		{
			if (t < 0f)
			{
				t += 1f;
			}
			if (t > 1f)
			{
				t -= 1f;
			}
			float num;
			if (t < 0.16666667f)
			{
				num = p + (q - p) * 6f * t;
			}
			else if (t < 0.5f)
			{
				num = q;
			}
			else if (t < 0.6666667f)
			{
				num = p + (q - p) * (0.6666667f - t) * 6f;
			}
			else
			{
				num = p;
			}
			return num;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000041EC File Offset: 0x000023EC
		private static void HSLtoRGB(GDI4.HSL hsl, out byte r, out byte g, out byte b)
		{
			float h = hsl.h;
			float s = hsl.s;
			float l = hsl.l;
			float num3;
			float num2;
			float num;
			if (s == 0f)
			{
				num = (num2 = (num3 = l));
			}
			else
			{
				float num4 = ((l < 0.5f) ? (l * (1f + s)) : (l + s - l * s));
				float num5 = 2f * l - num4;
				num2 = GDI4.hue2rgb(num5, num4, h + 0.33333334f);
				num = GDI4.hue2rgb(num5, num4, h);
				num3 = GDI4.hue2rgb(num5, num4, h - 0.33333334f);
			}
			r = (byte)(num2 * 255f);
			g = (byte)(num * 255f);
			b = (byte)(num3 * 255f);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000429C File Offset: 0x0000249C
		private static void rot()
		{
			GDI4.POINT[] array = new GDI4.POINT[3];
			double num = GDI4.a * 0.017453292519943;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			int num4 = GDI4.sw >> 1;
			int num5 = GDI4.sh >> 1;
			int num6 = -num4;
			int num7 = -num5;
			int num8 = GDI4.sw - num4;
			int num9 = -num5;
			int num10 = -num4;
			int num11 = GDI4.sh - num5;
			array[0].x = num4 + (int)((double)num6 * num2 - (double)num7 * num3);
			array[0].y = num5 + (int)((double)num6 * num3 + (double)num7 * num2);
			array[1].x = num4 + (int)((double)num8 * num2 - (double)num9 * num3);
			array[1].y = num5 + (int)((double)num8 * num3 + (double)num9 * num2);
			array[2].x = num4 + (int)((double)num10 * num2 - (double)num11 * num3);
			array[2].y = num5 + (int)((double)num10 * num3 + (double)num11 * num2);
			GDI4.PlgBlt(GDI4.rotdc, array, GDI4.srcdc, 0, 0, GDI4.sw, GDI4.sh, IntPtr.Zero, 0, 0);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000043D0 File Offset: 0x000025D0
		private unsafe static void HSLThread()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			int num = GDI4.sw;
			int num2 = GDI4.sh;
			float num3 = 0.08f;
			byte* ptr = (byte*)(void*)GDI4.outbits;
			while (DateTime.Now - now < timeSpan)
			{
				for (int i = 0; i < num * num2; i++)
				{
					byte* ptr2 = ptr + i * 4;
					GDI4.HSL hsl = GDI4.RGBtoHSL(ptr2[2], ptr2[1], *ptr2);
					hsl.s *= 2f;
					if (hsl.s > 1f)
					{
						hsl.s = 1f;
					}
					hsl.l *= 1.25f;
					if (hsl.l > 1f)
					{
						hsl.l = 1f;
					}
					hsl.h += num3;
					if (hsl.h > 1f)
					{
						hsl.h -= 1f;
					}
					byte b;
					byte b2;
					byte b3;
					GDI4.HSLtoRGB(hsl, out b, out b2, out b3);
					ptr2[2] = b;
					ptr2[1] = b2;
					*ptr2 = b3;
				}
				GDI4.BitBlt(GDI4.sdc, 0, 0, num, num2, GDI4.outdc, 0, 0, 13369376U);
				Thread.Sleep(1);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000452C File Offset: 0x0000272C
		public static void ROXT()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				GDI4.sdc = GDI4.GetDC(IntPtr.Zero);
				GDI4.sw = GDI4.GetSystemMetrics(0);
				GDI4.sh = GDI4.GetSystemMetrics(1);
				GDI4.srcdc = GDI4.CreateCompatibleDC(GDI4.sdc);
				GDI4.rotdc = GDI4.CreateCompatibleDC(GDI4.sdc);
				GDI4.outdc = GDI4.CreateCompatibleDC(GDI4.sdc);
				GDI4.BITMAPINFO bitmapinfo = default(GDI4.BITMAPINFO);
				bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(GDI4.BITMAPINFOHEADER));
				bitmapinfo.bmiHeader.biWidth = GDI4.sw;
				bitmapinfo.bmiHeader.biHeight = -GDI4.sh;
				bitmapinfo.bmiHeader.biPlanes = 1;
				bitmapinfo.bmiHeader.biBitCount = 32;
				bitmapinfo.bmiHeader.biCompression = 0U;
				GDI4.srcbmp = GDI4.CreateDIBSection(GDI4.sdc, ref bitmapinfo, 0U, out GDI4.srcbits, IntPtr.Zero, 0U);
				GDI4.rotbmp = GDI4.CreateDIBSection(GDI4.sdc, ref bitmapinfo, 0U, out GDI4.rotbits, IntPtr.Zero, 0U);
				GDI4.outbmp = GDI4.CreateDIBSection(GDI4.sdc, ref bitmapinfo, 0U, out GDI4.outbits, IntPtr.Zero, 0U);
				GDI4.SelectObject(GDI4.srcdc, GDI4.srcbmp);
				GDI4.SelectObject(GDI4.rotdc, GDI4.rotbmp);
				GDI4.SelectObject(GDI4.outdc, GDI4.outbmp);
				GDI4.BitBlt(GDI4.srcdc, 0, 0, GDI4.sw, GDI4.sh, GDI4.sdc, 0, 0, 13369376U);
				GDI4.BitBlt(GDI4.rotdc, 0, 0, GDI4.sw, GDI4.sh, GDI4.srcdc, 0, 0, 13369376U);
				GDI4.BitBlt(GDI4.outdc, 0, 0, GDI4.sw, GDI4.sh, GDI4.rotdc, 0, 0, 13369376U);
				new Thread(new ThreadStart(GDI4.HSLThread)).Start();
				while (DateTime.Now - now < timeSpan)
				{
					double num = (double)(new Random().Next(2000) + 1) / 100.0;
					GDI4.a += (double)GDI4.dir * 0.1;
					if (GDI4.a >= num)
					{
						GDI4.a = 0.0;
						GDI4.dir = -1;
					}
					if (GDI4.a <= -num)
					{
						GDI4.a = 0.0;
						GDI4.dir = 1;
					}
					GDI4.rot();
					GDI4.BitBlt(GDI4.outdc, 0, 0, GDI4.sw, GDI4.sh, GDI4.rotdc, 0, 0, 13369376U);
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x04000035 RID: 53
		private const uint SRCCOPY = 13369376U;

		// Token: 0x04000036 RID: 54
		private static IntPtr sdc;

		// Token: 0x04000037 RID: 55
		private static IntPtr srcdc;

		// Token: 0x04000038 RID: 56
		private static IntPtr rotdc;

		// Token: 0x04000039 RID: 57
		private static IntPtr outdc;

		// Token: 0x0400003A RID: 58
		private static IntPtr srcbmp;

		// Token: 0x0400003B RID: 59
		private static IntPtr rotbmp;

		// Token: 0x0400003C RID: 60
		private static IntPtr outbmp;

		// Token: 0x0400003D RID: 61
		private static IntPtr srcbits;

		// Token: 0x0400003E RID: 62
		private static IntPtr rotbits;

		// Token: 0x0400003F RID: 63
		private static IntPtr outbits;

		// Token: 0x04000040 RID: 64
		private static int sw;

		// Token: 0x04000041 RID: 65
		private static int sh;

		// Token: 0x04000042 RID: 66
		private static double a = 0.0;

		// Token: 0x04000043 RID: 67
		private static int dir = 1;

		// Token: 0x04000044 RID: 68
		private static bool stop = false;

		// Token: 0x02000019 RID: 25
		private struct POINT
		{
			// Token: 0x0400008A RID: 138
			public int x;

			// Token: 0x0400008B RID: 139
			public int y;
		}

		// Token: 0x0200001A RID: 26
		private struct BITMAPINFOHEADER
		{
			// Token: 0x0400008C RID: 140
			public uint biSize;

			// Token: 0x0400008D RID: 141
			public int biWidth;

			// Token: 0x0400008E RID: 142
			public int biHeight;

			// Token: 0x0400008F RID: 143
			public ushort biPlanes;

			// Token: 0x04000090 RID: 144
			public ushort biBitCount;

			// Token: 0x04000091 RID: 145
			public uint biCompression;

			// Token: 0x04000092 RID: 146
			public uint biSizeImage;

			// Token: 0x04000093 RID: 147
			public int biXPelsPerMeter;

			// Token: 0x04000094 RID: 148
			public int biYPelsPerMeter;

			// Token: 0x04000095 RID: 149
			public uint biClrUsed;

			// Token: 0x04000096 RID: 150
			public uint biClrImportant;
		}

		// Token: 0x0200001B RID: 27
		private struct BITMAPINFO
		{
			// Token: 0x04000097 RID: 151
			public GDI4.BITMAPINFOHEADER bmiHeader;

			// Token: 0x04000098 RID: 152
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public uint[] bmiColors;
		}

		// Token: 0x0200001C RID: 28
		private struct HSL
		{
			// Token: 0x04000099 RID: 153
			public float h;

			// Token: 0x0400009A RID: 154
			public float s;

			// Token: 0x0400009B RID: 155
			public float l;
		}
	}
}
