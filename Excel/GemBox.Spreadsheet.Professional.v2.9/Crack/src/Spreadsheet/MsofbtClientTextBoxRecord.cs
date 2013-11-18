namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class MsofbtClientTextBoxRecord : MsoBaseRecord
    {
        private byte[] cashedData;
        private XLSRecord formattingRuns;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTCLIENTTEXTBOX");
        private XLSRecord textContinue;
        private TxoRecord textObject;

        public MsofbtClientTextBoxRecord() : base(staticDescriptor, MsoType.ClientTextbox)
        {
            base.InitializeBody((byte[]) null);
        }

        public MsofbtClientTextBoxRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.ClientTextbox)
        {
        }

        /// <summary>
        /// Converts mso structure' representation to bytes.
        /// </summary>
        public override byte[] ConvertToBytes(MsoDelayedRecords delayedRecords)
        {
            byte[] buffer = base.ConvertToBytes(delayedRecords);
            ArrayList list = new ArrayList();
            list.AddRange(new XLSRecord[] { this.TextObject, this.TextContinue, this.FormattingRuns });
            delayedRecords.DelayedRecords.Add(list);
            delayedRecords.Lengths.Add(delayedRecords.Offset);
            return buffer;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            return base.Read(reader);
        }

        private void SetBody()
        {
            if (this.cashedData == null)
            {
                OptimizedBuffer buffer = new OptimizedBuffer();
                MemoryStream output = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(output);
                this.textObject.Write(bw);
                byte[] buffer2 = output.ToArray();
                buffer.Write(output.ToArray(), 0);
                output.Close();
                byte[] bytes = Utilities.ConvertXlsRecordToBytes(this.textContinue);
                buffer.Write(bytes, buffer2.Length);
                buffer.Write(Utilities.ConvertXlsRecordToBytes(this.formattingRuns), bytes.Length + buffer2.Length);
                this.cashedData = buffer.Buffer;
            }
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            return new byte[0];
        }

        protected override void WriteBody(BinaryWriter bw)
        {
            this.SetBody();
            bw.Write(this.cashedData);
        }

        protected override int BodySize
        {
            get
            {
                if (this.Body == null)
                {
                    this.SetBody();
                    return this.cashedData.Length;
                }
                return this.Body.Length;
            }
        }

        public XLSRecord FormattingRuns
        {
            get
            {
                return this.formattingRuns;
            }
            set
            {
                this.formattingRuns = value;
            }
        }

        public XLSRecord TextContinue
        {
            get
            {
                return this.textContinue;
            }
            set
            {
                this.textContinue = value;
            }
        }

        public TxoRecord TextObject
        {
            get
            {
                return this.textObject;
            }
            set
            {
                this.textObject = value;
            }
        }
    }
}

