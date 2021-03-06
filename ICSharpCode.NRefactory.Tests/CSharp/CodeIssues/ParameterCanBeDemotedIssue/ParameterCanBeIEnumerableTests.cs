//
// ParameterCanBeIEnumerableTests.cs
//
// Author:
//       Ciprian Khlud <ciprian.mustiata@yahoo.com>
//
// Copyright (c) 2013 Ciprian Khlud
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
using ICSharpCode.NRefactory.PlayScript.CodeActions;
using ICSharpCode.NRefactory.PlayScript.Refactoring;

namespace ICSharpCode.NRefactory.PlayScript.CodeIssues
{
	[TestFixture]
	public class ParameterCanBeIEnumerableTests : InspectionActionTestBase
	{
		[Test]
		public void CheckIEnumerable()
		{
			var input = @"
using System.Collections.Generic;
class TestClass
{
	void Write(string[] texts)
	{
		foreach(var item in texts) { }
	}
}";
			TestRefactoringContext context;
			var issues = GetIssues(new ParameterCanBeIEnumerableIssue(), input, out context);
			Assert.AreEqual(1, issues.Count);
			var issue = issues[0];
			// Suggested types: IList<T> and IReadOnlyList<T>
			Assert.AreEqual(1, issue.Actions.Count);

            CheckFix(context, issues[0], @"
using System.Collections.Generic;
class TestClass
{
	void Write(IEnumerable<string> texts)
	{
		foreach(var item in texts) { }
	}
}");
		}
        [Test]
        public void CheckIList()
        {
            var input = @"
using System.Collections.Generic;
class TestClass
{
	void Write(List<string> texts)
	{
        if(texts.Count == 0)return;
		foreach(var item in texts) { }
	}
}";
            TestRefactoringContext context;
            var issues = GetIssues(new ParameterCanBeIEnumerableIssue(), input, out context);
            Assert.AreEqual(1, issues.Count);
            var issue = issues[0];
            // Suggested types: IList<T> and IReadOnlyList<T>
            Assert.AreEqual(1, issue.Actions.Count);

            CheckFix(context, issues[0], @"
using System.Collections.Generic;
class TestClass
{
	void Write(ICollection<string> texts)
	{
        if(texts.Count == 0)return;
		foreach(var item in texts) { }
	}
}");
        }

		[Ignore("Bug")]
		[Test]
		public void TestBug()
		{
			var input = @"
using System;
class TestClass
{
	void Write(int x = 34)
	{
		x = 5;
		Console.WriteLine (x);
	}
}";
			TestWrongContext <ParameterCanBeIEnumerableIssue> (input);
		}

	}
}
