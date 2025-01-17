/*
 * This file is auto-generated, do not modify directly.
 */

using System.Collections.Generic;
using Overlayer.Core.JavaScript;

namespace Overlayer.Core.JavaScript.Library
{

	public partial class WeakMapConstructor
	{

		private static object __STUB__Call(ScriptEngine engine, object thisObj, object[] args)
		{
			thisObj = TypeConverter.ToObject(engine, thisObj);
			if (!(thisObj is WeakMapConstructor))
				throw new JavaScriptException(ErrorType.TypeError, "The method 'Call' is not generic.");
			return ((WeakMapConstructor)thisObj).Call();
		}

		private static ObjectInstance __STUB__Construct(ScriptEngine engine, FunctionInstance thisObj, FunctionInstance newTarget, object[] args)
		{
			switch (args.Length)
			{
				case 0:
					return ((WeakMapConstructor)thisObj).Construct(Undefined.Value);
				default:
					return ((WeakMapConstructor)thisObj).Construct(args[0]);
			}
		}
	}

}
