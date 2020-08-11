
using System;

namespace CyanMod {
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	sealed class CommandAttribute : Attribute {
		// See the attribute guidelines at 
		//  http://go.microsoft.com/fwlink/?LinkId=85236
		public CommandAttribute(string command) {
			Value = command;
		}

		public string Value { get; private set; }
	}
}