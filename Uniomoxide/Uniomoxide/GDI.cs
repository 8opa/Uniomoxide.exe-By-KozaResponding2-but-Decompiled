using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Uniomoxide
{
	// Token: 0x02000008 RID: 8
	internal class GDI
	{
		// Token: 0x0600000B RID: 11
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x0600000C RID: 12
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x0600000D RID: 13
		[DllImport("gdi32.dll")]
		private static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

		// Token: 0x0600000E RID: 14
		[DllImport("gdi32.dll")]
		private static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop);

		// Token: 0x0600000F RID: 15
		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hwnd);

		// Token: 0x06000010 RID: 16 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private static HSL RGBtoHSL(byte r, byte g, byte b)
		{
			float num = (float)r / 255f;
			float num2 = (float)g / 255f;
			float num3 = (float)b / 255f;
			float num4 = Math.Max(num, Math.Max(num2, num3));
			float num5 = Math.Min(num, Math.Min(num2, num3));
			float num6 = (num4 + num5) / 2f;
			float num7;
			float num8;
			if (num4 == num5)
			{
				num7 = 0f;
				num8 = 0f;
			}
			else
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
			return new HSL
			{
				h = num7,
				s = num8,
				l = num6
			};
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002BDC File Offset: 0x00000DDC
		private static void HSLtoRGB(HSL hsl, out byte r, out byte g, out byte b)
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
				num2 = GDI.HueToRGB(num5, num4, h + 0.33333334f);
				num = GDI.HueToRGB(num5, num4, h);
				num3 = GDI.HueToRGB(num5, num4, h - 0.33333334f);
			}
			r = (byte)(num2 * 255f);
			g = (byte)(num * 255f);
			b = (byte)(num3 * 255f);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C8C File Offset: 0x00000E8C
		private static float HueToRGB(float p, float q, float t)
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002D0C File Offset: 0x00000F0C
		public static void GDIBow()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(140.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				BITMAPINFO bitmapinfo = default(BITMAPINFO);
				bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
				bitmapinfo.bmiHeader.biWidth = GDI.w;
				bitmapinfo.bmiHeader.biHeight = -GDI.hgt;
				bitmapinfo.bmiHeader.biPlanes = 1;
				bitmapinfo.bmiHeader.biBitCount = 32;
				bitmapinfo.bmiHeader.biCompression = 0U;
				IntPtr intPtr2;
				IntPtr intPtr = GDI.CreateDIBSection(dc, ref bitmapinfo, 0U, out intPtr2, IntPtr.Zero, 0U);
				IntPtr intPtr3 = GDI.CreateCompatibleDC(dc);
				GDI.SelectObject(intPtr3, intPtr);
				GDI.BitBlt(intPtr3, 0, 0, GDI.w, GDI.hgt, dc, 0, 0, 13369376U);
				int num = GDI.w * GDI.hgt;
				int[] array = new int[num];
				Marshal.Copy(intPtr2, array, 0, num);
				new Random();
				float num2 = 0.08f;
				while (DateTime.Now - now < timeSpan)
				{
					for (int i = 0; i < num; i++)
					{
						int num3 = array[i];
						byte b = (byte)(num3 & 255);
						byte b2 = (byte)((num3 >> 8) & 255);
						byte b3 = (byte)((num3 >> 16) & 255);
						HSL hsl = GDI.RGBtoHSL(b3, b2, b);
						hsl.h += num2;
						if (hsl.h > 1f)
						{
							hsl.h -= 1f;
						}
						GDI.HSLtoRGB(hsl, out b3, out b2, out b);
						array[i] = (int)b | ((int)b2 << 8) | ((int)b3 << 16);
					}
					Marshal.Copy(array, 0, intPtr2, num);
					GDI.BitBlt(dc, 0, 0, GDI.w, GDI.hgt, intPtr3, 0, 0, 13369376U);
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002F1C File Offset: 0x0000111C
		public static void GDIBow2()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				BITMAPINFO bitmapinfo = default(BITMAPINFO);
				bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
				bitmapinfo.bmiHeader.biWidth = GDI.w;
				bitmapinfo.bmiHeader.biHeight = -GDI.hgt;
				bitmapinfo.bmiHeader.biPlanes = 1;
				bitmapinfo.bmiHeader.biBitCount = 32;
				bitmapinfo.bmiHeader.biCompression = 0U;
				IntPtr intPtr2;
				IntPtr intPtr = GDI.CreateDIBSection(dc, ref bitmapinfo, 0U, out intPtr2, IntPtr.Zero, 0U);
				IntPtr intPtr3 = GDI.CreateCompatibleDC(dc);
				GDI.SelectObject(intPtr3, intPtr);
				GDI.BitBlt(intPtr3, 0, 0, GDI.w, GDI.hgt, dc, 0, 0, 13369376U);
				int num = GDI.w * GDI.hgt;
				int[] array = new int[num];
				Marshal.Copy(intPtr2, array, 0, num);
				new Random();
				float num2 = 0.08f;
				while (DateTime.Now - now < timeSpan)
				{
					for (int i = 0; i < num; i++)
					{
						int num3 = array[i];
						byte b = (byte)(num3 & 255);
						byte b2 = (byte)((num3 >> 14) & 255);
						byte b3 = (byte)((num3 >> 16) & 255);
						HSL hsl = GDI.RGBtoHSL(b3, b2, b);
						hsl.h += num2;
						if (hsl.h > 1f)
						{
							hsl.h -= 5f;
						}
						GDI.HSLtoRGB(hsl, out b3, out b2, out b);
						array[i] = (int)b2 | ((int)b2 << 14) | ((int)b3 << 16);
					}
					Marshal.Copy(array, 0, intPtr2, num);
					GDI.BitBlt(dc, 0, 0, GDI.w, GDI.hgt, intPtr3, 0, 0, 13369376U);
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000312C File Offset: 0x0000132C
		public static void GDIBow3()
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = TimeSpan.FromSeconds(30.0);
			while (DateTime.Now - now < timeSpan)
			{
				IntPtr dc = GDI.GetDC(IntPtr.Zero);
				BITMAPINFO bitmapinfo = default(BITMAPINFO);
				bitmapinfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
				bitmapinfo.bmiHeader.biWidth = GDI.w;
				bitmapinfo.bmiHeader.biHeight = -GDI.hgt;
				bitmapinfo.bmiHeader.biPlanes = 1;
				bitmapinfo.bmiHeader.biBitCount = 32;
				bitmapinfo.bmiHeader.biCompression = 0U;
				IntPtr intPtr2;
				IntPtr intPtr = GDI.CreateDIBSection(dc, ref bitmapinfo, 0U, out intPtr2, IntPtr.Zero, 0U);
				IntPtr intPtr3 = GDI.CreateCompatibleDC(dc);
				GDI.SelectObject(intPtr3, intPtr);
				GDI.BitBlt(intPtr3, 0, 0, GDI.w, GDI.hgt, dc, 0, 0, 13369376U);
				int num = GDI.w * GDI.hgt;
				int[] array = new int[num];
				Marshal.Copy(intPtr2, array, 0, num);
				new Random();
				float num2 = 0.1f;
				while (DateTime.Now - now < timeSpan)
				{
					for (int i = 0; i < num; i++)
					{
						int num3 = array[i];
						byte b = (byte)(num3 & 255);
						byte b2 = (byte)((num3 / 8) & 255);
						byte b3 = (byte)((num3 >> 16) & 255);
						byte b4 = (byte)((num3 >> 16) & 255);
						HSL hsl = GDI.RGBtoHSL(b3, b2, b);
						hsl.h += num2;
						if (hsl.h > 1f)
						{
							hsl.h -= 1f;
						}
						GDI.HSLtoRGB(hsl, out b3, out b2, out b);
						array[i] = (int)b | ((int)b3 << 8) | ((int)b4 << 16);
					}
					Marshal.Copy(array, 0, intPtr2, num);
					GDI.BitBlt(dc, 0, 0, GDI.w, GDI.hgt, intPtr3, 0, 0, 13369376U);
					Thread.Sleep(1);
				}
			}
		}

		// Token: 0x04000012 RID: 18
		private const int BI_RGB = 0;

		// Token: 0x04000013 RID: 19
		private const uint SRCCOPY = 13369376U;

		// Token: 0x04000014 RID: 20
		private static int w = 1920;

		// Token: 0x04000015 RID: 21
		private static int hgt = 1080;

		// Token: 0x04000016 RID: 22
		private static IntPtr g_hdcScreen;

		// Token: 0x04000017 RID: 23
		private static IntPtr g_hdcMem;

		// Token: 0x04000018 RID: 24
		private static IntPtr g_hbmTemp;

		// Token: 0x04000019 RID: 25
		private static int g_w;

		// Token: 0x0400001A RID: 26
		private static int g_h;
	}
}
