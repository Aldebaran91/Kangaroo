using Kangaroo.Core;
using System;

namespace Kangaroo.Docu.Implementations
{
	#region Example1

	// Create a custom converter implementation from type Exception to type string
	public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
	{
		// Implement the Convert method to take an object of type Exception as input and to return a string
		public string Convert(Exception data)
		{
			// In this example the ToString method of the object is used for generating the string output, which is returned as result of the conversion
			return data.ToString();
		}
	}

	#endregion Example1
}