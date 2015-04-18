//------------------------------------------------------------------------------------------------- 
// <copyright file="ImportLog.cs" company="inxin bvba">
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
    using System.Collections.Generic;
    using System.Text;

    public class ImportLog : IImportLog
    {
        private readonly List<Marker> markers;
        private readonly List<string> warnings;
        private readonly List<Exception> errors;
        private DateTime? started;
        private DateTime? finished;
    
        public ImportLog()
        {
            this.markers = new List<Marker>();
            this.warnings = new List<string>();
            this.errors = new List<Exception>();
        }

        public bool HasErrors
        {
            get
            {
                return this.errors.Count > 0;
            }
        }
        
        public bool HasWarnings
        {
            get
            {
                return this.warnings.Count > 0;
            }
        }

        public void AddMarker(string name, int? total)
        {
            if (!this.started.HasValue)
            {
                this.started = DateTime.Now;
            }

            var marker = new Marker { Name = name, Total = total, TimeStamp = DateTime.Now };
            this.markers.Add(marker);

            this.finished = DateTime.Now;
        }

        public void AddWarning(string message)
        {
            this.warnings.Add(message);
        }

        public void AddError(Exception exception)
        {
            this.errors.Add(exception);
        }

        public void Tick()
        {
            if (this.markers.Count > 0)
            {
                var marker = this.markers[this.markers.Count - 1];
                ++marker.Counter;
                this.finished = DateTime.Now;
            }
        }

        public override string ToString()
        {
            var toString = new StringBuilder();

            toString.AppendLine("Overview");
            toString.AppendLine("--------");

            for (var i = 0; i < this.markers.Count; i++)
            {
                var marker = this.markers[i];
                toString.Append(marker.Name);
                if (marker.Total.HasValue)
                {
                    toString.Append(" (" + marker.Counter + "/" + marker.Total + ")");
                }

                var nextTimeStamp = i + 1 < this.markers.Count ? this.markers[i + 1].TimeStamp : this.finished;
                if (nextTimeStamp.HasValue)
                {
                    toString.AppendLine(" " + (nextTimeStamp.Value - marker.TimeStamp).Seconds + " seconds.");
                }
            }

            if (this.warnings.Count > 0)
            {
                toString.AppendLine();
                toString.AppendLine("Warnings");
                toString.AppendLine("--------");

                foreach (var warning in this.warnings)
                {
                    toString.AppendLine(warning);
                }
            }

            if (this.errors.Count > 0)
            {
                toString.AppendLine();
                toString.AppendLine("Errors");
                toString.AppendLine("------");

                foreach (var error in this.errors)
                {
                    toString.AppendLine(error.ToString());
                    toString.AppendLine(error.StackTrace);
                }
            }

            return toString.ToString();
        }

        private class Marker
        {
            public string Name { get; set; }

            public int Counter { get; set; }

            public int? Total { get; set; }

            public DateTime TimeStamp { get; set; }
        }
    }
}
