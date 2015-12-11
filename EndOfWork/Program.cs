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
            if (args != null && args[0] == "-b") {
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
            listWorkFolder.Add(@"d:\temp");
            listWorkFolder.Add(@"d:\!Tickets\!Test");

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
            string ticketsFolder = @"d:\!Tickets\";
            string solvedFolder = @"d:\!Tickets\!Solved\";

            var allDirectories = Directory.GetDirectories(ticketsFolder).ToList();
            foreach (string dir in allDirectories) {
                DirectoryInfo di = new DirectoryInfo(dir);
                var nm = di.Name;
                if (nm[0] != '!') {
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
            string deployPath = @"D:\Dropbox\Deploy\BackupSQLDeploy\BackupSql.exe";
            Process proc = Process.Start(deployPath, "-b");
            Console.WriteLine("Backup complete");
        }

    }
}
