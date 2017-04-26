using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EndOfWork {
    class Program {
        static void Main(string[] args) {
            bool isWithBackup = true;
            if (args.Count()>0 && args[0] == "-b") {
                isWithBackup = false;
            }


            WorkFlow f = new WorkFlow(isWithBackup);
        }





    }

    public class WorkFlow {
        public WorkFlow(bool isWithBackup) {

            ClearWorkFolders();
            ReplaceTickets();
            if (isWithBackup)
                BackupSQL();

            Console.ReadLine();
        }

        void ClearWorkFolders() {
            List<string> listWorkFolder = new List<string>();
            listWorkFolder.Add(@"c:\temp");
            listWorkFolder.Add(@"c:\!Tickets\!Test");

            foreach (string fold in listWorkFolder) {
                try {
                    Directory.Delete(fold, true);
                    Thread.Sleep(100);
                    Directory.CreateDirectory(fold);
                    Console.WriteLine(string.Format("{0} clear", fold));
                }
                catch {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("!error {0}", fold));
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.WriteLine();
        }

        void ReplaceTickets() {
            string ticketsFolder = @"c:\!Tickets\";
            string solvedFolder = @"c:\!Tickets\!Solved\";
            var allDirectories = Directory.GetDirectories(ticketsFolder).ToList();
            foreach (string dir in allDirectories) {
                DirectoryInfo di = new DirectoryInfo(dir);
                var nm = di.Name;
                if (nm[0] != '!') {
                    var binDirs = Directory.GetDirectories(dir, "bin", SearchOption.AllDirectories);
                    var objDirs = Directory.GetDirectories(dir, "obj", SearchOption.AllDirectories);
                    var res = objDirs.Concat(binDirs).ToList();
                    if (res.Count > 0) {
                        foreach (var d in res) {
                            Directory.Delete(d, true);
                        }
                    }

                    string fullTargetName = solvedFolder + di.Name;
                    try {
                        Directory.Move(dir, fullTargetName);
                        Console.WriteLine(string.Format("--ok-{0}", dir));
                    }
                    catch (Exception e) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Format("!error: {0}", dir));
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
            }
        }


        private void BackupSQL() {
            Console.WriteLine("--------");
            string deployPath = @"c:\Dropbox\Deploy\BackupSQLDeploy\BackupSql.exe";
            Process proc = new Process();
            proc.StartInfo.FileName = deployPath;
            proc.StartInfo.Arguments = "-b";
            proc.Start();
            proc.WaitForExit();
            Console.WriteLine("Backup complete");
        }

    }
}
