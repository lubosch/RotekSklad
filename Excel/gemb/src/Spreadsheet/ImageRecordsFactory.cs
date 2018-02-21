namespace GemBox.Spreadsheet
{
    using System;
    using System.IO;

    /// <summary>
    /// Factory class for creation image records
    /// </summary>
    internal sealed class ImageRecordsFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GemBox.Spreadsheet.ImageRecordsFactory" /> class.
        /// </summary>
        private ImageRecordsFactory()
        {
        }

        /// <summary>
        /// Creates the mso container from specified mso type.
        /// </summary>
        /// <param name="type">The type used to create appropriate container.</param>
        public static MsoContainerRecord CreateContainer(MsoType type)
        {
            switch (type)
            {
                case MsoType.DggContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTDGGCONTAINER"), type);

                case MsoType.BstoreContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTBSTORECONTAINER"), type);

                case MsoType.DgContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTDGCONTAINER"), type);

                case MsoType.SpgrContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTSPGRCONTAINER"), type);

                case MsoType.SpContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTSPCONTAINER"), type);
            }
            throw new ArgumentException("Unallowed mso type for recreation mso container.");
        }

        /// <summary>
        /// Creates the mso container from specified mso type and reader.
        /// </summary>
        /// <param name="reader">The reader used to create appropriate container.</param>
        /// <param name="type">The type used to create appropriate container.</param>
        /// <returns></returns>
        public static MsoContainerRecord CreateContainer(BinaryReader reader, MsoType type)
        {
            switch (type)
            {
                case MsoType.DggContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTDGGCONTAINER"), reader, type);

                case MsoType.BstoreContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTBSTORECONTAINER"), reader, type);

                case MsoType.DgContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTDGCONTAINER"), reader, type);

                case MsoType.SpgrContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTSPGRCONTAINER"), reader, type);

                case MsoType.SpContainer:
                    return new MsoContainerRecord(XLSDescriptors.GetByName("MSOFBTSPCONTAINER"), reader, type);
            }
            throw new ArgumentException("Unallowed mso type for recreation mso container.");
        }

        /// <summary>
        /// Creates image record from reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>created image record</returns>
        public static MsoBaseRecord CreateFromReader(BinaryReader reader)
        {
            ushort atom = reader.ReadUInt16();
            MsoType type = (MsoType) reader.ReadUInt16();
            switch (type)
            {
                case MsoType.DggContainer:
                case MsoType.BstoreContainer:
                case MsoType.DgContainer:
                case MsoType.SpgrContainer:
                case MsoType.SpContainer:
                    return CreateContainer(reader, type);

                case MsoType.Dgg:
                    return new MsofbtDggRecord(reader);

                case MsoType.BSE:
                    return new MsofbtBseRecord(reader, atom);

                case MsoType.Dg:
                    return new MsofbtDgRecord(reader);

                case MsoType.Spgr:
                    return new MsofbtSpgrRecord(reader);

                case MsoType.Sp:
                    return new MsofbtSpRecord(reader);

                case MsoType.OPT:
                    return new MsofbtOptRecord(reader);

                case MsoType.ClientAnchor:
                    return new MsofbtClientAnchorRecord(reader);

                case MsoType.ClientData:
                    return new MsofbtClientDataRecord(reader);

                case MsoType.SplitMenuColors:
                    return new MsoPreservedRecord(type, reader);
            }
            SkipUnknownMsoRecord(reader);
            return null;
        }

        private static void SkipUnknownMsoRecord(BinaryReader reader)
        {
            int count = reader.ReadInt32();
            reader.ReadBytes(count);
        }
    }
}

