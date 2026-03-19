using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Uniomoxide.Properties
{
	// Token: 0x0200000E RID: 14
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000022C4 File Offset: 0x000004C4
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004AF4 File Offset: 0x00002CF4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("Uniomoxide.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004B34 File Offset: 0x00002D34
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000237F File Offset: 0x0000057F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400005E RID: 94
		private static ResourceManager resourceMan;

		// Token: 0x0400005F RID: 95
		private static CultureInfo resourceCulture;
	}
}
