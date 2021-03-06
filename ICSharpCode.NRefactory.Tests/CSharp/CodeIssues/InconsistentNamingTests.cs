﻿// 
// InconsistentNamingTests.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2012 Xamarin <http://xamarin.com>
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
using System.Linq;
using ICSharpCode.NRefactory.PlayScript.TypeSystem;
using ICSharpCode.NRefactory.Ps.Utils;
using ICSharpCode.NRefactory.Ps;

namespace ICSharpCode.NRefactory.PlayScript.CodeIssues
{
	[TestFixture]
	public class InconsistentNamingTests : InspectionActionTestBase
	{
		void CheckNaming (string input, string output, bool shouldBeEmpty = false)
		{
			TestRefactoringContext context;
			var issues = GetIssues (new InconsistentNamingIssue (), input, out context);
			if (shouldBeEmpty) {
				Assert.AreEqual (0, issues.Count ());
				return;
			}
			Assert.Greater (issues.Count, 0);
			CheckFix (context, issues [0], output);
		}

		[Ignore]
		[Test]
		public void TestUnderscoreFix ()
		{
			var input = @"namespace FIX_1 {}";
			var output = @"namespace Fix1 {}";
			CheckNaming (input, output);
		}

		[Ignore]
		[Test]
		public void TestNamespaceName ()
		{
			var input = @"namespace anIssue {}";
			var output = @"namespace AnIssue {}";
			CheckNaming (input, output);
		}
		
		[Test]
		public void TestClassName ()
		{
			var input = @"public class anIssue {}";
			var output = @"public class AnIssue {}";
			CheckNaming (input, output);
		}

		[Test]
		public void TestAttributeName ()
		{
			var input = @"public class test : System.Attribute {}";
			var output = @"public class TestAttribute : System.Attribute {}";
			CheckNaming (input, output);
		}
		
		[Test]
		public void TestEventArgsName ()
		{
			var input = @"public class test : System.EventArgs {}";
			var output = @"public class TestEventArgs : System.EventArgs {}";
			CheckNaming (input, output);
		}

		[Test]
		public void TestEventArgsSuffixOptimizationName ()
		{
			var input = @"			public class TestArgs : System.EventArgs {}";
			var output = @"			public class TestEventArgs : System.EventArgs {}";
			CheckNaming (input, output);
		}

		[Test]
		public void TestException ()
		{
			var input = @"class test : System.Exception {}";
			var output = @"class TestException : System.Exception {}";
			CheckNaming (input, output);
		}
		
		[Test]
		public void TestStructName ()
		{
			var input = @"public struct anIssue {}";
			var output = @"public struct AnIssue {}";
			CheckNaming (input, output);
		}

		[Test]
		public void TestInterfaceName ()
		{
			var input = @"public interface anIssue {}";
			var output = @"public interface IAnIssue {}";
			CheckNaming (input, output);
		}

		[Test]
		public void TestEnumName ()
		{
			var input = @"public enum anIssue {}";
			var output = @"public enum AnIssue {}";
			CheckNaming (input, output);
		}

		[Test]
		public void TestDelegateName ()
		{
			var input = @"public delegate void anIssue ();";
			var output = @"public delegate void AnIssue ();";
			CheckNaming (input, output);
		}

//		[Test]
//		public void TestPrivateFieldName ()
//		{
//			var input = @"class AClass { int Field; }";
//			var output = @"class AClass { int field; }";
//			CheckNaming (input, output);
//		}
		
//		[Test]
//		public void TestUnderscoreFieldName ()
//		{
//			var input = @"class AClass { int _Field; }";
//			var output = @"class AClass { int _field; }";
//			CheckNaming (input, output);
//		}
		
		[Test]
		public void TestPublicFieldName ()
		{
			var input = @"class AClass { public int field; }";
			var output = @"class AClass { public int Field; }";
			CheckNaming (input, output);
		}
		
//		[Test]
//		public void TestPrivateConstantFieldName ()
//		{
//			var input = @"class AClass { const int field = 5; }";
//			var output = @"class AClass { const int Field = 5; }";
//			CheckNaming (input, output);
//		}
		
