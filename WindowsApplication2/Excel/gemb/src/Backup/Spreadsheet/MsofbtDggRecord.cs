namespace GemBox.Spreadsheet
{
    using System;
    using System.Collections;
    using System.IO;

    internal class MsofbtDggRecord : MsoBaseRecord
    {
        private ArrayList clusters;
        private uint clustersCount;
        private uint drawingsCount;
        private uint shapesCount;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTDGG");

        public MsofbtDggRecord() : base(staticDescriptor, MsoType.Dgg)
        {
            this.clusters = new ArrayList();
            base.InitializeBody((byte[]) null);
        }

        public MsofbtDggRecord(BinaryReader reader) : base(staticDescriptor, reader, MsoType.Dgg)
        {
            this.clusters = new ArrayList();
        }

        private void AddCluster(uint groupId, uint number)
        {
            ClusterID rid = new ClusterID(groupId, number);
            this.clusters.Add(rid);
            this.clustersCount = (uint) (this.clusters.Count + 1);
            this.drawingsCount = (uint) this.clusters.Count;
        }

        public void InitializeByWorksheets(ExcelWorksheetCollection worksheets)
        {
            uint groupId = 0;
            foreach (ExcelWorksheet worksheet in worksheets)
            {
                this.shapesCount += worksheet.Shapes.ShapesCount;
                groupId++;
                this.drawingsCount++;
                this.AddCluster(groupId, worksheet.Shapes.ShapesCount);
            }
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            reader.ReadUInt32();
            this.clustersCount = reader.ReadUInt32();
            this.shapesCount = reader.ReadUInt32();
            this.drawingsCount = reader.ReadUInt32();
            for (int i = 0; i < (this.clustersCount - 1); i++)
            {
                this.clusters.Add(new ClusterID(reader));
            }
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            OptimizedBuffer buffer = new OptimizedBuffer();
            buffer.Write((uint) 0x400, 0);
            buffer.Write(this.clustersCount, 4);
            buffer.Write(this.shapesCount, 8);
            buffer.Write(this.drawingsCount, 12);
            int offset = 12;
            for (int i = 0; i < this.clusters.Count; i++)
            {
                byte[] bytes = ((ClusterID) this.clusters[i]).ConvertToBytes();
                buffer.Write(bytes, offset, 0, bytes.Length);
                offset += bytes.Length;
            }
            return buffer.Buffer;
        }
    }
}

