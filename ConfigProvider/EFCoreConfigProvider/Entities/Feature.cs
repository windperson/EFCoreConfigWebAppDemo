using System;

namespace EFCoreConfigProvider.Entities
{
    public class Feature : IComparable<Feature>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        
        public int CompareTo(Feature other)
        {
            return string.CompareOrdinal(this.Key, other.Key);
        }
    }
}