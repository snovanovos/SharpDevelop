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
using ICSharpCode.Core;
using ICSharpCode.FormsDesigner;
using ICSharpCode.RubyBinding;
using ICSharpCode.Scripting.Tests.Utils;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Gui;
using NUnit.Framework;
using RubyBinding.Tests.Utils;

namespace RubyBinding.Tests.Designer
{
	/// <summary>
	/// Tests the RubyFormsDesignerDisplayBinding.
	/// </summary>
	[TestFixture]
	public class FormsDesignerDisplayBindingTestFixture
	{
		DerivedRubyFormsDesignerDisplayBinding displayBinding;
		MockTextEditorViewContent viewContent;
		bool canAttachToDesignableClass;
		ParseInformation parseInfo;
		
		[SetUp]
		public void SetUp()
		{
			displayBinding = new DerivedRubyFormsDesignerDisplayBinding();
			viewContent = new MockTextEditorViewContent();
			viewContent.PrimaryFileName =  new FileName("test.rb");
			viewContent.TextEditor.Document.Text = "text content";
			parseInfo = new ParseInformation(new DefaultCompilationUnit(new DefaultProjectContent()));
			displayBinding.ParseServiceParseInfoToReturn = parseInfo;
			displayBinding.IsParseInfoDesignable = true;
			canAttachToDesignableClass = displayBinding.CanAttachTo(viewContent);
		}
		
		[Test]
		public void ReattachWhenParserServiceIsReady()
		{
			Assert.IsTrue(displayBinding.ReattachWhenParserServiceIsReady);
		}
		
		[Test]
		public void CanAttachToNullViewContent()
		{
			Assert.IsFalse(displayBinding.CanAttachTo(null));
		}
		
		[Test]
		public void CanAttachToDesignableClass()
		{
			Assert.IsTrue(canAttachToDesignableClass);
		}
		
		[Test]
		public void CannotAttachToNonTextEditorViewContent()
		{
			MockViewContent viewContent = new MockViewContent();
			Assert.IsFalse(displayBinding.CanAttachTo(viewContent));
		}
		
		[Test]
		public void ParseInfoPassedToFormsDesignerIsDesignableMethod()
		{
			Assert.AreEqual(parseInfo, displayBinding.ParseInfoTestedForDesignability);
		}
		
		[Test]
		public void ParseInfoIsNotDesignable()
		{
			displayBinding.IsParseInfoDesignable = false;
			Assert.IsFalse(displayBinding.CanAttachTo(viewContent));
		}
		
		[Test]
		public void NullViewContentFileName()
		{
			viewContent.PrimaryFileName = null;
			Assert.IsFalse(displayBinding.CanAttachTo(viewContent));
		}
		
		[Test]
		public void FileNamePassedToGetParseInfo()
		{
			Assert.AreEqual("test.rb", displayBinding.FileNamePassedToGetParseInfo);
		}
		
		[Test]
		public void TextContentPassedToGetParseInfo()
		{
			Assert.AreEqual("text content", displayBinding.TextContentPassedToGetParseInfo);
		}
		
		[Test]
		public void NonRubyFileNameCannotBeAttachedTo()
		{
			viewContent.PrimaryFileName = new FileName("test.cs");
			Assert.IsFalse(displayBinding.CanAttachTo(viewContent));
		}
		
		[Test]
		public void NullViewContentPrimaryFileName()
		{
			viewContent.PrimaryFileName = null;
			Assert.IsFalse(displayBinding.CanAttachTo(viewContent));
		}
		
		[Test]
		public void CreatesRubyFormsDesigner()
		{
			MockTextEditorViewContent view = new MockTextEditorViewContent();
			IViewContent[] views = displayBinding.CreateSecondaryViewContent(view, new MockTextEditorOptions());
			Assert.AreEqual(1, views.Length);
			Assert.IsTrue(views[0] is FormsDesignerViewContent);
			views[0].Dispose();
		}
		
		[Test]
		public void FormDesignerNotCreatedIfAlreadyAttached()
		{
			MockTextEditorViewContent view = new MockTextEditorViewContent();			
			IViewContent[] views = null;
			using (FormsDesignerViewContent formsDesigner = new FormsDesignerViewContent(view, new MockOpenedFile("test.rb"))) {
				view.SecondaryViewContents.Add(formsDesigner);
				views = displayBinding.CreateSecondaryViewContent(view, new MockTextEditorOptions());
			}
			Assert.AreEqual(0, views.Length);
		}
	}
}
