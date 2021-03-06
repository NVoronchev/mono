//
// ListViewVirtualItemsSelectionRangeChangedEventArgs.cs
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2006 Novell, Inc.
//
// Authors:
//	Jonathan Pobst (monkey@jpobst.com)
//

namespace System.Windows.Forms
{
	public class ListViewVirtualItemsSelectionRangeChangedEventArgs : EventArgs
	{
		private bool is_selected;
		private int end_index;
		private int start_index;

		#region Public Constructors
		public ListViewVirtualItemsSelectionRangeChangedEventArgs (int startIndex, int endIndex, bool isSelected) : base ()
		{
			this.start_index = startIndex;
			this.end_index = endIndex;
			this.is_selected = isSelected;
		}
		#endregion	// Public Constructors

		#region Public Instance Properties
		public int StartIndex {
			get { return this.start_index; }
		}

		public bool IsSelected {
			get { return this.is_selected; }
		}

		public int EndIndex {
			get { return this.end_index; }
		}
		#endregion	// Public Instance Properties
	}
}