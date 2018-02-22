namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Security.Cryptography;

    internal class MsoBitmapPictureRecord : MsoBaseRecord
    {
        private System.Drawing.Image image;
        private byte[] imageBytes;
        private bool loadedFromExcel;
        private MsofbtBseRecord record;
        private static XLSDescriptor staticDescriptor = XLSDescriptors.GetByName("MSOFBTBSE");

        public MsoBitmapPictureRecord() : base(staticDescriptor, MsoType.Bitmap)
        {
            this.loadedFromExcel = true;
            base.Instance = 0x6e0;
            base.InitializeBody((byte[]) null);
        }

        public MsoBitmapPictureRecord(int bodyLength, BinaryReader br, AbsXLSRec previousRecord, IoOperationInfo operationInfo) : base(staticDescriptor, bodyLength, br, MsoType.Bitmap, operationInfo)
        {
            this.loadedFromExcel = true;
            base.Instance = 0x6e0;
        }

        public static ImageFormat ConvertBlipTypeToImageFormat(MsoBlipType blipType)
        {
            MsoBlipType type = blipType;
            return ImageFormat.Png;
        }

        /// <summary>
        /// Reads data from the specified reader.
        /// </summary>
        /// <param name="reader">The source binary reader.</param>
        public override int Read(BinaryReader reader)
        {
            int num = base.Read(reader);
            reader.ReadBytes(0x11);
            int count = num - 0x11;
            this.ImageBytes = reader.ReadBytes(count);
            try
            {
                this.Image = System.Drawing.Image.FromStream(new MemoryStream(this.ImageBytes));
            }
            catch
            {
            }
            return num;
        }

        /// <summary>
        /// Sets the inner data( mso structure' data without header: atom, type, length ).
        /// </summary>
        protected override byte[] SetData(MsoDelayedRecords delayedRecords)
        {
            int length;
            OptimizedBuffer buffer = new OptimizedBuffer();
            if (this.Image != null)
            {
                MemoryStream stream = new MemoryStream();
                MsoBlipType blipType = (base.Parent as MsofbtBseRecord).BlipType;
                this.Image.Save(stream, ConvertBlipTypeToImageFormat(blipType));
                this.ImageBytes = stream.GetBuffer();
                length = (int) stream.Length;
            }
            else
            {
                length = this.ImageBytes.Length;
            }
            byte[] bytes = new MD5CryptoServiceProvider().ComputeHash(this.imageBytes);
            buffer.Write(bytes, 0, 0, bytes.Length);
            buffer.Write((byte) 0, bytes.Length);
            buffer.Write(this.imageBytes, bytes.Length + 1, 0, length);
            return buffer.Buffer;
        }

        public System.Drawing.Image Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
            }
        }

        public byte[] ImageBytes
        {
            get
            {
                return this.imageBytes;
            }
            set
            {
                this.imageBytes = value;
            }
        }

        public bool LoadedFromExcel
        {
            get
            {
                return this.loadedFromExcel;
            }
            set
            {
                this.loadedFromExcel = value;
            }
        }

        public MsofbtBseRecord Record
        {
            get
            {
                return this.record;
            }
            set
            {
                this.record = value;
            }
        }
    }
}

