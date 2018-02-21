namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents a picture object used by <seealso cref="T:GemBox.Spreadsheet.ExcelPictureCollection">ExcelPictureCollection</seealso> in the worksheet.
    /// </summary>	
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelPictureCollection">ExcelPictureCollection</seealso>				
    /// <example>
    /// Following code demonstrates how to use images. It shows next features: 
    /// <list type="number">
    /// <item> bmp, jpeg loading </item>
    /// <item> bmp, jpeg loading with custom coordinates and dimensions </item>
    /// </list>
    /// <code lang="Visual Basic">
    /// sheet.Pictures.Add( "Image.bmp" ) 
    /// sheet.Pictures.Add( "Image.bmp", New Rectangle(10, 50, 100, 100) )
    /// </code>
    /// <code lang="C#">
    /// sheet.Pictures.Add( "Image.bmp" );
    /// sheet.Pictures.Add( "Image.bmp", new Rectangle( 10, 50, 100, 100 ) );
    /// </code>
    /// </example>
    public class ExcelPicture
    {
        private Rectangle boundingRectangle;
        private System.Drawing.Image image;
        private int index;
        private ExcelPictureCollection parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ExcelPicture" /> class.
        /// </summary>
        /// <param name="parent">The workbook parent.</param>
        /// <param name="index">The index in picture collection.</param>
        /// <param name="image">The image to be added.</param>
        /// <param name="boundingRectangle">The bounding rectangle.</param>
        internal ExcelPicture(ExcelPictureCollection parent, int index, System.Drawing.Image image, Rectangle boundingRectangle)
        {
            this.parent = parent;
            this.index = index;
            this.image = image;
            this.boundingRectangle = boundingRectangle;
        }

        /// <summary>
        /// Deletes this picture from picture collection.
        /// </summary>
        public void Delete()
        {
            this.parent.DeleteInternal(this.index);
        }

        /// <summary>
        /// Gets the image bounding rectangle.
        /// </summary>
        /// <value>The image bounding rectangle.</value>
        public Rectangle BoundingRectangle
        {
            get
            {
                return this.boundingRectangle;
            }
        }

        /// <summary>
        /// Gets the image previously loaded.
        /// </summary>
        /// <value>The image previously loaded.</value>
        public System.Drawing.Image Image
        {
            get
            {
                return this.image;
            }
        }

        /// <summary>
        /// Gets or sets the index in parent collection.
        /// </summary>
        /// <value>The index in parent collection.</value>
        internal int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }
    }
}

