namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Collection of the <seealso cref="T:GemBox.Spreadsheet.ExcelPicture">ExcelPicture</seealso>.
    /// </summary>	
    /// <seealso cref="T:GemBox.Spreadsheet.ExcelWorksheet">ExcelWorksheet</seealso>
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
    public class ExcelPictureCollection : IEnumerable
    {
        internal ArrayList items;
        private ExcelWorksheet worksheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ExcelPictureCollection" /> class.
        /// </summary>
        /// <param name="worksheet">The worksheet to initialize with.</param>
        internal ExcelPictureCollection(ExcelWorksheet worksheet)
        {
            this.worksheet = worksheet;
            this.items = new ArrayList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ExcelPictureCollection" /> class.
        /// </summary>
        /// <param name="worksheet">The worksheet to initialize with.</param>
        /// <param name="sourcePictures">The collection to initialize with.</param>
        internal ExcelPictureCollection(ExcelWorksheet worksheet, ExcelPictureCollection sourcePictures) : this(worksheet)
        {
            this.items = sourcePictures.items;
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="image">The image to be added.</param>
        public void Add(Image image)
        {
            this.AddImage(image);
            this.Worksheet.Shapes.Add(image);
        }

        /// <summary>
        /// Adds the image from specified fileName.
        /// </summary>
        /// <param name="fileName">The fileName.</param>
        public void Add(string fileName)
        {
            MemoryStream stream = new MemoryStream();
            Image.FromFile(fileName).Save(stream, ImageFormat.Bmp);
            Bitmap bitmap = (Bitmap) Image.FromStream(stream);
            Image image = Image.FromHbitmap(bitmap.GetHbitmap());
            stream.Close();
            bitmap.Dispose();
            this.Add(image);
        }

        internal void Add(Image image, MsoBlipType blipType)
        {
            this.AddImage(image);
            this.Worksheet.Shapes.Add(image, blipType);
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="image">The image to be added.</param>
        /// <param name="rect">The destination rectangle.</param>
        public void Add(Image image, Rectangle rect)
        {
            this.AddImage(image, rect);
            this.Worksheet.Shapes.Add(image, rect);
        }

        /// <summary>
        /// Adds the image from specified fileName.
        /// </summary>
        /// <param name="fileName">The fileName.</param>
        /// <param name="rect">The destination rectangle.</param>
        public void Add(string fileName, Rectangle rect)
        {
            MemoryStream stream = new MemoryStream();
            Image.FromFile(fileName).Save(stream, ImageFormat.Bmp);
            Bitmap bitmap = (Bitmap) Image.FromStream(stream);
            Image image = Image.FromHbitmap(bitmap.GetHbitmap());
            stream.Close();
            bitmap.Dispose();
            this.Add(image, rect);
        }

        internal void Add(Image image, Rectangle rect, MsoBlipType blipType)
        {
            this.AddImage(image, rect);
            this.Worksheet.Shapes.Add(image, rect, blipType);
        }

        private void AddImage(Image image)
        {
            Rectangle boundingRectangle = new Rectangle(0, 0, image.Width, image.Height);
            this.items.Add(new ExcelPicture(this, this.items.Count, image, boundingRectangle));
        }

        private void AddImage(Image image, Rectangle rect)
        {
            this.items.Add(new ExcelPicture(this, this.items.Count, image, rect));
        }

        /// <summary>
        /// Deletes picture at specified index.
        /// </summary>
        /// <param name="index">The specified index.</param>
        internal void DeleteInternal(int index)
        {
            this.items.RemoveAt(index);
            this.FixAllIndexes(index, -1);
            this.worksheet.Shapes.DeleteInternal(index);
        }

        private void FixAllIndexes(int index, int offset)
        {
            for (int i = index; i < this.items.Count; i++)
            {
                ExcelPicture picture = (ExcelPicture) this.items[i];
                picture.Index += offset;
            }
        }

        /// <summary>
        /// Returns an enumerator that can iterate through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" />
        /// that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Gets the count of pictures items.
        /// </summary>
        /// <value>The count of pictures items.</value>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// Gets the <see cref="T:GemBox.Spreadsheet.ExcelPicture" /> at the specified index.
        /// </summary>
        /// <value>the <see cref="T:GemBox.Spreadsheet.ExcelPicture" /></value>
        public ExcelPicture this[int index]
        {
            get
            {
                return (this.items[index] as ExcelPicture);
            }
        }

        /// <summary>
        /// Gets the worksheet.
        /// </summary>
        /// <value>The worksheet.</value>
        internal ExcelWorksheet Worksheet
        {
            get
            {
                return this.worksheet;
            }
        }
    }
}

