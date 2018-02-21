namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class Window2Record : AbsXLSRec
    {
        public int firstColumn;
        public int firstRow;
        public int pageBreakViewZoom;
        private bool smallRecord;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("Window2");
        public WorksheetWindowOptions window2Options;
        public int zoom;

        public Window2Record(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo)
        {
            this.window2Options = (WorksheetWindowOptions) br.ReadUInt16();
            this.firstRow = br.ReadUInt16();
            this.firstColumn = br.ReadUInt16();
            if (bodyLength == 0x12)
            {
                this.smallRecord = false;
                br.ReadUInt16();
                br.ReadUInt16();
                this.pageBreakViewZoom = br.ReadUInt16();
                this.zoom = br.ReadUInt16();
                br.ReadUInt32();
            }
            else
            {
                if (bodyLength != 10)
                {
                    throw new Exception("Internal error: Window2 record of size " + this.BodySize);
                }
                this.smallRecord = true;
                br.ReadUInt32();
            }
        }

        public Window2Record(WorksheetWindowOptions window2Options, int firstRow, int firstColumn, int pageBreakViewZoom, int zoom)
        {
            this.smallRecord = false;
            this.window2Options = window2Options;
            this.firstRow = firstRow;
            this.firstColumn = firstColumn;
            this.pageBreakViewZoom = pageBreakViewZoom;
            this.zoom = zoom;
        }

        protected override void WriteBody(BinaryWriter bw)
        {
            bw.Write((ushort) this.window2Options);
            bw.Write((ushort) this.firstRow);
            bw.Write((ushort) this.firstColumn);
            bw.Write((ushort) 0x40);
            bw.Write((ushort) 0);
            bw.Write((ushort) this.pageBreakViewZoom);
            bw.Write((ushort) this.zoom);
            bw.Write(0);
        }

        protected override int BodySize
        {
            get
            {
                if (this.smallRecord)
                {
                    return 10;
                }
                return 0x12;
            }
        }

        public override string FormattedBody
        {
            get
            {
                string str = string.Concat(new object[] { this.window2Options.ToString(), " firstRow:", this.firstRow, " firstColumn:", this.firstColumn });
                if (!this.smallRecord)
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, " pageBreakViewZoom:", this.pageBreakViewZoom, " zoom:", this.zoom });
                }
                return str;
            }
        }

        public override string Name
        {
            get
            {
                return staticDescriptor.Name;
            }
        }

        internal override int RecordCode
        {
            get
            {
                return staticDescriptor.Code;
            }
        }
    }
}