		[Test]
		public void TestPublicReadOnlyFieldName ()
		{
			var input = @"class AClass { public readonly int field; }";
			var output = @"class AClass { public readonly int Field; }";
			CheckNaming (input, output);
		}
		
		[Test]
		public void TestPrivateStaticReadOnlyFieldName ()
		{
			var input = @"class AClass { static readonly int Field; }";
			var output = @"class AClass { static readonly int Field; }";
			CheckNaming (input, output, true);
		}
		
		[Test]
		public void TestPrivateStaticReadOnlyFieldNameCase2 ()
		{
			var input = @"class AClass { static readonly int field; }";
			var output = @"class AClass { static readonly int field; }";
			CheckNaming (input, output, true);
		}

//		[Test]
//		public void TestPrivateStaticFieldName ()
//		{
//			var input = @"class AClass { static int Field; }";
//			var output = @"class AClass { static int field; }";
//			CheckNaming (input, output);
//		}

		[Test]
		public void TestPublicStaticReadOnlyFieldName ()
		{
			var input = @"class AClass { public static readonly int field = 5; }";
			var output = @"class AClass { public static readonly int Field = 5; }";
			CheckNaming (input, output);
		}
		
//		[Test]
//		public void TestPrivateReadOnlyFieldName ()
//		{
//			var input = @"class AClass { readonly int Field; }";
//			var output = @"class AClass { readonly int field; }";
//			CheckNaming (input, output);
//		}
		
		[Test]
		public void TestPublicConstantFieldName ()
		{
			var input = @"class AClass { public const int field = 5; }";
			var output = @"class AClass { public const int Field = 5; }";
			CheckNaming (input, output);
		}
		
		[Test]
		public void TestMethodName ()
		{
			var input = @"class AClass { public int method () {} }";
			var output = @"class AClass { public int Method () {} }";
			CheckNaming (input, output);
		}

		[Test]
		public void TestPropertyName ()
		{
			var input = @"class AClass { public int property { get; set; } }";
			var output = @"class AClass { public int Property { get; set; } }";
			CheckNaming (input, output);
		}

		[Test]
		public void TestParameterName ()
		{
			var input = @"class AClass { int Method (int Param) {} }";
			var output = @"class AClass { int Method (int param) {} }";
			CheckNaming (input, output);
		}

		[Test]
		public void TestTypeParameterName ()
		{
			var input = @"struct Str<K> { K k;}";
			var output = @"struct Str<TK> { TK k;}";
			CheckNaming (input, output);
		}


		[Test]
		public void TestOverrideMembers ()
		{
			var input = @"
class Base { public virtual int method (int Param) {} }
class MyClass : Base { public override int method (int Param) {} }";
			TestRefactoringContext context;
			var issues = GetIssues (new InconsistentNamingIssue (), input, out context);
			Assert.AreEqual (2, issues.Count);
		}

		[Test]
		public void TestOverrideMembersParameterNameCaseMismatch ()
		{
			var input = @"
class Base { public virtual int Method (int param) {} }
class MyClass : Base { public override int Method (int Param) {} }";
			TestRefactoringContext context;
			var issues = GetIssues (new InconsistentNamingIssue (), input, out context);
			foreach (var issue in issues)
				Console.WriteLine(issue.Description);
			Assert.AreEqual (1, issues.Count);
		}
	}

	[TestFixture]
	public class WordParserTests
	{
		[Test]
		public void TestPascalCaseWords ()
		{
			var result = WordParser.BreakWords ("SomeVeryLongName");
			Assert.AreEqual (4, result.Count);
			Assert.AreEqual ("Some", result [0]);
			Assert.AreEqual ("Very", result [1]);
			Assert.AreEqual ("Long", result [2]);
			Assert.AreEqual ("Name", result [3]);
		}

