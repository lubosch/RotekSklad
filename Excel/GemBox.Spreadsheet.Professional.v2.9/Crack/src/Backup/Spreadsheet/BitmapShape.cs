namespace GemBox.Spreadsheet
{
    using System;
    using System.Drawing;

    internal class BitmapShape : ExcelShape
    {
        private MsofbtBseRecord bseRecord;
        private int height;
        private System.Drawing.Image image;
        private int width;

        public BitmapShape(ExcelShapeCollection shapes, System.Drawing.Image image) : base(shapes)
        {
            this.image = image;
        }

        private void OnImageChanded(System.Drawing.Image image)
        {
            this.Width = image.Width;
            this.Height = image.Height;
        }

        public override void Save(MsoContainerRecord spgrContainer, uint shapeId)
        {
            MsoContainerRecord item = ImageRecordsFactory.CreateContainer(MsoType.SpContainer);
            MsofbtSpRecord record2 = new MsofbtSpRecord();
            record2.Parent = spgrContainer;
            record2.Instance = 0x6d6;
            MsofbtOptRecord record3 = new MsofbtOptRecord();
            record2.ShapeId = shapeId;
            FopteStructure property = new FopteStructure();
            property.Id = MsoOptions.BlipId;
            property.UintValue = (uint) base.Id;
            property.IsValid = true;
            record3.Add(property);
            property = new FopteStructure();
            property.Id = MsoOptions.BlipName;
            record3.Version = 3;
            record3.Instance = 2;
            item.Add(record2);
            item.Add(record3);
            item.Add(base.GenerateClientAnchorRecord());
            spgrContainer.Add(item);
        }

        public MsofbtBseRecord Child
        {
            get
            {
                return this.bseRecord;
            }
            set
            {
                this.bseRecord = value;
            }
        }

        public override int Height
        {
            get
            {
                return this.image.Height;
            }
            set
            {
                this.height = value;
            }
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
                this.OnImageChanded(value);
            }
        }

        public override int Width
        {
            get
            {
                return this.image.Width;
            }
            set
            {
                this.width = value;
            }
        }
    }
}

