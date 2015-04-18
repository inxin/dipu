//------------------------------------------------------------------------------------------------- 
// <copyright file="Program.cs" company="inxin bvba">
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

namespace Allors
{
    namespace Console
    {
        using System;
        using System.Configuration;
        using System.Globalization;
        using System.IO;
        using System.Linq;
        using System.Xml;

        using Allors;
        using Allors.Integrations;
        using Allors.Workspaces.Memory.IntegerId;

        public class Program
        {
            private const string PopulationFileName = "population.xml";

            private enum Options
            {
                /// <summary>
                /// Saves the current population to population.xml
                /// </summary>
                Save,

                /// <summary>
                /// Loads a the population from population.xml
                /// </summary>
                Load,

                /// <summary>
                /// Upgrades the current population
                /// </summary>
                Upgrade,

                /// <summary>
                /// Creates a new population
                /// </summary>
                Populate,

                /// <summary>
                /// Import data
                /// </summary>
                Import,

                /// <summary>
                /// Generate
                /// </summary>
                Demo,

                /// <summary>
                /// Exist the application
                /// </summary>
                Exit
            }

            public static void Main(string[] args)
            {
                var configuration = new Databases.Object.SqlClient.Configuration
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                    ObjectFactory = Config.ObjectFactory,
                    WorkspaceFactory = new WorkspaceFactory(),
                    //IsolationLevel = System.Data.IsolationLevel.RepeatableRead,
                    CommandTimeout = 300
                };
                Config.Default = new Databases.Object.SqlClient.Database(configuration);

                Console.WriteLine("Please select an option:\n");
                foreach (var option in Enum.GetValues(typeof(Options)))
                {
                    Console.WriteLine((int)option + ". " + Enum.GetName(typeof(Options), option));
                }

                Console.WriteLine();

                try
                {
                    var key = Console.ReadKey(true).KeyChar.ToString(CultureInfo.InvariantCulture);
                    Options option;
                    if (Enum.TryParse(key, out option))
                    {
                        Console.WriteLine("-> " + (int)option + ". " + Enum.GetName(typeof(Options), option));
                        Console.WriteLine();

                        switch (option)
                        {
                            case Options.Demo:
                                Demo();
                                break;

                            case Options.Save:
                                Save();
                                break;

                            case Options.Load:
                                Load();
                                break;

                            case Options.Upgrade:
                                Upgrade();
                                break;

                            case Options.Populate:
                                Populate();
                                break;

                            case Options.Import:
                                Import();
                                break;

                            case Options.Exit:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown option");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }
                finally
                {
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey(false);
                }
            }

            private static void Save()
            {
                using (var writer = new XmlTextWriter(PopulationFileName, System.Text.Encoding.UTF8))
                {
                    Config.Default.Save(writer);
                }

                Console.WriteLine("Saved");
            }

            private static void Load()
            {
                using (var reader = new XmlTextReader(PopulationFileName))
                {
                    Config.Default.Load(reader);
                }

                Console.WriteLine("Loaded");
            }

            private static void Upgrade()
            {
                var database = Config.Default;

                using (var reader = new XmlTextReader(PopulationFileName))
                {
                    var domain = database.MetaPopulation;

                    database.ObjectNotLoaded += delegate(object sender, ObjectNotLoadedEventArgs args)
                    {
                        var composite = domain.Composites.FirstOrDefault(obj => obj.Id.Equals(args.ObjectTypeId));
                        if (composite != null)
                        {
                            Console.WriteLine("ObjectNotLoaded: type=" + composite + " id=" + args.ObjectId);
                        }
                        else
                        {
                            Console.WriteLine("ObjectNotLoaded: type=" + args.ObjectTypeId + " id=" + args.ObjectId);
                        }
                    };

                    database.RelationNotLoaded += delegate(object sender, RelationNotLoadedEventArgs args)
                    {
                        var relationType = domain.RelationTypes.FirstOrDefault(obj => obj.Id.Equals(args.RelationTypeId));
                        if (relationType != null)
                        {
                            Console.WriteLine("RelationNotLoaded: type=" + relationType + " contents=" + args.RoleContents);
                        }
                        else
                        {
                            Console.WriteLine("RelationNotLoaded: type=" + args.RelationTypeId + " contents=" + args.RoleContents);
                        }
                    };

                    database.Load(reader);
                }

                using (var session = database.CreateSession())
                {
                    // TODO: Force derivation
                    //var derivation = new Derivation(session, session.Extent<Location>().ToArray());
                    //var derivationLog = derivation.Derive();
                    //if (derivationLog.HasErrors)
                    //{
                    //    foreach (var error in derivationLog.Errors)
                    //    {
                    //        Console.WriteLine(error.Message);
                    //    }
                    //}

                    session.Derive();
                    session.Commit();
                }

                Console.WriteLine("Upgraded");
            }

            private static void Populate()
            {
                Console.WriteLine("Are you sure, all current data will be destroyed? (Y/N)\n");
                var confirmationKey = Console.ReadKey(true).KeyChar.ToString(CultureInfo.InvariantCulture);
                if (confirmationKey.ToLower().Equals("y"))
                {
                    var database = Config.Default;
                    database.Init();

                    using (var session = database.CreateSession())
                    {
                        new Setup(session).Apply();

                        var derivationLog = session.Derive();
                        if (derivationLog.HasErrors)
                        {
                            foreach (var error in derivationLog.Errors)
                            {
                                Console.WriteLine(error.ToString());
                            }

                            Console.WriteLine("Rollback");
                        }
                        else
                        {
                            Console.WriteLine("Commit");
                            session.Commit();
                        }
                    }
                }
            }

            private static void Import()
            {
                Console.WriteLine("Import");
                Console.WriteLine(DateTime.Now);

                var database = Config.Default;
                var dataPath = ConfigurationManager.AppSettings["dataPath"];

                var importLog = new ImportLog();
                var consoleImportLog = new ConsoleImportLog(importLog);

                // Integration.Import(database, dataPath, consoleImportLog);

                //Console.Clear();
                //Console.WriteLine(importLog.ToString());

                File.WriteAllText(@"..\..\..\import.log", importLog.ToString());
            }

            private static void Demo()
            {
                Console.WriteLine("Demo");

                try
                {
                    Populate();

                    Import();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                Console.WriteLine("Finished");
            }
        }
    }
}
