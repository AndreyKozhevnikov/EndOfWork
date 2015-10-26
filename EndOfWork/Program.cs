using System;
using System.Collections.Generic;
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
        }
        void ClearWorkFolders() {
            List<string> listWorkFolder = new List<string>();
            listWorkFolder.Add(@"d:\temp");
            listWorkFolder.Add(@"d:\!Tickets\!Test");
            listWorkFolder.Add(@"C:\Users\kozhevnikov.andrey\Downloads");

            foreach (string fold in listWorkFolder) {
                Directory.Delete(fold, true);
                Directory.CreateDirectory(fold);
            }
        }
    }
}
