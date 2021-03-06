﻿// 
// SuggestUseVarKeywordEvidentTests.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2012 Xamarin Inc. (http://xamarin.com)
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
using NUnit.Framework;
using ICSharpCode.NRefactory.PlayScript.Refactoring;
using ICSharpCode.NRefactory.PlayScript.CodeActions;

namespace ICSharpCode.NRefactory.PlayScript.CodeIssues
{
	[TestFixture]
	public class SuggestUseVarKeywordEvidentTests : InspectionActionTestBase
	{
		[Test]
		public void TestInspectorCase1 ()
		{
			var input = @"class Foo
{
	void Bar (object o)
	{
		Foo foo = (Foo)o;
	}
}";

			TestRefactoringContext context;
			var issues = GetIssues (new SuggestUseVarKeywordEvidentIssue (), input, out context);
			Assert.AreEqual (1, issues.Count);
			// Fix is done by code action.
		}

		[Test]
		public void TestV2 ()
		{
			var input = @"class Foo
{
	void Bar (object o)
	{
		Foo foo = (Foo)o;
	}
}";

			TestRefactoringContext context;
			CSharpParser parser = new CSharpParser();
			parser.CompilerSettings.LanguageVersion = new Version(2, 0, 0);
			var issues = GetIssues (new SuggestUseVarKeywordEvidentIssue (), input, out context, false, parser);
			Assert.AreEqual (0, issues.Count);
		}
	}
}