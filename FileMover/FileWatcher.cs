using FileMover.model;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static FileMover.context.EnumContext;

namespace FileMover
{
    public class FileWatcher : IDisposable
    {
        public Response res;
        private RuleItem rule;
        private ListHistoryContext historyContext;
        private FileSystemWatcher watcher;
        private ListSettingsContext settingsContext;
        private Panel dg;

        public FileWatcher(RuleItem rule, ListHistoryContext historyContext, Panel dg, ListSettingsContext settingsContext)
        {
            try
            {
                this.rule = rule;
                this.historyContext = historyContext;
                this.settingsContext = settingsContext;
                this.dg = dg;
                FWCheck();        // ФЛК правила (мало ли)
                FWExecuteRule();  // обрабатываем файлы, что уже были до запуска FW
                FWInstance();     // реализация FW для правила

                res = new Response()
                {
                    respType = Response.RespType.Success,
                    Message = "Правило № " + rule.Id + " запущено"
                };
            }
            catch (Exception ex)
            {
                res.respType = Response.RespType.Error;
                res.Message = "Ошибка запуска правила № " + rule.Id + ":\n" + ex.Message;
            }
        }

        private void FWCheck()
        {
        }

        public void FWInstance()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = rule.DirStart;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Filter = rule.FileMask;
            watcher.Created += FWEvent;
            watcher.Changed += FWEvent;
            watcher.EnableRaisingEvents = true;
        }

        private void FWEvent(object source, FileSystemEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            FWExecuteRule();
            watcher.EnableRaisingEvents = true;
        }

        public void FWExecuteRule()
        {
            string[] dir = Directory.GetFiles(rule.DirStart, rule.FileMask);

            for (int i = 0; i < dir.Length; i++)
            {
                DateTime timeStart = DateTime.Now;
                int fileSize = (int)new FileInfo(dir[i]).Length;
                Response res = new Response();

                string p = rule.DirDest + @"\" + Path.GetFileName(dir[i]);
                string gen = GenNewFName(p);

                try
                {
                    using (var fs = File.Open(dir[i], FileMode.Open, FileAccess.Read, FileShare.None)) { }
                }
                catch (IOException)
                {
                    continue;
                }

                try
                {
                    File.Copy(
                        dir[i],
                        rule.IfExist == ifEx.rename ? gen : p,
                        rule.IfExist == ifEx.rewrite
                    );

                    if (rule.Operation == op.move)
                        File.Delete(dir[i]);

                    res = new Response()
                    {
                        respType = Response.RespType.Success,
                        Message = "Выполнено"
                    };
                }
                catch (Exception e)
                {
                    res = new Response()
                    {
                        respType = Response.RespType.Error,
                        Message = "Ошибка " + e.Message
                    };
                    continue;
                }
                finally
                {
                    historyContext.history.Item.Add(new HistoryItem()
                    {
                        Id = historyContext.history.Item.Count > 0 ? historyContext.history.Item.Max(f => f.Id) + 1 : 1,
                        DateMove = DateTime.Now,
                        Filename = Path.GetFileName(dir[i]),
                        DirStart = rule.DirStart + @"\" + Path.GetFileName(dir[i]),
                        DirDest = rule.DirDest + @"\" + Path.GetFileName(gen),
                        Duration = (int)(DateTime.Now - timeStart).TotalMilliseconds,
                        FileSize = fileSize,
                        result = res
                    });
                    historyContext.EditHistory();
                    _ = dg.Invoke((MethodInvoker)delegate
                    {
                        dg.GridRefresh();
                    });
                }
            }

            CheckDir();
            if (historyContext.history.Item.Count > settingsContext.settings.MaxCountHistory)
            {
                historyContext.history.Item.RemoveAll(x => x.DateMove < settingsContext.settings.LastClearHistory);
                historyContext.EditHistory();
                settingsContext.settings.LastClearHistory = DateTime.Now;
                settingsContext.EditSettings();
            }
        }

        private void CheckDir()
        {
            while (Directory.GetFiles(rule.DirStart, rule.FileMask).Length > 0)
            {
                Thread.Sleep(16000);
                FWExecuteRule();
            }
        }

        private string GenNewFName(string file)
        {
            int i = 1;
            string GenName = file;

            while (File.Exists(GenName))
                GenName = Path.GetDirectoryName(file) + @"\" + Path.GetFileNameWithoutExtension(file) + Const.RenameFile + i++ + Path.GetExtension(file);

            return GenName;
        }

        public void Dispose()
        {
            watcher.Dispose();
        }
    }
}