namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;
    using System.IO;

    internal class MsofbtClientAnchorRecord : MsoBaseRecord
    {
        private uint bottom;
        private uint left;
        private uint right;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTCLIENTANCHOR");
        private uint top;

        public MsofbtClientAnchorRecord() : base(staticDescriptor, MsoType.ClientAnchor)
        {
            base.InitializeBody((byte[]) null);
        }

        public MsofbtClientAnchorRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.ClientAnchor)
        {
        }

        private Rectangle CalculateAbsoluteBoundary()
        {
            return new Rectangle(this.CalculateLeft(), this.CalculateTop(), this.CalculateAbsoluteWidth(), this.CalculateAbsoluteHeight());
        }

        private int CalculateAbsoluteHeight()
        {
            return this.CalculateHeight(this.TopColumnIndex, this.TopOffset, this.BottomColumnIndex, this.BottomOffset);
        }

        private int CalculateAbsoluteWidth()
        {
            return this.CalculateWidth(this.LeftColumnIndex, this.LeftOffset, this.RightColumnIndex, this.RightOffset);
        }

        private int CalculateHeight(int row1, int offset1, int row2, int offset2)
        {
            int rowHeight = 0x11;
            int num2 = 0x11;
            int num3 = this.ConvertHeightOffsetIntoPixels(rowHeight, offset1);
            int num5 = this.ConvertHeightOffsetIntoPixels(num2, offset2) - num3;
            for (int i = row1; i < row2; i++)
            {
                num5 += 0x11;
            }
            return num5;
        }

        private int CalculateLeft()
        {
            return this.CalculateWidth(1, 0, this.LeftColumnIndex, this.LeftOffset);
        }

        private int CalculateTop()
        {
            return this.CalculateHeight(1, 0, this.TopColumnIndex, this.TopOffset);
        }

        private int CalculateWidth(int column1, int offset1, int column2, int offset2)
        {
            int columnWidth = 0x40;
            int num2 = 0x40;
            int num3 = this.ConvertWidthOffsetIntoPixels(columnWidth, Math.Min(offset1, 0x400));
            int num5 = this.ConvertWidthOffsetIntoPixels(num2, Math.Min(offset2, 0x400)) - num3;
            for (int i = column1; i < column2; i++)
            {
                num5 += 0x40;
            }
            return num5;
        }

        private int ConvertHeightOffsetIntoPixels(int rowHeight, int offset)
        {
            return (int) Math.Round((double) (((double) (offset * rowHeight)) / 256.0));
        }

        private int ConvertWidthOffsetIntoPixels(int columnWidth, int offset)
        {
            return (int) Math.Round((double) (((double) (offset * columnWidth)) / 1024.0));
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            reader.ReadUInt16();
            this.left = reader.ReadUInt32();
            this.top = reader.ReadUInt32();
            this.right = reader.ReadUInt32();
            this.bottom = reader.ReadUInt32();
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            byte[] array = new byte[0x12];
            BitConverter.GetBytes((ushort) 0).CopyTo(array, 0);
            BitConverter.GetBytes(this.left).CopyTo(array, 2);
            BitConverter.GetBytes(this.top).CopyTo(array, 6);
            BitConverter.GetBytes(this.right).CopyTo(array, 10);
            BitConverter.GetBytes(this.bottom).CopyTo(array, 14);
            return array;
        }

        public Rectangle AbsoluteBoundary
        {
            get
            {
                return this.CalculateAbsoluteBoundary();
            }
        }

        public int BottomColumnIndex
        {
            get
            {
                return ((((int) this.bottom) & 0xffff) + 1);
            }
            set
            {
                this.bottom = Utilities.SetBit(this.bottom, 0xffff, (uint) (value - 1));
            }
        }

        public int BottomOffset
        {
            get
            {
                return (int) ((this.bottom & -65536) >> 0x10);
            }
            set
            {
                this.bottom = Utilities.SetBit(this.bottom, 0xffff0000, (uint) (value << 0x10)) + 1;
            }
        }

        public int LeftColumnIndex
        {
            get
            {
                return ((((int) this.left) & 0xffff) + 1);
            }
            set
            {
                this.left = Utilities.SetBit(this.left, 0xffff, (uint) (value - 1));
            }
        }

        public int LeftOffset
        {
            get
            {
                return (int) ((this.left & -65536) >> 0x10);
            }
            set
            {
                this.left = Utilities.SetBit(this.left, 0xffff0000, (uint) (value << 0x10)) + 1;
            }
        }

        public int RightColumnIndex
        {
            get
            {
                return ((((int) this.right) & 0xffff) + 1);
            }
            set
            {
                this.right = Utilities.SetBit(this.right, 0xffff, (uint) (value - 1));
            }
        }

        public int RightOffset
        {
            get
            {
                return (int) ((this.right & -65536) >> 0x10);
            }
            set
            {
                this.right = Utilities.SetBit(this.right, 0xffff0000, (uint) (value << 0x10)) + 1;
            }
        }

        public int TopColumnIndex
        {
            get
            {
                return ((((int) this.top) & 0xffff) + 1);
            }
            set
            {
                this.top = Utilities.SetBit(this.top, 0xffff, (uint) (value - 1));
            }
        }

        public int TopOffset
        {
            get
            {
                return (int) ((this.top & -65536) >> 0x10);
            }
            set
            {
                this.top = Utilities.SetBit(this.top, 0xffff0000, (uint) (value << 0x10)) + 1;
            }
        }
    }
}

