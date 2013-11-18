namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;

    internal class CommentShape : ExcelShape
    {
        private ushort column;
        private const int continueFormattingRunSuze = 8;
        private bool isVisible;
        private ushort row;
        private const int singleFormattingRunSize = 4;
        private const int spInstance = 0xca;
        private const int spVersion = 2;
        private string text;

        public CommentShape(ExcelShapeCollection parent) : base(parent)
        {
            this.text = string.Empty;
        }

        private MsofbtClientTextBoxRecord AddFormattingRunsOnDemand(MsofbtClientTextBoxRecord textBox, TxoRecord txo)
        {
            if (this.text.Length != 0)
            {
                textBox.TextContinue = this.GenerateTextContinueRecord();
                byte[] formattingRunsBytes = this.ConvertFromShortToLongFR(this.SaveFormattingRuns());
                textBox.FormattingRuns = this.GenerateFormattingRunsContinueRecord(formattingRunsBytes);
                txo.FormattingRunsLen = (ushort) formattingRunsBytes.Length;
            }
            return textBox;
        }

        private byte[] ConvertFromShortToLongFR(byte[] shortFormBytes)
        {
            if (shortFormBytes == null)
            {
                return null;
            }
            int num = shortFormBytes.Length / 4;
            byte[] destinationArray = new byte[num * 8];
            for (int i = 0; i < num; i++)
            {
                Array.Copy(shortFormBytes, i * 4, destinationArray, i * 8, 4);
            }
            return destinationArray;
        }

        private MsofbtClientTextBoxRecord GenerateClientTextBox(MsoBaseRecord parent)
        {
            MsofbtClientTextBoxRecord textBox = new MsofbtClientTextBoxRecord();
            textBox.Parent = parent;
            TxoRecord txo = new TxoRecord();
            txo.TextLen = (ushort) this.text.Length;
            txo.FormattingRunsLen = 0x10;
            textBox.TextObject = txo;
            return this.AddFormattingRunsOnDemand(textBox, txo);
        }

        private XLSRecord GenerateFormattingRunsContinueRecord(byte[] formattingRunsBytes)
        {
            MemoryStream input = new MemoryStream();
            input.Write(formattingRunsBytes, 0, formattingRunsBytes.Length);
            input.Position = 0L;
            BinaryReader br = new BinaryReader(input);
            XLSRecord record = new XLSRecord(60, formattingRunsBytes.Length, br);
            input.Close();
            return record;
        }

        private XLSRecord GenerateTextContinueRecord()
        {
            MemoryStream input = new MemoryStream();
            input.Write(new byte[] { 1 }, 0, 1);
            byte[] bytes = Encoding.Unicode.GetBytes(this.text);
            input.Write(bytes, 0, bytes.Length);
            input.Position = 0L;
            BinaryReader br = new BinaryReader(input);
            XLSRecord record = new XLSRecord(60, (this.text.Length * 2) + 1, br);
            input.Close();
            return record;
        }

        public override void Save(MsoContainerRecord spgrContainer, uint shapeId)
        {
            MsoContainerRecord parent = ImageRecordsFactory.CreateContainer(MsoType.SpContainer);
            MsofbtSpRecord item = new MsofbtSpRecord();
            item.Parent = spgrContainer;
            MsofbtClientDataRecord record3 = new MsofbtClientDataRecord();
            record3.Parent = parent;
            CmoObjSubRecord subRecord = null;
            ObjRecord record5 = new ObjRecord();
            subRecord = new CmoObjSubRecord();
            record5.Add(subRecord);
            record3.Add(record5);
            subRecord.ID = (ushort) shapeId;
            item.Version = 2;
            item.Instance = 0xca;
            parent.Add(item);
            MsofbtOptRecord options = new MsofbtOptRecord();
            options.Parent = parent;
            this.SaveIsVisibleOption(options);
            parent.Add(options);
            base.Left = (this.column + 1) * 0x40;
            base.Top = this.row * 0x11;
            base.Right = base.Left + 100;
            base.Bottom = base.Top + 100;
            parent.Add(base.GenerateClientAnchorRecord());
            parent.Add(record3);
            parent.Add(this.GenerateClientTextBox(parent));
            spgrContainer.Add(parent);
            this.SaveNote(subRecord.ID);
        }

        private byte[] SaveFormattingRuns()
        {
            ArrayList list = new ArrayList();
            list.AddRange(BitConverter.GetBytes((ushort) 0));
            list.AddRange(BitConverter.GetBytes((ushort) 0));
            list.AddRange(BitConverter.GetBytes((ushort) this.text.Length));
            list.AddRange(BitConverter.GetBytes((ushort) 0));
            return (byte[]) list.ToArray(typeof(byte));
        }

        protected void SaveIsVisibleOption(MsofbtOptRecord options)
        {
            FopteStructure property = new FopteStructure();
            property.Id = (MsoOptions) 0x3bf;
            property.UintValue = 0x20002;
            property.IsValid = false;
            property.IsComplex = false;
            options.Add(property);
        }

        private void SaveNote(ushort id)
        {
            NoteRecord note = new NoteRecord();
            note.Row = this.Row;
            note.Column = this.Column;
            note.IsVisible = this.IsVisible;
            note.Id = id;
            base.parent.AddNote(note);
        }

        public ushort Column
        {
            get
            {
                return this.column;
            }
            set
            {
                this.column = value;
            }
        }

        public override int Height
        {
            get
            {
                return (this.column * 0x11);
            }
            set
            {
                this.column = (ushort) (value / 0x11);
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

        public ushort Row
        {
            get
            {
                return this.row;
            }
            set
            {
                this.row = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public override int Width
        {
            get
            {
                return (this.row * 0x40);
            }
            set
            {
                this.row = (ushort) (value / 0x40);
            }
        }
    }
}

