﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the BSD license (for details please see \src\AddIns\Debugger\Debugger.AddIn\license.txt)

using ICSharpCode.SharpDevelop.Gui.Pads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Debugger.AddIn.Service;
using ICSharpCode.Core;
using ICSharpCode.Core.Presentation;
using ICSharpCode.Core.WinForms;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Debugging;
using ICSharpCode.SharpDevelop.Editor;
using ICSharpCode.SharpDevelop.Gui;

namespace Debugger.AddIn
{
	public class IsActiveBreakpointCondition : IConditionEvaluator
	{
		public IsActiveBreakpointCondition()
		{
		}
		
		public bool IsValid(object caller, Condition condition)
		{
			ITextEditor editor = SD.GetActiveViewContentService<ITextEditor>();
			if (editor == null)
				return false;
			if (string.IsNullOrEmpty(editor.FileName))
				return false;
			
			BreakpointBookmark point = null;
			
			foreach (BreakpointBookmark breakpoint in DebuggerService.Breakpoints) {
				if ((breakpoint.FileName == editor.FileName) &&
				    (breakpoint.LineNumber == editor.Caret.Line)) {
					point = breakpoint;
					break;
				}
			}
			
			if (point != null) {
				return point.IsEnabled;
			}
			
			return false;
		}
	}
}
