using System;
using System.ComponentModel;
using System.Timers;

namespace GOINSP.Utility
{
    class SyncWorker
    {
            private BackgroundWorker worker;
        private SynchronizeHelper syncHelper;

        public SyncWorker()
        {
            //Create SyncHelper
            syncHelper = new SynchronizeHelper();
            //Create backgroundworker
            worker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //Run task every x time
            Timer timer = new Timer(60000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker w = (BackgroundWorker)sender;
            Console.WriteLine("Syncing database");
            //Sync database
            //syncHelper.work();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}
