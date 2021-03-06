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
using ICSharpCode.Scripting;
using ICSharpCode.Scripting.Tests.Designer;
using NUnit.Framework;
using RubyBinding.Tests.Utils;

namespace RubyBinding.Tests.Designer
{
	/// <summary>
	/// Tests that a null property value does not cause a NullReferenceException.
	/// </summary>
	[TestFixture]
	public class GenerateUserControlWithNullPropertyValueTests : GenerateUserControlWithNullPropertyValueTestsBase
	{		
		[Test]
		public void GeneratedCode()
		{
			string expectedCode =
				"@userControl1 = ICSharpCode::Scripting::Tests::Utils::NullPropertyUserControl.new()\r\n" +
				"self.SuspendLayout()\r\n" +
				"# \r\n" +
				"# userControl1\r\n" +
				"# \r\n" +
				"@userControl1.FooBar = nil\r\n" +
				"@userControl1.Location = System::Drawing::Point.new(0, 0)\r\n" +
				"@userControl1.Name = \"userControl1\"\r\n" +
				"@userControl1.Size = System::Drawing::Size.new(10, 10)\r\n" +
				"@userControl1.TabIndex = 0\r\n" +
				"# \r\n" +
				"# MainControl\r\n" +
				"# \r\n" +
				"self.Controls.Add(@userControl1)\r\n" +
				"self.Name = \"MainControl\"\r\n" +
				"self.Size = System::Drawing::Size.new(200, 300)\r\n" +
				"self.ResumeLayout(false)\r\n";
			
			Assert.AreEqual(expectedCode, generatedCode, generatedCode);
		}
		
		protected override IScriptingCodeDomSerializer CreateSerializer()
		{
			return RubyCodeDomSerializerHelper.CreateSerializer();
		}
	}
}