		[Test]
		public void TestCamelCaseWords ()
		{
			var result = WordParser.BreakWords ("someVeryLongName");
			Assert.AreEqual (4, result.Count);
			Assert.AreEqual ("some", result [0]);
			Assert.AreEqual ("Very", result [1]);
			Assert.AreEqual ("Long", result [2]);
			Assert.AreEqual ("Name", result [3]);
		}

		[Test]
		public void TestUpperCaseSubWord ()
		{
			var result = WordParser.BreakWords ("someVeryLongXMLName");
			Assert.AreEqual (5, result.Count);
			Assert.AreEqual ("some", result [0]);
			Assert.AreEqual ("Very", result [1]);
			Assert.AreEqual ("Long", result [2]);
			Assert.AreEqual ("XML", result [3]);
			Assert.AreEqual ("Name", result [4]);
		}

		[Test]
		public void TestUnderscore ()
		{
			var result = WordParser.BreakWords ("some_Very_long_NAME");
			Assert.AreEqual (4, result.Count);
			Assert.AreEqual ("some", result [0]);
			Assert.AreEqual ("Very", result [1]);
			Assert.AreEqual ("long", result [2]);
			Assert.AreEqual ("NAME", result [3]);
		}

		[Test]
		public void TestUnderscoreCase1 ()
		{
			var result = WordParser.BreakWords ("FIX_1");
			Assert.AreEqual (2, result.Count);
			Assert.AreEqual ("FIX", result [0]);
			Assert.AreEqual ("1", result [1]);
		}

	}

	[TestFixture]
	public class NamingRuleTest
	{
		[Test]
		public void TestPascalCase()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.PascalCase;
			Assert.IsTrue(rule.IsValid("PascalCase"));
			Assert.IsTrue(rule.IsValid("PascalCase_1_23"));
			Assert.IsFalse(rule.IsValid("Pascal_Case"));
			Assert.IsFalse(rule.IsValid("camelCase"));
		}

