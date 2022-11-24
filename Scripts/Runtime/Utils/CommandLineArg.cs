using System;
using System.Collections.Generic;

namespace MakotoStudio.Debugger.Utils {
	public static class CommandLineArg {
		public static Dictionary<string, string> GetApplicationStartArgs() {
			Dictionary<string, string> argDictionary = new Dictionary<string, string>();
			var args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; ++i) {
				var arg = args[i].ToLower();
				if (arg.StartsWith("-")) {
					var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
					value = (value?.StartsWith("-") ?? false) ? null : value;

					argDictionary.Add(arg, value);
				}
			}

			return argDictionary;
		}
	}
}