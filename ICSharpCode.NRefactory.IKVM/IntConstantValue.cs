//
// IntConstantValue.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
//
// Copyright (c) 2013 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
//using ICSharpCode.NRefactory.TypeSystem;
//using ICSharpCode.NRefactory.Semantics;

using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Documentation;
using ICSharpCode.NRefactory.Utils;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace ICSharpCode.NRefactory.PlayScript
{
	[Serializable]
	sealed class IntConstantValue : IConstantValue, ISupportsInterning 
	{
		readonly int value;

		public IntConstantValue (int value)
		{
			this.value = value;
		}

		public ResolveResult Resolve(ITypeResolveContext context)
		{
			return new ConstantResolveResult(KnownTypeReference.Int32.Resolve (context), value);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
			Justification = "The C# keyword is lower case")]
		public override string ToString()
		{
			return value.ToString ();
		}

		int ISupportsInterning.GetHashCodeForInterning()
		{
			return value.GetHashCode ();
		}

		bool ISupportsInterning.EqualsForInterning(ISupportsInterning other)
		{
			var scv = other as IntConstantValue;
			return scv != null && value == scv.value;
		}
	}
}