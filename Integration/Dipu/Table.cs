//------------------------------------------------------------------------------------------------- 
// <copyright file="Table.cs" company="inxin bvba">
// Copyright 2014-2015 inxin bvba.
// 
// Dual Licensed under
//   a) the Affero General Public Licence v3 (AGPL)
//   b) the Allors License
// 
// The AGPL License is included in the file LICENSE.
// The Allors License is an addendum to your contract.
// 
// Dipu is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// For more information visit http://www.dipu.com/legal
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Allors.Integrations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;

    public abstract class Table
    {
        // XmlSerializer creates temporary Assemblies and should
        // therfore be cached, otherwise it will leak.
        private static readonly Dictionary<Type, XmlSerializer> XmlSerializerByTableType;

        static Table()
        {
            XmlSerializerByTableType = new Dictionary<Type, XmlSerializer>();
        }

        public static T Load<T>(FileInfo fileInfo)
        {
            fileInfo.Refresh();
            using (var stream = fileInfo.OpenRead())
            {
                using (TextReader textReader = new StreamReader(stream))
                {
                    return Load<T>(textReader);
                }
            }
        }

        public static T Load<T>(string xml)
        {
            using (TextReader textReader = new StringReader(xml))
            {
                return Load<T>(textReader);
            }
        }

        public IEnumerable GetRecords()
        {
            var records = this.GetType().GetField("Records");
            return (IEnumerable)records.GetValue(this);
        }

        private static T Load<T>(TextReader textReader)
        {
            lock (XmlSerializerByTableType)
            {
                var tableType = typeof(T);

                XmlSerializer xmlSerializer;
                if (!XmlSerializerByTableType.TryGetValue(tableType, out xmlSerializer))
                {
                    var xmlRootAttribute = tableType.GetCustomAttribute<XmlRootAttribute>();
                    if (xmlRootAttribute != null)
                    {
                        xmlSerializer = new XmlSerializer(tableType);
                    }
                    else
                    {
                        xmlRootAttribute = new XmlRootAttribute(tableType.Name.Substring(0, tableType.Name.Length - "Table".Length));
                        xmlSerializer = new XmlSerializer(tableType, xmlRootAttribute);
                    }
                }

                var table = (T)xmlSerializer.Deserialize(textReader);
                return table;
            }
        }
    }
}
