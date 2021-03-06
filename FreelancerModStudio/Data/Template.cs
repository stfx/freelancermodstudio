using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace FreelancerModStudio.Data
{
    public class Template
    {
        public TemplateData Data = new TemplateData();

        public void Load(Stream stream)
        {
            Data = (TemplateData)Serializer.Load(stream, typeof(TemplateData));
        }

        public void Load(string path)
        {
            Data = (TemplateData)Serializer.Load(path, typeof(TemplateData));
        }

        public void Save(Stream stream)
        {
            Serializer.Save(stream, Data, typeof(TemplateData));
        }

        public void Save(string path)
        {
            Serializer.Save(path, Data, typeof(TemplateData));
        }

        [XmlRoot("FreelancerModStudio-Template-1.0")]
        public class TemplateData
        {
            [XmlArrayItem("File")]
            public List<File> Files = new List<File>();

            //public CostumTypes CostumTypes;
        }

        public class File
        {
            [XmlAttribute("name")]
            public string Name;

            //public FileType Type = FileType.ini;

            [XmlArrayItem("Path")]
            public List<string> Paths;

            [XmlArrayItem("Block")]
            public Table<string, Block> Blocks = new Table<string, Block>(StringComparer.OrdinalIgnoreCase);
        }

        public class Block : ITableRow<string>
        {
            [XmlAttribute("name")]
            public string Name;

            [XmlAttribute("multiple")]
            [DefaultValue(false)]
            public bool Multiple;

            [XmlAttribute("identifier")]
            public string Identifier;

            [XmlArrayItem("Option")]
            public Options Options;

            public string Id
            {
                get
                {
                    return Name;
                }
            }
        }

        public class Option : IComparable<Option>
        {
            [XmlAttribute("multiple")]
            [DefaultValue(false)]
            public bool Multiple;

            [XmlAttribute("parent")]
            public string Parent;

            //[XmlAttribute("type")]
            //public OptionType Type = OptionType.String;

            //[XmlAttribute("enum")]
            //public string EnumName;

            [XmlAttribute("renameFrom")]
            public string RenameFrom;

            [XmlAttribute("category")]
            public string Category;

            [XmlAttribute("description")]
            public string Description;

            [XmlText]
            public string Name;

            int IComparable<Option>.CompareTo(Option obj)
            {
                return string.CompareOrdinal(Name, obj.Name);
            }
        }

        /*public class Language
        {
            [XmlAttribute("id")]
            public string ID;

            public List<Description> Comments;

            public List<Category> Categories;
        }

        public class Category
        {
            [XmlAttribute("id")]
            public int ID;

            [XmlText]
            public string Value;
        }

        public class Description
        {
            [XmlAttribute("file")]
            public string File;

            [XmlAttribute("block")]
            public string Block;

            [XmlAttribute("option")]
            public string Option;

            [XmlText]
            public string Value;
        }

        public class CostumTypes
        {
            [XmlArrayItem("Enum")]
            public List<CostumEnum> Enums;
        }

        public class CostumEnum
        {
            [XmlAttribute("name")]
            public string Name;

            [XmlAttribute("type")]
            public OptionType Type = OptionType.String;

            [XmlArrayItem("Value")]
            public List<string> Values;
        }

        public enum FileType { ini, dll, exe, thn, utf, wav, db, threedb, cmp, mat, sur, txm, ale, vms, txt, hta, fl, other };

        public enum OptionType { String, Int, Bool, Point, Double, Enum, RGB, StringArray, IntArray, DoubleArray };*/

        public class Options : List<Option>
        {
            public int IndexOf(string name)
            {
                for (int i = 0; i < Count; ++i)
                {
                    if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return i;
                    }
                }

                return -1;
            }
        }
    }
}
