namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    internal class CellRecordHeader : BinaryWritable
    {
        public ushort Column;
        public ushort Row;
        public ushort StyleIndex;

        public CellRecordHeader(BinaryReader br)
        {
            this.Row = br.ReadUInt16();
            this.Column = br.ReadUInt16();
            this.StyleIndex = br.ReadUInt16();
        }

        public CellRecordHeader(ushort row, ushort column, ushort styleIndex)
        {
            this.Row = row;
            this.Column = column;
            this.StyleIndex = styleIndex;
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "Row:", this.Row, " Column:", this.Column, " StyleIndex:", this.StyleIndex });
        }

        public override void Write(BinaryWriter bw)
        {
            bw.Write(this.Row);
            bw.Write(this.Column);
            bw.Write(this.StyleIndex);
        }

        public override int Size
        {
            get
            {
                return 6;
            }
        }
    }
}

