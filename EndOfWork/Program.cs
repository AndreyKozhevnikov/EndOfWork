using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndOfWork {
    class Program {
        static void Main(string[] args) {

            WorkFlow f = new WorkFlow();

        }




        
    }

    public class WorkFlow {
        public WorkFlow() {
            ClearWorkFolders();
            ReplaceTickets();
            BackupSQL();

            Console.ReadLine();
        }

        void ClearWorkFolders() {
            List<string> listWorkFolder = new List<string>();
            listWorkFolder.Add(@"d:\temp");
            listWorkFolder.Add(@"d:\!Tickets\!Test");
            listWorkFolder.Add(@"C:\Users\kozhevnikov.andrey\Downloads");

            foreach (string fold in listWorkFolder) {
                try {
                    Directory.Delete(fold, true);
                    Directory.CreateDirectory(fold);
                    Console.WriteLine(string.Format("{0} clear", fold));
                }
                catch {
                    Console.WriteLine(string.Format("!error {0}", fold));
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
                        Console.WriteLine(string.Format("--ok-{0}" ,dir));
                    }
                    catch(Exception e) {
                        Console.WriteLine(string.Format("!error: {0}",dir));
                    }
                }
            }
        }


        private void BackupSQL() {
            Console.WriteLine("--------");
            string deployPath = @"D:\Dropbox\Deploy\BackupSQL\BackupSql.exe";
            Process proc = Process.Start(deployPath,"-b");
            Console.WriteLine("Backup complete");
        }
        
    }
}
