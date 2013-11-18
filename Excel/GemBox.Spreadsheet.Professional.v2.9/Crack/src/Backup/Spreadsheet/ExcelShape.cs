namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    internal abstract class ExcelShape
    {
        private int bottom;
        private int id;
        private int left;
        protected ExcelShapeCollection parent;
        private int right;
        private int top;

        public ExcelShape(ExcelShapeCollection parent)
        {
            this.parent = parent;
        }

        private int ConvertPixelsToOffsetValue(int columnWidthOrRowHeight, int pixels, bool rowPixelsProcessing)
        {
            int num = rowPixelsProcessing ? 0x400 : 0x100;
            return ((pixels * num) / columnWidthOrRowHeight);
        }

        protected MsofbtClientAnchorRecord GenerateClientAnchorRecord()
        {
            MsofbtClientAnchorRecord rec = new MsofbtClientAnchorRecord();
            this.SetLeftPosition(rec);
            this.SetRightPosition(rec);
            this.SetBottomPosition(rec);
            this.SetTopPosition(rec);
            return rec;
        }

        private void OnImageChanded(Image image)
        {
            this.Width = image.Width;
            this.Height = image.Height;
        }

        public abstract void Save(MsoContainerRecord spgrContainer, uint shapeId);
        private void SetBottomPosition(MsofbtClientAnchorRecord rec)
        {
            int columnWidthOrRowHeight = 0x11;
            int num2 = 0;
            int bottom = this.bottom;
            while (bottom > columnWidthOrRowHeight)
            {
                bottom -= columnWidthOrRowHeight;
                num2++;
            }
            rec.BottomColumnIndex = num2;
            rec.BottomOffset = this.ConvertPixelsToOffsetValue(columnWidthOrRowHeight, bottom, false);
        }

        private void SetLeftPosition(MsofbtClientAnchorRecord rec)
        {
            int columnWidthOrRowHeight = 0x40;
            int num2 = 0;
            int left = this.left;
            while (left > columnWidthOrRowHeight)
            {
                left -= columnWidthOrRowHeight;
                num2++;
            }
            rec.LeftColumnIndex = num2;
            rec.LeftOffset = this.ConvertPixelsToOffsetValue(columnWidthOrRowHeight, left, true);
        }

        private void SetRightPosition(MsofbtClientAnchorRecord rec)
        {
            int columnWidthOrRowHeight = 0x40;
            int num2 = 0;
            int right = this.right;
            while (right > columnWidthOrRowHeight)
            {
                right -= columnWidthOrRowHeight;
                num2++;
            }
            rec.RightColumnIndex = num2;
            rec.RightOffset = this.ConvertPixelsToOffsetValue(columnWidthOrRowHeight, right, true);
        }

        private void SetTopPosition(MsofbtClientAnchorRecord rec)
        {
            int columnWidthOrRowHeight = 0x11;
            int num2 = 0;
            int top = this.top;
            while (top > columnWidthOrRowHeight)
            {
                top -= columnWidthOrRowHeight;
                num2++;
            }
            rec.TopColumnIndex = num2;
            rec.TopOffset = this.ConvertPixelsToOffsetValue(columnWidthOrRowHeight, top, false);
        }

        internal int Bottom
        {
            get
            {
                return this.bottom;
            }
            set
            {
                this.bottom = value;
            }
        }

        public abstract int Height { get; set; }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        internal int Left
        {
            get
            {
                return this.left;
            }
            set
            {
                this.left = value;
            }
        }

        internal int Right
        {
            get
            {
                return this.right;
            }
            set
            {
                this.right = value;
            }
        }

        public int Top
        {
            get
            {
                return this.top;
            }
            set
            {
                this.top = value;
            }
        }

        public abstract int Width { get; set; }
    }
}

