namespace GemBox.Spreadsheet
{
    using System;

    /// <summary>
    /// Represents a comment object used by <seealso cref="P:GemBox.Spreadsheet.ExcelCell.Comment">Comment</seealso> in the worksheet.
    /// </summary>
    /// <example>
    /// Following code demonstrates how to use comments. It shows next features: 
    /// <list type="number">
    /// <item> comment text setting </item>
    /// <item> comment' IsVisible property in action </item>
    /// </list>
    /// <code lang="Visual Basic">		
    /// excelFile.Worksheets(0).Cells(0, 0).Comment.Text = "comment1" 
    /// excelFile.Worksheets(0).Cells(0, 0).Comment.IsVisible = False
    /// </code>
    /// <code lang="C#">
    /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.Text = "comment1";
    /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.IsVisible = false;
    /// </code>
    /// </example>
    public class ExcelComment
    {
        private CommentShape shape;

        internal ExcelComment()
        {
        }

        internal void SetColumn(int column)
        {
            this.shape.Column = (ushort) column;
        }

        internal void SetRow(int row)
        {
            this.shape.Row = (ushort) row;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        /// <example>
        /// Following code demonstrates how to use comments. It shows next features: 
        /// <list type="number">
        /// <item> comment text setting </item>
        /// <item> comment' IsVisible property in action </item>
        /// </list>
        /// <code lang="Visual Basic">		
        /// excelFile.Worksheets(0).Cells(0, 0).Comment.Text = "comment1" 
        /// excelFile.Worksheets(0).Cells(0, 0).Comment.IsVisible = False
        /// </code>
        /// <code lang="C#">
        /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.Text = "comment1";
        /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.IsVisible = false;
        /// </code>
        /// </example>
        public bool IsVisible
        {
            get
            {
                return this.shape.IsVisible;
            }
            set
            {
                this.shape.IsVisible = value;
            }
        }

        internal CommentShape Shape
        {
            get
            {
                return this.shape;
            }
            set
            {
                this.shape = value;
            }
        }

        /// <summary>
        /// Gets the comment text assigned to excel cell
        /// </summary>
        /// <value>The comment text assigned to excel cell.</value>
        /// <example>
        /// Following code demonstrates how to use comments. It shows next features: 
        /// <list type="number">
        /// <item> comment text setting </item>
        /// <item> comment' IsVisible property in action </item>
        /// </list>
        /// <code lang="Visual Basic">		
        /// excelFile.Worksheets(0).Cells(0, 0).Comment.Text = "comment1" 
        /// excelFile.Worksheets(0).Cells(0, 0).Comment.IsVisible = False
        /// </code>
        /// <code lang="C#">
        /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.Text = "comment1";
        /// excelFile.Worksheets[ 0 ].Cells[ 0, 0 ].Comment.IsVisible = false;
        /// </code>
        /// </example>
        public string Text
        {
            get
            {
                return this.shape.Text;
            }
            set
            {
                this.shape.Text = value;
            }
        }
    }
}

