using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace libCBlindness
{
    [Serializable]
    public class ImageDescriptor : List<Circle>
    {
        public void SaveToFile(FileInfo file)
        {
            using (var oFile = file.OpenWrite())
            {
                var serializer = new BinaryFormatter();

                serializer.Serialize(oFile, this);
            }
        }

        public static ImageDescriptor LoadFromFile(FileInfo file)
        {
            using (var iFile = file.OpenRead())
            {
                var serializer = new BinaryFormatter();

                return serializer.Deserialize(iFile) as ImageDescriptor;
            }
        }

        public void Apply(Graphics g)
        {
            foreach (var c in this)
            {
                g.FillEllipse(new SolidBrush(c.C), c.X - c.Rad, c.Y - c.Rad, c.Rad * 2, c.Rad * 2);
            }
        }

    }
}