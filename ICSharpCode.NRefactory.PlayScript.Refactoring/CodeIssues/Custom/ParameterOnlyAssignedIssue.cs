﻿// 
// ParameterOnlyAssignedIssue.cs
// 
// Author:
//      Mansheng Yang <lightyang0@gmail.com>
// 
// Copyright (c) 2012 Mansheng Yang <lightyang0@gmail.com>
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
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.Refactoring;

namespace ICSharpCode.NRefactory.PlayScript.Refactoring
{
	[IssueDescription ("Parameter is only assigned",
					   Description = "Parameter is assigned but its value is never used.",
					   Category = IssueCategories.CodeQualityIssues,
					   Severity = Severity.Warning)]
	public class ParameterOnlyAssignedIssue : VariableOnlyAssignedIssue
	{
		protected override IGatherVisitor CreateVisitor(BaseRefactoringContext context)
		{
			return new GatherVisitor(context);
		}

		private class GatherVisitor : GatherVisitorBase<ParameterOnlyAssignedIssue>
		{
			public GatherVisitor(BaseRefactoringContext ctx)
				: base (ctx)
			{
			}

			public override void VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
			{
				base.VisitParameterDeclaration(parameterDeclaration);

				var resolveResult = ctx.Resolve(parameterDeclaration) as LocalResolveResult;
				if (resolveResult == null)
					return;

				var parameterModifier = parameterDeclaration.ParameterModifier;
				if (parameterModifier == ParameterModifier.Out || parameterModifier == ParameterModifier.Ref ||
					!TestOnlyAssigned(ctx, parameterDeclaration.Parent, resolveResult.Variable)) {
					return;
				}
				AddIssue(new CodeIssue(parameterDeclaration.NameToken, 
					ctx.TranslateString("Parameter is assigned but its value is never used")));
			}
		}
	}
}