using System;
using System.Windows.Forms;

namespace Uniomoxide
{
	// Token: 0x0200000D RID: 13
	internal static class Program
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00002368 File Offset: 0x00000568
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
