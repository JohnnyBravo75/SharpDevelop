﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using ICSharpCode.RubyBinding;
using ICSharpCode.SharpDevelop.Dom;
using NUnit.Framework;

namespace RubyBinding.Tests.Parsing
{
	[TestFixture]
	public class ParseMethodWithOptionalParametersTestFixture
	{
		IMethod method;
		IParameter parameterA;
		IParameter parameterB;
		
		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			string Ruby = "class Test\r\n" +
							"\tdef foo(a = 1, b = 'test')\r\n" +
							"\tend\r\n" +
							"end";
			
			DefaultProjectContent projectContent = new DefaultProjectContent();
			RubyParser parser = new RubyParser();
			ICompilationUnit compilationUnit = parser.Parse(projectContent, @"C:\test.rb", Ruby);			
			if (compilationUnit.Classes.Count > 0) {
				IClass c = compilationUnit.Classes[0];
				method = c.Methods[0];
				if (method.Parameters.Count > 1) {
					parameterA = method.Parameters[0];
					parameterB = method.Parameters[1];
				}
			}
		}
		
		[Test]
		public void MethodHasTwoParameters()
		{
			Assert.AreEqual(2, method.Parameters.Count);
		}
		
		[Test]
		public void FirstParameterIsSender()
		{
			Assert.AreEqual("a", parameterA.Name);
		}
		
		[Test]
		public void SecondParameterIsEventArgs()
		{
			Assert.AreEqual("b", parameterB.Name);
		}
	}
}
