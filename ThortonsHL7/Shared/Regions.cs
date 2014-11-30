using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Region
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public float PSTRate { get; set; }
        public float HSTRate { get; set; }
        public float GSTRate { get; set; }
    }
    public class Regions
    {
        private readonly static List<Region> regions = new List<Region>()
        {
            new Region() { Code = "NL", Name = "Newfoundland", PSTRate = 0, HSTRate = 13, GSTRate = 0 },
            new Region() { Code = "NS", Name = "Nova Scotia", PSTRate = 0, HSTRate = 15, GSTRate = 0 },
            new Region() { Code = "NB", Name = "New Brunswick", PSTRate = 0, HSTRate = 13, GSTRate = 0 },
            new Region() { Code = "PE", Name = "Prince Edward Island", PSTRate = 10, HSTRate = 0, GSTRate = 5 },
            new Region() { Code = "QC", Name = "Quebec", PSTRate = 9.5f, HSTRate = 0, GSTRate = 5 },
            new Region() { Code = "ON", Name = "Ontario", PSTRate = 0, HSTRate = 13, GSTRate = 0 },
            new Region() { Code = "MB", Name = "Manitoba", PSTRate = 7, HSTRate = 0, GSTRate = 5 },
            new Region() { Code = "SK", Name = "Saskatchewan", PSTRate = 5, HSTRate = 0, GSTRate = 5 },
            new Region() { Code = "AB", Name = "Alberta", PSTRate = 0, HSTRate = 0, GSTRate = 5 },
            new Region() { Code = "BC", Name = "British Colombia", PSTRate = 0, HSTRate = 12, GSTRate = 0 },
            new Region() { Code = "YT", Name = "Yukon Territories", PSTRate = 0, HSTRate = 0, GSTRate = 5 },
            new Region() { Code = "NT", Name = "Northwest Territories", PSTRate = 0, HSTRate = 0, GSTRate = 5 },
            new Region() { Code = "NU", Name = "Nunavut", PSTRate = 0, HSTRate = 0, GSTRate = 5 }
        };

        public static IEnumerable<Region> GetAllRegions()
        {
            return regions.OrderBy(region => region.Code).ToArray();
        }

        public static IEnumerable<string> GetCodes()
        {
            return regions.OrderBy(region => region.Code).Select(region => region.Code).ToArray();
        }

        public static IEnumerable<string> GetNames()
        {
            return regions.OrderBy(region => region.Name).Select(region => region.Name).ToArray();
        }

        public static Region GetRegionByName(string name)
        {
            return regions.SingleOrDefault(region => region.Name == name);
        }

    }
}
