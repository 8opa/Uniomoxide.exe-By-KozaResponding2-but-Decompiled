using System;

namespace Uniomoxide
{
	// Token: 0x02000005 RID: 5
	public struct BITMAPINFOHEADER
	{
		// Token: 0x04000002 RID: 2
		public uint biSize;

		// Token: 0x04000003 RID: 3
		public int biWidth;

		// Token: 0x04000004 RID: 4
		public int biHeight;

		// Token: 0x04000005 RID: 5
		public ushort biPlanes;

		// Token: 0x04000006 RID: 6
		public ushort biBitCount;

		// Token: 0x04000007 RID: 7
		public uint biCompression;

		// Token: 0x04000008 RID: 8
		public uint biSizeImage;

		// Token: 0x04000009 RID: 9
		public int biXPelsPerMeter;

		// Token: 0x0400000A RID: 10
		public int biYPelsPerMeter;

		// Token: 0x0400000B RID: 11
		public uint biClrUsed;

		// Token: 0x0400000C RID: 12
		public uint biClrImportant;
	}
}
