namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;

    internal class ExcelShapeCollection : IEnumerable
    {
        internal ArrayList items;
        private ArrayList notes;
        private uint uniqueShapeId;
        private ExcelWorksheet worksheet;

        internal ExcelShapeCollection(ExcelWorksheet worksheet)
        {
            this.notes = new ArrayList();
            this.items = new ArrayList();
            this.worksheet = worksheet;
        }

        internal ExcelShapeCollection(ExcelWorksheet worksheet, ExcelShapeCollection sourceShapes) : this(worksheet)
        {
            for (int i = 0; i < sourceShapes.Count; i++)
            {
                BitmapShape shape = (BitmapShape) sourceShapes[i];
                Rectangle rect = new Rectangle(shape.Left, shape.Top, shape.Right - shape.Left, shape.Bottom - shape.Top);
                this.Add(shape.Image, rect);
            }
        }

        public CommentShape Add(ExcelComment comment)
        {
            CommentShape shape = new CommentShape(this);
            comment.Shape = shape;
            this.items.Add(shape);
            return shape;
        }

        public BitmapShape Add(Image image)
        {
            BitmapShape shape = this.Add(image, ImageFormatToBlipType(image.RawFormat));
            shape.Child.Picture.LoadedFromExcel = false;
            return shape;
        }

        public BitmapShape Add(Image image, MsoBlipType blipType)
        {
            return this.Add(image, blipType, false);
        }

        public BitmapShape Add(Image image, Rectangle rect)
        {
            BitmapShape shape = this.Add(image, rect, ImageFormatToBlipType(image.RawFormat));
            shape.Child.Picture.LoadedFromExcel = false;
            return shape;
        }

        public BitmapShape Add(Image image, MsoBlipType blipType, bool loadedFromExcelFile)
        {
            MsofbtBseRecord record = new MsofbtBseRecord();
            record.BlipType = blipType;
            record.Version = 2;
            record.RequiredWin = 6;
            record.Picture = new MsoBitmapPictureRecord();
            record.Picture.LoadedFromExcel = loadedFromExcelFile;
            record.Picture.Parent = record;
            record.Picture.Image = image;
            this.Worksheet.Parent.Pictures.Add(record);
            BitmapShape shape = new BitmapShape(this, image);
            shape.Child = record;
            shape.Right = image.Width;
            shape.Bottom = image.Height;
            shape.Id = this.Worksheet.Parent.Pictures.Count;
            this.items.Add(shape);
            return shape;
        }

        public BitmapShape Add(Image image, Rectangle rect, MsoBlipType blipType)
        {
            BitmapShape shape = this.Add(image, blipType, true);
            shape.Left = rect.Left;
            shape.Top = rect.Top;
            shape.Right = rect.Right;
            shape.Bottom = rect.Bottom;
            return shape;
        }

        internal void AddNote(NoteRecord note)
        {
            this.notes.Add(note);
        }

        /// <summary>
        /// Deletes shape at specified index.
        /// </summary>
        /// <param name="shape">The shape.</param>
        internal void DeleteInternal(ExcelShape shape)
        {
            this.items.Remove(shape);
        }

        /// <summary>
        /// Deletes shape at specified index.
        /// </summary>
        /// <param name="index">The specified index.</param>
        internal void DeleteInternal(int index)
        {
            this.items.RemoveAt(index);
        }

        private uint GenerateUniqueShapeId()
        {
            this.uniqueShapeId++;
            return this.uniqueShapeId;
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Images the type of the format to blip.
        /// </summary>
        /// <param name="format">The image format.</param>
        /// <returns>MsoBlipType value</returns>
        private static MsoBlipType ImageFormatToBlipType(ImageFormat format)
        {
            if (format == ImageFormat.Wmf)
            {
                return MsoBlipType.Wmf;
            }
            if (format == ImageFormat.Emf)
            {
                return MsoBlipType.Emf;
            }
            return MsoBlipType.Png;
        }

        private void SaveMsoDrawing(AbsXLSRecords records, MsoContainerRecord dgContainer)
        {
            XLSRecord record;
            MsoDelayedRecords delayedRecords = new MsoDelayedRecords();
            delayedRecords.Offset = 8;
            byte[] sourceArray = dgContainer.ConvertToBytes(delayedRecords);
            sourceArray[0] = 15;
            int sourceIndex = 0;
            if (delayedRecords.Lengths.Count > 0)
            {
                int num2 = 0;
                int count = delayedRecords.Lengths.Count;
                while (num2 < count)
                {
                    int num4 = (int) delayedRecords.Lengths[num2];
                    ArrayList list = (ArrayList) delayedRecords.DelayedRecords[num2];
                    int length = num4 - sourceIndex;
                    byte[] destinationArray = new byte[length];
                    Array.Copy(sourceArray, sourceIndex, destinationArray, 0, length);
                    new OptimizedBuffer();
                    if (num4 > 0x2020)
                    {
                        MemoryStream input = new MemoryStream();
                        input.Write(BitConverter.GetBytes((ushort) length), 0, 2);
                        input.Write(destinationArray, 0, destinationArray.Length);
                        input.Position = 0L;
                        record = new XLSRecord(60, destinationArray.Length + 2, new BinaryReader(input));
                        input.Close();
                    }
                    else
                    {
                        record = new MsoDrawingRecord();
                        (record as MsoDrawingRecord).Data = destinationArray;
                    }
                    sourceIndex = num4;
                    records.Add(record);
                    for (int i = 0; i < list.Count; i++)
                    {
                        XLSRecord record2 = (XLSRecord) list[i];
                        records.Add(record2);
                    }
                    num2++;
                }
            }
            else
            {
                record = new MsoDrawingRecord();
                (record as MsoDrawingRecord).Data = sourceArray;
                records.Add(record);
            }
        }

        private void SaveNotes(AbsXLSRecords records)
        {
            for (int i = 0; i < this.notes.Count; i++)
            {
                NoteRecord record = (NoteRecord) this.notes[i];
                records.Add(record);
            }
        }

        private uint SaveShapes(MsoContainerRecord spgrContainer, uint shapeId)
        {
            foreach (ExcelShape shape in this.items)
            {
                if (shape is BitmapShape)
                {
                    shape.Save(spgrContainer, shapeId);
                    shapeId = this.GenerateUniqueShapeId();
                }
            }
            foreach (ExcelShape shape2 in this.items)
            {
                if (shape2 is CommentShape)
                {
                    shape2.Save(spgrContainer, shapeId);
                    shapeId = this.GenerateUniqueShapeId();
                }
            }
            return shapeId;
        }

        public void WriteOnDemand(AbsXLSRecords records)
        {
            if (this.Count != 0)
            {
                MsoContainerRecord dgContainer = ImageRecordsFactory.CreateContainer(MsoType.DgContainer);
                MsofbtDgRecord item = new MsofbtDgRecord();
                item.Parent = dgContainer;
                MsoContainerRecord record3 = ImageRecordsFactory.CreateContainer(MsoType.SpgrContainer);
                record3.Parent = dgContainer;
                MsoContainerRecord record4 = ImageRecordsFactory.CreateContainer(MsoType.SpContainer);
                record4.Parent = record3;
                MsofbtSpgrRecord record5 = new MsofbtSpgrRecord();
                record5.Parent = record4;
                uint shapeId = this.GenerateUniqueShapeId();
                MsofbtSpRecord record6 = new MsofbtSpRecord();
                record6.Parent = record5;
                record6.ShapeId = shapeId;
                record6.IsTopMost = true;
                dgContainer.Add(item);
                dgContainer.Add(record3);
                record3.Add(record4);
                record4.Add(record5);
                record4.Add(record6);
                record4.Parent = record3;
                shapeId = this.SaveShapes(record3, shapeId);
                item.ShapesNumber = (uint) (this.Worksheet.Shapes.Count + 1);
                item.LastId = shapeId;
                this.SaveMsoDrawing(records, dgContainer);
                this.SaveNotes(records);
            }
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public ExcelShape this[int index]
        {
            get
            {
                return (this.items[index] as ExcelShape);
            }
        }

        public uint ShapesCount
        {
            get
            {
                if (this.Count <= 0)
                {
                    return 0;
                }
                return (uint) (this.Count + 1);
            }
        }

        public ExcelWorksheet Worksheet
        {
            get
            {
                return this.worksheet;
            }
        }
    }
}

