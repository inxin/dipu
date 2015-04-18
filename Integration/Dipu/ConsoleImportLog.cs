//------------------------------------------------------------------------------------------------- 
// <copyright file="ConsoleImportLog.cs" company="inxin bvba">
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

    /// <summary>
    /// ConsoleImportLog writes log information to the console.
    /// It can also decorate another ImportLog.
    /// </summary>
    public class ConsoleImportLog : IImportLog
    {
        private readonly IImportLog chainedLog;
        private Marker marker;
        private string lastWarning;
        private Exception lastError;

        public ConsoleImportLog(IImportLog chainedLog)
        {
            this.chainedLog = chainedLog;
        }

        public bool HasWarnings
        {
            get
            {
                return this.lastWarning != null;
            }
        }

        public bool HasErrors
        {
            get
            {
                return this.lastError != null;
            }
        }

        public void AddMarker(string name, int? total)
        {
            this.marker = new Marker { Name = name, Total = total, TimeStamp = DateTime.Now };
            
            Console.Clear();
            this.Write();

            if (this.chainedLog != null)
            {
                this.chainedLog.AddMarker(name, total);
            }
        }

        public void AddWarning(string message)
        {
            this.lastWarning = message;
            this.Write();

            if (this.chainedLog != null)
            {
                this.chainedLog.AddWarning(message);
            }
        }

        public void AddError(Exception exception)
        {
            this.lastError = exception;
            this.Write();

            if (this.chainedLog != null)
            {
                this.chainedLog.AddError(exception);
            }
        }

        public void Tick()
        {
            if (this.marker != null)
            {
                ++this.marker.Counter;
            }

            this.Write();

            if (this.chainedLog != null)
            {
                this.chainedLog.Tick();
            }
        }

        private void Write()
        {
            if (this.marker != null)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(this.marker.Name);
                if (this.marker.Total.HasValue)
                {
                    Console.Write(" (" + this.marker.Counter + "/" + this.marker.Total + ")");
                }

                Console.WriteLine(" " + (DateTime.Now - this.marker.TimeStamp).Seconds + " seconds.");

                if (this.lastError != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Last Error");
                    Console.WriteLine("-----");
                    Console.WriteLine(this.lastError.ToString());
                    Console.WriteLine(this.lastError.StackTrace);
                }

                if (this.lastWarning != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Last Warning");
                    Console.WriteLine("------------");
                    Console.WriteLine(this.lastWarning);
                }
            }
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