		[Test]
		public void TestCamelCase()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.CamelCase;
			Assert.IsFalse(rule.IsValid("PascalCase"));
			Assert.IsFalse(rule.IsValid("camel_Case"));
			Assert.IsTrue(rule.IsValid("camelCase"));
			Assert.IsTrue(rule.IsValid("camelCase_10_11"));
		}

		[Test]
		public void TestAllUpper()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.AllUpper;
			Assert.IsFalse(rule.IsValid("PascalCase"));
			Assert.IsFalse(rule.IsValid("camelCase"));
			Assert.IsTrue(rule.IsValid("ALL_UPPER"));
			Assert.IsTrue(rule.IsValid("ALLUPPER"));
		}

		[Test]
		public void TestAllLower()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.AllLower;
			Assert.IsFalse(rule.IsValid("PascalCase"));
			Assert.IsFalse(rule.IsValid("camelCase"));
			Assert.IsTrue(rule.IsValid("all_lower"));
			Assert.IsTrue(rule.IsValid("alllower"));
		}

		[Test]
		public void TestFirstUpper()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.FirstUpper;
			Assert.IsFalse(rule.IsValid("PascalCase"));
			Assert.IsFalse(rule.IsValid("camelCase"));
			Assert.IsTrue(rule.IsValid("First_upper"));
			Assert.IsTrue(rule.IsValid("Firstupper"));
		}

		[Test]
		public void UnderscoreTolerantPascalCaseWithUpperStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.PascalCaseWithUpperLetterUnderscore;
			Assert.IsFalse(rule.IsValid("camelCase"));
			Assert.IsFalse(rule.IsValid("PascalCase_underscoreTolerant"));
			Assert.IsFalse(rule.IsValid("PascalCase__UnderscoreTolerant"));
			Assert.IsFalse(rule.IsValid("_PascalCase_UnderscoreTolerant"));
			Assert.IsTrue(rule.IsValid("PascalCase_UnderscoreTolerant"));
			Assert.IsTrue(rule.IsValid("PascalCase_UnderscoreTolerant_12"));
		}

		[Test]
		public void UnderscoreTolerantPascalCaseWithLowStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.PascalCaseWithLowerLetterUnderscore;
			Assert.IsFalse(rule.IsValid("camelCase"));
			Assert.IsFalse(rule.IsValid("PascalCase_UnderscoreTolerant"));
			Assert.IsFalse(rule.IsValid("PascalCase__underscoreTolerant"));
			Assert.IsFalse(rule.IsValid("_PascalCase_underscoreTolerant"));
			Assert.IsTrue(rule.IsValid("PascalCase_underscoreTolerant"));
		}

		[Test]
		public void UnderscoreTolerantCamelCaseWithLowStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.CamelCaseWithLowerLetterUnderscore;
			Assert.IsFalse(rule.IsValid("PascalCase"));
			Assert.IsFalse(rule.IsValid("camelCase_UnderscoreTolerant"));
			Assert.IsFalse(rule.IsValid("camelCase__underscoreTolerant"));
			Assert.IsTrue(rule.IsValid("camelCase_underscoreTolerant"));
		}

		[Test]
		public void UnderscoreTolerantCamelCaseWithUpperStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.CamelCaseWithUpperLetterUnderscore;
			Assert.IsFalse(rule.IsValid("PascalCase"));
			Assert.IsFalse(rule.IsValid("camelCase_underscoreTolerant"));
			Assert.IsFalse(rule.IsValid("camelCase__UnderscoreTolerant"));
			Assert.IsTrue(rule.IsValid("camelCase_UnderscoreTolerant"));
		}

		[Test]
		public void TestSuggestionForCamelCaseWithUpperStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.CamelCaseWithUpperLetterUnderscore;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "camelCase_underscoreTolerant", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("camelCase_UnderscoreTolerant"));
		}

		[Test]
		public void TestSuggestionForCamelCaseWithUpperStartWithUnderscoreStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.CamelCaseWithUpperLetterUnderscore;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "_camelCase_underscoreTolerant", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("camelCase_UnderscoreTolerant"));
		}

		[Test]
		public void TestSuggestionForCamelCaseWithLowerStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.CamelCaseWithLowerLetterUnderscore;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "camelCase_UnderscoreTolerant", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("camelCase_underscoreTolerant"));
		}

		[Test]
		public void TestSuggestionForCamelCaseWithLowerStartMultipleUnderscores()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.CamelCaseWithLowerLetterUnderscore;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "camelCase_____UnderscoreTolerant", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("camelCase_underscoreTolerant"));
		}

		[Test]
		public void TestSuggestionForPascalCaseWithUpperStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.PascalCaseWithUpperLetterUnderscore;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "PascalCase_underscoreTolerant", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("PascalCase_UnderscoreTolerant"));
		}

		[Test]
		public void TestSuggestionForPascalCaseWithLowerStart()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.PascalCaseWithLowerLetterUnderscore;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "PascalCase_UnderscoreTolerant", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("PascalCase_underscoreTolerant"));
		}


		[Test]
		public void TestSuggestionForPascalCase()
		{
			var rule = new NamingRule(AffectedEntity.Class);
			rule.NamingStyle = NamingStyle.PascalCase;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "pascalCase_12_____12323", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("PascalCase_12_12323"));
		}
		
		/// <summary>
		/// Bug in Xamarin Studio "Warning: no known errors"
		/// </summary>
		[Test]
		public void TestCase70925()
		{
			var rule = new NamingRule(AffectedEntity.Field);
			rule.NamingStyle = NamingStyle.PascalCase;
			System.Collections.Generic.IList<string> suggestedNames;
			rule.GetErrorMessage(new TestRefactoringContext (null, TextLocation.Empty, null), "_taskStatus", out suggestedNames); 
			Assert.IsTrue(suggestedNames.Contains("TaskStatus"));
		}
	}
}

