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
using System.Diagnostics;
using ICSharpCode.Core;
using ICSharpCode.RubyBinding;
using NUnit.Framework;
using RubyBinding.Tests.Utils;

namespace RubyBinding.Tests.Testing
{
	[TestFixture]
	public class RubyConsoleApplicationTests
	{
		RubyConsoleApplication app;
		RubyAddInOptions options;
		
		[SetUp]
		public void Init()
		{
			options = new RubyAddInOptions(new Properties());
			options.RubyFileName = @"C:\IronRuby\ir.exe";
			app = new RubyConsoleApplication(options);
		}
		
		[Test]
		public void FileName_NewInstance_FileNameIsRubyFileNameFromAddInOptions()
		{
			string fileName = app.FileName;
			string expectedFileName = @"C:\IronRuby\ir.exe";
			Assert.AreEqual(expectedFileName, fileName);
		}
		
		[Test]
		public void GetArguments_DebugIsTrue_ReturnsDisableGemsAndDebugOption()
		{
			app.Debug = true;
			string args = app.GetArguments();
			string expectedCommandLine = "--disable-gems -D";
			
			Assert.AreEqual(expectedCommandLine, args);
		}
		
		[Test]
		public void GetArguments_ScriptFileNameIsSet_ReturnsQuotedRubyScriptFileName()
		{
			app.ScriptFileName = @"d:\projects\my ruby\test.rb";
			string args = app.GetArguments();
			string expectedCommandLine = "--disable-gems \"d:\\projects\\my ruby\\test.rb\"";
			
			Assert.AreEqual(expectedCommandLine, args);
		}
		
		[Test]
		public void GetArguments_ScriptFileNameAndScriptCommandLineArgumentsSet_ReturnsQuotedRubyScriptFileNameAndItsCommandLineArguments()
		{
			app.Debug = true;
			app.ScriptFileName = @"d:\projects\my ruby\test.rb";
			app.ScriptCommandLineArguments = "-- responseFile.txt";
			string args = app.GetArguments();
			
			string expectedCommandLine =
				"--disable-gems -D \"d:\\projects\\my ruby\\test.rb\" -- responseFile.txt";
			
			Assert.AreEqual(expectedCommandLine, args);
		}
		
		[Test]
		public void GetProcessStartInfo_NewInstance_HasFileNameThatEqualsIronRubyConsoleApplicationExeFileName()
		{
			ProcessStartInfo startInfo = app.GetProcessStartInfo();
			string expectedFileName = @"C:\IronRuby\ir.exe";
			
			Assert.AreEqual(expectedFileName, startInfo.FileName);
		}
		
		[Test]
		public void GetProcessStartInfo_DebugIsTrue_HasDebugFlagSetInArguments()
		{
			app.Debug = true;
			ProcessStartInfo startInfo = app.GetProcessStartInfo();
			string expectedCommandLine = "--disable-gems -D";
			
			Assert.AreEqual(expectedCommandLine, startInfo.Arguments);
		}
		
		[Test]
		public void GetProcessStartInfo_WorkingDirectorySet_ProcessStartInfoHasMatchingWorkingDirectory()
		{
			app.WorkingDirectory = @"d:\temp";
			ProcessStartInfo startInfo = app.GetProcessStartInfo();
			Assert.AreEqual(@"d:\temp", startInfo.WorkingDirectory);
		}
		
		[Test]
		public void GetProcessStartInfo_ChangingOptionsRubyFileName_ChangesProcessStartInfoFileName()
		{
			options.RubyFileName = @"d:\temp\test\ir.exe";
			ProcessStartInfo startInfo = app.GetProcessStartInfo();
			
			Assert.AreEqual(@"d:\temp\test\ir.exe", startInfo.FileName);
		}
	}
}
