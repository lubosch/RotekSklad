namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class MsofbtOptRecord : MsoBaseRecord
    {
        private ArrayList ids;
        private ArrayList properties;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTDGG");

        public MsofbtOptRecord() : base(staticDescriptor, MsoType.OPT)
        {
            this.ids = new ArrayList();
            this.properties = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public MsofbtOptRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.OPT)
        {
            this.ids = new ArrayList();
            this.properties = new ArrayList();
        }

        public void Add(FopteStructure property)
        {
            if (this.ids.Contains(property.Id))
            {
                this.AddItem(property);
            }
            else
            {
                this.ids.Add(property.Id);
                this.properties.Add(property);
            }
        }

        private void AddItem(FopteStructure item)
        {
            for (int i = 0; i < this.properties.Count; i++)
            {
                FopteStructure structure = this.properties[i] as FopteStructure;
                if (structure.Id == item.Id)
                {
                    this.properties[i] = item;
                }
            }
        }

        protected override void InitializeDelayed()
        {
            object[] args = new object[0];
            base.InitializeDelayed(args);
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            int num2 = 0;
            while ((num % 6) != 0)
            {
                num--;
                num2++;
            }
            int num3 = num / 6;
            for (int i = 0; i < num3; i++)
            {
                this.Add(new FopteStructure(reader));
            }
            for (int j = 0; j < num2; j++)
            {
                reader.ReadByte();
            }
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            byte[] bytes = new byte[this.properties.Count * 6];
            base.atom = 0x33;
            this.SetInstance();
            this.SetDataFromProperties(bytes);
            return bytes;
        }

        private void SetDataFromProperties(byte[] bytes)
        {
            int index = 0;
            for (int i = 0; i < this.properties.Count; i++)
            {
                FopteStructure structure = this.properties[i] as FopteStructure;
                structure.ConvertToBytes().CopyTo(bytes, index);
                index += 6;
            }
        }

        private void SetInstance()
        {
            if (this.properties.Count > 0)
            {
                int num2 = 1;
                int id = (int) (this.properties[0] as FopteStructure).Id;
                for (int i = 1; i < this.properties.Count; i++)
                {
                    FopteStructure structure = this.properties[i] as FopteStructure;
                    if ((int)structure.Id <= id)
                    {
                        break;
                    }
                    num2++;
                    id = (int) structure.Id;
                }
                base.Instance = num2;
            }
        }
    }
}

